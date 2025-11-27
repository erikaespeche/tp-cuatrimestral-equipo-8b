using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaWeb.Pantasllas_Perfil_Administrador
{
    public partial class EditarEspecialidadAdm : System.Web.UI.Page
    {
        EspecialidadNegocio negocio = new EspecialidadNegocio();
        int idEspecialidad;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out idEspecialidad))
            {
                Response.Redirect("GestionarEspecialidadesAdm.aspx");
            }

            if (!IsPostBack)
            {
                CargarEspecialidad();
            }
        }

        private void CargarEspecialidad()
        {
            Especialidad esp = negocio.ObtenerPorId(idEspecialidad);
            if (esp != null)
            {
                txtNombre.Text = esp.Nombre;
                txtDescripcion.Text = esp.Descripcion;
            }
            else
            {
                lblMensaje.Text = "No se encontró la especialidad.";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblMensaje.Text = "El nombre de la especialidad no puede estar vacío.";
                return;
            }

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                lblMensaje.Text = "La descripción no puede estar vacía.";
                return;
            }

            // verifica duplicados
            if (negocio.ExisteNombre(nombre, idEspecialidad))
            {
                lblMensaje.Text = "Ya existe otra especialidad con ese nombre.";
                return;
            }

            try
            {
                Especialidad esp = new Especialidad
                {
                    IdEspecialidad = idEspecialidad,
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                negocio.Modificar(esp);
                Response.Redirect("GestionarEspecialidadesAdm.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarEspecialidadesAdm.aspx");
        }
    }
}
