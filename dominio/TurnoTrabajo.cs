using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class TurnoTrabajo
    {
        public int IdTurnoTrabajo { get; set; }
        public string Nombre { get; set; } // por ejemplo Mañana, Tarde, Noche  
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; } // timespan para manejar el rango horario



    }
}
