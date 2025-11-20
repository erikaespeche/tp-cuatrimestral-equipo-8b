using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;


namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class GestionarProfesionales : System.Web.UI.Page
    {
        MedicoNegocio negocio = new MedicoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarProfesionales();
            }
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio espNegocio = new EspecialidadNegocio();
            ddlEspecialidad.DataSource = espNegocio.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Filtrar por Especialidad", "0"));
        }


        private void CargarProfesionales()
        {
            repProfesionales.DataSource = negocio.Listar();
            repProfesionales.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var lista = negocio.Listar();
            string nom = Normalizar(txtNombre.Text);
            string ape = Normalizar(txtApellido.Text);


            int idEsp = int.Parse(ddlEspecialidad.SelectedValue);

            var filtrado = lista.Where(p =>
                Normalizar(p.Nombre).Contains(nom) &&
                Normalizar(p.Apellido).Contains(ape) &&
                (idEsp == 0 || p.Especialidades.Any(espI => espI.IdEspecialidad == idEsp))
            ).ToList();


            repProfesionales.DataSource = filtrado;
            repProfesionales.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarNuevoProfesional.aspx");
        }

        private string Normalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            string s = texto.ToLower();

            s = s.Replace("á", "a").Replace("é", "e").Replace("í", "i")
                 .Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n");

            return s.Trim();
        }
    }
}