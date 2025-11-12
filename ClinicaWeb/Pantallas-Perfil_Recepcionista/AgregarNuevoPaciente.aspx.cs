using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class AgregarNuevoPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Este es el método que soluciona tu error
        protected void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {
            // Por ahora solo mostramos un mensaje.
            // Más adelante podés conectar esto con la base de datos.
            Response.Write("<script>alert('Paciente registrado correctamente');</script>");
        }
    }
}