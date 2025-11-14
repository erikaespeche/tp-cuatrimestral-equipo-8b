using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Contraseña, Rol, Email FROM USUARIOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Contraseña = datos.Lector["Contraseña"].ToString();
                    aux.Rol = datos.Lector["Rol"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }

        public int Agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    INSERT INTO USUARIOS (Nombre, Contraseña, Rol, Email)
                    VALUES (@nombre, @contraseña, @rol, @mail);
                    SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@contraseña", nuevo.Contraseña);
                datos.setearParametro("@rol", nuevo.Rol);
                datos.setearParametro("@mail", nuevo.Email);

                return datos.obtenerId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    UPDATE USUARIOS SET 
                        Nombre = @nombre,
                        Contraseña = @contraseña,
                        Rol = @rol,
                        Email = @mail
                    WHERE IdUsuario = @id");

                datos.setearParametro("@id", usuario.IdUsuario);
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@contraseña", usuario.Contraseña);
                datos.setearParametro("@rol", usuario.Rol);
                datos.setearParametro("@mail", usuario.Email);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario: " + ex.Message);
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
                datos.setearConsulta("DELETE FROM USUARIOS WHERE IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Contraseña, Rol, Email FROM USUARIOS WHERE IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario()
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Contraseña = datos.Lector["Contraseña"].ToString(),
                        Rol = datos.Lector["Rol"].ToString(),
                        Email = datos.Lector["Email"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return usuario;
        }
    }
}