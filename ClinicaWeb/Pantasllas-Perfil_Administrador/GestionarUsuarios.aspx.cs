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

            if (e.CommandName == "Eliminar")
            {
                // Guardamos el ID en un HiddenField
                hfIdAEliminar.Value = e.CommandArgument.ToString();

                // Abrimos el modal desde el servidor
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModalEliminar",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar')); myModal.show();", true);
            }
            if (e.CommandName == "Editar")
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario u = negocio.ObtenerPorId(id);

                hfIdEditar.Value = u.IdUsuario.ToString();
                txtNombreEdit.Text = u.Nombres;
                txtApellidoEdit.Text = u.Apellidos;
                txtDniEdit.Text = u.DniUsuario.ToString();
                txtUsuarioEdit.Text = u.NombreUsuario;
                txtEmailEdit.Text = u.Email;

                ddlRolEdit.Items.Clear();
                ddlRolEdit.Items.Add(new ListItem("-- Seleccione --", ""));
                ddlRolEdit.Items.Add(new ListItem("Administrador", "1"));
                ddlRolEdit.Items.Add(new ListItem("Médico", "2"));
                ddlRolEdit.Items.Add(new ListItem("Recepcionista", "3"));
                ddlRolEdit.SelectedValue = u.IdRol.ToString();

                ScriptManager.RegisterStartupScript(this, GetType(), "abrirEditar",
                    "var m = new bootstrap.Modal(document.getElementById('modalEditarUsuario')); m.show();",
                    true);
            }
        }




        /// ------------------------------------------------------
        // BOTON ELIMINAR
        // ------------------------------------------------------
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si el ID a eliminar fue guardado en el HiddenField
            if (int.TryParse(hfIdAEliminar.Value, out int idUsuarioAEliminar))
            {
                try
                {
                    // 1. Instancia la capa de Negocio (asumiendo que tienes una clase llamada UsuarioNegocio)
                    // Reemplaza 'UsuarioNegocio' con el nombre real de tu clase de negocio.
                    negocio.UsuarioNegocio negocio = new negocio.UsuarioNegocio();

                    // 2. Llama al método de eliminación.
                    negocio.Eliminar(idUsuarioAEliminar);

                    // 3. Si la eliminación es exitosa:
                    //    - Registra un script para mostrar el modal de éxito (definido en JavaScript)
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoEliminar", "mostrarModalExitoEliminar();", true);
                }
                catch (Exception ex)
                {
                    // 1. Limpia el contenido anterior (importante)
                    modalErrorBody.Controls.Clear();

                    // 2. Crea el literal con el mensaje de error
                    Literal litError = new Literal();
                    litError.Text = $"<p>{ex.Message}</p>";

                    // 3. Agrega el mensaje al div de error
                    modalErrorBody.Controls.Add(litError);

                    // 4. Muestra el modal de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorEliminar", "mostrarModal('modalError');", true);
                }
                finally
                {
                    // 5. Limpia el HiddenField y fuerza la actualización del UpdatePanel
                    hfIdAEliminar.Value = string.Empty;
                    updForm.Update();
                }
            }
            else
            {
                // Esto debería ser un caso raro (ID no se guardó correctamente)
                // Puedes registrar un script de error genérico aquí si lo deseas.
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorID", "alert('Error: No se pudo obtener el ID del usuario a eliminar.');", true);
                updForm.Update();
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
