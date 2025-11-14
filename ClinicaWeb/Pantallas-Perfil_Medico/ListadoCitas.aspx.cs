using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaWeb.Medico
{
    public partial class ListadoCitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PacienteNegocio negocio = new PacienteNegocio();
                var lista = negocio.Listar();

                gvListadoPacientes.DataSource = lista;
                gvListadoPacientes.DataBind();

                lblCantidadPacientes.Text = "Total de pacientes: " + lista.Count;
            }
        }
    }
}