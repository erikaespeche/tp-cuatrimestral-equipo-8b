using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HistoriaClinica
{
    public int IdHistoriaClinica { get; set; }
    public int IdPaciente { get; set; }
    public int IdMedico { get; set; }
    public DateTime FechaConsulta { get; set; }

    public string Observaciones { get; set; }
    public string Diagnostico { get; set; }
    public string Tratamientos { get; set; }
    public string ProximosPasos { get; set; }

    public string ArchivosAdjuntos { get; set; } // rutas separadas por ;
}
