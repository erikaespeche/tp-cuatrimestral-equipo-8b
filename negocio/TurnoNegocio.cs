using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using ClinicaWeb.DTO;


namespace negocio
{
    public class TurnoNegocio
    {
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


    }
}