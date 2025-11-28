using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class AgendaNegocio
    {
        // Guardar agenda completa
        public int CrearAgenda(Agenda agenda)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    @"INSERT INTO AGENDA_PROFESIONAL 
                      (IdMedico, IdEspecialidad, DuracionTurno, PacientesPorTurno, FechaDesde, FechaHasta)
                      OUTPUT Inserted.IdAgenda
                      VALUES (@med, @esp, @duracion, @pacientes, @desde, @hasta)");

                datos.setearParametro("@med", agenda.IdMedico);
                datos.setearParametro("@esp", agenda.IdEspecialidad);
                datos.setearParametro("@duracion", agenda.DuracionTurno);
                datos.setearParametro("@pacientes", agenda.PacientesPorTurno);
                datos.setearParametro("@desde", agenda.FechaDesde);
                datos.setearParametro("@hasta", agenda.FechaHasta);

                int idAgenda = (int)datos.ejecutarAccionScalar();
                return idAgenda;
            }
            finally { datos.cerrarConexion(); }
        }

        // Guardar disponibilidad semanal
        public void GuardarDisponibilidad(int idAgenda, List<Disponibilidad> lista)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                foreach (var d in lista)
                {
                    datos.setearConsulta(
                        @"INSERT INTO AGENDA_DISPONIBILIDAD (IdAgenda, DiaSemana, Hora)
                          VALUES (@idAgenda, @dia, @hora)");

                    datos.setearParametro("@idAgenda", idAgenda);
                    datos.setearParametro("@dia", d.DiaSemana);
                    datos.setearParametro("@hora", d.Hora);

                    datos.ejecutarAccion();
                }
            }
            finally { datos.cerrarConexion(); }
        }


        public bool ExisteAgendaIgual(int idMedico, int idEspecialidad, DateTime desde, DateTime hasta, List<Disponibilidad> nuevas)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1) Buscar agendas que se superpongan en fechas
                datos.setearConsulta(@"
            SELECT IdAgenda
            FROM AGENDA_PROFESIONAL
            WHERE IdMedico = @medico
              AND IdEspecialidad = @especialidad
              AND (FechaDesde <= @hasta AND FechaHasta >= @desde)
        ");

                datos.setearParametro("@medico", idMedico);
                datos.setearParametro("@especialidad", idEspecialidad);
                datos.setearParametro("@desde", desde.Date);
                datos.setearParametro("@hasta", hasta.Date);

                datos.ejecutarLectura();

                List<int> agendasSuperpuestas = new List<int>();

                while (datos.Lector.Read())
                    agendasSuperpuestas.Add((int)datos.Lector["IdAgenda"]);

                datos.cerrarConexion();

                if (!agendasSuperpuestas.Any())
                    return false;

                // Convertir nuevas disponibilidades a clave
                var clavesNuevas = nuevas
                    .Select(d => $"{d.DiaSemana}-{d.Hora:hh\\:mm}")
                    .ToHashSet();

                // 2) Para cada agenda existente verificar cruces de horarios
                foreach (int idAgenda in agendasSuperpuestas)
                {
                    AccesoDatos datos2 = new AccesoDatos();

                    datos2.setearConsulta(@"
                SELECT DiaSemana, Hora
                FROM AGENDA_DISPONIBILIDAD
                WHERE IdAgenda = @id");

                    datos2.setearParametro("@id", idAgenda);

                    datos2.ejecutarLectura();

                    var clavesExistentes = new HashSet<string>();

                    while (datos2.Lector.Read())
                    {
                        string dia = datos2.Lector["DiaSemana"].ToString();
                        string hora = TimeSpan.Parse(datos2.Lector["Hora"].ToString()).ToString(@"hh\:mm");
                        clavesExistentes.Add($"{dia}-{hora}");
                    }

                    datos2.cerrarConexion();

                    // ✔ Si hay AL MENOS un horario igual → es duplicada
                    if (clavesNuevas.Any(x => clavesExistentes.Contains(x)))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






















































    }
}
