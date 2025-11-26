using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;



namespace Clinic.Pantallas_Inicio_Menu
{
    public partial class CambiarContrasenaRecepcionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    // Validación: si no hay usuario logueado, redirige
            //    if (Session["Usuario"] == null)
            //    {
            //        Response.Redirect("/Pantallas-Inicio_Menu/Login.aspx");
            //    }
            //}
        }







        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool hayErrores = false;

            lblErrorActual.Text = "";
            lblErrorNueva.Text = "";
            lblErrorRepetir.Text = "";

            string actual = txtActual.Text.Trim();
            string nueva = txtNueva.Text.Trim();
            string repetir = txtRepetir.Text.Trim();

            Usuario usuario = (Usuario)Session["Usuario"];
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (string.IsNullOrEmpty(actual) || string.IsNullOrEmpty(nueva) || string.IsNullOrEmpty(repetir))
            {
                lblErrorActual.Text = "Debe completar todos los campos.";
                hayErrores = true;
            }

            if (nueva != repetir)
            {
                lblErrorRepetir.Text = "Las contraseñas nuevas no coinciden.";
                hayErrores = true;
            }

            if (usuario.Contrasena != actual)
            {
                lblErrorActual.Text = "La contraseña actual es incorrecta.";
                hayErrores = true;
            }

            // SI HAY ERRORES → NO REGISTRAR NINGÚN SCRIPT
            if (hayErrores) return;

            try
            {
                usuario.Contrasena = nueva;
                negocio.Modificar(usuario);

                Session["Usuario"] = usuario;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarExito",
                    "var modal = new bootstrap.Modal(document.getElementById('modalExito')); modal.show();",
                    true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarError",
                    "var modal = new bootstrap.Modal(document.getElementById('modalError')); modal.show();",
                    true);
            }
        }








        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pantallas-Perfil_Recepcionista/ListarPaciente.aspx"); // o a donde quieras volver
        }


        //BOTON MODAL EXITO "ACEPTAR"
        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pantallas-Perfil_Recepcionista/ListarPaciente.aspx");
        }

        //BOTON MODAL ERROR "ACEPTAR"
        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pantallas-Perfil_Recepcionista/ListarPaciente.aspx");
        }






    }
}
