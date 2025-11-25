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
            string actual = txtActual.Text.Trim();
            string nueva = txtNueva.Text.Trim();
            string repetir = txtRepetir.Text.Trim();

            Usuario usuario = (Usuario)Session["Usuario"];
            UsuarioNegocio negocio = new UsuarioNegocio();

            // 1. Validar coincidencia
            if (nueva != repetir)
            {
                lblMensaje.Text = "Las contraseñas nuevas no coinciden.";
                lblMensaje.CssClass = "mensaje-resultado error";
                return;
            }

            // 2. Validar contraseña actual
            if (usuario.Contrasena != actual)
            {
                lblMensaje.Text = "La contraseña actual es incorrecta.";
                lblMensaje.CssClass = "mensaje-resultado error";
                return;
            }

            try
            {
                // 3. Actualizar contraseña
                usuario.Contrasena = nueva;

                negocio.Modificar(usuario);

                // 4. Actualizar sesión
                Session["Usuario"] = usuario;

                lblMensaje.Text = "Contraseña actualizada correctamente.";
                lblMensaje.CssClass = "mensaje-resultado ok";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Hubo un error al cambiar la contraseña: " + ex.Message;
                lblMensaje.CssClass = "mensaje-resultado error";
            }
        }


       
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pantallas-Perfil_Recepcionista/ListarPaciente.aspx"); // o a donde quieras volver
        }






    }
}
