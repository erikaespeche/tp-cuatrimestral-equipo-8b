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
            LimpiarErrores();

            if (!ValidarCampos())
            {
                // Evita que JS recargue valores
                ScriptManager.RegisterStartupScript(this, GetType(), "ModoEdicion", "window.modoEdicion = true;", true);

                // Reabrir modal
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalEditar();", true);
                return;
            }

            PacienteNegocio negocio = new PacienteNegocio();
            Paciente p = new Paciente();

            string dniQuery = Request.QueryString["dni"];
            var pacienteOriginal = negocio.BuscarPorDni(dniQuery);

            p.IdPaciente = pacienteOriginal.IdPaciente;
            p.TipoDocumento = pacienteOriginal.TipoDocumento;

            p.DniPaciente = int.Parse(txtDniEdit.Text);
            p.Nombres = txtNombreEdit.Text;
            p.Apellidos = txtApellidoEdit.Text;
            p.Email = txtMailEdit.Text;
            p.Celular = txtCelEdit.Text;
            p.Telefono = txtTelEdit.Text;
            p.Direccion = txtDirEdit.Text;
            p.Ciudad = txtCiudadEdit.Text;
            p.Provincia = txtProvEdit.Text;
            p.CodigoPostal = txtCpEdit.Text;
            p.ObraSocial = txtObraEdit.Text;
            p.NumeroObraSocial = txtNumObraEdit.Text;
            p.FechaNacimiento = DateTime.Parse(txtFechaEdit.Text);
            p.Sexo = char.Parse(ddlSexoEdit.SelectedValue);

            negocio.Modificar(p);

            Response.Redirect("DetallePaciente.aspx?dni=" + p.DniPaciente + "&ok=1");
        }


        // ======================== VALIDACIÓN ========================
        private bool ValidarCampos()
        {
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
                MarcarError(txtNombreEdit, invalidNombre, ref ok);

            if (string.IsNullOrWhiteSpace(txtApellidoEdit.Text))
                MarcarError(txtApellidoEdit, invalidApellido, ref ok);

            if (!int.TryParse(txtDniEdit.Text, out _))
                MarcarError(txtDniEdit, invalidDni, ref ok);

            if (!Regex.IsMatch(txtMailEdit.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                MarcarError(txtMailEdit, invalidMail, ref ok);

            if (!DateTime.TryParse(txtFechaEdit.Text, out _))
                MarcarError(txtFechaEdit, invalidFecha, ref ok);

            if (string.IsNullOrWhiteSpace(txtDirEdit.Text))
                MarcarError(txtDirEdit, invalidDireccion, ref ok);

            if (string.IsNullOrWhiteSpace(txtCiudadEdit.Text))
                MarcarError(txtCiudadEdit, invalidCiudad, ref ok);

            if (string.IsNullOrWhiteSpace(txtProvEdit.Text))
                MarcarError(txtProvEdit, invalidProvincia, ref ok);

            if (!Regex.IsMatch(txtCpEdit.Text, @"^\d{3,5}$"))
                MarcarError(txtCpEdit, invalidCp, ref ok);

            if (!string.IsNullOrWhiteSpace(txtTelEdit.Text) && !txtTelEdit.Text.All(char.IsDigit))
                MarcarError(txtTelEdit, invalidTel, ref ok);

            if (!string.IsNullOrWhiteSpace(txtCelEdit.Text) && !txtCelEdit.Text.All(char.IsDigit))
                MarcarError(txtCelEdit, invalidCel, ref ok);

            return ok;
        }


        // ======================== MARCAR ERROR ========================
        private void MarcarError(WebControl control, Control feedback, ref bool ok)
        {
            control.CssClass += " is-invalid";

            if (feedback != null)
                feedback.Visible = true;

            ok = false;
        }


        // ======================== LIMPIAR ========================
        private void LimpiarErrores()
        {
            foreach (var ctrl in new List<WebControl>
            {
                txtNombreEdit, txtApellidoEdit, txtDniEdit, txtMailEdit, txtTelEdit,
                txtCelEdit, txtFechaEdit, txtDirEdit, txtCiudadEdit, txtProvEdit, txtCpEdit
            })
            {
                ctrl.CssClass = ctrl.CssClass.Replace("is-invalid", "").Trim();
            }

            invalidNombre.Visible =
            invalidApellido.Visible =
            invalidDni.Visible =
            invalidMail.Visible =
            invalidFecha.Visible =
            invalidDireccion.Visible =
            invalidCiudad.Visible =
            invalidProvincia.Visible =
            invalidCp.Visible =
            invalidTel.Visible =
            invalidCel.Visible = false;
        }
    }
}
