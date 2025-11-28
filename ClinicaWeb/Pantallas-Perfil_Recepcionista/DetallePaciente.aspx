<%@ Page Title="Clinica - Detalle Paciente" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="DetallePaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.DetallePaciente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <%-- Para que se ejcute el Modal de Exito o Error el formulario tiene que estar dentro de "asp:UpdatePanel" y ContentTemplate quedando los Modales de Exito o Error afuera --%>
    <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="pantalla-detallepaciente">
                <!-- ⬅️ ID agregado -->

                <div class="container py-4">

                    <!-- Encabezado -->
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <h2 class="titulo">Detalle de Paciente</h2>
                        <button type="button" class="btn btn-outline-warning btn-sm me-1" data-bs-toggle="modal" data-bs-target="#modalEditar">
                            <i class="bi bi-pencil"></i>
                        </button>
                    </div>

                    <!-- Datos del Paciente -->
                    <div class="card p-4 mb-4">

                        <!-- Row 1 -->
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <p class="label-text">Nombre</p>
                                <asp:Label ID="lblNombre" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Apellido</p>
                                <asp:Label ID="lblApellido" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Tipo de Documento</p>
                                <asp:Label ID="lblTipoDocumento" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Documento</p>
                                <asp:Label ID="lblDni" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <!-- Row 2 -->
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <p class="label-text">Mail</p>
                                <asp:Label ID="lblMail" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Celular</p>
                                <asp:Label ID="lblCelular" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Teléfono</p>
                                <asp:Label ID="lblTelefono" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Fecha de Nacimiento</p>
                                <asp:Label ID="lblFechaNacimiento" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <!-- Row 3 -->
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <p class="label-text">Dirección</p>
                                <asp:Label ID="lblDireccion" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Ciudad</p>
                                <asp:Label ID="lblCiudad" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Provincia</p>
                                <asp:Label ID="lblProvincia" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Código Postal</p>
                                <asp:Label ID="lblCodigoPostal" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <!-- Row 4 -->
                        <div class="row">
                            <div class="col-md-3">
                                <p class="label-text">Obra Social</p>
                                <asp:Label ID="lblObraSocial" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Número de Obra Social</p>
                                <asp:Label ID="lblNroObraSocial" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Sexo</p>
                                <asp:Label ID="lblSexo" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <!-- Tabs (solo maqueta) -->
                    <ul class="nav nav-tabs mb-3">
                        <li class="nav-item"><a class="nav-link active" href="#">Citas</a></li>
                    </ul>

                    <!-- Maqueta de turnos -->
                    <div class="card p-4">
                        <div class="d-flex justify-content-end mb-3">


                            <asp:Button ID="btnAbrirTurno" runat="server"
                                CssClass="btn btn-add"
                                Text="+ Agregar Nuevo Turno"
                                OnClick="btnAbrirTurno_Click" />


                        </div>

                        <table class="custom-table align-middle w-100">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>FECHA</th>
                                    <th>HORA</th>
                                    <th>MÉDICO</th>
                                    <th>ESTADO</th>
                                    <th>OBSERVACIONES</th>
                                    <th>ACCIONES</th>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:Repeater ID="rptCitas" runat="server" OnItemCommand="rptCitas_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("IdTurno") %></td>
                                            <td><%# Eval("Fecha") %></td>
                                            <td><%# Eval("Hora") %></td>
                                            <td><%# Eval("Medico") %></td>
                                            <td>
                                                <span class='badge 
                                        <%# Eval("EstadoAdmin").ToString() == "Confirmado" ? "badge-confirmado" : 
                                            Eval("EstadoAdmin").ToString() == "Cancelado" ? "badge-cancelado" : 
                                            "badge-pendiente" %>'>
                                                    <%# Eval("EstadoAdmin") ?? "Pendiente" %>
                                                </span>
                                            </td>
                                            <td><%# Eval("Observaciones") %></td>
                                            <td>
                                                <!-- BOTÓN RECORDATORIO -->

                                                <asp:Button ID="btnRecordatorio" runat="server" Text="Enviar recordatorio"
                                                    CssClass="btn btn-primary"
                                                    CommandName="Recordatorio"
                                                    CommandArgument='<%# Eval("IdTurno") %>' />


                                                <!-- BOTÓN REPROGRAMAR -->
                                                <asp:Button ID="btnReprogramar" runat="server" Text="Reprogramar"
                                                    CssClass="btn btn-warning"
                                                    CommandName="Reprogramar"
                                                    CommandArgument='<%# Eval("IdTurno") %>' />


                                                <!-- BOTÓN CANCELAR -->
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Turno"
                                                    CssClass="btn btn-danger"
                                                    CommandName="Cancelar"
                                                    CommandArgument='<%# Eval("IdTurno") %>' />


                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>

                        </table>
                    </div>

                </div>


                
                <div class="modal fade" id="modalReprogramar" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Reprogramar Turno</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                            </div>

                            <div class="modal-body">
                                <div class="mb-3">
                                    <label class="form-label">Nueva fecha</label>
                                    <asp:TextBox ID="txtFechaNuevoTurno" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>

                                <div class="mb-3">
                                    <label class="form-label">Nueva hora</label>
                                    <asp:DropDownList ID="ddlHoraNuevoTurno" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Seleccionar hora" Value="" />
                                        <asp:ListItem Text="08:00" Value="08:00" />
                                        <asp:ListItem Text="09:00" Value="09:00" />
                                        <asp:ListItem Text="10:00" Value="10:00" />
                                        <asp:ListItem Text="11:00" Value="11:00" />
                                        <asp:ListItem Text="12:00" Value="12:00" />
                                        <asp:ListItem Text="13:00" Value="13:00" />
                                        <asp:ListItem Text="14:00" Value="14:00" />
                                        <asp:ListItem Text="15:00" Value="15:00" />
                                        <asp:ListItem Text="16:00" Value="16:00" />
                                    </asp:DropDownList>
                                </div>

                                <div class="mb-3 text-muted">
                                    Asegurate de seleccionar fecha y hora válidas.
                                </div>
                            </div>

                            <div class="modal-footer">
                                
                                <asp:Button ID="btnConfirmarReprogramacion" runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Guardar cambios"
                                    OnClick="btnConfirmarReprogramacion_Click" />
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>






                <!-- =========================================== -->
                <!-- MODAL EDITAR PACIENTE -->
                <!-- =========================================== -->

                <div class="modal fade" id="modalEditar" tabindex="-1" aria-hidden="true">
                    <div class="modal-dialog modal-xl">
                        <div class="ventana-editar-paciente modal-content bg-dark text-light p-3">

                            <div class="modal-header border-0 contenedor-titulo-editar-paciente">
                                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                                <h3 class="modal-title">Editar Paciente</h3>
                                <p class="modal-title">Actualiza la información del paciente. Los campos marcados con * son obligatorios.</p>
                            </div>

                            <div class="modal-body">
                                <div class="row g-3">

                                    <!-- Nombre -->
                                    <div class="col-md-6">
                                        <label class="form-label">Nombre*</label>
                                        <asp:TextBox ID="txtNombreEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valNombreReq" runat="server"
                                            ControlToValidate="txtNombreEdit" ErrorMessage="El nombre es obligatorio."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valNombreRegex" runat="server"
                                            ControlToValidate="txtNombreEdit"
                                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$"
                                            ErrorMessage="Ingrese un nombre válido (solo letras)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Apellido -->
                                    <div class="col-md-6">
                                        <label class="form-label">Apellido*</label>
                                        <asp:TextBox ID="txtApellidoEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valApellidoReq" runat="server"
                                            ControlToValidate="txtApellidoEdit" ErrorMessage="El apellido es obligatorio."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valApellidoRegex" runat="server"
                                            ControlToValidate="txtApellidoEdit"
                                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$"
                                            ErrorMessage="Ingrese un apellido válido (solo letras)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Tipo documento -->
                                    <div class="col-md-6">
                                        <label class="form-label">Tipo de Documento*</label>
                                        <asp:DropDownList ID="ddlTipoDocEdit" runat="server" CssClass="form-select">
                                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                            <asp:ListItem Value="DNI">DNI</asp:ListItem>
                                            <asp:ListItem Value="PASAPORTE">Pasaporte</asp:ListItem>
                                            <asp:ListItem Value="OTRO">Otro</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valTipoDocReq" runat="server"
                                            ControlToValidate="ddlTipoDocEdit" InitialValue=""
                                            ErrorMessage="Seleccione un tipo de documento."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Documento -->
                                    <div class="col-md-6">
                                        <label class="form-label">Documento*</label>
                                        <asp:TextBox ID="txtDniEdit" runat="server" CssClass="form-control" MaxLength="8" />
                                        <asp:RequiredFieldValidator ID="valDniReq" runat="server"
                                            ControlToValidate="txtDniEdit"
                                            ErrorMessage="El DNI es obligatorio."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valDniRegex" runat="server"
                                            ControlToValidate="txtDniEdit"
                                            ValidationExpression="^[0-9]{7,8}$"
                                            ErrorMessage="Ingrese un DNI válido (7 a 8 dígitos numéricos)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Mail -->
                                    <div class="col-md-6">
                                        <label class="form-label">Mail*</label>
                                        <asp:TextBox ID="txtMailEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valMailReq" runat="server"
                                            ControlToValidate="txtMailEdit"
                                            ErrorMessage="El mail es obligatorio."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valMailRegex" runat="server"
                                            ControlToValidate="txtMailEdit"
                                            ValidationExpression="^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"
                                            ErrorMessage="Formato de mail inválido."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Celular -->
                                    <div class="col-md-6">
                                        <label class="form-label">Celular</label>
                                        <asp:TextBox ID="txtCelEdit" runat="server" CssClass="form-control" MaxLength="13" />
                                        <asp:RegularExpressionValidator ID="valCelRegex" runat="server"
                                            ControlToValidate="txtCelEdit"
                                            ValidationExpression="^[0-9]{10,13}$"
                                            ErrorMessage="Ingrese un celular válido (10 a 13 dígitos)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Teléfono -->
                                    <div class="col-md-6">
                                        <label class="form-label">Teléfono</label>
                                        <asp:TextBox ID="txtTelEdit" runat="server" CssClass="form-control" MaxLength="10" />
                                        <asp:RegularExpressionValidator ID="valTelRegex" runat="server"
                                            ControlToValidate="txtTelEdit"
                                            ValidationExpression="^[0-9]{7,10}$"
                                            ErrorMessage="Ingrese un teléfono válido (7 a 10 dígitos)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Fecha nacimiento -->
                                    <div class="col-md-6">
                                        <label class="form-label">Fecha de Nacimiento*</label>
                                        <asp:TextBox ID="txtFechaEdit" runat="server" CssClass="form-control" TextMode="Date" />
                                        <asp:RequiredFieldValidator ID="valFechaReq" runat="server"
                                            ControlToValidate="txtFechaEdit"
                                            ErrorMessage="La fecha es obligatoria."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RangeValidator ID="valFechaRango" runat="server"
                                            ControlToValidate="txtFechaEdit" Type="Date"
                                            MinimumValue="1905-01-01"
                                            MaximumValue=""
                                            ErrorMessage="Ingrese una fecha válida entre 1905 y hoy."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Sexo -->
                                    <div class="col-md-6">
                                        <label class="form-label">Sexo*</label>
                                        <asp:DropDownList ID="ddlSexoEdit" runat="server" CssClass="form-select">
                                            <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                            <asp:ListItem Value="F">Femenino</asp:ListItem>
                                            <asp:ListItem Value="O">Otro</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valSexoReq" runat="server"
                                            ControlToValidate="ddlSexoEdit" InitialValue=""
                                            ErrorMessage="Seleccione un sexo válido."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Dirección -->
                                    <div class="col-md-12">
                                        <label class="form-label">Dirección*</label>
                                        <asp:TextBox ID="txtDirEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valDirReq" runat="server"
                                            ControlToValidate="txtDirEdit"
                                            ErrorMessage="La dirección es obligatoria."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valDirRegex" runat="server"
                                            ControlToValidate="txtDirEdit"
                                            ValidationExpression="^(?=.*[A-Za-zÁÉÍÓÚáéíóúÑñ])(?=.*\d)[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ .,-]+$"
                                            ErrorMessage="Ingrese una dirección válida (calle + numeración)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Ciudad -->
                                    <div class="col-md-4">
                                        <label class="form-label">Ciudad*</label>
                                        <asp:TextBox ID="txtCiudadEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valCiudadReq" runat="server"
                                            ControlToValidate="txtCiudadEdit"
                                            ErrorMessage="La ciudad es obligatoria."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valCiudadRegex" runat="server"
                                            ControlToValidate="txtCiudadEdit"
                                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$"
                                            ErrorMessage="Ingrese una ciudad válida (solo letras)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Provincia -->
                                    <div class="col-md-4">
                                        <label class="form-label">Provincia*</label>
                                        <asp:TextBox ID="txtProvEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valProvReq" runat="server"
                                            ControlToValidate="txtProvEdit"
                                            ErrorMessage="La provincia es obligatoria."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valProvRegex" runat="server"
                                            ControlToValidate="txtProvEdit"
                                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$"
                                            ErrorMessage="Ingrese una provincia válida (solo letras)."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Código Postal -->
                                    <div class="col-md-4">
                                        <label class="form-label">Código Postal*</label>
                                        <asp:TextBox ID="txtCpEdit" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="valCpReq" runat="server"
                                            ControlToValidate="txtCpEdit"
                                            ErrorMessage="El código postal es obligatorio."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                        <asp:RegularExpressionValidator ID="valCpRegex" runat="server"
                                            ControlToValidate="txtCpEdit"
                                            ValidationExpression="^(\d{4}|[A-Za-z]\d{4}[A-Za-z]{3})$"
                                            ErrorMessage="Formato inválido. Ej: 1000 o C1000ABC."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Obra Social -->
                                    <div class="col-md-6">
                                        <label class="form-label">Obra Social</label>
                                        <asp:TextBox ID="txtObraEdit" runat="server" CssClass="form-control" />
                                        <asp:RegularExpressionValidator ID="valObraRegex" runat="server"
                                            ControlToValidate="txtObraEdit"
                                            ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$"
                                            ErrorMessage="Ingrese una obra social válida."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                    <!-- Número Obra Social -->
                                    <div class="col-md-6">
                                        <label class="form-label">Número de Obra Social</label>
                                        <asp:TextBox ID="txtNumObraEdit" runat="server" CssClass="form-control" />
                                        <asp:RegularExpressionValidator ID="valNumObraRegex" runat="server"
                                            ControlToValidate="txtNumObraEdit"
                                            ValidationExpression="^[A-Za-z0-9\-\/]+$"
                                            ErrorMessage="Ingrese un número válido (letras, números, '-' o '/')."
                                            CssClass="text-danger" ValidationGroup="EditarPaciente" />
                                    </div>

                                </div>
                            </div>

                            <!-- Botones -->
                            <div class="modal-footer border-0 d-flex justify-content-end">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                                <asp:Button ID="btnGuardarCambios" runat="server"
                                    Text="Guardar"
                                    CssClass="btn btn-primary"
                                    ValidationGroup="EditarPaciente"
                                    UseSubmitBehavior="false"
                                    OnClientClick="return ejecutarGuardado();"
                                    OnClick="btnGuardarCambios_Click" />


                            </div>

                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="upRecordatorio" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <!-- =========================================== -->
                        <!-- MODAL RECORDATORIO -->
                        <!-- =========================================== -->
                        <div class="modal fade" id="modalRecordatorio" runat="server" clientidmode="Static" tabindex="-1">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Enviar Recordatorio</h5>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblMensajeRecordatorio" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnEnviarRecordatorio" runat="server"
                                            Text="Enviar" CssClass="btn btn-success"
                                            OnClick="btnEnviarRecordatorio_Click"
                                            OnClientClick="var m = bootstrap.Modal.getInstance(document.getElementById('modalRecordatorio')); if(m){ m.hide(); }" />
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- =========================================== -->
                        <!-- MODAL ENVIAR MAIL -->
                        <!-- =========================================== -->
                        <div class="modal fade" id="modalResultadoRecordatorio" tabindex="-1">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Enviar Recordatorio</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblResultadoRecordatorio" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- =========================================== -->
                        <!-- MODAL CANCELAR -->
                        <!-- =========================================== -->

                        <div class="modal fade" id="modalCancelar" tabindex="-1">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Cancelar Turno</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                    </div>
                                    <div class="modal-body">
                                        ¿Seguro que quiere cancelar este turno?
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnConfirmarCancelar" runat="server" Text="Cancelar el turno"
                                            CssClass="btn btn-secondary"
                                            OnClick="btnConfirmarCancelar_Click"
                                            OnClientClick="$('#modalCancelar').modal('hide');" />
                                    </div>
                                </div>
                            </div>
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- ⬅️ cierre del div raiz -->
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--    <!-- MODAL — CAMBIOS GUARDADOS -->
    <div class="modal fade" id="modalGuardado" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-success text-white p-4 rounded">
                <h4 class="mb-3">Cambios guardados correctamente</h4>

                <div class="text-end">
                    <asp:Button ID="btnAceptarGuardado" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClick="btnAceptarGuardado_Click" />
                </div>
            </div>
        </div>
    </div>--%>
    <div class="modal fade" id="modalExitoCancelar" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content bg-success text-white p-4 rounded">
            <h4 class="mb-3">Turno cancelado correctamente</h4>

            <div class="text-end">
                <asp:Button ID="btnAceptarExitoCancelar" runat="server"
                    CssClass="btn btn-light"
                    Text="Aceptar"
                    OnClientClick="location.reload(); return false;" />
            </div>
        </div>
    </div>
