using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Agenda
    {
        public int IdAgenda { get; set; }
        public int IdMedico { get; set; }
        public int IdEspecialidad { get; set; }
        public int DuracionTurno { get; set; }   // minutos
        public int PacientesPorTurno { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public List<Disponibilidad> Disponibilidades { get; set; }
    }
}

