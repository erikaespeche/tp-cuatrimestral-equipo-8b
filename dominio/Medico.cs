using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int IdTurnoTrabajo { get; set; }
        public string Estado { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
