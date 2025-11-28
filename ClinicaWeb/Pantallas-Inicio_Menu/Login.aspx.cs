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
                        Response.Redirect("~/Pantallas-Inicio_Menu/Login.aspx");
                        break;
                }
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
            }
        }


        //TRAE EL MAIL ASOCIADO AL USUARIO (PARA RECUPERAR CONTRASEÑA)
        protected void lnkOlvido_Click(object sender, EventArgs e)
        {
            lblRecuperarError.Text = "";

            string usuarioIngresado = txtUsuario.Text.Trim();

            if (string.IsNullOrEmpty(usuarioIngresado))
            {
                lblError.Text = "Primero ingresá tu usuario para recuperar la contraseña.";
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario encontrado = negocio.ValidarLoginSoloUsuario(usuarioIngresado);

            if (encontrado == null)
            {
                lblError.Text = "No existe un usuario con ese nombre.";
                return;
            }

            txtRecuperarEmail.Text = encontrado.Email;

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ShowModalRecupero",
                "setTimeout(function(){ var modal = new bootstrap.Modal(document.getElementById('modalRecuperar')); modal.show(); }, 200);",
                true
            );
        }






        protected void btnEnviarRecupero_Click(object sender, EventArgs e)
        {
            string mail = txtRecuperarEmail.Text.Trim();

            
            if (string.IsNullOrEmpty(mail))
            {
                lblRecuperarError.Text = "Ingrese un email válido.";
                lblRecuperarError.CssClass = "text-warning d-block mb-3";
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario encontrado = negocio.GetByEmail(mail);

            if (encontrado == null)
            {
                lblRecuperarError.Text = "No existe un usuario con ese email.";
                lblRecuperarError.CssClass = "text-warning d-block mb-3";
                return;
            }

            try
            {
                EmailService emailService = new EmailService();

                string asunto = "Recuperación de contraseña - Clínica";
                string cuerpo = $@"
                                <h3>Recuperación de contraseña</h3>
                                <p>Hola <b>{encontrado.Nombres}</b>,</p>
                                <p>Recibimos una solicitud para recuperar tu contraseña.</p>
                                <p><b>Tu contraseña actual es:</b> {encontrado.Contrasena}</p>
                                <br>
                                <p>Si vos no solicitaste esto, ignorá este mensaje.</p>
                                <p>Clínica - Sistema de Gestión de Turnos</p>
                                 ";

                emailService.armarCorreo(mail, asunto, cuerpo);
                emailService.enviarEmail();

                // Mostrar modal de éxito
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "MostrarExitoRecupero",
                    @"
                       setTimeout(function() {
                         var m1 = bootstrap.Modal.getInstance(document.getElementById('modalRecuperar'));
                         if(m1){ m1.hide(); }

                         var m2 = new bootstrap.Modal(document.getElementById('modalRecuperoExito'));
                         m2.show();
                      }, 300);
                    ",
                    true
                );
            }
            catch (Exception ex)
            {
                lblRecuperarError.Text = "Error al enviar correo: " + ex.Message;
                lblRecuperarError.CssClass = "text-danger d-block mb-3";
            }

        }

    }
}