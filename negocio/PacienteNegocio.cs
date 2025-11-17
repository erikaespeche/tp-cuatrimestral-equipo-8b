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
                datos.setearConsulta (@"SELECT IdPaciente, TipoDocumento, DniPaciente, Nombres, Apellidos, 
                                                FechaNacimiento, Sexo, Email, Telefono, Celular, Direccion, 
                                                Ciudad, Provincia, CodigoPostal, ObraSocial, NumeroObraSocial  FROM PACIENTES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.TipoDocumento = datos.Lector["TipoDocumento"].ToString();
                    aux.DniPaciente = (int)datos.Lector["DniPaciente"];
                    aux.Nombres = datos.Lector["Nombres"].ToString();
                    aux.Apellidos = datos.Lector["Apellidos"].ToString();
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Sexo = char.Parse(datos.Lector["Sexo"].ToString());
                    aux.Email = datos.Lector["Email"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Celular = datos.Lector["Celular"].ToString();
                    aux.Direccion = datos.Lector["Direccion"].ToString();
                    aux.Ciudad = datos.Lector["Ciudad"].ToString();
                    aux.Provincia = datos.Lector["Provincia"].ToString();
                    aux.CodigoPostal = datos.Lector["CodigoPostal"].ToString();
                    aux.ObraSocial = datos.Lector["ObraSocial"].ToString();
                    aux.NumeroObraSocial = datos.Lector["NumeroObraSocial"].ToString();

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
                datos.setearConsulta(@"
                   INSERT INTO PACIENTES 
                          (TipoDocumento, DniPaciente, Nombres, Apellidos, FechaNacimiento, Sexo,
                          Email, Telefono, Celular, Direccion, Ciudad, Provincia, CodigoPostal, ObraSocial, NumeroObraSocial)
                    VALUES 
                          (@tipoDoc, @dni, @nombres, @apellidos, @fecha, @sexo,
                           @mail, @tel, @cel, @dir, @ciudad, @prov, @cp, @obraSocial, @numObraSocial);
                    SELECT CAST(SCOPE_IDENTITY() AS INT); ");

                datos.setearParametro("@tipoDoc", nuevo.TipoDocumento);
                datos.setearParametro("@dni", nuevo.DniPaciente);
                datos.setearParametro("@nombres", nuevo.Nombres);
                datos.setearParametro("@apellidos", nuevo.Apellidos);
                datos.setearParametro("@fecha", nuevo.FechaNacimiento);
                datos.setearParametro("@sexo", nuevo.Sexo);
                datos.setearParametro("@mail", nuevo.Email);
                datos.setearParametro("@tel", nuevo.Telefono);
                datos.setearParametro("@cel", nuevo.Celular);
                datos.setearParametro("@dir", nuevo.Direccion);
                datos.setearParametro("@ciudad", nuevo.Ciudad);
                datos.setearParametro("@prov", nuevo.Provincia);
                datos.setearParametro("@cp", nuevo.CodigoPostal);
                datos.setearParametro("@obraSocial", nuevo.ObraSocial);
                datos.setearParametro("@numObraSocial", nuevo.NumeroObraSocial);

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

                datos.setearConsulta(@"
                        UPDATE PACIENTES SET 
                            TipoDocumento=@tipoDoc,
                            DniPaciente=@dni,
                            Nombres=@nombres,
                            Apellidos=@apellidos,
                            FechaNacimiento=@fecha,
                            Sexo=@sexo,
                            Email=@mail,
                            Telefono=@tel,
                            Celular=@cel,
                            Direccion=@dir,
                            Ciudad=@ciudad,
                            Provincia=@prov,
                            CodigoPostal=@cp,
                            ObraSocial=@obraSocial,
                            NumeroObraSocial=@numObraSocial
                        WHERE IdPaciente=@id");


                datos.setearParametro("@id", paciente.IdPaciente);
                datos.setearParametro("@tipoDoc", paciente.TipoDocumento);
                datos.setearParametro("@dni", paciente.DniPaciente);
                datos.setearParametro("@nombres", paciente.Nombres);
                datos.setearParametro("@apellidos", paciente.Apellidos);
                datos.setearParametro("@fecha", paciente.FechaNacimiento);
                datos.setearParametro("@sexo", paciente.Sexo);
                datos.setearParametro("@mail", paciente.Email);
                datos.setearParametro("@tel", paciente.Telefono);
                datos.setearParametro("@cel", paciente.Celular);
                datos.setearParametro("@dir", paciente.Direccion);
                datos.setearParametro("@ciudad", paciente.Ciudad);
                datos.setearParametro("@prov", paciente.Provincia);
                datos.setearParametro("@cp", paciente.CodigoPostal);
                datos.setearParametro("@obraSocial", paciente.ObraSocial);
                datos.setearParametro("@numObraSocial", paciente.NumeroObraSocial);

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

        public List<Paciente> Buscar(string documento, string nombre, string apellido)
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT IdPaciente, TipoDocumento, DniPaciente, Nombres, Apellidos,
                                   FechaNacimiento, Sexo, Email, Telefono, Celular, Direccion,
                                   Ciudad, Provincia, CodigoPostal, ObraSocial, NumeroObraSocial
                            FROM PACIENTES
                            WHERE 1=1";

                if (!string.IsNullOrEmpty(documento))
                    consulta += " AND DniPaciente LIKE @doc";

                if (!string.IsNullOrEmpty(nombre))
                    consulta += " AND Nombres LIKE @nom";

                if (!string.IsNullOrEmpty(apellido))
                    consulta += " AND Apellidos LIKE @ape";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(documento))
                    datos.setearParametro("@doc", "%" + documento + "%");

                if (!string.IsNullOrEmpty(nombre))
                    datos.setearParametro("@nom", "%" + nombre + "%");

                if (!string.IsNullOrEmpty(apellido))
                    datos.setearParametro("@ape", "%" + apellido + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.TipoDocumento = datos.Lector["TipoDocumento"].ToString();
                    aux.DniPaciente = (int)datos.Lector["DniPaciente"];
                    aux.Nombres = datos.Lector["Nombres"].ToString();
                    aux.Apellidos = datos.Lector["Apellidos"].ToString();
                    aux.ObraSocial = datos.Lector["ObraSocial"].ToString();
                    aux.NumeroObraSocial = datos.Lector["NumeroObraSocial"].ToString();

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pacientes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


        //BUSCAR PACIENTE POR DNI
        public Paciente BuscarPorDni(string dni)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT IdPaciente, TipoDocumento, DniPaciente, Nombres, Apellidos,
                           FechaNacimiento, Sexo, Email, Telefono, Celular, Direccion,
                           Ciudad, Provincia, CodigoPostal, ObraSocial, NumeroObraSocial
                        FROM PACIENTES
                        WHERE DniPaciente = @dni");

                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.TipoDocumento = datos.Lector["TipoDocumento"].ToString();
                    aux.DniPaciente = (int)datos.Lector["DniPaciente"];
                    aux.Nombres = datos.Lector["Nombres"].ToString();
                    aux.Apellidos = datos.Lector["Apellidos"].ToString();
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Sexo = char.Parse(datos.Lector["Sexo"].ToString());
                    aux.Email = datos.Lector["Email"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Celular = datos.Lector["Celular"].ToString();
                    aux.Direccion = datos.Lector["Direccion"].ToString();
                    aux.Ciudad = datos.Lector["Ciudad"].ToString();
                    aux.Provincia = datos.Lector["Provincia"].ToString();
                    aux.CodigoPostal = datos.Lector["CodigoPostal"].ToString();
                    aux.ObraSocial = datos.Lector["ObraSocial"].ToString();
                    aux.NumeroObraSocial = datos.Lector["NumeroObraSocial"].ToString();

                    return aux;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }













    }

}
