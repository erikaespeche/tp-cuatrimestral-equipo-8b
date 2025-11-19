using dominio;
using negocio;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class AgregarNuevaEspecialidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // ============================
        //   BOTÓN REGISTRAR
        // ============================
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
            else if (negocio.ExisteNombre(nombre)) // verifica si hay duplicados
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

            // Mostrar modal si hay errores
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

            // Guardar en BD
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



        // ============================
        //   MARCAR INPUTS OK / ERROR
        // ============================
        private void MarcarError(HtmlControl input)
        {
            input.Attributes["class"] = "form-control is-invalid";
        }

        private void MarcarOk(HtmlControl input)
        {
            input.Attributes["class"] = "form-control is-valid";
        }

        // ============================
        //   MODAL ÉXITO - ACEPTAR
        // ============================
        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarEspecialidades.aspx");
        }

        // ============================
        //   MODAL ERROR - ACEPTAR
        // ============================
        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // No hacemos nada: Bootstrap cierra el modal solo
        }

        // ============================
        //   BOTÓN CANCELAR
        // ============================
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarEspecialidades.aspx");
        }
    }
}
