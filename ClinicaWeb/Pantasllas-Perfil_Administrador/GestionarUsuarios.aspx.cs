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

            // Marca campos inválidos con borde rojo (Esto debe funcionar correctamente)
            // Asegúrate de que el CSS "border-danger" esté definido en tu proyecto.
            foreach (var validator in Page.Validators)
            {
                if (validator is BaseValidator v && !v.IsValid)
                {
                    Control ctrl = FindControl(v.ControlToValidate);
                    if (ctrl is TextBox txt)
                    {
                        // Se verifica que no se agregue la clase si ya la tiene
                        if (!txt.CssClass.Contains("border-danger"))
                        {
                            txt.CssClass += " border-danger";
                        }
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

            // ⚠️ CORRECCIÓN: Se elimina el ScriptManager que cerraba el modal inmediatamente.
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
                // Reabrimos el modal de agregar para que el usuario vea los errores de validación.
                ScriptManager.RegisterStartupScript(this, GetType(), "reabrir",
                    "reabrirModalAgregarUsuario();", true);
                return;
            }

            try
            {
                Usuario nuevo = new Usuario()
                {
                    // Asumiendo que txtDniAgregar es un número (int.Parse) y la validación previa es correcta.
                    DniUsuario = int.Parse(txtDniAgregar.Text),
                    Nombres = txtNombreAgregar.Text.Trim(),
                    Apellidos = txtApellidoAgregar.Text.Trim(),
                    NombreUsuario = txtNombreUsuarioAgregar.Text.Trim(),
                    Contrasena = txtContrasena.Text.Trim(),
                    Email = txtMailAgregar.Text.Trim(),
                    IdRol = int.Parse(ddlRol2.SelectedValue)
                };

                // 🔄 CORRECCIÓN: Se usa la instancia de la clase (this.negocio) en lugar de crear una nueva.
                this.negocio.Agregar(nuevo);

                // ✅ PRIMERO: Refresco la tabla
                CargarUsuarios();

                // ✅ SEGUNDO: Limpio los campos del modal
                LimpiarCamposModal();

                // ✅ TERCERO: Muestro el modal de éxito (El UpdatePanel se encarga de que se ejecute)
                ScriptManager.RegisterStartupScript(this, GetType(),
                    "abrirExito", "abrirModalExito();", true);

            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "\\'").Replace("\r", "").Replace("\n", " ");

                // 1. Mostrar Modal de Error
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "error_" + DateTime.Now.Ticks,
                    $"document.getElementById('modalErrorBody').innerHTML = '{msg}'; mostrarModal('modalError');",
                    true
                );

                // 2. Reabrir el modal de agregar para que no pierda los datos (opcional si ya reabrirModalAgregarUsuario() lo hace)
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "reabrirAdd_" + DateTime.Now.Ticks,
                    "setTimeout(function() { reabrirModalAgregarUsuario(); }, 200);",
                    true
                );
            }
            finally
            {
                // ✅ CUARTO: Actualizo el UpdatePanel para que se vean los cambios/errores
                updForm.Update();
            }
        }

        // ------------------------------------------------------
        // CARGAR USUARIOS
        // ------------------------------------------------------
        private void CargarUsuarios()
        {
            // 🔄 CORRECCIÓN: Se usa la instancia de la clase (this.negocio) en lugar de crear una nueva.
            List<Usuario> lista = this.negocio.Listar();

            repUsuarios.DataSource = lista;
            repUsuarios.DataBind();
        }

        // ===============================
        //     BOTÓN BUSCAR
        // ===============================
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔄 CORRECCIÓN: Se usa la instancia de la clase (this.negocio) en lugar de crear una nueva.
                List<Usuario> lista = this.negocio.Listar();

                // FILTROS
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                    lista = lista.FindAll(u => u.Nombres.ToUpper().Contains(txtNombre.Text.ToUpper()));

                if (!string.IsNullOrWhiteSpace(txtApellido.Text))
                    lista = lista.FindAll(u => u.Apellidos.ToUpper().Contains(txtApellido.Text.ToUpper()));

                if (!string.IsNullOrWhiteSpace(TextDocumento.Text))
                    lista = lista.FindAll(u => u.DniUsuario.ToString().Contains(TextDocumento.Text));

                if (!string.IsNullOrWhiteSpace(TextUsuario.Text))
                    lista = lista.FindAll(u => u.NombreUsuario.ToUpper().Contains(TextUsuario.Text.ToUpper()));

                // Nota: Asumiendo que ddlRol.SelectedValue es el NombreRol o un valor que se compara correctamente.
                if (!string.IsNullOrWhiteSpace(ddlRol.SelectedValue))
                    lista = lista.FindAll(u => u.Rol != null && u.Rol.NombreRol.ToUpper() == ddlRol.SelectedValue.ToUpper());

                repUsuarios.DataSource = lista;
                repUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                // En lugar de hacer 'throw new Exception', es mejor mostrar el error al usuario.
                MostrarModalError("Error al filtrar usuarios: " + ex.Message);
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
            // Opcional: limpiar los estilos de error.
            txtNombreAgregar.CssClass = txtNombreAgregar.CssClass.Replace(" border-danger", "").Trim();
            // ... repetir para los demás TextBox
        }

        // ------------------------------------------------------
        // MANEJO DE ACCIONES DEL REPEATER (EDITAR / ELIMINAR)
        // ------------------------------------------------------
        protected void repUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            // 🔄 CORRECCIÓN: Se usa la instancia de la clase (this.negocio)
            // Ya no es necesario crear una nueva instancia aquí.

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
                // Usamos la instancia de la clase (this.negocio)
                Usuario usuario = this.negocio.ObtenerPorId(id);

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

                    // 4. Abrir el modal de edición
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
            // --- 0. VALIDACIÓN INICIAL DEL SERVIDOR ---
            if (!Page.IsValid)
            {
                // Si la validación falla (ej. RequiredFieldValidator), reabrimos el modal
                ScriptManager.RegisterStartupScript(this, GetType(), "ReabrirEdicion", "reabrirModalEdicion();", true);
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
                    // Manejar la conversión de DNI a int de forma segura
                    DniUsuario = int.Parse(txtDniEdit.Text.Trim()),
                    NombreUsuario = txtUsuarioEdit.Text.Trim(),
                    Email = txtEmailEdit.Text.Trim(),
                    IdRol = int.Parse(ddlRolEdit.SelectedValue),
                    Contrasena = null // La contraseña no se modifica desde este modal
                };

                // 2. VALIDACIÓN DE REGLAS DE NEGOCIO (DNI DUPLICADO)
                if (this.negocio.ExisteDniDuplicado(usuario.DniUsuario, usuario.IdUsuario))
                {
                    // Error específico: DNI duplicado
                    MostrarModalErrorEdit("El DNI ingresado **ya existe** para otro usuario. Por favor, ingrese uno diferente.");
                    // Reabrimos el modal de edición para que el usuario corrija
                    ScriptManager.RegisterStartupScript(this, GetType(), "ReabrirEdicion", "reabrirModalEdicion();", true);
                    return; // Salir del método
                }

                // 3. Llamar a la lógica de negocio para modificar
                this.negocio.Modificar(usuario);

                // 4. Mostrar Modal de Éxito de Edición
                ScriptManager.RegisterStartupScript(this, GetType(), "MostrarExito", "mostrarModalExitoEdit();", true);

                // 5. Recargar el Repeater para mostrar los cambios
                CargarUsuarios();
            }
            catch (FormatException)
            {
                // Error si el DNI o ID de Rol no son números válidos (aunque los Validators deberían prevenir esto)
                MostrarModalErrorEdit("Error de formato: El DNI o el Rol no son números válidos.");
                ScriptManager.RegisterStartupScript(this, GetType(), "ReabrirEdicion", "reabrirModalEdicion();", true);
            }
            catch (Exception ex)
            {
                // 6. Mostrar Modal de Error para cualquier otra excepción
                MostrarModalErrorEdit("Error al guardar los cambios: " + ex.Message);
                // Reabrimos el modal de edición para que el usuario vea el formulario
                ScriptManager.RegisterStartupScript(this, GetType(), "ReabrirEdicion", "reabrirModalEdicion();", true);
            }
            finally
            {
                // Actualizar el UpdatePanel en todos los casos
                updForm.Update();
            }
        }

        // -----------------------------------------------------------------------

        /// <summary>
        /// Muestra el modal de error de Edición, actualizando el cuerpo del mensaje.
        /// </summary>
        private void MostrarModalErrorEdit(string mensaje)
        {
            // Asume que modalErrorEditBody es el control que contiene el mensaje dentro del modal de error
            modalErrorEditBody.InnerText = mensaje;
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarError", "abrirModalErrorEdit();", true);
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
                    // 🔄 CORRECCIÓN: Se usa la instancia de la clase (this.negocio) y se elimina la redeclaración
                    this.negocio.Eliminar(idUsuarioAEliminar);

                    // 3. Si la eliminación es exitosa:
                    CargarUsuarios();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorID", "alert('Error: No se pudo obtener el ID del usuario a eliminar.');", true);
                updForm.Update();
            }
        }

        // Asumiendo que tienes un método general para mostrar errores
        private void MostrarModalError(string mensaje)
        {
            // Asigna el mensaje de error al div dentro del modal de error
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