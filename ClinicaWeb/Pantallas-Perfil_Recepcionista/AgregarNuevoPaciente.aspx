<%@ Page Title="Clinica - Agregar Paciente Nuevo" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" CodeBehind="AgregarNuevoPaciente.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.AgregarNuevoPaciente" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 

    <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="pantalla-agregarPaciente" class="container py-5" style="margin-top:50px;">

                <div class="card p-4 mb-4">
                    <h2 class="titulo mb-4">Registro de Nuevo Paciente</h2>
                    <p class="label-text mb-3">Complete los siguientes campos para registrar un nuevo paciente.</p>

                    <!-- FILA 1 -->
                    <div class="row mb-3">
                        <div class="col-md-3">
                            <label class="label-text">Nombre</label>
                            <input id="txtNombre" runat="server" type="text" class="form-control" placeholder="Ingrese el nombre" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Apellido</label>
                            <input id="txtApellido" runat="server" type="text" class="form-control" placeholder="Ingrese el apellido" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Tipo de Documento</label>
                            <select id="ddlTipoDocumento" runat="server" class="form-select">
                                <option value="DNI">DNI</option>
                                <option value="Pasaporte">Pasaporte</option>
                                <option value="Otro">Otro</option>
                            </select>
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Número de Documento</label>
                            <input id="txtNumeroDocumento" runat="server" type="text" class="form-control" placeholder="Ingrese el número" />
                        </div>
                    </div>

                    <!-- FILA 2 -->
                    <div class="row mb-3">
                        <div class="col-md-3">
                            <label class="label-text">Fecha de Nacimiento</label>
                            <input id="txtFechaNacimiento" runat="server" type="date" class="form-control" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Sexo</label>
                            <select id="ddlSexo" runat="server" class="form-select">
                                <option>Seleccione el sexo</option>
                                <option value="F">Femenino</option>
                                <option value="M">Masculino</option>
                                <option value="O">Otro</option>
                            </select>
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Dirección</label>
                            <input id="txtDireccion" runat="server" type="text" class="form-control" placeholder="Ingrese la dirección" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Código Postal</label>
                            <input id="txtCodigoPostal" runat="server" type="text" class="form-control" placeholder="Ingrese el código postal" />
                        </div>
                    </div>

                    <!-- FILA 3 -->
                    <div class="row mb-3">
                        <div class="col-md-3">
                            <label class="label-text">Ciudad</label>
                            <input id="txtCiudad" runat="server" type="text" class="form-control" placeholder="Ingrese la localidad" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Provincia</label>
                            <input id="txtProvincia" runat="server" type="text" class="form-control" placeholder="Ingrese la provincia" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Celular</label>
                            <input id="txtCelular" runat="server" type="text" class="form-control" placeholder="Ingrese el número de celular" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Teléfono</label>
                            <input id="txtTelefono" runat="server" type="text" class="form-control" placeholder="Ingrese el número de teléfono" />
                        </div>
                    </div>

                    <!-- FILA 4 -->
                    <div class="fila-4 row mb-4">
                        <div class="col-md-3">
                            <label class="label-text">Mail</label>
                            <input id="txtMail" runat="server" type="email" class="form-control" placeholder="Ingrese el mail" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Obra Social</label>
                            <input id="txtObraSocial" runat="server" type="text" class="form-control" placeholder="Ingrese la obra social" />
                        </div>

                        <div class="col-md-3">
                            <label class="label-text">Número de Afiliado</label>
                            <input id="txtNumeroAfiliado" runat="server" type="text" class="form-control" placeholder="Ingrese el número de afiliado" />
                        </div>
                    </div>

                    <!-- BOTONES -->
                    <div class="d-flex justify-content-end gap-3">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="btn btn-danger" OnClick="btnCancelar_Click" />

                        <asp:Button ID="btnRegistrarPaciente" runat="server"
                            CssClass="btn btn-success"
                            Text="Registrar Paciente"
                            OnClick="btnRegistrarPaciente_Click"
                            UseSubmitBehavior="false" />
                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>




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

    <!-- MODAL DNI EXISTENTE -->
    <div class="modal fade" id="modalDniExistente" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-warning text-dark p-4 rounded">
                <h4 class="mb-3">El DNI ingresado ya se encuentra registrado</h4>

                <div class="text-end">
                    <button type="button" class="btn btn-dark" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>





</asp:Content>



