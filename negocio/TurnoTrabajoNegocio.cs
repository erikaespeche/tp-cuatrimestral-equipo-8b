//using System;
//using dominio;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace negocio
//{
//    public class TurnoTrabajoNegocio
//    {
//        public List<TurnoTrabajo> Listar()
//        {
//            List<TurnoTrabajo> lista = new List<TurnoTrabajo>();
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta(@"SELECT IdTurnoTrabajo, Nombre, HoraEntrada, HoraSalida
//                                       FROM TURNO_TRABAJO");
//                datos.ejecutarLectura();

//                while (datos.Lector.Read())
//                {
//                    TurnoTrabajo aux = new TurnoTrabajo();
//                    aux.IdTurnoTrabajo = (int)datos.Lector["IdTurnoTrabajo"];
//                    aux.Nombre = datos.Lector["Nombre"].ToString();
//                    aux.HoraEntrada = (TimeSpan)datos.Lector["HoraEntrada"];
//                    aux.HoraSalida = (TimeSpan)datos.Lector["HoraSalida"];

//                    lista.Add(aux);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error al listar turnos de trabajo: " + ex.Message);
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }

//            return lista;
//        }


//        public int Agregar(TurnoTrabajo turno)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta(@"INSERT INTO TURNO_TRABAJO 
//                                       (Nombre, HoraEntrada, HoraSalida)
//                                       VALUES (@nombre, @entrada, @salida);
//                                       SELECT SCOPE_IDENTITY();");

//                datos.setearParametro("@nombre", turno.Nombre);
//                datos.setearParametro("@entrada", turno.HoraEntrada);
//                datos.setearParametro("@salida", turno.HoraSalida);

//                return datos.obtenerId();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error al agregar turno de trabajo: " + ex.Message);
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }
//        }

//        public void Modificar(TurnoTrabajo turno)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta(@"UPDATE TURNO_TRABAJO SET 
//                                          Nombre = @nombre,
//                                          HoraEntrada = @entrada,
//                                          HoraSalida = @salida
//                                       WHERE IdTurnoTrabajo = @id");

//                datos.setearParametro("@id", turno.IdTurnoTrabajo);
//                datos.setearParametro("@nombre", turno.Nombre);
//                datos.setearParametro("@entrada", turno.HoraEntrada);
//                datos.setearParametro("@salida", turno.HoraSalida);

//                datos.ejecutarAccion();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error al modificar turno de trabajo: " + ex.Message);
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }
//        }


//        public void Eliminar(int id)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta("DELETE FROM TURNO_TRABAJO WHERE IdTurnoTrabajo = @id");
//                datos.setearParametro("@id", id);
//                datos.ejecutarAccion();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error al eliminar turno de trabajo: " + ex.Message);
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }
//        }
//    }
//}
