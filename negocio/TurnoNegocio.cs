using ClinicaWeb.DTO;
using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class TurnoNegocio
    {
        public List<TurnoAgendaDTO> ListarPorPaciente(int idPaciente)
        {
            List<TurnoAgendaDTO> lista = new List<TurnoAgendaDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT  
                T.IdTurno,
                T.Fecha,
                T.Estado,
                T.Observaciones,
                P.IdPaciente,
                P.Nombres + ' ' + P.Apellidos AS Paciente,
                P.DniPaciente AS DNI,
                P.ObraSocial,
                M.Nombre + ' ' + M.Apellido AS Medico,
                E.Nombre AS Especialidad
            FROM TURNO T
            INNER JOIN PACIENTES P ON P.IdPaciente = T.IdPaciente
            INNER JOIN MEDICO M ON M.IdMedico = T.IdMedico
            INNER JOIN ESPECIALIDAD E ON E.IdEspecialidad = T.IdEspecialidad
            WHERE P.IdPaciente = @idPaciente
            ORDER BY T.Fecha DESC
        ");
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoAgendaDTO aux = new TurnoAgendaDTO
                    {
                        IdTurno = (int)datos.Lector["IdTurno"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Estado = datos.Lector["Estado"].ToString(),
                        Observaciones = datos.Lector["Observaciones"].ToString(),
                        Paciente = datos.Lector["Paciente"].ToString(),
                        DNI = datos.Lector["DNI"].ToString(),
                        ObraSocial = datos.Lector["ObraSocial"].ToString(),
                        Medico = datos.Lector["Medico"].ToString(),
                        Especialidad = datos.Lector["Especialidad"].ToString(),
                        IdPaciente = (int)datos.Lector["IdPaciente"]
                    };
                    lista.Add(aux);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }

        public List<TurnoAgendaDTO> ListarAgendaPorFecha(DateTime fecha)
        {
            List<TurnoAgendaDTO> lista = new List<TurnoAgendaDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT  
                T.IdTurno,
                T.Fecha,
                T.Estado,
                T.Observaciones,
                -- PACIENTE
                P.Nombres + ' ' + P.Apellidos AS Paciente,
                P.DniPaciente AS DNI,
                P.ObraSocial,

                -- MEDICO
                M.Nombre + ' ' + M.Apellido AS Medico,

                -- ESPECIALIDAD
                E.Nombre AS Especialidad

            FROM TURNO T
            INNER JOIN PACIENTES P ON P.IdPaciente = T.IdPaciente
            INNER JOIN MEDICO M ON M.IdMedico = T.IdMedico
            INNER JOIN ESPECIALIDAD E ON E.IdEspecialidad = T.IdEspecialidad

            WHERE CONVERT(date, T.Fecha) = @fecha
            ORDER BY T.Fecha
        ");

                datos.setearParametro("@fecha", fecha);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoAgendaDTO aux = new TurnoAgendaDTO();

                    aux.IdTurno = (int)datos.Lector["IdTurno"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Estado = datos.Lector["Estado"].ToString();
                    aux.Observaciones = datos.Lector["Observaciones"].ToString();

                    aux.Paciente = datos.Lector["Paciente"].ToString();
                    aux.DNI = datos.Lector["DNI"].ToString();
                    aux.ObraSocial = datos.Lector["ObraSocial"].ToString();

                    aux.Medico = datos.Lector["Medico"].ToString();
                    aux.Especialidad = datos.Lector["Especialidad"].ToString();

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar agenda: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }



        public void CambiarEstado(int idTurno, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE TURNO SET Estado = @estado WHERE IdTurno = @id");
                datos.setearParametro("@estado", nuevoEstado);
                datos.setearParametro("@id", idTurno);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Medico> ListarMedicos()
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMedico, Nombre, Apellido FROM MEDICO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Apellido = datos.Lector["Apellido"].ToString();

                    lista.Add(aux);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


        public List<Especialidad> ListarEspecialidades()
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdEspecialidad, Nombre FROM ESPECIALIDAD");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad aux = new Especialidad();
                    aux.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(aux);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }


        public List<TurnoAgendaDTO> ListarAgendaTodasLasFechas()
        {
            List<TurnoAgendaDTO> lista = new List<TurnoAgendaDTO>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT  
                T.IdTurno,
                T.Fecha,
                T.Estado,
                T.Observaciones,
                P.IdPaciente,
                P.Nombres + ' ' + P.Apellidos AS Paciente,
                P.DniPaciente AS DNI,
                P.ObraSocial,

                M.Nombre + ' ' + M.Apellido AS Medico,
                E.Nombre AS Especialidad

            FROM TURNO T
            INNER JOIN PACIENTES P ON P.IdPaciente = T.IdPaciente
            INNER JOIN MEDICO M ON M.IdMedico = T.IdMedico
            INNER JOIN ESPECIALIDAD E ON E.IdEspecialidad = T.IdEspecialidad
            ORDER BY T.Fecha DESC
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoAgendaDTO aux = new TurnoAgendaDTO();

                    aux.IdTurno = (int)datos.Lector["IdTurno"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Estado = datos.Lector["Estado"].ToString();
                    aux.Observaciones = datos.Lector["Observaciones"].ToString();
                    aux.Paciente = datos.Lector["Paciente"].ToString();
                    aux.DNI = datos.Lector["DNI"].ToString();
                    aux.ObraSocial = datos.Lector["ObraSocial"].ToString();
                    aux.Medico = datos.Lector["Medico"].ToString();
                    aux.Especialidad = datos.Lector["Especialidad"].ToString();
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];

                    lista.Add(aux);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }

        public void Agregar(Turno turno)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO TURNO 
                (IdPaciente, IdEspecialidad, IdMedico, Fecha, Estado, Observaciones)
            VALUES 
                (@IdPaciente, @IdEspecialidad, @IdMedico, @Fecha, @Estado, @Observaciones)
        ");

                datos.setearParametro("@IdPaciente", turno.IdPaciente);
                datos.setearParametro("@IdEspecialidad", turno.IdEspecialidad);
                datos.setearParametro("@IdMedico", turno.IdMedico);
                datos.setearParametro("@Fecha", turno.Fecha); // DateTime completo
                datos.setearParametro("@Estado", "Pendiente"); // Estado inicial
                datos.setearParametro("@Observaciones", turno.Observaciones ?? "");

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar turno: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Hora> HorasDisponibles(int idMedico, DateTime fecha)
        {
            List<Hora> lista = new List<Hora>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT Hora
            FROM HORARIO
            WHERE IdMedico = @idMedico
              AND Hora NOT IN (
                  SELECT CONVERT(time, T.Fecha)
                  FROM TURNO T
                  WHERE T.IdMedico = @idMedico
                    AND CONVERT(date, T.Fecha) = @fecha
              )
            ORDER BY Hora
        ");
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@fecha", fecha);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Hora { HoraStr = datos.Lector["Hora"].ToString() });
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }

        public class Hora
        {
            public string HoraStr { get; set; }
        }

        public void EliminarTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM TURNO WHERE IdTurno = @id");
                datos.setearParametro("@id", idTurno);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el turno: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Turno turno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE Turnos SET 
             IdMedico = @IdMedico, 
             IdEspecialidad = @IdEspecialidad, 
             Fecha = @Fecha,
             Observaciones = @Observaciones
             WHERE IdTurno = @IdTurno");
                datos.setearParametro("@IdMedico", turno.IdMedico);
                datos.setearParametro("@IdEspecialidad", turno.IdEspecialidad);
                datos.setearParametro("@Fecha", turno.Fecha);
                datos.setearParametro("@Observaciones", turno.Observaciones);
                datos.setearParametro("@IdTurno", turno.IdTurno);

                datos.ejecutarAccion(); // <- actualizar en DB, no leer
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el turno: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}