</div>


    <!-- ===================== -->
    <!--     MODAL ÉXITO       -->
    <!-- ===================== -->
    <div class="modal fade" id="modalExito" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-success text-white p-4 rounded">
                <h4 class="mb-3">Paciente registrado correctamente</h4>

                <div class="text-end">
                    <asp:Button ID="btnAceptarExito" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClientClick="location.reload(); return false;" />
                </div>
            </div>
        </div>
    </div>

    <!-- ===================== -->
    <!--      MODAL ERROR      -->
    <!-- ===================== -->
    <div class="modal fade" id="modalError" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-danger text-white p-4 rounded">
                <h4 class="mb-3">Error al registrar el paciente</h4>

                <!-- Aquí se inyecta el mensaje -->
                <div id="modalErrorBody" class="mb-3"></div>

                <div class="text-end">
                    <asp:Button ID="btnAceptarError" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClientClick="location.reload(); return false;" />
                </div>
            </div>
        </div>
    </div>

    <!-- =============================== -->
    <!--      MODAL AGREGAR TURNO        -->
    <!-- =============================== -->
    <div class="modal fade" id="modalNuevoTurno" tabindex="-1">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content modal-turno-dark">

                <!-- TÍTULO -->
                <div class="modal-header border-0">
                    <h3 class="text-white fw-bold">Agendar Nuevo Turno</h3>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">

                    <asp:UpdatePanel ID="updTurno" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row g-4">

                                <!-- Especialidad -->
                                <div class="col-md-6">
                                    <label class="form-label text-light">Especialidad</label>
                                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select dropdown-dark" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                                        <asp:ListItem Text="Seleccionar especialidad" Value="" />
                                    </asp:DropDownList>
                                </div>

                                <!-- Médico -->
                                <div class="col-md-6">
                                    <label class="form-label text-light">Profesional o Médico</label>
                                    <asp:DropDownList ID="ddlProfesional" runat="server" CssClass="form-select dropdown-dark" AutoPostBack="true" OnSelectedIndexChanged="ddlProfesional_SelectedIndexChanged">
                                        <asp:ListItem Text="Seleccionar profesional" Value="" />
                                    </asp:DropDownList>
                                </div>

                                <!-- Fecha -->
                                <div class="col-md-6">
                                    <label class="form-label text-light">Fecha</label>
                                    <asp:TextBox ID="txtFechaTurno" runat="server"
                                        CssClass="form-control"
                                        TextMode="Date"></asp:TextBox>
                                </div>

                                <!-- Horario -->
                                <div class="col-md-6">
                                    <label class="form-label text-light">Hora</label>
                                    <asp:DropDownList ID="ddlHora" runat="server" CssClass="form-select dropdown-dark">
                                        <asp:ListItem Text="Seleccionar hora" Value="" />
                                    </asp:DropDownList>
                                </div>

                                <!-- Observaciones -->
                                <div class="col-12">
                                    <label class="form-label text-light">Observaciones</label>
                                    <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" Rows="3" runat="server"
                                        CssClass="form-control comments-dark"
                                        placeholder="Añada notas adicionales aquí..."></asp:TextBox>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <!-- FOOTER -->
                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        Cancelar
                    </button>

                    <asp:Button ID="btnAgendarTurno" runat="server" Text="Agendar Turno"
                        CssClass="btn btn-primary btn-turno" OnClick="btnAgendarTurno_Click" />

                </div>

            </div>
        </div>
    </div>





    <!-- =========================================== -->
    <!-- JS: CARGA AUTOMÁTICA DE DATOS AL MODAL -->
    <!-- =========================================== -->
    <script>
        (function () {
            // ============================
            // UTILIDADES
            // ============================
            const id = (serverId) => document.getElementById(serverId);

            // Referencias a controles
            const txtNombre = id('<%= txtNombreEdit.ClientID %>');
            const txtApellido = id('<%= txtApellidoEdit.ClientID %>');
            const ddlTipoDoc = id('<%= ddlTipoDocEdit.ClientID %>');
            const txtDni = id('<%= txtDniEdit.ClientID %>');
            const txtMail = id('<%= txtMailEdit.ClientID %>');
            const txtCel = id('<%= txtCelEdit.ClientID %>');
            const txtTel = id('<%= txtTelEdit.ClientID %>');
            const txtFecha = id('<%= txtFechaEdit.ClientID %>');
            const ddlSexo = id('<%= ddlSexoEdit.ClientID %>');
            const txtDir = id('<%= txtDirEdit.ClientID %>');
            const txtCiudad = id('<%= txtCiudadEdit.ClientID %>');
            const txtProv = id('<%= txtProvEdit.ClientID %>');
            const txtCp = id('<%= txtCpEdit.ClientID %>');
            const txtObra = id('<%= txtObraEdit.ClientID %>');
            const txtNumObra = id('<%= txtNumObraEdit.ClientID %>');
            const btnGuardar = id('<%= btnGuardarCambios.ClientID %>');

            // Regex y mensajes
            const regex = {
                nombre: /^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$/,
                apellido: /^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$/,
                dni: /^[0-9]{7,8}$/,
                mail: /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/,
                cel: /^[0-9]{10,13}$/,
                tel: /^[0-9]{7,10}$/,
                dir: /^(?=.*[A-Za-zÁÉÍÓÚáéíóúÑñ])(?=.*\d)[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ .,-]+$/,
                ciudadProv: /^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$/,
                cp: /^(\d{4}|[A-Za-z]\d{4}[A-Za-z]{3})$/
            };

            const mensajes = {
                nombre: "Ingrese un nombre válido (solo letras).",
                apellido: "Ingrese un apellido válido (solo letras).",
                dni: "Ingrese un DNI válido (7 a 8 dígitos numéricos).",
                mail: "Formato de mail inválido.",
                cel: "Ingrese un celular válido (10 a 13 dígitos).",
                tel: "Ingrese un teléfono válido (7 a 10 dígitos).",
                fecha: "Ingrese una fecha válida entre 1905 y hoy.",
                dir: "Ingrese una dirección válida (calle + numeración).",
                ciudad: "Ingrese una ciudad válida (solo letras).",
                prov: "Ingrese una provincia válida (solo letras).",
                cp: "Formato inválido. Ej: 1000 o C1000ABC.",
                tipoDoc: "Seleccione un tipo de documento.",
                sexo: "Seleccione un sexo válido."
            };

            // Feedback visual cerca del control (sin tocar el layout)
            function showFeedback(el, msg) {
                el.classList.remove('is-valid');
                el.classList.add('is-invalid');
                let fb = el.parentNode.querySelector('.invalid-feedback-inline');
                if (!fb) {
                    fb = document.createElement('div');
                    fb.className = 'invalid-feedback invalid-feedback-inline';
                    el.parentNode.appendChild(fb);
                }
                fb.innerHTML = msg;
            }
            function showOk(el) {
                el.classList.remove('is-invalid');
                el.classList.add('is-valid');
                let fb = el.parentNode.querySelector('.invalid-feedback-inline');
                if (fb) fb.innerHTML = '';
            }
            function clearValidation(el) {
                el.classList.remove('is-invalid', 'is-valid');
                let fb = el.parentNode.querySelector('.invalid-feedback-inline');
                if (fb) fb.innerHTML = '';
            }

            // ============================
            // VALIDADORES INDIVIDUALES
            // ============================
            function validarNombre() {
                const v = txtNombre.value.trim();
                if (v.length === 0) { showFeedback(txtNombre, "El nombre es obligatorio."); return false; }
                if (!regex.nombre.test(v)) { showFeedback(txtNombre, mensajes.nombre); return false; }
                showOk(txtNombre); return true;
            }
            function validarApellido() {
                const v = txtApellido.value.trim();
                if (v.length === 0) { showFeedback(txtApellido, "El apellido es obligatorio."); return false; }
                if (!regex.apellido.test(v)) { showFeedback(txtApellido, mensajes.apellido); return false; }
                showOk(txtApellido); return true;
            }
            function validarTipoDoc() {
                const v = ddlTipoDoc.value.trim();
                if (v === "") { showFeedback(ddlTipoDoc, mensajes.tipoDoc); return false; }
                showOk(ddlTipoDoc); return true;
            }
            function validarDni() {
                const v = txtDni.value.trim();
                if (v.length === 0) { showFeedback(txtDni, "El DNI es obligatorio."); return false; }
                if (!regex.dni.test(v)) { showFeedback(txtDni, mensajes.dni); return false; }
                showOk(txtDni); return true;
            }
            function validarMail() {
                const v = txtMail.value.trim();
                if (v.length === 0) { showFeedback(txtMail, "El mail es obligatorio."); return false; }
                if (!regex.mail.test(v)) { showFeedback(txtMail, mensajes.mail); return false; }
                showOk(txtMail); return true;
            }
            function validarCelular() {
                const v = txtCel.value.trim();
                if (v.length === 0) { clearValidation(txtCel); return true; }
                if (!regex.cel.test(v)) { showFeedback(txtCel, mensajes.cel); return false; }
                showOk(txtCel); return true;
            }
            function validarTelefono() {
                const v = txtTel.value.trim();
                if (v.length === 0) { clearValidation(txtTel); return true; }
                if (!regex.tel.test(v)) { showFeedback(txtTel, mensajes.tel); return false; }
                showOk(txtTel); return true;
            }
            function validarFecha() {
                const v = txtFecha.value;
                if (!v) { showFeedback(txtFecha, "La fecha es obligatoria."); return false; }
                const d = new Date(v);
                if (isNaN(d.getTime())) { showFeedback(txtFecha, mensajes.fecha); return false; }
                const min = new Date("1905-01-01");
                const hoy = new Date(); // normalizamos a fecha sin hora
                const dNorm = new Date(d.getFullYear(), d.getMonth(), d.getDate());
                const hoyNorm = new Date(hoy.getFullYear(), hoy.getMonth(), hoy.getDate());
                if (dNorm < min || dNorm > hoyNorm) { showFeedback(txtFecha, mensajes.fecha); return false; }
                showOk(txtFecha); return true;
            }
            function validarSexo() {
                const v = ddlSexo.value.trim();
                if (v === "") { showFeedback(ddlSexo, mensajes.sexo); return false; }
                showOk(ddlSexo); return true;
            }
            function validarDireccion() {
                const v = txtDir.value.trim();
                if (v.length === 0) { showFeedback(txtDir, "La dirección es obligatoria."); return false; }
                if (!regex.dir.test(v)) { showFeedback(txtDir, mensajes.dir); return false; }
                showOk(txtDir); return true;
            }
            function validarCiudad() {
                const v = txtCiudad.value.trim();
                if (v.length === 0) { showFeedback(txtCiudad, "La ciudad es obligatoria."); return false; }
                if (!regex.ciudadProv.test(v)) { showFeedback(txtCiudad, mensajes.ciudad); return false; }
                showOk(txtCiudad); return true;
            }
            function validarProvincia() {
                const v = txtProv.value.trim();
                if (v.length === 0) { showFeedback(txtProv, "La provincia es obligatoria."); return false; }
                if (!regex.ciudadProv.test(v)) { showFeedback(txtProv, mensajes.prov); return false; }
                showOk(txtProv); return true;
            }
            function validarCp() {
                const v = txtCp.value.trim();
                if (v.length === 0) { showFeedback(txtCp, "El código postal es obligatorio."); return false; }
                if (!regex.cp.test(v)) { showFeedback(txtCp, mensajes.cp); return false; }
                showOk(txtCp); return true;
            }
            function validarObra() {
                const v = txtObra.value.trim();
                if (v.length === 0) { clearValidation(txtObra); return true; }
                const r = /^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$/;
                if (!r.test(v)) { showFeedback(txtObra, "Ingrese una obra social válida."); return false; }
                showOk(txtObra); return true;
            }
            function validarNumObra() {
                const v = txtNumObra.value.trim();
                if (v.length === 0) { clearValidation(txtNumObra); return true; }
                const r = /^[A-Za-z0-9\-\/]+$/;
                if (!r.test(v)) { showFeedback(txtNumObra, "Ingrese un número válido (letras, números, '-' o '/')."); return false; }
                showOk(txtNumObra); return true;
            }

            // ============================
            // VALIDACIÓN GLOBAL + HABILITAR GUARDAR
            // ============================
            function validarTodo() {
                const results = [
                    validarNombre(),
                    validarApellido(),
                    validarTipoDoc(),
                    validarDni(),
                    validarMail(),
                    validarCelular(),
                    validarTelefono(),
                    validarFecha(),
                    validarSexo(),
                    validarDireccion(),
                    validarCiudad(),
                    validarProvincia(),
                    validarCp(),
                    validarObra(),
                    validarNumObra()
                ];
                const allOk = results.every(r => r === true);
                if (btnGuardar) btnGuardar.disabled = !allOk;
                return allOk;
            }

            // Exponer para usar al abrir el modal
            window.__validarEditarModal = validarTodo;

            // ============================
            // ATTACH DE EVENTOS DE ENTRADA
            // ============================
            function attach() {
                if (!txtNombre) return; // seguridad
                const mapInput = [
                    [txtNombre, validarNombre],
                    [txtApellido, validarApellido],
                    [ddlTipoDoc, validarTipoDoc],
                    [txtDni, validarDni],
                    [txtMail, validarMail],
                    [txtCel, validarCelular],
                    [txtTel, validarTelefono],
                    [txtFecha, validarFecha],
                    [ddlSexo, validarSexo],
                    [txtDir, validarDireccion],
                    [txtCiudad, validarCiudad],
                    [txtProv, validarProvincia],
                    [txtCp, validarCp],
                    [txtObra, validarObra],
                    [txtNumObra, validarNumObra]
                ];
                mapInput.forEach(([el, fn]) => {
                    if (!el) return;
                    el.addEventListener('input', function () { fn(); validarTodo(); });
                    el.addEventListener('change', function () { fn(); validarTodo(); });
                });
                if (btnGuardar) btnGuardar.disabled = true;
            }

            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', attach);
            } else {
                attach();
            }

            // ============================
            // CARGA AUTOMÁTICA AL ABRIR MODAL
            // ============================
            document.addEventListener("DOMContentLoaded", function () {
                window.modoEdicion = false;
                const modalEditar = document.getElementById('modalEditar');

                modalEditar.addEventListener('show.bs.modal', function () {
                    if (window.modoEdicion === true) {
                        window.modoEdicion = false;
                        return;
                    }

                    // Cargar labels → inputs
                    id('<%= txtNombreEdit.ClientID %>').value = id('<%= lblNombre.ClientID %>').innerText.trim();
                    id('<%= txtApellidoEdit.ClientID %>').value = id('<%= lblApellido.ClientID %>').innerText.trim();
                    id('<%= ddlTipoDocEdit.ClientID %>').value = id('<%= lblTipoDocumento.ClientID %>').innerText.trim();
                    id('<%= txtDniEdit.ClientID %>').value = id('<%= lblDni.ClientID %>').innerText.trim();
                    id('<%= txtMailEdit.ClientID %>').value = id('<%= lblMail.ClientID %>').innerText.trim();
                    id('<%= txtCelEdit.ClientID %>').value = id('<%= lblCelular.ClientID %>').innerText.trim();
                    id('<%= txtTelEdit.ClientID %>').value = id('<%= lblTelefono.ClientID %>').innerText.trim();

                    // Fecha dd/MM/yyyy → yyyy-MM-dd
                    let fechaTexto = id('<%= lblFechaNacimiento.ClientID %>').innerText.trim();
                    let partes = fechaTexto.split('/');
                    let fechaISO = `${partes[2]}-${partes[1]}-${partes[0]}`;
                    id('<%= txtFechaEdit.ClientID %>').value = fechaISO;

                    id('<%= ddlSexoEdit.ClientID %>').value = id('<%= lblSexo.ClientID %>').innerText.trim();
                    id('<%= txtDirEdit.ClientID %>').value = id('<%= lblDireccion.ClientID %>').innerText.trim();
                    id('<%= txtCiudadEdit.ClientID %>').value = id('<%= lblCiudad.ClientID %>').innerText.trim();
                    id('<%= txtProvEdit.ClientID %>').value = id('<%= lblProvincia.ClientID %>').innerText.trim();
                    id('<%= txtCpEdit.ClientID %>').value = id('<%= lblCodigoPostal.ClientID %>').innerText.trim();
                    id('<%= txtObraEdit.ClientID %>').value = id('<%= lblObraSocial.ClientID %>').innerText.trim();
                    id('<%= txtNumObraEdit.ClientID %>').value = id('<%= lblNroObraSocial.ClientID %>').innerText.trim();

                    // Ejecutar validación inicial para estado del botón
                    setTimeout(function () { validarTodo(); }, 10);
                });

                // Abrir modal manualmente desde code-behind si es necesario
                window.abrirModalEditar = function () {
                    window.modoEdicion = true;
                    var modal = new bootstrap.Modal(document.getElementById('modalEditar'));
                    modal.show();
                };

                // Mostrar modal de guardado si ?ok=1 (si lo usás)
                const urlParams = new URLSearchParams(window.location.search);
                if (urlParams.get("ok") === "1") {
                    const modal = new bootstrap.Modal(document.getElementById("modalGuardado"));
                    modal.show();
                    urlParams.delete("ok");
                    const newUrl = window.location.pathname + "?" + urlParams.toString();
                    window.history.replaceState({}, "", newUrl);
                }
            });

            // ============================
            // VALIDACIÓN FINAL AL CLIC EN GUARDAR
            // ============================
            window.validarModalEditar = function () {
                // Ejecuta validación de cliente propia
                const clientOk = validarTodo();

                // Ejecuta validaciones de ASP.NET si están disponibles
                if (typeof (Page_ClientValidate) === 'function') {
                    Page_ClientValidate('EditarPaciente');
                }

                // Si ASP.NET define Page_IsValid, usamos eso; si no, confiamos en clientOk
                const aspValid = (typeof window.Page_IsValid !== 'undefined') ? window.Page_IsValid : true;

                if (!clientOk || !aspValid) {
                    // Mostrar modal de error
                    var modalError = new bootstrap.Modal(document.getElementById('modalError'));
                    modalError.show();

                    // Re-abrir el modal de edición (por si se cerró)
                    setTimeout(function () {
                        var modal = new bootstrap.Modal(document.getElementById('modalEditar'));
                        modal.show();
                    }, 150);

                    return false; // Evita el postback si hay errores
                }

                // Todo OK → permitir postback para ejecutar btnGuardarCambios_Click
                return true;
            };
        })();









        f<%--unction ejecutarGuardado() {
            if (typeof Page_ClientValidate === 'function') {
                Page_ClientValidate('EditarPaciente');
            }

            if (!Page_IsValid) {
                var modalError = new bootstrap.Modal(document.getElementById('modalError'));
                modalError.show();
                return false;
            }

            // ✅ Usar el name del control para garantizar el postback
            __doPostBack(document.getElementById('<%= btnGuardarCambios.ClientID %>').name, '');

                return false; // evita doble envío
        }--%>





        function ejecutarGuardado() {
            if (typeof Page_ClientValidate === 'function') {
                Page_ClientValidate('EditarPaciente');
            }

            if (!Page_IsValid) {
                var modalError = new bootstrap.Modal(document.getElementById('modalError'));
                modalError.show();
                return false;
            }

            __doPostBack(document.getElementById('<%= btnGuardarCambios.ClientID %>').name, '');
            return false;
        }







    </script>


    <%--JS para seleccionar horario--%>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const ddl = document.getElementById('<%= ddlHora.ClientID %>');
            ddl.addEventListener("change", function () {
                console.log("Hora seleccionada:", ddl.value);
            });
        });
    </script>
















</asp:Content>
