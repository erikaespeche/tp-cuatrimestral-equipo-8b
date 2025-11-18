using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class DetallePaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Setear fecha máxima para el validator (hoy)
                valFechaRango.MaximumValue = DateTime.Now.ToString("yyyy-MM-dd");


                // Tu código original
                string dni = Request.QueryString["dni"];

                if (!string.IsNullOrEmpty(dni))
                    CargarDatosPaciente(dni);
                else
                    Response.Redirect("ListarPaciente.aspx?error=SinDNI");
            }
        }



        // ======================== CARGAR DATOS ========================
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
            lblTipoDocumento.Text = paciente.TipoDocumento;
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


        // ======================== BOTÓN GUARDAR ========================
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) VALIDAR ASP.NET
                if (!Page.IsValid)
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "abrirModal",
                        "abrirModalEditar();",
                        true
                    );
                    return;
                }

                PacienteNegocio negocio = new PacienteNegocio();

                string dniQuery = Request.QueryString["dni"];
                var pacienteOriginal = negocio.BuscarPorDni(dniQuery);

                if (pacienteOriginal == null)
                {
                    Response.Redirect("ListarPaciente.aspx?error=PacienteNoEncontrado");
                    return;
                }

                // =============================
                // 2) CREAR OBJETO PACIENTE A MODIFICAR
                // =============================
                Paciente p = pacienteOriginal;   // <-- CLAVE: partimos del original

                // 3) Asignar campos editados
                p.Nombres = txtNombreEdit.Text.Trim();
                p.Apellidos = txtApellidoEdit.Text.Trim();
                p.Email = txtMailEdit.Text.Trim();
                p.Celular = txtCelEdit.Text.Trim();
                p.Telefono = txtTelEdit.Text.Trim();
                p.Direccion = txtDirEdit.Text.Trim();
                p.Ciudad = txtCiudadEdit.Text.Trim();
                p.Provincia = txtProvEdit.Text.Trim();
                p.CodigoPostal = txtCpEdit.Text.Trim();
                p.ObraSocial = txtObraEdit.Text.Trim();
                p.NumeroObraSocial = txtNumObraEdit.Text.Trim();

                if (int.TryParse(txtDniEdit.Text, out int dniParsed))
                    p.DniPaciente = dniParsed;

                if (DateTime.TryParse(txtFechaEdit.Text, out DateTime fechaN))
                    p.FechaNacimiento = fechaN;

                if (!string.IsNullOrEmpty(ddlSexoEdit.SelectedValue))
                    p.Sexo = char.Parse(ddlSexoEdit.SelectedValue);

                // =============================
                // 4) GUARDAR CAMBIOS
                // =============================
                negocio.Modificar(p);

                // =============================
                // 5) MOSTRAR MODAL DE ÉXITO
                // =============================
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalExito",
                    "var m = new bootstrap.Modal(document.getElementById('modalExito')); m.show();",
                    true
                );
            }
            catch (Exception ex)
            {
                string errorMsg = "Error al guardar el paciente: " + ex.Message;
                errorMsg = errorMsg.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
                ScriptManager.RegisterStartupScript(
                 this,
                 GetType(),
                 "modalError",
                 $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{errorMsg}';",
                 true
                );
            }
        }


        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarPaciente.aspx");
        }

        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // Cierra el modal. Bootstrap ya lo maneja.
        }





    }
}
