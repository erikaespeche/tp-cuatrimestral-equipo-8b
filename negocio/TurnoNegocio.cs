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
                EA.NombreEstadoTurno AS EstadoAdmin,
                EM.NombreEstadoTurno AS EstadoMedico,
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
            LEFT JOIN ESTADO_TURNO EA ON T.IdEstadoTurnoAdmin = EA.IdEstadoTurno
            LEFT JOIN ESTADO_TURNO EM ON T.IdEstadoTurnoMedico = EM.IdEstadoTurno
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
                        EstadoAdmin = datos.Lector["EstadoAdmin"].ToString(),
                        EstadoMedico = datos.Lector["EstadoMedico"].ToString(),
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
                EA.NombreEstadoTurno AS EstadoAdmin,
                EM.NombreEstadoTurno AS EstadoMedico,
                T.Observaciones,
                -- PACIENTE
                P.IdPaciente,
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
            LEFT JOIN ESTADO_TURNO EA ON T.IdEstadoTurnoAdmin = EA.IdEstadoTurno
            LEFT JOIN ESTADO_TURNO EM ON T.IdEstadoTurnoMedico = EM.IdEstadoTurno

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
                    aux.EstadoAdmin = datos.Lector["EstadoAdmin"].ToString();
                    aux.EstadoMedico = datos.Lector["EstadoMedico"].ToString();
                    aux.Observaciones = datos.Lector["Observaciones"].ToString();

                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
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



        public void CambiarEstadoPorNombre(int idTurno, string nombreEstado, bool esAdmin)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Buscamos el Id del estado
                datos.setearConsulta("SELECT IdEstadoTurno FROM ESTADO_TURNO WHERE NombreEstadoTurno = @nombre");
                datos.setearParametro("@nombre", nombreEstado);
                datos.ejecutarLectura();

                int idEstado = 0;
                if (datos.Lector.Read())
                    idEstado = (int)datos.Lector["IdEstadoTurno"];
                else
                    throw new Exception("Estado no encontrado");

                datos.cerrarConexion();

                // Actualizamos el turno
                string columna = esAdmin ? "IdEstadoTurnoAdmin" : "IdEstadoTurnoMedico";
                datos.setearConsulta($"UPDATE TURNO SET {columna} = @estado WHERE IdTurno = @id");
                datos.setearParametro("@estado", idEstado);
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
                EA.NombreEstadoTurno AS EstadoAdmin,
                EM.NombreEstadoTurno AS EstadoMedico,
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
            LEFT JOIN ESTADO_TURNO EA ON T.IdEstadoTurnoAdmin = EA.IdEstadoTurno
            LEFT JOIN ESTADO_TURNO EM ON T.IdEstadoTurnoMedico = EM.IdEstadoTurno
            ORDER BY T.Fecha DESC
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TurnoAgendaDTO aux = new TurnoAgendaDTO();

                    aux.IdTurno = (int)datos.Lector["IdTurno"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.EstadoAdmin = datos.Lector["EstadoAdmin"].ToString();
                    aux.EstadoMedico = datos.Lector["EstadoMedico"].ToString();
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
                (IdPaciente, IdEspecialidad, IdMedico, Fecha, IdEstadoTurnoAdmin, IdEstadoTurnoMedico, Observaciones)
            VALUES 
                (@IdPaciente, @IdEspecialidad, @IdMedico, @Fecha, @EstadoAdmin, @EstadoMedico, @Observaciones)
        ");

                datos.setearParametro("@IdPaciente", turno.IdPaciente);
                datos.setearParametro("@IdEspecialidad", turno.IdEspecialidad);
                datos.setearParametro("@IdMedico", turno.IdMedico);
                datos.setearParametro("@Fecha", turno.Fecha); // DateTime completo
                int idEstadoPendiente = 1; 
                datos.setearParametro("@EstadoAdmin", idEstadoPendiente);
                datos.setearParametro("@EstadoMedico", DBNull.Value);
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
        public void CancelarTurno(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                int idEstadoCancelado = 3;
                datos.setearConsulta("UPDATE TURNO SET IdEstadoTurnoAdmin = @estadoCancelado, IdEstadoTurnoMedico = @estadoCancelado WHERE IdTurno = @id");
                datos.setearParametro("@estadoCancelado", idEstadoCancelado);
                datos.setearParametro("@id", idTurno);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cancelar el turno: " + ex.Message);
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

        public void ModificarFechaHora(int idTurno, DateTime nuevaFecha)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int idReprogramado = 2;
                datos.setearConsulta(
                    "UPDATE TURNO SET Fecha = @fecha, IdEstadoTurnoAdmin = @estado WHERE IdTurno = @id"
                );

                datos.setearParametro("@fecha", nuevaFecha);
                datos.setearParametro("@estado", idReprogramado);
                datos.setearParametro("@id", idTurno);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reprogramar turno: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool TurnoDisponible(int idMedico, int idPaciente, DateTime fecha)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int idCancelado = 3;
                datos.setearConsulta(@"
            SELECT COUNT(*) 
            FROM TURNO
            WHERE Fecha = @fecha
              AND IdEstadoTurnoAdmin <> @idCancelado
              AND (IdMedico = @idMedico OR IdPaciente = @idPaciente)
        ");

                datos.setearParametro("@fecha", fecha);
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idPaciente", idPaciente);
                datos.setearParametro("@idCancelado", idCancelado);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad == 0;
                }

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool MedicoEstaLibre(int idMedico, DateTime fechaCompleta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int idCancelado = 3;
                datos.setearConsulta(@"
            SELECT COUNT(*) 
            FROM TURNO
            WHERE IdMedico = @id
              AND CONVERT(datetime, Fecha) = @fecha
              AND IdEstadoTurnoAdmin <> @idCancelado
        ");

                datos.setearParametro("@id", idMedico);
                datos.setearParametro("@fecha", fechaCompleta);
                datos.setearParametro("@idCancelado", idCancelado);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0] == 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool PacienteEstaLibre(int idPaciente, DateTime fechaCompleta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int idCancelado = 3;
                datos.setearConsulta(@"
            SELECT COUNT(*) 
            FROM TURNO
            WHERE IdPaciente = @id
              AND CONVERT(datetime, Fecha) = @fecha
              AND IdEstadoTurnoAdmin <> @idCancelado
        ");

                datos.setearParametro("@id", idPaciente);
                datos.setearParametro("@fecha", fechaCompleta);
                datos.setearParametro("@idCancelado", idCancelado);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0] == 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}