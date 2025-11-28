using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disponibilidad
    {
        public int IdDisponibilidad { get; set; }
        public int IdAgenda { get; set; }
        public int DiaSemana { get; set; } // 1 = lunes ... 7 = domingo
        public TimeSpan Hora { get; set; }
    }
}

