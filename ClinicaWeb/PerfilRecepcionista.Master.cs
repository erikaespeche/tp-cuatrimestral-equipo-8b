using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Clinic
{
    public partial class PerfilRecepcionistaMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Código opcional al cargar la Master Page
        }


        //BOTON SALIR
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pantallas-Inicio_Menu/Login.aspx");
        }

    }
}
