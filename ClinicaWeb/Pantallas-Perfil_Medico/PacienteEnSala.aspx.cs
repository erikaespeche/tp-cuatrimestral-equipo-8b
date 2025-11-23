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

            //  En Espera
            rptTurnosEspera.DataSource = todos;
            rptTurnosEspera.DataBind();

            rptTurnosAtendiendose.DataSource = new List<TurnoAgendaDTO>();
            rptTurnosAtendiendose.DataBind();

            rptTurnosAtendido.DataSource = new List<TurnoAgendaDTO>();
            rptTurnosAtendido.DataBind();
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