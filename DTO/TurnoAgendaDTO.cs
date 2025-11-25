using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWeb.DTO
{
    public class TurnoAgendaDTO
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }

        public int IdPaciente { get; set; }
        public string Hora => Fecha.ToString("HH:mm");

        public string Paciente { get; set; }
        public string DNI { get; set; }
        public string ObraSocial { get; set; }

        public string Medico { get; set; }
        public string Especialidad { get; set; }

        public string Estado { get; set; }
        public string Observaciones { get; set; }

    }
}