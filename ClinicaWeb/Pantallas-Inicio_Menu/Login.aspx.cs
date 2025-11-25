using dominio;
using negocio;
using System;
using System.Web.UI;

namespace Clinic
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario logueado = negocio.ValidarLogin(txtUsuario.Text, txtPassword.Text);

            if (logueado != null)
            {
                Session["usuario"] = logueado;

                // REDIRECCIÓN SEGÚN ROL
                switch (logueado.Rol.NombreRol.ToUpper())
                {
                    case "MEDICO":
                        Response.Redirect("~/Pantallas-Perfil_Medico/PacienteEnSala.aspx");
                        break;

                    case "RECEPCIONISTA":
                        Response.Redirect("~/Pantallas-Perfil_Recepcionista/ListarPaciente.aspx");
                        break;

                    case "ADMINISTRADOR":
                        Response.Redirect("~/Pantasllas-Perfil_Administrador/GestionarUsuarios.aspx");
                        break;

                    default:
                        Response.Redirect("~/Login.aspx");
                        break;
                }
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}