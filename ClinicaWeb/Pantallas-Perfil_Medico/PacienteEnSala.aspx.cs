using ClinicaWeb.DTO;
using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaWeb.Medico
{
    public partial class PacientesEnSala : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTurnosHoy();
            }
        }

        private void CargarTurnosHoy()
        {
            TurnoNegocio negocio = new TurnoNegocio();
            var todos = negocio.ListarAgendaPorFecha(DateTime.Today);

            // Filtro solo los turnos que se marcaron como presentes en el campo de estado admin y el campo de estado medico esta vacio
            var enEspera = todos.Where(t => t.EstadoAdmin == "Presente" && string.IsNullOrEmpty(t.EstadoMedico)).ToList();

            // En Espera
            rptTurnosEspera.DataSource = enEspera;
            rptTurnosEspera.DataBind();

            // Atendiéndose
            rptTurnosAtendiendose.DataSource = todos.Where(t => t.EstadoMedico == "Atendiéndose").ToList();
            rptTurnosAtendiendose.DataBind();

            // Atendido
            rptTurnosAtendido.DataSource = todos.Where(t => t.EstadoMedico == "Atendido").ToList();
            rptTurnosAtendido.DataBind();
        }

        protected void rptTurnosEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // ejemplo dentro del ItemCommand de "Atender"
            if (e.CommandName == "Atender")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);
                TurnoNegocio negocio = new TurnoNegocio();

                // Cambiamos el estado médico a "En Sala"
                negocio.CambiarEstadoPorNombre(idTurno, "Atendiéndose", esAdmin: false);

                // Recargamos la lista para que se vea reflejado en el repeater
                CargarTurnosHoy();
            }

        }


        protected void rptTurnosAtendiendose_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idTurno = Convert.ToInt32(e.CommandArgument);
            TurnoNegocio negocio = new TurnoNegocio();

            if (e.CommandName == "Atender")
            {
                var turno = negocio.ListarAgendaPorFecha(DateTime.Today).FirstOrDefault(t => t.IdTurno == idTurno);
                if (turno != null)
                {
                    // mover a Atendiéndose
                    var lista = new List<TurnoAgendaDTO> { turno };
                    rptTurnosAtendiendose.DataSource = lista;
                    rptTurnosAtendiendose.DataBind();

                    // remover de En Espera
                    var espera = (List<TurnoAgendaDTO>)rptTurnosEspera.DataSource;
                    CargarTurnosHoy(); 
                }
            }

            if (e.CommandName == "Finalizar")
            {
                var turno = negocio.ListarAgendaPorFecha(DateTime.Today).FirstOrDefault(t => t.IdTurno == idTurno);
                if (turno != null)
                {
                    // mover a Atendido
                    var lista = new List<TurnoAgendaDTO> { turno };
                    rptTurnosAtendido.DataSource = lista;
                    rptTurnosAtendido.DataBind();
                }
            }
        }
    }
}