using dominio;
using negocio;
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


      
        //BOTON "REGISTRAR PACIENTE"
        protected void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente nuevo = new Paciente();
                nuevo.TipoDocumento = ddlTipoDocumento.Value;
                nuevo.DniPaciente = int.Parse(txtNumeroDocumento.Value);
                nuevo.Nombres = txtNombre.Value;
                nuevo.Apellidos = txtApellido.Value;
                nuevo.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Value);
                nuevo.Sexo = ddlSexo.Value[0];
                nuevo.GrupoSanguineo = "";
                nuevo.Email = txtMail.Value;
                nuevo.Telefono = txtTelefono.Value;
                nuevo.Celular = txtCelular.Value;
                nuevo.Direccion = txtDireccion.Value;
                nuevo.Ciudad = txtCiudad.Value;
                nuevo.Provincia = txtProvincia.Value;
                nuevo.CodigoPostal = txtCodigoPostal.Value;
                nuevo.ObraSocial = txtObraSocial.Value;
                nuevo.NumeroObraSocial = txtNumeroAfiliado.Value;

                PacienteNegocio negocio = new PacienteNegocio();
                negocio.Agregar(nuevo);

                // Mostrar modal de éxito
                ScriptManager.RegisterStartupScript(
                   this,
                   GetType(),
                  "modalExito",
                  "var m = new bootstrap.Modal(document.getElementById('modalExito')); m.show();",
                  true
                );

            }
            catch (Exception)
            {
                // Mostrar modal de error
                ScriptManager.RegisterStartupScript(
                 this,
                 GetType(),
                 "modalError",
                 "var m = new bootstrap.Modal(document.getElementById('modalError')); m.show();",
                 true
                );
            }
        }

        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            // Refresca la página
            Response.Redirect("AgregarNuevoPaciente.aspx");
        }

        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // Solo cierra el modal (Bootstrap lo hace automáticamente)
            // No recarga la página
        }



        //BOTON "CANCELAR"
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarPaciente.aspx");
        }







    }
}