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
        // ==========================================================
        // ESTE ES EL LUGAR CORRECTO: Declaración a nivel de Clase
        // ==========================================================
        private UsuarioNegocio negocio = new UsuarioNegocio();

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
            UsuarioNegocio negocio = new UsuarioNegocio();

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
                int idUsuario = int.Parse(e.CommandArgument.ToString());
                CargarDatosUsuarioParaEdicion(idUsuario);
            }
        }


        private void CargarDatosUsuarioParaEdicion(int id)
        {
            try
            {
                
                // Usamos el método ObtenerPorId de la Capa de Negocio (DAL)
                Usuario usuario = negocio.ObtenerPorId(id);

                if (usuario != null)
                {
                    // 1. Asignar el ID al HiddenField del modal de edición
                    hfIdEditar.Value = usuario.IdUsuario.ToString();

                    // 2. Precargar los datos en los controles del modal
                    txtNombreEdit.Text = usuario.Nombres;
                    txtApellidoEdit.Text = usuario.Apellidos;
                    txtDniEdit.Text = usuario.DniUsuario.ToString();
                    txtUsuarioEdit.Text = usuario.NombreUsuario;
                    txtEmailEdit.Text = usuario.Email;

                    // 3. Seleccionar el Rol
                    ListItem itemRol = ddlRolEdit.Items.FindByValue(usuario.IdRol.ToString());
                    if (itemRol != null)
                    {
                        ddlRolEdit.ClearSelection();
                        itemRol.Selected = true;
                    }

                    // 4. Abrir el modal de edición (usando ScriptManager para ASP.NET AJAX)
                    // Este script debe coincidir con el nombre de la función JS que agregamos.
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "AbrirModal", "abrirModalEdicion();", true);

                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MostrarModalError("Error al obtener los datos del usuario: " + ex.Message);
            }
            finally
            {
                // Debe actualizar el UpdatePanel para que se ejecute el script y se vea el modal.
                updForm.Update();
            }
        }


        protected void btnGuardarCambiosEdit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                // Si la validación falla en el servidor, reabrimos el modal
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ReabrirEdicion", "reabrirModalEdicion();", true);
                updForm.Update();
                return;
            }

            try
            {
                // 1. Obtener los datos del modal
                Usuario usuario = new Usuario
                {
                    IdUsuario = int.Parse(hfIdEditar.Value),
                    Nombres = txtNombreEdit.Text.Trim(),
                    Apellidos = txtApellidoEdit.Text.Trim(),
                    DniUsuario = int.Parse(txtDniEdit.Text.Trim()),
                    NombreUsuario = txtUsuarioEdit.Text.Trim(),
                    Email = txtEmailEdit.Text.Trim(),
                    IdRol = int.Parse(ddlRolEdit.SelectedValue),
                    // No incluimos la contraseña, ya que no se editó en el modal
                    Contrasena = null
                };

                // 2. Llamar a la lógica de negocio para modificar
                negocio.Modificar(usuario);

                // 3. Mostrar Modal de Éxito de Edición
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MostrarExito", "mostrarModalExitoEdit();", true);

                // Recargar el GridView/Repeater para mostrar los cambios
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                // 4. Mostrar Modal de Error y reabrir el modal de edición
                MostrarModalError("Error al guardar los cambios: " + ex.Message);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ReabrirEdicion", "reabrirModalEdicion();", true);
            }
            finally
            {
                // Actualizar el UpdatePanel para mostrar el modal de éxito/error o reabrir el de edición
                updForm.Update();
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



        // Asumiendo que tienes un método general para mostrar errores
        private void MostrarModalError(string mensaje)
        {
            // Opcional: Asigna el mensaje de error al div dentro del modal de error
            modalErrorBody.InnerHtml = "<p>" + Server.HtmlEncode(mensaje) + "</p>";

            // Llama a la función JS para mostrar el modal de error
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MostrarError", "mostrarModal('modalError');", true);
        }


        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {

            Response.Redirect("GestionarUsuarios.aspx");

        }




        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("GestionarUsuarios.aspx");
        }


        
    }
}
