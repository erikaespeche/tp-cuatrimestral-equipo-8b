using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class MedicoNegocio
    {
        public List<Medico> Listar()
        {
            List<Medico> listaMedico = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT IdMedico, Nombre, Apellido, Dni, Telefono, Email, Estado 
                                       FROM MEDICO 
                                       WHERE Estado = 'Activo'");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Apellido = datos.Lector["Apellido"].ToString();
                    aux.Dni = datos.Lector["Dni"].ToString();
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "";
                    aux.Email = datos.Lector["Email"] != DBNull.Value ? datos.Lector["Email"].ToString() : "";
                    aux.Estado = datos.Lector["Estado"].ToString();

                    aux.Especialidades = ObtenerEspecialidades(aux.IdMedico);

                    listaMedico.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos: " + ex.Message);
            }

            return listaMedico;
        }

        public int Agregar(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO MEDICO 
                                       (Nombre, Apellido, Dni, Telefono, Email)
                                       VALUES (@nombre, @apellido, @dni, @tel, @mail);
                                       SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@tel", nuevo.Telefono);
                datos.setearParametro("@mail", nuevo.Email);

                return datos.obtenerId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar medico: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();
            MedicoEspecialidadNegocio negME = new MedicoEspecialidadNegocio();

            try
            {
                // -------------------------
                // 1) ACTUALIZAR DATOS DEL MÉDICO
                // -------------------------
                datos.setearConsulta(@"UPDATE MEDICO 
                               SET Nombre=@nombre, Apellido=@apellido,
                                   Dni=@dni, Telefono=@tel, Email=@mail
                               WHERE IdMedico=@id");

                datos.setearParametro("@id", medico.IdMedico);
                datos.setearParametro("@nombre", medico.Nombre);
                datos.setearParametro("@apellido", medico.Apellido);
                datos.setearParametro("@dni", medico.Dni);
                datos.setearParametro("@tel", medico.Telefono);
                datos.setearParametro("@mail", medico.Email);

                datos.ejecutarAccion();
                datos.cerrarConexion(); // cierro antes de operar con la tabla puente


                // -------------------------
                // 2) ELIMINAR ESPECIALIDADES ANTERIORES
                // -------------------------
                negME.EliminarEspecialidadesDeMedico(medico.IdMedico);


                // -------------------------
                // 3) AGREGAR NUEVAS ESPECIALIDADES
                // -------------------------
                if (medico.Especialidades != null && medico.Especialidades.Count > 0)
                {
                    negME.AgregarEspecialidades(medico.IdMedico, medico.Especialidades);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar medico: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void DarDeBaja(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MEDICO SET Estado = 'Baja' WHERE IdMedico = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dar de baja al médico: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private List<Especialidad> ObtenerEspecialidades(int idMedico)
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT E.IdEspecialidad, E.Nombre, E.Descripcion
            FROM MEDICO_ESPECIALIDAD ME
            INNER JOIN ESPECIALIDAD E ON E.IdEspecialidad = ME.IdEspecialidad
            WHERE ME.IdMedico = @IdMedico
        ");

                datos.setearParametro("@IdMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
                    esp.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    esp.Nombre = datos.Lector["Nombre"].ToString();
                    esp.Descripcion = datos.Lector["Descripcion"].ToString();

                    lista.Add(esp);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }

        public List<Medico> ListarPorEspecialidad(int idEspecialidad)
        {
            List<Medico> listaFiltrada = new List<Medico>();

            foreach (var medico in Listar())
            {
                if (medico.Especialidades.Any(e => e.IdEspecialidad == idEspecialidad))
                {
                    listaFiltrada.Add(medico);
                }
            }

            return listaFiltrada;
        }

        public Medico BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Medico medico = null;

            try
            {
                datos.setearConsulta(@"SELECT IdMedico, Nombre, Apellido, Dni, Telefono, Email, Estado 
                                       FROM MEDICO 
                                       WHERE IdMedico = @id");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    medico = new Medico
                    {
                        IdMedico = (int)datos.Lector["IdMedico"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        Dni = datos.Lector["Dni"].ToString(),
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "",
                        Email = datos.Lector["Email"] != DBNull.Value ? datos.Lector["Email"].ToString() : "",
                        Especialidades = ObtenerEspecialidades((int)datos.Lector["IdMedico"]),
                        Estado = datos.Lector["Estado"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar médico por ID: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return medico;
        }
    }
}
