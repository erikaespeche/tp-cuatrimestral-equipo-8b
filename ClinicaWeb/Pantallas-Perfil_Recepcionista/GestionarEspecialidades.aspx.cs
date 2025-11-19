using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class GestionarEspecialidades : System.Web.UI.Page
    {
        EspecialidadNegocio negocio = new EspecialidadNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarEspecialidades();
        }

        private void CargarEspecialidades(string filtro = "")
        {
            List<Especialidad> lista;

            if (string.IsNullOrWhiteSpace(filtro))
                lista = negocio.Listar();  
            else
                lista = negocio.BuscarPorNombre(filtro);

            repEspecialidades.DataSource = lista;
            repEspecialidades.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            string filtro = txtBuscarEspecialidad.Text.Trim();

            repEspecialidades.DataSource = negocio.BuscarPorNombre(filtro);
            repEspecialidades.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarEspecialidad.Text = "";
            CargarEspecialidades();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarNuevaEspecialidad.aspx");
        }

        protected void repEspecialidades_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idEspecialidad = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Editar")
            {
                Response.Redirect("EditarEspecialidad.aspx?id=" + idEspecialidad);
            }

            if (e.CommandName == "Eliminar")
            {
                EspecialidadNegocio neg = new EspecialidadNegocio();
                neg.Eliminar(idEspecialidad);

                CargarEspecialidades();
            }
        }


    }
}