using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> listaEspecialidad = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT IdEspecialidad, Nombre, Descripcion 
                                                FROM ESPECIALIDAD");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad aux = new Especialidad();
                    aux.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Descripcion = datos.Lector["Descripcion"].ToString();

                    listaEspecialidad.Add(aux);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos: " + ex.Message);
            }

            return listaEspecialidad;
        }
        public int Agregar(Especialidad nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO ESPECIALIDAD 
                    (Nombre, Descripcion)
                    VALUES (@nombre, @descripcion)");

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);

                return datos.obtenerId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar especialidad: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Especialidad especialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE ESPECIALIDAD SET 
                        Nombre=@nombre, Descripcion=@descripcion
                    WHERE IdEspecialidad=@id");

                datos.setearParametro("@id", especialidad.IdEspecialidad);
                datos.setearParametro("@nombre", especialidad.Nombre);
                datos.setearParametro("@descripcion", especialidad.Descripcion);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar especialidad: " + ex.Message);
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
                datos.setearConsulta("DELETE FROM ESPECIALIDAD WHERE IdEspecialidad = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (SqlException ex)
            {
               
                if (ex.Number == 547)
                {
                    throw new Exception("No se puede eliminar la especialidad porque está asignada a uno o más médicos.");
                }

                throw new Exception("Error al eliminar especialidad: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar especialidad: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Especialidad> BuscarPorNombre(string nombre)
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IdEspecialidad, Nombre " +
                    "FROM ESPECIALIDAD " +
                    "WHERE Nombre COLLATE Latin1_General_CI_AI LIKE @nombre COLLATE Latin1_General_CI_AI");

                datos.setearParametro("@nombre", "%" + nombre + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Especialidad
                    {
                        IdEspecialidad = (int)datos.Lector["IdEspecialidad"],
                        Nombre = datos.Lector["Nombre"].ToString()
                    });
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


    }
}
