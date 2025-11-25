using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            ScriptManager.RegisterStartupScript(
               this,
                GetType(),
               "cerrarModalAgregar",
                @"var modalAdd = bootstrap.Modal.getInstance(document.getElementById('modalAgregarUsuario'));
                if(modalAdd) modalAdd.hide();",
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
                    DniUsuario = int.Parse(txtDniAgregar.Text),
                    Nombres = txtNombreAgregar.Text.Trim(),
                    Apellidos = txtApellidoAgregar.Text.Trim(),
                    NombreUsuario = txtNombreUsuarioAgregar.Text.Trim(),
                    Contrasena = txtContrasena.Text.Trim(),
                    Email = txtMailAgregar.Text.Trim(),
                    IdRol = int.Parse(ddlRol2.SelectedValue)
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(nuevo);

                // ✅ PRIMERO: Refresco la tabla
                CargarUsuarios();

                // ✅ SEGUNDO: Limpio los campos del modal
                LimpiarCamposModal();

                // ✅ TERCERO: Actualizo el UpdatePanel para que se vean los cambios
                updForm.Update();

                // ✅ CUARTO: Muestro el modal de éxito DESPUÉS de actualizar



                // ===============================
                // MOSTRAR MODAL DE ÉXITO
                // ===============================
                // ⬅️ ABRIR MODAL DE ÉXITO DESPUÉS DE GUARDAR
                ScriptManager.RegisterStartupScript(this, GetType(),
                    "abrirExito", "abrirModalExito();", true);


            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "\\'").Replace("\r", "").Replace("\n", " ");

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "error_" + DateTime.Now.Ticks,
                    $"document.getElementById('modalErrorBody').innerHTML = '{msg}'; mostrarModal('modalError');",
                    true
                );

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "reabrirAdd_" + DateTime.Now.Ticks,
                    "setTimeout(function() { reabrirModalAgregarUsuario(); }, 200);",
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
            txtNombreAgregar.Text = "";
            txtApellidoAgregar.Text = "";
            txtDniAgregar.Text = "";
            txtNombreUsuarioAgregar.Text = "";
            txtContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
            txtMailAgregar.Text = "";
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
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario u = negocio.ObtenerPorId(id);

                // Guardar ID
                hfIdEditar.Value = u.IdUsuario.ToString();

                // Cargar datos
                txtNombreEdit.Text = u.Nombres;
                txtApellidoEdit.Text = u.Apellidos;
                txtDniEdit.Text = u.DniUsuario.ToString();
                txtUsuarioEdit.Text = u.NombreUsuario;
                txtEmailEdit.Text = u.Email;

                // Cargar roles dinámicamente
                ddlRolEdit.Items.Clear();
                ddlRolEdit.Items.Add(new ListItem("-- Seleccione --", ""));
                ddlRolEdit.Items.Add(new ListItem("Administrador", "1"));
                ddlRolEdit.Items.Add(new ListItem("Médico", "2"));
                ddlRolEdit.Items.Add(new ListItem("Recepcionista", "3"));
                ddlRolEdit.SelectedValue = u.IdRol.ToString();

                // Abrimos el modal
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirEditar",
                    "var m = new bootstrap.Modal(document.getElementById('modalEditarUsuario')); m.show();",
                    true);
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
    @"var modalConfirm = bootstrap.Modal.getInstance(document.getElementById('modalConfirmarEliminar'));
      if(modalConfirm) modalConfirm.hide();
      mostrarModal('modalExitoEliminar');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "errorEliminar",
                    $"document.getElementById('modalErrorBody').innerText = '{ex.Message.Replace("'", "")}'; mostrarModal('modalError');",
                    true);
            }
        }





        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {

            Response.Redirect("GestionarUsuarios.aspx");

        }




        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("GestionarUsuarios.aspx");
        }


        protected void btnGuardarCambiosEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = new Usuario();
                u.IdUsuario = int.Parse(hfIdEditar.Value);
                u.Nombres = txtNombreEdit.Text.Trim();
                u.Apellidos = txtApellidoEdit.Text.Trim();
                u.DniUsuario = int.Parse(txtDniEdit.Text.Trim());
                u.NombreUsuario = txtUsuarioEdit.Text.Trim();
                u.Email = txtEmailEdit.Text.Trim();
                u.IdRol = int.Parse(ddlRolEdit.SelectedValue);

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Modificar(u);  

             
                CargarUsuarios();
                updForm.Update();

                
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "abrirExitoEdit",
                    "var m = new bootstrap.Modal(document.getElementById('modalExitoEdit')); m.show();",
                    true
                );
            }
            catch (Exception ex)
            {
               
                string msg = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "errorEdit",
                    $"document.getElementById('modalErrorBody').innerText = '{msg}'; var m = new bootstrap.Modal(document.getElementById('modalError')); m.show();",
                    true
                );
            }
        }
    }
}
