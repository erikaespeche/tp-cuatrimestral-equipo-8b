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
                datos.setearConsulta(@"SELECT IdMedico, Nombre, Apellido, Dni, Telefono, Email, IdTurnoTrabajo 
                                                FROM MEDICO");
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
                    aux.IdTurnoTrabajo = datos.Lector["IdTurnoTrabajo"] != DBNull.Value ? (int)datos.Lector["IdTurnoTrabajo"] : 0;

                    //aux.IdEspecialidades = ObtenerEspecialidades(aux.IdMedico);

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
                                       (Nombre, Apellido, Dni, Telefono, Email, IdTurnoTrabajo)
                                       VALUES (@nombre, @apellido, @dni, @tel, @mail, @turno);
                                       SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@tel", nuevo.Telefono);
                datos.setearParametro("@mail", nuevo.Email);
                datos.setearParametro("@turno", nuevo.IdTurnoTrabajo);

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
            try
            {
                datos.setearConsulta(@"UPDATE MEDICO SET Nombre=@nombre, Apellido=@apellido,
                                       Dni=@dni, Telefono=@tel, Email=@mail, IdTurnoTrabajo=@turno
                                       WHERE IdMedico=@id");

                datos.setearParametro("@id", medico.IdMedico);
                datos.setearParametro("@nombre", medico.Nombre);
                datos.setearParametro("@apellido", medico.Apellido);
                datos.setearParametro("@dni", medico.Dni);
                datos.setearParametro("@tel", medico.Telefono);
                datos.setearParametro("@mail", medico.Email);
                datos.setearParametro("@turno", medico.IdTurnoTrabajo);
                datos.ejecutarAccion();
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM MEDICO WHERE IdMedico = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar medico: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
