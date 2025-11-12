using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class ListarPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //Este metodo soluciona el error
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Por ahora mostramos un mensaje de prueba
            // (más adelante acá irá la lógica de búsqueda)
            Response.Write("<script>alert('Búsqueda ejecutada correctamente');</script>");
        }
    }
}