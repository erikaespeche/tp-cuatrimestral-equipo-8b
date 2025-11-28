using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace Clinic
{
    public partial class PerfilAdministrador : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["Usuario"];

                if (user != null)
                {
                    lblUsuarioNavbar.Text = user.Nombres + " " + user.Apellidos;
                    lblRolNavbar.Text = user.Rol.NombreRol;
                }
            }
        }


        //BOTON SALIR
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pantallas-Inicio_Menu/Login.aspx");
        }

    }


}