<%@ Page Title="Clinica - Detalle Paciente" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="DetallePaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.DetallePaciente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
            <li class="nav-item"><a class="nav-link" href="#">Historia Clínica</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Etiquetas</a></li>
        </ul>

        <!-- Maqueta de turnos -->
        <div class="card p-4">
            <div class="d-flex justify-content-end mb-3">
                <button type="button" class="btn btn-add">+ Agregar Nuevo Turno</button>
            </div>

            <table class="custom-table align-middle w-100">
                <thead>
                    <tr>
                        <th>FECHA</th>
                        <th>HORA</th>
                        <th>MÉDICO</th>
                        <th>TRATAMIENTO</th>
                        <th>ESTADO</th>
                        <th>ACCIONES</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>25/05/2024</td>
                        <td>10:00</td>
                        <td>Dr. Alan Grant</td>
                        <td>Consulta General</td>
                        <td><span class="badge badge-confirmado">Confirmado</span></td>
                        <td>
                            <button class="btn btn-primary btn-sm me-1">Enviar Recordatorio</button>
                            <button class="btn btn-warning btn-sm me-1 text-dark">Reprogramar</button>
                            <button class="btn btn-danger btn-sm">Cancelar</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

    </div>

    <!-- =========================================== -->
    <!-- MODAL EDITAR PACIENTE -->
    <!-- =========================================== -->

    <div class="modal fade" id="modalEditar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content bg-dark text-light p-3">

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
                        OnClick="btnGuardarCambios_Click"
                        ValidationGroup="EditarPaciente"
                        UseSubmitBehavior="false"
                        OnClientClick="return validarModalEditar();" />
                </div>

            </div>
        </div>
    </div>


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


    <!-- =========================================== -->
    <!-- MODAL CORRECTO -->
    <!-- =========================================== -->

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
                        OnClick="btnAceptarExito_Click" />
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

                <div class="text-end">
                    <asp:Button ID="btnAceptarError" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClick="btnAceptarError_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- =========================================== -->
    <!-- JS: CARGA AUTOMÁTICA DE DATOS AL MODAL -->
    <!-- =========================================== -->

    <script>
        document.addEventListener("DOMContentLoaded", function () {

            window.modoEdicion = false;

            const modalEditar = document.getElementById('modalEditar');

            modalEditar.addEventListener('show.bs.modal', function () {

                if (window.modoEdicion === true) {
                    window.modoEdicion = false;
                    return;
                }

                // Cargar labels → inputs
                document.getElementById('<%= txtNombreEdit.ClientID %>').value = document.getElementById('<%= lblNombre.ClientID %>').innerText.trim();
                document.getElementById('<%= txtApellidoEdit.ClientID %>').value = document.getElementById('<%= lblApellido.ClientID %>').innerText.trim();
                document.getElementById('<%= ddlTipoDocEdit.ClientID %>').value = document.getElementById('<%= lblTipoDocumento.ClientID %>').innerText.trim();
                document.getElementById('<%= txtDniEdit.ClientID %>').value = document.getElementById('<%= lblDni.ClientID %>').innerText.trim();
                document.getElementById('<%= txtMailEdit.ClientID %>').value = document.getElementById('<%= lblMail.ClientID %>').innerText.trim();

                document.getElementById('<%= txtCelEdit.ClientID %>').value = document.getElementById('<%= lblCelular.ClientID %>').innerText.trim();
                document.getElementById('<%= txtTelEdit.ClientID %>').value = document.getElementById('<%= lblTelefono.ClientID %>').innerText.trim();

                // Fecha dd/MM/yyyy → yyyy-MM-dd
                let fechaTexto = document.getElementById('<%= lblFechaNacimiento.ClientID %>').innerText.trim();
                let partes = fechaTexto.split('/');
                let fechaISO = `${partes[2]}-${partes[1]}-${partes[0]}`;
                document.getElementById('<%= txtFechaEdit.ClientID %>').value = fechaISO;

                document.getElementById('<%= ddlSexoEdit.ClientID %>').value = document.getElementById('<%= lblSexo.ClientID %>').innerText.trim();
                document.getElementById('<%= txtDirEdit.ClientID %>').value = document.getElementById('<%= lblDireccion.ClientID %>').innerText.trim();
                document.getElementById('<%= txtCiudadEdit.ClientID %>').value = document.getElementById('<%= lblCiudad.ClientID %>').innerText.trim();
                document.getElementById('<%= txtProvEdit.ClientID %>').value = document.getElementById('<%= lblProvincia.ClientID %>').innerText.trim();
                document.getElementById('<%= txtCpEdit.ClientID %>').value = document.getElementById('<%= lblCodigoPostal.ClientID %>').innerText.trim();

                document.getElementById('<%= txtObraEdit.ClientID %>').value = document.getElementById('<%= lblObraSocial.ClientID %>').innerText.trim();
                document.getElementById('<%= txtNumObraEdit.ClientID %>').value = document.getElementById('<%= lblNroObraSocial.ClientID %>').innerText.trim();

                // run real-time validation on open
                setTimeout(function () { window.__validarEditarModal && window.__validarEditarModal(); }, 10);
            });
        });

        function abrirModalEditar() {
            window.modoEdicion = true;
            var modal = new bootstrap.Modal(document.getElementById('modalEditar'));
            modal.show();
        }
    </script>

    <!-- =========================================== -->
    <!-- JS: MOSTRAR MODAL DE GUARDADO SI ?ok=1 -->
    <!-- =========================================== -->

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const urlParams = new URLSearchParams(window.location.search);

            if (urlParams.get("ok") === "1") {
                const modal = new bootstrap.Modal(document.getElementById("modalGuardado"));
                modal.show();

                urlParams.delete("ok");
                const newUrl = window.location.pathname + "?" + urlParams.toString();
                window.history.replaceState({}, "", newUrl);
            }
        });
    </script>

    <!-- =========================================== -->
    <!-- JS: VALIDACIÓN CLIENTE (tiempo real SOLO en el modal) -->
    <!-- =========================================== -->

    <script>
        (function () {
            // Obtener referencias a controles server-side (client IDs)
            const id = (serverId) => document.getElementById(serverId);

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

            // Regex y mensajes (mismos que en server)
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

            // Helpers para mostrar feedback (crea <div class="invalid-feedback"> si no existe)
            function showFeedback(el, msg) {
                // remove success text node if present
                el.classList.remove('is-valid');
                el.classList.add('is-invalid');

                // find existing feedback
                let fb = el.parentNode.querySelector('.invalid-feedback-inline');
                if (!fb) {
                    fb = document.createElement('div');
                    fb.className = 'invalid-feedback invalid-feedback-inline';
                    // keep same visual placement: appended right after the control
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

            // Cada validador devuelve true/false
            function validarNombre() {
                const v = txtNombre.value.trim();
                if (v.length === 0) {
                    showFeedback(txtNombre, "El nombre es obligatorio.");
                    return false;
                }
                if (!regex.nombre.test(v)) { showFeedback(txtNombre, mensajes.nombre); return false; }
                showOk(txtNombre); return true;
            }

            function validarApellido() {
                const v = txtApellido.value.trim();
                if (v.length === 0) {
                    showFeedback(txtApellido, "El apellido es obligatorio.");
                    return false;
                }
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
                if (v.length === 0) { clearValidation(txtCel); return true; } // opcional
                if (!regex.cel.test(v)) { showFeedback(txtCel, mensajes.cel); return false; }
                showOk(txtCel); return true;
            }

            function validarTelefono() {
                const v = txtTel.value.trim();
                if (v.length === 0) { clearValidation(txtTel); return true; } // opcional
                if (!regex.tel.test(v)) { showFeedback(txtTel, mensajes.tel); return false; }
                showOk(txtTel); return true;
            }

            function validarFecha() {
                const v = txtFecha.value;
                if (!v) { showFeedback(txtFecha, "La fecha es obligatoria."); return false; }
                const d = new Date(v);
                if (isNaN(d.getTime())) { showFeedback(txtFecha, mensajes.fecha); return false; }
                const min = new Date("1905-01-01");
                const hoy = new Date();
                // normalize dates (timezones)
                const dNorm = new Date(d.getFullYear(), d.getMonth(), d.getDate());
                if (dNorm < min || dNorm > new Date(hoy.getFullYear(), hoy.getMonth(), hoy.getDate())) { showFeedback(txtFecha, mensajes.fecha); return false; }
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
                // obra social es opcional en modal/servidor valida formato: regex permite letras/números
                const v = txtObra.value.trim();
                if (v.length === 0) { clearValidation(txtObra); return true; }
                // simple regex check (server has regex) - reuse dir? server: ^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$
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

            // Ejecutar todos
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
                btnGuardar.disabled = !allOk;
                return allOk;
            }

            // Exponer una función global para disparar validación (usada al abrir modal)
            window.__validarEditarModal = validarTodo;

            // Attach events (input/change)
            function attach() {
                if (!txtNombre) return;

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
                    // input events catch typing; change for selects/date
                    el.addEventListener('input', function () { fn(); validarTodo(); });
                    el.addEventListener('change', function () { fn(); validarTodo(); });
                    // remove server-side validator messages when user types
                });

                // inicializar estado
                btnGuardar.disabled = true;
            }

            // Run attach on DOM ready (and also when modal is created)
            if (document.readyState === 'loading') {
                document.addEventListener('DOMContentLoaded', attach);
            } else {
                attach();
            }

            // Keep original validarModalEditar behavior (for ASP.NET validators on server)
            window.validarModalEditar = function () {
                // run client validation first
                const clientOk = validarTodo();

                // run ASP.NET client validators if available
                if (typeof (Page_ClientValidate) === 'function') {
                    Page_ClientValidate('EditarPaciente');
                }

                if (!Page_IsValid || !clientOk) {
                    setTimeout(function () {
                        var modal = new bootstrap.Modal(document.getElementById('modalEditar'));
                        modal.show();
                    }, 150);
                    return false;
                }
                return true;
            };

        })();
    </script>

</asp:Content>
