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
                CargarUsuarios();

                
            }

            // Marca campos inválidos con borde rojo
            foreach (var validator in Page.Validators)
            {
                if (validator is BaseValidator v && !v.IsValid)
                {
                    Control ctrl = FindControl(v.ControlToValidate);
                    if (ctrl is TextBox txt)
                    {
                        txt.CssClass += " border-danger";
                    }
                }
            }
        }

        // ------------------------------------------------------
        // BOTÓN AGREGAR NUEVO USUARIO
        // ------------------------------------------------------
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCamposModal();

            ScriptManager.RegisterStartupScript(
                this, GetType(), "abrirModalAgregar",
                "var m = new bootstrap.Modal(document.getElementById('modalAgregarUsuario')); m.show();",
                true
            );
        }

        // ------------------------------------------------------
        // GUARDAR USUARIO (INSERT)
        // ------------------------------------------------------
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "reabrir",
                    "reabrirModalAgregarUsuario();", true);
                return;
            }

            try
            {
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

                // 🔥 IMPORTANTE: refresco la tabla
                CargarUsuarios();

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

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "reabrirAdd",
                    "reabrirModalAgregarUsuario();",
                    true
                );
            }
        }

        // ------------------------------------------------------
        // CARGAR USUARIOS
        // ------------------------------------------------------
        private void CargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> lista = negocio.Listar();

            // PROBAR SI TRAE DATOS
            Response.Write($"<script>console.log('Usuarios encontrados: {lista.Count}');</script>");

            repUsuarios.DataSource = lista;
            repUsuarios.DataBind();
        }



        


        // ===============================
        //     BOTÓN BUSCAR
        // ===============================
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                List<Usuario> lista = negocio.Listar();

                // FILTROS
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                    lista = lista.FindAll(u => u.Nombres.ToUpper().Contains(txtNombre.Text.ToUpper()));

                if (!string.IsNullOrWhiteSpace(txtApellido.Text))
                    lista = lista.FindAll(u => u.Apellidos.ToUpper().Contains(txtApellido.Text.ToUpper()));

                if (!string.IsNullOrWhiteSpace(TextDocumento.Text))
                    lista = lista.FindAll(u => u.DniUsuario.ToString().Contains(TextDocumento.Text));

                if (!string.IsNullOrWhiteSpace(TextUsuario.Text))
                    lista = lista.FindAll(u => u.NombreUsuario.ToUpper().Contains(TextUsuario.Text.ToUpper()));

                if (!string.IsNullOrWhiteSpace(ddlRol.SelectedValue))
                    lista = lista.FindAll(u => u.Rol.NombreRol.ToUpper() == ddlRol.SelectedValue.ToUpper());

                repUsuarios.DataSource = lista;
                repUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar usuarios: " + ex.Message);
            }
        }

        // ------------------------------------------------------
        // METODOS AUXILIARES
        // ------------------------------------------------------
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

        // ------------------------------------------------------
        // MANEJO DE ACCIONES DEL REPEATER (EDITAR / ELIMINAR)
        // ------------------------------------------------------
        protected void repUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Editar")
            {
                Response.Redirect("EditarUsuario.aspx?id=" + id);
            }

            if (e.CommandName == "Eliminar")
            {
                // Guardo el ID del usuario a eliminar
                hfIdAEliminar.Value = e.CommandArgument.ToString();

                // Mostrar modal de confirmación
                ScriptManager.RegisterStartupScript(this, GetType(), "modalConfirmar",
                    "var m = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar')); m.show();", true);
            }
        }



        /// ------------------------------------------------------
        // BOTON ELIMINAR
        // ------------------------------------------------------
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(hfIdAEliminar.Value, out int id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "errorEliminar",
                    "document.getElementById('modalErrorBody').innerText = 'No se recibió un ID válido.'; mostrarModal('modalError');",
                    true);
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Eliminar(id);   // <-- AHORA SÍ, CORRECTO

                CargarUsuarios();

                ScriptManager.RegisterStartupScript(this, GetType(), "exitoEliminar",
                    "mostrarModal('modalExito');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "errorEliminar",
                    $"document.getElementById('modalErrorBody').innerText = '{ex.Message.Replace("'", "")}'; mostrarModal('modalError');",
                    true);
            }
        }


    }
}
