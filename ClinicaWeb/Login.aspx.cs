using System;
using System.Web.UI;

namespace Clinic
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se ejecuta cada vez que se carga la página.
            // Ideal para inicializar controles o limpiar mensajes.
            if (!IsPostBack)
            {
                // Código de inicialización (vacío por ahora)
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Evento del botón "Ingresar".
            // Acá más adelante se agregará la lógica de autenticación.
        }
    }
}
