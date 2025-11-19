<%@ Page Title="Clinica - Agregar Especialidad" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" CodeBehind="AgregarNuevaEspecialidad.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.AgregarNuevaEspecialidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="pantalla-agregarEspecialidad" class="container py-5" style="margin-top:50px;">

                <div class="card p-4 mb-4">
                    <h2 class="titulo mb-4">Registrar Nueva Especialidad</h2>
                    <p class="label-text mb-3">Complete los siguientes campos para agregar una nueva especialidad médica.</p>

                    <!-- FILA 1 -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="label-text">Nombre de la Especialidad</label>
                            <input id="txtNombreEspecialidad" runat="server" type="text"
                                   class="form-control" placeholder="Ej: Cardiología" />
                        </div>

                        <div class="col-md-6">
                            <label class="label-text">Descripción</label>
                            <input id="txtDescripcion" runat="server" type="text"
                                   class="form-control" placeholder="Breve descripción" />
                        </div>
                    </div>

                    <!-- BOTONES -->
                    <div class="d-flex justify-content-end gap-3">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="btn btn-danger" OnClick="btnCancelar_Click" />

                        <asp:Button ID="btnRegistrarEspecialidad" runat="server"
                            CssClass="btn btn-success"
                            Text="Registrar Especialidad"
                            OnClick="btnRegistrarEspecialidad_Click"
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
                <h4 class="mb-3">Especialidad registrada correctamente</h4>

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
                <h4 class="mb-3">Error al registrar la especialidad</h4>

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
