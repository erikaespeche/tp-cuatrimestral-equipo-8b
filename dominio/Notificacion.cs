using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Notificacion
    {
        public int IdNotificacion { get; set; }
        public int IdTurno { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Medio { get; set; }     // si es por mail, msj, etc
        public string Estado { get; set; }
    }
}
