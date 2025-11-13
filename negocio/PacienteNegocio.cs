using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace negocio
{
    public class PacienteNegocio
    {
        public List<Paciente> Listar()
        {
            List<Paciente> listaPacientes = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta (@"SELECT IdPaciente, DniPaciente, Nombres, Apellidos, 
                                                FechaNacimiento, Sexo, GrupoSanguineo, Email, Telefono, Celular, Direccion, 
                                                Ciudad, Provincia, CodigoPostal FROM PACIENTES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.DniPaciente = (int)datos.Lector["DniPaciente"];
                    aux.Nombres = datos.Lector["Nombres"].ToString();
                    aux.Apellidos = datos.Lector["Apellidos"].ToString();
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Sexo = char.Parse(datos.Lector["Sexo"].ToString());
                    aux.GrupoSanguineo = datos.Lector["GrupoSanguineo"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Celular = datos.Lector["Celular"].ToString();
                    aux.Direccion = datos.Lector["Direccion"].ToString();
                    aux.Ciudad = datos.Lector["Ciudad"].ToString();
                    aux.Provincia = datos.Lector["Provincia"].ToString();
                    aux.CodigoPostal = datos.Lector["CodigoPostal"].ToString();

                    listaPacientes.Add(aux);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos: " + ex.Message);
            }

            return listaPacientes;
        }
            public int Agregar(Paciente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO PACIENTES 
                    (DniPaciente, Nombres, Apellidos, FechaNacimiento, Sexo, GrupoSanguineo,
                     Email, Telefono, Celular, Direccion, Ciudad, Provincia, CodigoPostal)
                    VALUES (@dni, @nombres, @apellidos, @fecha, @sexo, @grupo,
                            @mail, @tel, @cel, @dir, @ciudad, @prov, @cp)");

                datos.setearParametro("@dni", nuevo.DniPaciente);
                datos.setearParametro("@nombres", nuevo.Nombres);
                datos.setearParametro("@apellidos", nuevo.Apellidos);
                datos.setearParametro("@fecha", nuevo.FechaNacimiento);
                datos.setearParametro("@sexo", nuevo.Sexo);
                datos.setearParametro("@grupo", nuevo.GrupoSanguineo);
                datos.setearParametro("@mail", nuevo.Email);
                datos.setearParametro("@tel", nuevo.Telefono);
                datos.setearParametro("@cel", nuevo.Celular);
                datos.setearParametro("@dir", nuevo.Direccion);
                datos.setearParametro("@ciudad", nuevo.Ciudad);
                datos.setearParametro("@prov", nuevo.Provincia);
                datos.setearParametro("@cp", nuevo.CodigoPostal);

                return datos.obtenerId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar paciente: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE PACIENTES SET 
                        DniPaciente=@dni, Nombres=@nombres, Apellidos=@apellidos, 
                        FechaNacimiento=@fecha, Sexo=@sexo, GrupoSanguineo=@grupo,
                        Email=@mail, Telefono=@tel, Celular=@cel, Direccion=@dir,
                        Ciudad=@ciudad, Provincia=@prov, CodigoPostal=@cp
                    WHERE IdPaciente=@id");

                datos.setearParametro("@id", paciente.IdPaciente);
                datos.setearParametro("@dni", paciente.DniPaciente);
                datos.setearParametro("@nombres", paciente.Nombres);
                datos.setearParametro("@apellidos", paciente.Apellidos);
                datos.setearParametro("@fecha", paciente.FechaNacimiento);
                datos.setearParametro("@sexo", paciente.Sexo);
                datos.setearParametro("@grupo", paciente.GrupoSanguineo);
                datos.setearParametro("@mail", paciente.Email);
                datos.setearParametro("@tel", paciente.Telefono);
                datos.setearParametro("@cel", paciente.Celular);
                datos.setearParametro("@dir", paciente.Direccion);
                datos.setearParametro("@ciudad", paciente.Ciudad);
                datos.setearParametro("@prov", paciente.Provincia);
                datos.setearParametro("@cp", paciente.CodigoPostal);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar paciente: " + ex.Message);
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
                datos.setearConsulta("DELETE FROM PACIENTES WHERE IdPaciente = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar paciente: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }

}
