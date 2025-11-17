using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class DetallePaciente : System.Web.UI.Page
    {

        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dni = Request.QueryString["dni"];

                if (!string.IsNullOrEmpty(dni))
                    CargarDatosPaciente(dni);
                else
                    Response.Redirect("ListarPaciente.aspx?error=SinDNI");
            }
        }

        //Ver Detalle del PACIENTE
        private void CargarDatosPaciente(string dni)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            var paciente = negocio.BuscarPorDni(dni);

            if (paciente == null)
            {
                Response.Redirect("ListarPaciente.aspx?error=NoEncontrado");
                return;
            }

            lblNombre.Text = paciente.Nombres;
            lblApellido.Text = paciente.Apellidos;
            lblDni.Text = paciente.DniPaciente.ToString();
            lblMail.Text = paciente.Email;
            lblCelular.Text = paciente.Celular;
            lblTelefono.Text = paciente.Telefono;
            lblFechaNacimiento.Text = paciente.FechaNacimiento.ToString("dd/MM/yyyy");
            lblDireccion.Text = paciente.Direccion;
            lblCiudad.Text = paciente.Ciudad;
            lblProvincia.Text = paciente.Provincia;
            lblObraSocial.Text = paciente.ObraSocial;
            lblNroObraSocial.Text = paciente.NumeroObraSocial;
            lblCodigoPostal.Text = paciente.CodigoPostal;
            lblSexo.Text = paciente.Sexo.ToString();
        }



    }
}