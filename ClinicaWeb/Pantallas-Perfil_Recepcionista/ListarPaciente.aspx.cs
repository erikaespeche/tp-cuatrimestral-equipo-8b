using negocio;
using System;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class ListarPaciente : System.Web.UI.Page
    {


        //Buscar Paciente
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string doc = txtDocumento.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();

            PacienteNegocio negocio = new PacienteNegocio();
            var resultados = negocio.Buscar(doc, nombre, apellido);

            repPacientes.DataSource = resultados;
            repPacientes.DataBind();


        }

        //
        private void RefrescarPagina()
        {
            Response.Redirect(Request.RawUrl);
        }

        //
        private void DetectarRefresh()
        {
            // Si es la primera vez que carga, creo clave única
            if (!IsPostBack)
            {
                ViewState["RefreshKey"] = Session["RefreshKey"] = Guid.NewGuid().ToString();
            }
            else
            {
                // Si la clave cambia → el usuario tocó F5 o Refresh
                if (ViewState["RefreshKey"].ToString() != Session["RefreshKey"].ToString())
                {
                    RefrescarPagina();  //  MÉTODO
                }

                // Genero nueva clave para próximos postbacks
                Session["RefreshKey"] = Guid.NewGuid().ToString();
                ViewState["RefreshKey"] = Session["RefreshKey"];
            }
        }

        //
        protected void Page_Load(object sender, EventArgs e)
        {
            DetectarRefresh();

            if (!IsPostBack)
            {
                // Aquí tu lógica normal de carga inicial
                txtDocumento.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
            }
        }

        //Detalle del Paciente
        protected void RepPacientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                string dni = e.CommandArgument.ToString();
                Response.Redirect("DetallePaciente.aspx?dni=" + dni);
            }
        }













    }
}