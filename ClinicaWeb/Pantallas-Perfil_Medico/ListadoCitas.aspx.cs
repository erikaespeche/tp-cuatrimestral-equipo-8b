using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Medico
{
    public partial class ListadoCitas : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        PacienteNegocio negocio = new PacienteNegocio();
        //        var lista = negocio.Listar();

        //        gvListadoPacientes.DataSource = lista;
        //        gvListadoPacientes.DataBind();

        //        lblCantidadPacientes.Text = "Total de pacientes: " + lista.Count;
        //    }
        //}





        public class Cita
        {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DNI { get; set; }
            public string ObraSocial { get; set; }
            public bool Reservada { get; set; } // true = tomada; false = disponible
        }

        private static List<Cita> _citasBase;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime hoy = DateTime.Today;

                Calendar1.VisibleDate = hoy;
                Calendar1.SelectedDate = hoy;

                
                CargarCitas(hoy, SearchText.Text);
            }
        }


        private void CargarCitas(DateTime fecha, string search)
        {
            TurnoNegocio negocio = new TurnoNegocio();

            
            var citasBD = negocio.ListarAgendaPorFecha(fecha);

           
            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = search.Trim().ToLowerInvariant();
                citasBD = citasBD.Where(c =>
                    (!string.IsNullOrEmpty(c.Paciente) && c.Paciente.ToLowerInvariant().Contains(s)) ||
                    (!string.IsNullOrEmpty(c.DNI) && c.DNI.ToLowerInvariant().Replace(".", "").Contains(s.Replace(".", "")))
                ).ToList();
            }

            var listaParaRepeater = citasBD.Select(c => new
            {
                Id = c.IdTurno,
                Fecha = c.Fecha,
                Hora = c.Fecha.ToString("HH:mm"), 
                Nombre = string.IsNullOrWhiteSpace(c.Paciente) ? "—" : c.Paciente.Split(' ')[0],
                Apellido = string.IsNullOrWhiteSpace(c.Paciente) ? "—" : string.Join(" ", c.Paciente.Split(' ').Skip(1)),
                DNI = string.IsNullOrWhiteSpace(c.DNI) ? "—" : c.DNI,
                ObraSocial = string.IsNullOrWhiteSpace(c.ObraSocial) ? "—" : c.ObraSocial
            }).ToList();

            CitasRepeater.DataSource = listaParaRepeater;
            CitasRepeater.DataBind();

            TotalCountLabel.Text = citasBD.Count.ToString();
            AvailableCountLabel.Text = citasBD.Count(c => c.Estado.ToLower() != "reservado").ToString();
           
            SelectedDateLabel.Text = fecha.ToString("dd/MM/yyyy");
        }


        

      
        private void SeedData()
        {
            if (_citasBase != null && _citasBase.Count > 0) return;

            var fecha = new DateTime(2024, 6, 12);
            var lista = new List<Cita>();
            int id = 1;

            // Slots cada 15 min de 09:00 a 12:00 (25 turnos) simulando 8 disponibles
            string[] horas = {
            "09:00","09:15","09:30","09:45",
            "10:00","10:15","10:30","10:45",
            "11:00","11:15","11:30","11:45",
            "12:00","12:15","12:30","12:45",
            "13:00","13:15","13:30","13:45",
            "14:00","14:15","14:30","14:45","15:00"
        };

            // Algunos ocupados como en el boceto
            var ocupados = new List<Cita>
        {
            new Cita{ Id=id++, Fecha=fecha, Hora="09:00", Nombre="Juan", Apellido="Pérez", DNI="12.345.678", ObraSocial="OSDE", Reservada=true },
            new Cita{ Id=id++, Fecha=fecha, Hora="09:15", Nombre="María", Apellido="González", DNI="23.456.789", ObraSocial="Swiss Medical", Reservada=true },
            new Cita{ Id=id++, Fecha=fecha, Hora="09:30", Nombre="Carlos", Apellido="Rodríguez", DNI="34.567.890", ObraSocial="Galeno", Reservada=true },
            new Cita{ Id=id++, Fecha=fecha, Hora="10:00", Nombre="Laura", Apellido="Martínez", DNI="45.678.901", ObraSocial="OSDE", Reservada=true }
        };

            // Completar el resto con disponibles
            var ocupadasHoras = ocupados.Select(o => o.Hora).ToHashSet();
            foreach (var h in horas)
            {
                if (!ocupadasHoras.Contains(h))
                {
                    lista.Add(new Cita
                    {
                        Id = id++,
                        Fecha = fecha,
                        Hora = h,
                        Nombre = "",
                        Apellido = "",
                        DNI = "",
                        ObraSocial = "",
                        Reservada = false
                    });
                }
            }

            // Mezclar
            _citasBase = ocupados.Concat(lista).OrderBy(c => TimeSpan.Parse(c.Hora)).ToList();

            // Agregar algunas citas de otros días para probar cambio de fecha
            var otroDia = new DateTime(2024, 6, 13);
            _citasBase.Add(new Cita { Id = id++, Fecha = otroDia, Hora = "09:00", Nombre = "Ana", Apellido = "Suárez", DNI = "31.234.567", ObraSocial = "OSP", Reservada = true });
            _citasBase.Add(new Cita { Id = id++, Fecha = otroDia, Hora = "09:15", Reservada = false, Nombre = "", Apellido = "", DNI = "", ObraSocial = "" });
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            CargarCitas(Calendar1.SelectedDate, SearchText.Text);
        }

        protected void PrevMonthBtn_Click(object sender, EventArgs e)
        {
            var v = Calendar1.VisibleDate == DateTime.MinValue ? DateTime.Today : Calendar1.VisibleDate;
            Calendar1.VisibleDate = v.AddMonths(-1);
        }

        protected void NextMonthBtn_Click(object sender, EventArgs e)
        {
            var v = Calendar1.VisibleDate == DateTime.MinValue ? DateTime.Today : Calendar1.VisibleDate;
            Calendar1.VisibleDate = v.AddMonths(1);
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            CargarCitas(Calendar1.SelectedDate, SearchText.Text); ;
        }

        private void BindForDate(DateTime fecha, string search)
        {
            var delDia = _citasBase.Where(c => c.Fecha.Date == fecha.Date);

            // Filtro por búsqueda (nombre, apellido, DNI)
            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = search.Trim().ToLowerInvariant();
                delDia = delDia.Where(c =>
                    (!string.IsNullOrEmpty(c.Nombre) && c.Nombre.ToLowerInvariant().Contains(s)) ||
                    (!string.IsNullOrEmpty(c.Apellido) && c.Apellido.ToLowerInvariant().Contains(s)) ||
                    (!string.IsNullOrEmpty(c.DNI) && c.DNI.ToLowerInvariant().Replace(".", "").Contains(s.Replace(".", "")))
                );
            }

            var lista = delDia.OrderBy(c => TimeSpan.Parse(c.Hora)).ToList();
            CitasRepeater.DataSource = lista.Select(c => new
            {
                c.Id,
                c.Fecha,
                c.Hora,
                Nombre = string.IsNullOrWhiteSpace(c.Nombre) ? "—" : c.Nombre,
                Apellido = string.IsNullOrWhiteSpace(c.Apellido) ? "—" : c.Apellido,
                DNI = string.IsNullOrWhiteSpace(c.DNI) ? "—" : c.DNI,
                ObraSocial = string.IsNullOrWhiteSpace(c.ObraSocial) ? "—" : c.ObraSocial
            });
            CitasRepeater.DataBind();

            // Contadores
            int total = _citasBase.Count(c => c.Fecha.Date == fecha.Date);
            int disponibles = _citasBase.Count(c => c.Fecha.Date == fecha.Date && !c.Reservada);

            TotalCountLabel.Text = total.ToString();
            AvailableCountLabel.Text = disponibles.ToString();
            SelectedDateLabel.Text = fecha.ToString("dd/MM/yyyy");
        }


        protected void CitasRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {
                int dni = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("DetallePaciente.aspx?dni=" + dni);
            }


        }

     }
}