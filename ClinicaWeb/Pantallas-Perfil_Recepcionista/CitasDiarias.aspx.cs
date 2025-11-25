using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class CitasDiarias : System.Web.UI.Page
    {
        TurnoNegocio negocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Today;

            if (!IsPostBack)
            {
                CargarFiltros();    
            }

            
            if (!string.IsNullOrEmpty(Request["fecha"]))
            {

                fecha = DateTime.Parse(Request["fecha"]);

            }
            cargarAgenda(fecha);
            lblFechaSeleccionada.Text = fecha.ToString("dd/MM/yyyy");
        }

        private void cargarAgenda(DateTime fecha)
        {
            var lista = negocio.ListarAgendaPorFecha(fecha);
            repTurnos.DataSource = lista;
            repTurnos.DataBind();
        }

        protected void repTurnos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                string[] datos = e.CommandArgument.ToString().Split('|');

                int idTurno = int.Parse(datos[0]);
                string nuevoEstado = datos[1];

                TurnoNegocio neg = new TurnoNegocio();
                neg.CambiarEstado(idTurno, nuevoEstado);

                DateTime fechaActual = ObtenerFechaSeleccionada();
                cargarAgenda(fechaActual);
            }
        }

        private DateTime ObtenerFechaSeleccionada()
        {
            string fechaSeleccionada = Request["fecha"];

            if (!string.IsNullOrEmpty(fechaSeleccionada))
                return DateTime.Parse(fechaSeleccionada);

            return DateTime.Today;
        }

        protected void txtBuscarDNI_TextChanged(object sender, EventArgs e)
        {
            string dni = txtBuscarDNI.Text.Trim();

            var lista = negocio.ListarAgendaTodasLasFechas();

            if (!string.IsNullOrWhiteSpace(dni))
            {
                lista = lista
                    .Where(x => x.DNI != null && x.DNI.Contains(dni))
                    .ToList();
            }

            repTurnos.DataSource = lista;
            repTurnos.DataBind();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }


        private void AplicarFiltros()
        {
            
            DateTime fecha = ObtenerFechaSeleccionada();

            var lista = negocio.ListarAgendaPorFecha(fecha);

            
            if (!string.IsNullOrWhiteSpace(txtBuscarDNI.Text))
                lista = lista.Where(x => x.DNI.Contains(txtBuscarDNI.Text)).ToList();

            
            if (!string.IsNullOrWhiteSpace(ddlEstado.SelectedValue))
                lista = lista.Where(x => x.Estado == ddlEstado.SelectedValue).ToList();

            
            if (!string.IsNullOrWhiteSpace(ddlMedico.SelectedValue))
                lista = lista.Where(x => x.Medico == ddlMedico.SelectedItem.Text).ToList();

            if (!string.IsNullOrWhiteSpace(ddlEspecialidad.SelectedValue))
                lista = lista.Where(x => x.Especialidad == ddlEspecialidad.SelectedItem.Text).ToList();

            repTurnos.DataSource = lista;
            repTurnos.DataBind();
        }

        private void CargarFiltros()
        {
           
            ddlMedico.Items.Clear();
            ddlMedico.Items.Add(new ListItem("Médico", ""));

            var medicos = negocio.ListarMedicos();
            foreach (var m in medicos)
                ddlMedico.Items.Add(new ListItem(m.Nombre + " " + m.Apellido, m.IdMedico.ToString()));

            
            ddlEspecialidad.Items.Clear();
            ddlEspecialidad.Items.Add(new ListItem("Especialidad", ""));

            var especialidades = negocio.ListarEspecialidades();
            foreach (var e in especialidades)
                ddlEspecialidad.Items.Add(new ListItem(e.Nombre, e.IdEspecialidad.ToString()));
        }


      




    }
}
