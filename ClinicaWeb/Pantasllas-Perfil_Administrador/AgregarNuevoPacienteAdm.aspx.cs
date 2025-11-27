using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ClinicaWeb.Pantasllas_Perfil_Administrador
{
    public partial class AgregarNuevoPacienteAdm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        //BOTON "REGISTRAR PACIENTE"
        protected void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {

            bool valido = true;
            string mensajeError = "";

            // DNI
            if (!int.TryParse(txtNumeroDocumento.Value, out int dni))
            {
                MarcarError(txtNumeroDocumento);
                mensajeError += "DNI inválido<br>";
                valido = false;
            }
            else
                MarcarOk(txtNumeroDocumento);


            // VALIDAR SI DNI YA EXISTE
            PacienteNegocio negocioDni = new PacienteNegocio();
            Paciente existente = negocioDni.BuscarPorDni(txtNumeroDocumento.Value.Trim());

            if (existente != null)
            {
                MarcarError(txtNumeroDocumento);
                // Mostrar modal DNI repetido
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalDniExistente",
                    "var m = new bootstrap.Modal(document.getElementById('modalDniExistente')); m.show();",
                    true
                );

                return; // NO seguir con el guardado
            }


            // Nombre
            var resNombre = Validador.ValidarNombre(txtNombre.Value);
            if (!resNombre.esValido)
            {
                MarcarError(txtNombre);
                mensajeError += resNombre.mensaje + "<br>";
                valido = false;
            }
            else
                MarcarOk(txtNombre);

            // Apellido
            var resApellido = Validador.ValidarApellido(txtApellido.Value);
            if (!resApellido.esValido)
            {
                MarcarError(txtApellido);
                mensajeError += resApellido.mensaje + "<br>";
                valido = false;
            }
            else
                MarcarOk(txtApellido);

            // Sexo
            string sexoSeleccionado = ddlSexo.Value.Trim().ToUpper(); // toma "F", "M" o "O"
            char sexoChar = ' ';
            if (sexoSeleccionado == "F" || sexoSeleccionado == "M" || sexoSeleccionado == "O")
            {
                sexoChar = sexoSeleccionado[0]; // 'F', 'M' o 'O'
                MarcarOk(ddlSexo);
            }
            else
            {
                MarcarError(ddlSexo);
                mensajeError += "Sexo inválido<br>";
                valido = false;
            }

            // Email
            var resEmail = Validador.ValidarEmail(txtMail.Value);
            if (!resEmail.esValido)
            {
                MarcarError(txtMail);
                mensajeError += resEmail.mensaje + "<br>";
                valido = false;
            }
            else
                MarcarOk(txtMail);

            // Celular
            var resCelular = Validador.ValidarTelefono(txtCelular.Value);
            if (!resCelular.esValido)
            {
                MarcarError(txtCelular);
                mensajeError += resCelular.mensaje + "<br>";
                valido = false;
            }
            else
                MarcarOk(txtCelular);

            // Teléfono
            var resTelefono = Validador.ValidarTelefono(txtTelefono.Value);
            if (!resTelefono.esValido)
            {
                MarcarError(txtTelefono);
                mensajeError += resTelefono.mensaje + "<br>";
                valido = false;
            }
            else
                MarcarOk(txtTelefono);

            // Fecha de nacimiento
            if (!DateTime.TryParse(txtFechaNacimiento.Value, out DateTime fechaNac))
            {
                MarcarError(txtFechaNacimiento);
                mensajeError += "Fecha de nacimiento inválida<br>";
                valido = false;
            }
            else
                MarcarOk(txtFechaNacimiento);

            // Dirección
            if (string.IsNullOrWhiteSpace(txtDireccion.Value))
            {
                MarcarError(txtDireccion);
                mensajeError += "Dirección no puede estar vacía<br>";
                valido = false;
            }
            else
                MarcarOk(txtDireccion);

            // Ciudad
            if (string.IsNullOrWhiteSpace(txtCiudad.Value))
            {
                MarcarError(txtCiudad);
                mensajeError += "Ciudad no puede estar vacía<br>";
                valido = false;
            }
            else
                MarcarOk(txtCiudad);

            // Provincia
            if (string.IsNullOrWhiteSpace(txtProvincia.Value))
            {
                MarcarError(txtProvincia);
                mensajeError += "Provincia no puede estar vacía<br>";
                valido = false;
            }
            else
                MarcarOk(txtProvincia);

            // Código Postal
            if (!Regex.IsMatch(txtCodigoPostal.Value.Trim(), @"^\d{4,10}$"))
            {
                MarcarError(txtCodigoPostal);
                mensajeError += "Código Postal inválido<br>";
                valido = false;
            }
            else
                MarcarOk(txtCodigoPostal);

            // Obra Social
            //if (string.IsNullOrWhiteSpace(txtObraSocial.Value))
            //{
            //    MarcarError(txtObraSocial);
            //    mensajeError += "Obra Social no puede estar vacía<br>";
            //    valido = false;
            //}
            //else
            //    MarcarOk(txtObraSocial);

            //// Número de Afiliado
            //if (string.IsNullOrWhiteSpace(txtNumeroAfiliado.Value))
            //{
            //    MarcarError(txtNumeroAfiliado);
            //    mensajeError += "Número de Afiliado no puede estar vacío<br>";
            //    valido = false;
            //}
            //else
            //    MarcarOk(txtNumeroAfiliado);


            // Obra Social (opcional)
            MarcarOk(txtObraSocial);

            // Número de Afiliado (opcional)
            MarcarOk(txtNumeroAfiliado);





            // Si hay errores de validación, mostrar modal y salir
            if (!valido)
            {
                mensajeError = mensajeError.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{mensajeError}';",
                    true
                );
                return;
            }

            try
            {
                Paciente nuevo = new Paciente();
                nuevo.TipoDocumento = ddlTipoDocumento.Value;
                nuevo.DniPaciente = int.Parse(txtNumeroDocumento.Value);
                nuevo.Nombres = txtNombre.Value;
                nuevo.Apellidos = txtApellido.Value;
                nuevo.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Value);
                nuevo.Sexo = ddlSexo.Value[0];
                nuevo.Sexo = sexoChar;
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
            catch (Exception ex)
            {
                // Mostrar modal de error
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

        private void MarcarError(HtmlControl input)
        {
            input.Attributes["class"] = "form-control is-invalid";
        }

        private void MarcarOk(HtmlControl input)
        {
            input.Attributes["class"] = "form-control is-valid";
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