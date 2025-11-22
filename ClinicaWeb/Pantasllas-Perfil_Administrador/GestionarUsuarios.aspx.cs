using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantasllas_Perfil_Administrador
{
    public partial class GestionarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si necesitás cargar listado aquí, hacelo
                CargarUsuarios();
            }

            // Marca los campos inválidos con borde rojo
            foreach (var validator in Page.Validators)
            {
                if (validator is BaseValidator baseValidator && !baseValidator.IsValid)
                {
                    Control ctrl = FindControl(baseValidator.ControlToValidate);
                    if (ctrl is TextBox txt)
                    {
                        txt.CssClass = txt.CssClass.Replace("border-secondary", "");
                        txt.CssClass += " border-danger";
                    }
                }
            }
        }




        // ------------------- BOTÓN AGREGAR -----------------------
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCamposModal();

            ScriptManager.RegisterStartupScript(
                this, GetType(), "abrirModalAgregar",
                "var m = new bootstrap.Modal(document.getElementById('modalAgregarUsuario')); m.show();",
                true
            );
        }


        // ----------------- BOTÓN GUARDAR CAMBIOS -----------------
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                // Vuelve a abrir el modal para corregir datos inválidos
                ScriptManager.RegisterStartupScript(this, GetType(), "reabrir", "reabrirModalAgregarUsuario();", true);
                return;
            }

            try
            {
                // Construyo el usuario con los datos del formulario
                Usuario nuevo = new Usuario()
                {
                    DniUsuario = int.Parse(txtDniEdit.Text),
                    Nombres = txtNombreEdit.Text.Trim(),
                    Apellidos = txtApellidoEdit.Text.Trim(),
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    Contrasena = txtContrasena.Text.Trim(),
                    Email = txtMailEdit.Text.Trim(),
                    IdRol = int.Parse(ddlRol2.SelectedValue)
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(nuevo);

                // Mostrar modal de éxito
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "exito",
                    "mostrarModal('modalExito');",
                    true
                );
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "\\'");

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "error",
                    $"document.getElementById('modalErrorBody').innerHTML = '{msg}'; mostrarModal('modalError');",
                    true
                );

                // Reabrir modal agregar
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "reabrirAdd",
                    "reabrirModalAgregarUsuario();",
                    true
                );
            }
        }



        // ------------------------ MÉTODOS AUXILIARES -------------------------

        private void LimpiarCamposModal()
        {
            txtNombreEdit.Text = "";
            txtApellidoEdit.Text = "";
            txtDniEdit.Text = "";
            txtNombreUsuario.Text = "";
            txtContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
            txtMailEdit.Text = "";
            ddlRol2.SelectedIndex = 0;
        }

        private void MostrarModalExito()
        {
            ScriptManager.RegisterStartupScript(this, GetType(),
                "CerrarModalReg", "$('#modalAgregarUsuario').modal('hide');", true);

            ScriptManager.RegisterStartupScript(this, GetType(),
                "AbrirExito", "mostrarModal('modalExito');", true);
        }

        private void MostrarModalError(string mensaje)
        {
            mensaje = mensaje.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, GetType(),
                "CerrarModalReg", "$('#modalAgregarUsuario').modal('hide');", true);

            ScriptManager.RegisterStartupScript(this, GetType(),
                "AbrirError",
                $"document.getElementById('modalErrorBody').innerHTML = '{mensaje}'; mostrarModal('modalError');",
                true
            );
        }

        // ----------------- BOTÓN BUSCAR USUSARIOS -----------------
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            var lista = negocio.Listar();

            // FILTROS
            if (!string.IsNullOrEmpty(txtNombre.Text))
                lista = lista.FindAll(x => x.Nombres.ToUpper().Contains(txtNombre.Text.ToUpper()));

            if (!string.IsNullOrEmpty(txtApellido.Text))
                lista = lista.FindAll(x => x.Apellidos.ToUpper().Contains(txtApellido.Text.ToUpper()));

            if (!string.IsNullOrEmpty(TextDocumento.Text))
                lista = lista.FindAll(x => x.DniUsuario.ToString().Contains(TextDocumento.Text));

            if (!string.IsNullOrEmpty(TextUsuario.Text))
                lista = lista.FindAll(x => x.NombreUsuario.ToUpper().Contains(TextUsuario.Text.ToUpper()));

            if (!string.IsNullOrEmpty(ddlRol.SelectedValue))
                lista = lista.FindAll(x => x.Rol.NombreRol == ddlRol.SelectedValue);

            repUsuarios.DataSource = lista;
            repUsuarios.DataBind();
        }


        // ----------------- LISTAR USUSARIOS -----------------
        private void CargarUsuarios()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                List<Usuario> lista = negocio.Listar();

                repUsuarios.DataSource = lista;
                repUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de errores opcional
                string script = $"alert('Error al cargar usuarios: {ex.Message}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertaError", script, true);
            }
        }

    }
}
