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





        // negocio/UsuarioNegocio.cs

        // ... (métodos Listar, Modificar, Eliminar, etc.)

        public int Agregar(Usuario nuevoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            int idGenerado = 0;

            try
            {
                datos.setearConsulta(@"
                   INSERT INTO USUARIOS (DniUsuario, Nombres, Apellidos, NombreUsuario, Contrasena, Email, IdRol) 
                   VALUES (@dni, @nombres, @apellidos, @usuario, @contrasena, @mail, @rol);
                   SELECT CAST(SCOPE_IDENTITY() AS INT);");

                datos.setearParametro("@dni", nuevoUsuario.DniUsuario);
                datos.setearParametro("@nombres", nuevoUsuario.Nombres);
                datos.setearParametro("@apellidos", nuevoUsuario.Apellidos);
                datos.setearParametro("@usuario", nuevoUsuario.NombreUsuario);
                datos.setearParametro("@contrasena", nuevoUsuario.Contrasena);
                datos.setearParametro("@mail", nuevoUsuario.Email);
                datos.setearParametro("@rol", nuevoUsuario.IdRol);

                idGenerado = datos.ejecutarAccionScalar();
                return idGenerado;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Errores por violación de UNIQUE (duplicados)
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    if (ex.Message.Contains("UQ_Usuario_NombreUsuario"))
                        throw new Exception("El nombre de usuario ya está en uso, elija otro.");

                    throw new Exception("Ya existe un registro con valores únicos duplicados.");
                }

                throw new Exception("Error de base de datos al registrar el usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al intentar registrar el usuario: " + ex.Message);
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
                string consulta = "UPDATE USUARIOS SET DniUsuario=@dni, Nombres=@nombres, Apellidos=@apellidos, NombreUsuario=@user, Email=@mail, IdRol=@rol";

                // Solo actualizar contraseña si viene con valor
                if (!string.IsNullOrEmpty(usuario.Contrasena))
                    consulta += ", Contrasena=@pass";

                consulta += " WHERE IdUsuario=@id";

                datos.setearConsulta(consulta);

                datos.setearParametro("@id", usuario.IdUsuario);
                datos.setearParametro("@dni", usuario.DniUsuario);
                datos.setearParametro("@nombres", usuario.Nombres);
                datos.setearParametro("@apellidos", usuario.Apellidos);
                datos.setearParametro("@user", usuario.NombreUsuario);
                datos.setearParametro("@mail", usuario.Email);
                datos.setearParametro("@rol", usuario.IdRol);

                if (!string.IsNullOrEmpty(usuario.Contrasena))
                    datos.setearParametro("@pass", usuario.Contrasena);

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

        public Usuario ValidarLogin(string user, string pass)
        {
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
                  INNER JOIN ROL R ON R.IdRol = U.IdRol
                  WHERE U.NombreUsuario = @user COLLATE Latin1_General_CS_AS
                    AND U.Contrasena = @pass COLLATE Latin1_General_CS_AS
                ");



                datos.setearParametro("@user", user);
                datos.setearParametro("@pass", pass);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
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

                    aux.Rol = new Rol()
                    {
                        IdRol = aux.IdRol,
                        NombreRol = datos.Lector["NombreRol"].ToString()
                    };

                    return aux;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar login: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Usuario GetByEmail(string email)
        {
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
            INNER JOIN ROL R ON R.IdRol = U.IdRol
            WHERE U.Email = @Email
        ");

                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.IdUsuario = (int)datos.Lector["IdUsuario"];
                    u.DniUsuario = (int)datos.Lector["DniUsuario"];
                    u.Nombres = datos.Lector["Nombres"].ToString();
                    u.Apellidos = datos.Lector["Apellidos"].ToString();
                    u.NombreUsuario = datos.Lector["NombreUsuario"].ToString();
                    u.Contrasena = datos.Lector["Contrasena"].ToString();
                    u.Email = datos.Lector["Email"].ToString();
                    u.IdRol = (int)datos.Lector["IdRol"];

                    u.Rol = new Rol()
                    {
                        IdRol = u.IdRol,
                        NombreRol = datos.Lector["NombreRol"].ToString()
                    };

                    return u;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario por email: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void ActualizarPassword(int idUsuario, string nuevaPassword)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Password = @pass WHERE Id = @id");
                datos.setearParametro("@pass", nuevaPassword);
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("Error al actualizar contraseña: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }




        public Usuario ValidarLoginSoloUsuario(string user)
        {
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
            INNER JOIN ROL R ON R.IdRol = U.IdRol
            WHERE U.NombreUsuario = @user
        ");

                datos.setearParametro("@user", user);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
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

                    aux.Rol = new Rol()
                    {
                        IdRol = aux.IdRol,
                        NombreRol = datos.Lector["NombreRol"].ToString()
                    };

                    return aux;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}
