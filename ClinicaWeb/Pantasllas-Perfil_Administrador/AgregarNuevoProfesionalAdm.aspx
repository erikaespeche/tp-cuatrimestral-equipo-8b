<%@ Page Title="" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="AgregarNuevoProfesionalAdm.aspx.cs" Inherits="ClinicaWeb.Pantasllas_Perfil_Administrador.AgregarNuevoProfesionalAdm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="pantalla-agregarProfesional" class="container py-5" style="margin-top: 50px;">

                <div class="card p-4 mb-4">
                    <h2 class="titulo mb-4">Registrar Nuevo Profesional</h2>
                    <p class="label-text mb-3">Complete los siguientes campos para agregar un nuevo profesional.</p>

                    <!-- FILA 1 -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="label-text">Nombre</label>
                            <input id="txtNombreProfesional" runat="server" type="text"
                                class="form-control" placeholder="Ej: Juan" />
                        </div>

                        <div class="col-md-6">
                            <label class="label-text">Apellido</label>
                            <input id="txtApellidoProfesional" runat="server" type="text"
                                class="form-control" placeholder="Ej: Pérez" />
                        </div>
                    </div>

                    <!-- FILA 2 -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="label-text">DNI</label>
                            <input id="txtDniProfesional" runat="server" type="text"
                                class="form-control" placeholder="Ej: 12345678" />
                        </div>

                        <div class="col-md-6">
                            <label class="label-text">Email</label>
                            <input id="txtEmailProfesional" runat="server" type="email"
                                class="form-control" placeholder="ejemplo@mail.com" />
                        </div>
                    </div>

                    <!-- FILA 3 -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="label-text">Teléfono</label>
                            <input id="txtTelefonoProfesional" runat="server" type="text"
                                class="form-control" placeholder="Opcional" />
                        </div>

                        <div class="col-md-6">
                            <label class="label-text">Turno de Trabajo</label>
                            <asp:DropDownList ID="ddlTurnoTrabajo" runat="server"
                                CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- FILA 4 -->
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="label-text">Especialidades</label>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                            </asp:DropDownList>

                            <button type="button" class="btn btn-outline-primary btn-sm mt-2"
                                onclick="window.location='AgregarNuevaEspecialidadAdm.aspx?volver=agregarMedico'">
                                + Crear nueva especialidad
                            </button>
                        </div>
                    </div>

                    <!-- BOTONES -->
                    <div class="d-flex justify-content-end gap-3">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="btn btn-danger" OnClick="btnCancelar_Click" />

                        <asp:Button ID="btnRegistrarProfesional" runat="server"
                            CssClass="btn btn-success"
                            Text="Registrar Profesional"
                            OnClick="btnRegistrarProfesional_Click"
                            UseSubmitBehavior="false" />
                    </div>

                </div>

            </div>

        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRegistrarProfesional" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- ===================== -->
    <!--     MODAL ÉXITO       -->
    <!-- ===================== -->
    <div class="modal fade" id="modalExito" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-success text-white p-4 rounded">
                <h4 class="mb-3">Profesional registrado correctamente</h4>

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
                <h4 class="mb-3">Error al registrar el profesional</h4>
                <div id="modalErrorBody" class="mb-3"></div>

                <div class="text-end">
                    <asp:Button ID="btnAceptarError" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClick="btnAceptarError_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
