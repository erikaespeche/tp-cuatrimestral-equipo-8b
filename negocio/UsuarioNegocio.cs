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
                datos.setearConsulta(@"
                    SELECT 
                        U.IdUsuario,
                        U.DniUsuario,
                        U.Nombres,
                        U.Apellidos,
                        U.NombreUsuario,
                        U.Contrasena,
                        U.Email,
                        U.IdRol,
                        R.NombreRol
                    FROM USUARIOS U
                    INNER JOIN ROL R ON R.IdRol = U.IdRol");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.DniUsuario = (int)datos.Lector["DniUsuario"];
                    aux.Nombres = datos.Lector["Nombres"].ToString();
                    aux.Apellidos = datos.Lector["Apellidos"].ToString();
                    aux.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    aux.Contrasena = datos.Lector["Contrasena"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();
                    aux.IdRol = (int)datos.Lector["IdRol"];

                    // Cargar objeto Rol
                    aux.Rol = new Rol()
                    {
                        IdRol = aux.IdRol,
                        NombreRol = datos.Lector["NombreRol"].ToString()
                    };

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
                    INSERT INTO USUARIOS 
                    (DniUsuario, Nombres, Apellidos, NombreUsuario, Contrasena, Email, IdRol)
                    VALUES (@dni, @nombres, @apellidos, @user, @pass, @mail, @rol);
                    SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@dni", nuevo.DniUsuario);
                datos.setearParametro("@nombres", nuevo.Nombres);
                datos.setearParametro("@apellidos", nuevo.Apellidos);
                datos.setearParametro("@user", nuevo.NombreUsuario);
                datos.setearParametro("@pass", nuevo.Contrasena);
                datos.setearParametro("@mail", nuevo.Email);
                datos.setearParametro("@rol", nuevo.IdRol);

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
                        DniUsuario = @dni,
                        Nombres = @nombres,
                        Apellidos = @apellidos,
                        NombreUsuario = @user,
                        Contrasena = @pass,
                        Email = @mail,
                        IdRol = @rol
                    WHERE IdUsuario = @id");

                datos.setearParametro("@id", usuario.IdUsuario);
                datos.setearParametro("@dni", usuario.DniUsuario);
                datos.setearParametro("@nombres", usuario.Nombres);
                datos.setearParametro("@apellidos", usuario.Apellidos);
                datos.setearParametro("@user", usuario.NombreUsuario);
                datos.setearParametro("@pass", usuario.Contrasena);
                datos.setearParametro("@mail", usuario.Email);
                datos.setearParametro("@rol", usuario.IdRol);

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
                datos.setearConsulta(@"
                    SELECT 
                        U.IdUsuario,
                        U.DniUsuario,
                        U.Nombres,
                        U.Apellidos,
                        U.NombreUsuario,
                        U.Contrasena,
                        U.Email,
                        U.IdRol,
                        R.NombreRol
                    FROM USUARIOS U
                    INNER JOIN ROL R ON R.IdRol = U.IdRol
                    WHERE U.IdUsuario = @id");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario()
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        DniUsuario = (int)datos.Lector["DniUsuario"],
                        Nombres = datos.Lector["Nombres"].ToString(),
                        Apellidos = datos.Lector["Apellidos"].ToString(),
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        Contrasena = datos.Lector["Contrasena"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        IdRol = (int)datos.Lector["IdRol"],
                        Rol = new Rol()
                        {
                            IdRol = (int)datos.Lector["IdRol"],
                            NombreRol = datos.Lector["NombreRol"].ToString()
                        }
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
