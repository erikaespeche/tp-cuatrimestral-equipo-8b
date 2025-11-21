<%@ Page Title="Clinica - Gestión de Usuarios" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="GestionarUsuarios.aspx.cs" Inherits="Clinic.Pantasllas_Perfil_Administrador.GestionarUsuarios" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Para que se ejcute el Modal de Exito o Error el formulario tiene que estar dentro de "asp:UpdatePanel" y ContentTemplate quedando los Modales de Exito o Error afuera --%>
  <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional">
    <ContentTemplate> 

    <div id="pantalla-usuarios" class="container-fluid text-light py-4" style="max-width: 85%; margin: auto;">

        
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Gestión de Usuarios</h2>
                <p class="text-secondary">Busque, agregue, modifique o elimine usuarios del sistema.</p>
            </div>
            <asp:Button ID="btnAgregar" runat="server" Text="+ Agregar Nuevo Usuario" CssClass="btn btn-primary fw-bold px-4 py-2" OnClick="btnAgregar_Click" />
        </div>

        
        <div class="card contenedor-filtro border-0 mb-4" style="border-radius: 10px;">
            <div class="card-body">
                <div class="row g-3 align-items-end">

                    <div class="col-md-4">
                        <label class="form-label text-white">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control bg-dark text-light border-secondary"
                            Placeholder="Buscar por Nombre" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label text-white">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server"
                            CssClass="form-control bg-dark text-light border-secondary"
                            Placeholder="Buscar por Apellido" />
                    </div>

                    <%-- DNI --%>
                    <div class="col-md-4">
                        <label class="form-label text-white">DNI</label>
                        <asp:TextBox ID="TextDocumento" runat="server"
                            CssClass="form-control bg-dark text-light border-secondary"
                            Placeholder="Buscar por DNI" />
                    </div>

                    <%-- USUARIO --%>
                    <div class="col-md-4">
                        <label class="form-label text-white">Nombre de Usuario</label>
                        <asp:TextBox ID="TextUsuario" runat="server"
                            CssClass="form-control bg-dark text-light border-secondary"
                            Placeholder="Buscar por Usuario" />
                    </div>

                    <%-- ROL--%>
                    <div class="col-md-4">
                        <label class="form-label text-white">Rol</label>
                        <div class="d-flex gap-2">
                            <asp:DropDownList ID="ddlRol" runat="server"
                                CssClass="form-select bg-dark text-light border-secondary">
                            </asp:DropDownList>

                            <asp:Button ID="btnBuscar" runat="server"
                                CssClass="btn btn-primary fw-bold px-4"
                                Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

        
        <div class="card border-0" style="border-radius: 10px;">
            <div class="table-responsive">
                <table class="custom-table align-middle w-100" style="border-radius: 10px;">
                    <thead class="border-bottom border-secondary">
                        <tr>
                            <th>NOMBRE</th>
                            <th>APELLIDO</th>
                            <th>DNI</th>
                            <th>NOMBRE USUARIO</th>
                            <th>ROL</th>
                            <th>ACCIONES</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater ID="repUsuarios" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombres") %></td>
                                    <td><%# Eval("Apellidos") %></td>
                                    <td><%# Eval("DniUsuario") %></td>
                                    <td><%# Eval("NombreUsuario") %></td>
                                    <td><%# Eval("Rol.NombreRol") %></td>

                                    <td>
                                        <button class="btn btn-outline-info btn-sm me-1">
                                            <i class="bi bi-eye"></i>
                                        </button>
                                        <button class="btn btn-outline-warning btn-sm me-1">
                                            <i class="bi bi-pencil"></i>
                                        </button>
                                        <button class="btn btn-outline-danger btn-sm">
                                            <i class="bi bi-trash"></i>
                                        </button>
                                    </td>
                                </tr>
                            </ItemTemplate>

                        </asp:Repeater>
                    </tbody>

                </table>
            </div>
        </div>

    </div>  <%-- CIERRE DIV RAIZ --%>



        <!-- =========================================== -->
        <!-- MODAL AGREGAR USUARIO -->
        <!-- =========================================== -->

        <div class="modal fade" id="modalAgregarUsuario" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-xl">
                <div class="ventana-editar-paciente modal-content bg-dark text-light p-3">

                    <div class="modal-header border-0 contenedor-titulo-editar-paciente">
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        <h3 class="modal-title">Agregar Nuevo Usuario</h3>
                        <p class="modal-title">Agrega un usuario. Los campos marcados con * son obligatorios.</p>
                    </div>

                    <div class="modal-body">
                        <div class="row g-3">

                            <!-- Nombre -->
                            <div class="col-md-6">
                                <label class="form-label">Nombre*</label>
                                <asp:TextBox ID="txtNombreEdit" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="valNombreReq" runat="server"
                                    ControlToValidate="txtNombreEdit" ErrorMessage="El nombre es obligatorio."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                                <asp:RegularExpressionValidator ID="valNombreRegex" runat="server"
                                    ControlToValidate="txtNombreEdit"
                                    ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$"
                                    ErrorMessage="Ingrese un nombre válido (solo letras)."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>

                            <!-- Apellido -->
                            <div class="col-md-6">
                                <label class="form-label">Apellido*</label>
                                <asp:TextBox ID="txtApellidoEdit" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="valApellidoReq" runat="server"
                                    ControlToValidate="txtApellidoEdit" ErrorMessage="El apellido es obligatorio."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                                <asp:RegularExpressionValidator ID="valApellidoRegex" runat="server"
                                    ControlToValidate="txtApellidoEdit"
                                    ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$"
                                    ErrorMessage="Ingrese un apellido válido (solo letras)."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>

                            

                            <!-- Documento -->
                            <div class="col-md-6">
                                <label class="form-label">DNI*</label>
                                <asp:TextBox ID="txtDniEdit" runat="server" CssClass="form-control" MaxLength="8" />
                                <asp:RequiredFieldValidator ID="valDniReq" runat="server"
                                    ControlToValidate="txtDniEdit"
                                    ErrorMessage="El DNI es obligatorio."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                                <asp:RegularExpressionValidator ID="valDniRegex" runat="server"
                                    ControlToValidate="txtDniEdit"
                                    ValidationExpression="^[0-9]{7,8}$"
                                    ErrorMessage="Ingrese un DNI válido (7 a 8 dígitos numéricos)."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>


                            <!-- Nombre de Usuario -->
                            <div class="col-md-6">
                                <label class="form-label">Nombre de Usuario</label>
                                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
                                <asp:RegularExpressionValidator ID="valNumObraRegex" runat="server"
                                    ControlToValidate="txtNombreUsuario"
                                    ValidationExpression="^[A-Za-z0-9\-\/]+$"
                                    ErrorMessage="Ingrese un usuario válido (letras, números, '-' o '/')."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>



                            <!-- Contraseña -->
                            <div class="col-md-6">
                                <label class="form-label">Contraseña*</label>
                                <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" />

                                <asp:RequiredFieldValidator ID="valPassReq" runat="server"
                                    ControlToValidate="txtContrasena"
                                    ErrorMessage="La contraseña es obligatoria."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />

                                <!-- Valida: mínimo 6 caracteres, 1 mayúscula, 1 número, 1 caracter especial -->
                                <asp:RegularExpressionValidator ID="valPassStrong" runat="server"
                                    ControlToValidate="txtContrasena"
                                    ValidationExpression="^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':&quot;\\|,.&lt;&gt;\/?])(?=.{6,}).+$"
                                    ErrorMessage="La contraseña debe tener al menos 6 caracteres, una mayúscula, un número y un caracter especial."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>

                            <!-- Confirmar Contraseña -->
                            <div class="col-md-6">
                                <label class="form-label">Confirmar Contraseña*</label>
                                <asp:TextBox ID="txtConfirmarContrasena" runat="server" CssClass="form-control" TextMode="Password" />

                                <asp:RequiredFieldValidator ID="valPassConfReq" runat="server"
                                    ControlToValidate="txtConfirmarContrasena"
                                    ErrorMessage="Debe confirmar la contraseña."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />

                                <asp:CompareValidator ID="valPassCompare" runat="server"
                                    ControlToValidate="txtConfirmarContrasena"
                                    ControlToCompare="txtContrasena"
                                    ErrorMessage="Las contraseñas no coinciden."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>



                            <!-- Mail -->
                            <div class="col-md-6">
                                <label class="form-label">Mail*</label>
                                <asp:TextBox ID="txtMailEdit" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="valMailReq" runat="server"
                                    ControlToValidate="txtMailEdit"
                                    ErrorMessage="El mail es obligatorio."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                                <asp:RegularExpressionValidator ID="valMailRegex" runat="server"
                                    ControlToValidate="txtMailEdit"
                                    ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"
                                    ErrorMessage="Formato de mail inválido."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>


                            <!-- Rol -->
                            <div class="col-md-6">
                                <label class="form-label">Rol*</label>
                                <asp:DropDownList ID="ddlRol2" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                    <asp:ListItem Value="ADMINISTRADOR">Administrador</asp:ListItem>
                                    <asp:ListItem Value="MEDICO">Médico</asp:ListItem>
                                    <asp:ListItem Value="RECEPCIONISTA">Recepcionista</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valRol" runat="server"
                                    ControlToValidate="ddlRol2" InitialValue=""
                                    ErrorMessage="Seleccione el rol del usuario."
                                    CssClass="text-danger" ValidationGroup="AgregarUsuario" />
                            </div>





                        </div>
                    </div> <%-- CIERRE MODAL --%>

                    <!-- Botones -->
                    <div class="modal-footer border-0 d-flex justify-content-end">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                        <asp:Button ID="btnGuardarCambios" runat="server"
                            Text="Guardar"
                            CssClass="btn btn-primary"
                            ValidationGroup="AgregarUsuario"
                            UseSubmitBehavior="false"
                            OnClientClick="return ejecutarGuardado();"
                            OnClick="btnGuardarCambios_Click" />


                    </div>

                </div>
            </div>
        </div>






  </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>