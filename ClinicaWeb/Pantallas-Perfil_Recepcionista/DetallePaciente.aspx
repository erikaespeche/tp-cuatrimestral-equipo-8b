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
                    <p class="label-text">Documento</p>
                    <asp:Label ID="lblDni" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Mail</p>
                    <asp:Label ID="lblMail" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

            <!-- Row 2 -->
            <div class="row mb-3">
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

                <div class="col-md-3">
                    <p class="label-text">Dirección</p>
                    <asp:Label ID="lblDireccion" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

            <!-- Row 3 -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Ciudad</p>
                    <asp:Label ID="lblCiudad" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Provincia</p>
                    <asp:Label ID="lblProvincia" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Obra Social</p>
                    <asp:Label ID="lblObraSocial" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Número de Obra Social</p>
                    <asp:Label ID="lblNroObraSocial" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

            <!-- Row 4 -->
            <div class="row">
                <div class="col-md-3">
                    <p class="label-text">Código Postal</p>
                    <asp:Label ID="lblCodigoPostal" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Sexo</p>
                    <asp:Label ID="lblSexo" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

        </div>

        <!-- Tabs -->
        <ul class="nav nav-tabs mb-3">
            <li class="nav-item"><a class="nav-link active" href="#">Citas</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Historia Clínica</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Etiquetas</a></li>
        </ul>

        <!-- SOLO MAQUETA -->
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


    <!-- MODAL EDITAR PACIENTE -->
    <div class="modal fade" id="modalEditar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content bg-dark text-light p-3">

                <div class="modal-header border-0 contenedor-titulo-editar-paciente">
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    <h3 class="modal-title">Editar Paciente</h3>
                    <p class="modal-title">Actualiza la información del paciente. Los campos marcados con * son obligatorios. </p>
                </div>

                <div class="modal-body">
                    <div class="row g-3">

                        <!-- Nombre -->
                        <div class="col-md-6">
                            <label class="form-label">Nombre*</label>
                            <asp:TextBox ID="txtNombreEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidNombre">Ingrese un nombre válido.</div>
                        </div>

                        <!-- Apellido -->
                        <div class="col-md-6">
                            <label class="form-label">Apellido*</label>
                            <asp:TextBox ID="txtApellidoEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidApellido">Ingrese un apellido válido.</div>
                        </div>

                        <!-- DNI -->
                        <div class="col-md-6">
                            <label class="form-label">Documento*</label>
                            <asp:TextBox ID="txtDniEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidDni">Ingrese un DNI numérico válido.</div>
                        </div>

                        <!-- Mail -->
                        <div class="col-md-6">
                            <label class="form-label">Mail*</label>
                            <asp:TextBox ID="txtMailEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidMail">Ingrese un mail válido.</div>
                        </div>

                        <!-- Celular -->
                        <div class="col-md-6">
                            <label class="form-label">Celular</label>
                            <asp:TextBox ID="txtCelEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidCel">Ingrese un celular válido.</div>
                        </div>

                        <!-- Teléfono -->
                        <div class="col-md-6">
                            <label class="form-label">Teléfono</label>
                            <asp:TextBox ID="txtTelEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidTel">Ingrese un teléfono válido.</div>
                        </div>

                        <!-- Fecha -->
                        <div class="col-md-6">
                            <label class="form-label">Fecha de Nacimiento*</label>
                            <asp:TextBox ID="txtFechaEdit" runat="server" CssClass="form-control" TextMode="Date" />
                            <div class="invalid-feedback" runat="server" id="invalidFecha">Seleccione una fecha válida.</div>
                        </div>

                        <!-- Sexo -->
                        <div class="col-md-6">
                            <label class="form-label">Sexo*</label>
                            <asp:DropDownList ID="ddlSexoEdit" runat="server" CssClass="form-select">
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                                <asp:ListItem Value="O">Otro</asp:ListItem>
                            </asp:DropDownList>
                            <div class="invalid-feedback" runat="server" id="invalidSexo">Seleccione un sexo.</div>
                        </div>

                        <!-- Dirección -->
                        <div class="col-md-12">
                            <label class="form-label">Dirección*</label>
                            <asp:TextBox ID="txtDirEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidDireccion">Ingrese una dirección válida.</div>
                        </div>

                        <!-- Ciudad -->
                        <div class="col-md-4">
                            <label class="form-label">Ciudad*</label>
                            <asp:TextBox ID="txtCiudadEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidCiudad">Ingrese una ciudad válida.</div>
                        </div>

                        <!-- Provincia -->
                        <div class="col-md-4">
                            <label class="form-label">Provincia*</label>
                            <asp:TextBox ID="txtProvEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidProvincia">Ingrese una provincia válida.</div>
                        </div>

                        <!-- Código Postal -->
                        <div class="col-md-4">
                            <label class="form-label">Código Postal*</label>
                            <asp:TextBox ID="txtCpEdit" runat="server" CssClass="form-control" />
                            <div class="invalid-feedback" runat="server" id="invalidCp">Ingrese un código postal válido.</div>
                        </div>

                        <!-- Obra Social -->
                        <div class="col-md-6">
                            <label class="form-label">Obra Social</label>
                            <asp:TextBox ID="txtObraEdit" runat="server" CssClass="form-control" />
                        </div>

                        <!-- Número Obra Social -->
                        <div class="col-md-6">
                            <label class="form-label">Número de Obra Social</label>
                            <asp:TextBox ID="txtNumObraEdit" runat="server" CssClass="form-control" />
                        </div>

                    </div>
                </div>

                <div class="modal-footer border-0 d-flex justify-content-end">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                    <asp:Button ID="btnGuardarCambios" runat="server" CssClass="btn btn-success"
                        Text="Guardar Cambios" OnClick="btnGuardarCambios_Click"
                        UseSubmitBehavior="false" />

                </div>

            </div>
        </div>
    </div>



    <!-- MODAL CONFIRMACIÓN DE GUARDADO -->
    <div class="modal fade" id="modalGuardado" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content bg-success text-white">

                <div class="modal-header border-0">
                    <h5 class="modal-title fw-bold">✔ Cambios guardados correctamente</h5>
                </div>

                <div class="modal-body">
                    <p>Los datos del paciente fueron actualizados exitosamente.</p>
                </div>

                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal">Aceptar</button>
                </div>

            </div>
        </div>
    </div>

    
    <!-- CARGA AUTOMÁTICA DEL MODAL -->
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
                document.getElementById('<%= txtNombreEdit.ClientID %>').value = document.getElementById('<%= lblNombre.ClientID %>').innerText;
                document.getElementById('<%= txtApellidoEdit.ClientID %>').value = document.getElementById('<%= lblApellido.ClientID %>').innerText;
                document.getElementById('<%= txtDniEdit.ClientID %>').value = document.getElementById('<%= lblDni.ClientID %>').innerText;
                document.getElementById('<%= txtMailEdit.ClientID %>').value = document.getElementById('<%= lblMail.ClientID %>').innerText;
                document.getElementById('<%= txtCelEdit.ClientID %>').value = document.getElementById('<%= lblCelular.ClientID %>').innerText;
                document.getElementById('<%= txtTelEdit.ClientID %>').value = document.getElementById('<%= lblTelefono.ClientID %>').innerText;

                // Fecha
                let fechaTexto = document.getElementById('<%= lblFechaNacimiento.ClientID %>').innerText.trim();
                let partes = fechaTexto.split('/');
                let fechaISO = `${partes[2]}-${partes[1]}-${partes[0]}`;
                document.getElementById('<%= txtFechaEdit.ClientID %>').value = fechaISO;

                document.getElementById('<%= ddlSexoEdit.ClientID %>').value = document.getElementById('<%= lblSexo.ClientID %>').innerText;
                document.getElementById('<%= txtDirEdit.ClientID %>').value = document.getElementById('<%= lblDireccion.ClientID %>').innerText;
                document.getElementById('<%= txtCiudadEdit.ClientID %>').value = document.getElementById('<%= lblCiudad.ClientID %>').innerText;
                document.getElementById('<%= txtProvEdit.ClientID %>').value = document.getElementById('<%= lblProvincia.ClientID %>').innerText;
                document.getElementById('<%= txtCpEdit.ClientID %>').value = document.getElementById('<%= lblCodigoPostal.ClientID %>').innerText;
                document.getElementById('<%= txtObraEdit.ClientID %>').value = document.getElementById('<%= lblObraSocial.ClientID %>').innerText;
                document.getElementById('<%= txtNumObraEdit.ClientID %>').value = document.getElementById('<%= lblNroObraSocial.ClientID %>').innerText;

            });
        });

        // Apertura desde servidor en caso de errores
        function abrirModalEditar() {
            window.modoEdicion = true;
            var modal = new bootstrap.Modal(document.getElementById('modalEditar'));
            modal.show();
        }
    </script>


    <!-- Mostrar modal de guardado -->
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

</asp:Content>


