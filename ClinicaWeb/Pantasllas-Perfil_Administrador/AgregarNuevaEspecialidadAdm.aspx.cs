using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ClinicaWeb.Pantasllas_Perfil_Administrador
{
    public partial class AgregarNuevaEspecialidadAdm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnRegistrarEspecialidad_Click(object sender, EventArgs e)
        {
            bool valido = true;
            string mensajeError = "";

            EspecialidadNegocio negocio = new EspecialidadNegocio();

            string nombre = txtNombreEspecialidad.Value.Trim();
            string descripcion = txtDescripcion.Value.Trim();

            // Nombre
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MarcarError(txtNombreEspecialidad);
                mensajeError += "El nombre de la especialidad no puede estar vacío.<br>";
                valido = false;
            }
            else if (negocio.ExisteNombre(nombre)) 
            {
                MarcarError(txtNombreEspecialidad);
                mensajeError += "Ya existe una especialidad con ese nombre.<br>";
                valido = false;
            }
            else
            {
                MarcarOk(txtNombreEspecialidad);
            }

            // Descripción
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MarcarError(txtDescripcion);
                mensajeError += "La descripción no puede estar vacía.<br>";
                valido = false;
            }
            else
            {
                MarcarOk(txtDescripcion);
            }

            if (!valido)
            {
                mensajeError = mensajeError.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{mensajeError}';",
                    true
                );
                return;
            }

           
            try
            {
                Especialidad nueva = new Especialidad
                {
                    Nombre = nombre,
                    Descripcion = descripcion
                };
                negocio.Agregar(nueva);

                // Modal éxito
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalExito",
                    "var m = new bootstrap.Modal(document.getElementById('modalExito')); m.show();",
                    true
                );
            }
            catch (Exception ex)
            {
                string msg = "Error inesperado al registrar la especialidad: " + ex.Message;
                msg = msg.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{msg}';",
                    true
                );
            }
        }



        private void MarcarError(HtmlControl input)
        {
            input.Attributes["class"] = "form-control is-invalid";
        }

        private void MarcarOk(HtmlControl input)
        {
            input.Attributes["class"] = "form-control is-valid";
        }

    
        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarEspecialidadesAdm.aspx");
        }

        
        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // No hacemos nada: Bootstrap cierra el modal solo
        }

    
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarEspecialidadesAdm.aspx");
        }
    }
}
