<%@ Page Title="Gestionar Especialidades" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true"
    CodeBehind="GestionarEspecialidades.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.GestionarEspecialidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-especialidades" class="esp-wrapper container-fluid text-light">

        <div class="esp-header d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="esp-titulo fw-bold">Gestión de Especialidades</h2>
                <p class="esp-subtitulo text-secondary">Busque, agregue, modifique o elimine especialidades de la clínica.</p>
            </div>
            <asp:Button ID="btnAgregar" runat="server" Text="+ Agregar Nueva Especialidad" CssClass="btn btn-primary fw-bold px-4 py-2" OnClick="btnAgregar_Click" />
        </div>


        <div class="esp-card bg-dark p-4 rounded mb-4 border border-secondary">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <label class="form-label text-light">Buscar por Especialidad</label>
                    <asp:TextBox ID="txtBuscarEspecialidad" runat="server"
                        CssClass="form-control bg-dark text-light border-secondary" placeholder="Ej: Cardiología" />
                </div>

                <div class="boton-buscador col-md-4 d-flex align-items-end gap-2">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary fw-bold w-50"
                        OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtros" CssClass="btn btn-secondary fw-bold w-50"
                        OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </div>


        <div class="esp-card bg-dark p-4 rounded border border-secondary">
            <h5 class="fw-bold mb-3 text-light">Lista de Especialidades</h5>

            
            <asp:UpdatePanel ID="upEspecialidades" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="table-responsive">
                        <table class="table table-dark table-borderless align-middle mb-0">
                            <thead class="esp-tabla-header">
                                <tr>
                                    <th class="titulo-especialidad">Especialidad</th>
                                    <th class="text-end" style="width: 120px;">Acciones</th>
                                </tr>
                            </thead>

                            <tbody class="esp-tabla-body">
                                <asp:Repeater ID="repEspecialidades" runat="server" OnItemCommand="repEspecialidades_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Nombre") %></td>

                                            <td>
                                               
                                                <asp:LinkButton ID="btnEditar" runat="server"
                                                    CommandName="Editar"
                                                    CommandArgument='<%# Eval("IdEspecialidad") %>'
                                                    CssClass="ms-2 me-3">
                                                 <i class="bi bi-pencil-square text-primary"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEliminar" runat="server"
                                                    OnClientClick='<%# "abrirModal(" + Eval("IdEspecialidad") + "); return false;" %>'
                                                    CssClass="ms-2">
                                                <i class="bi bi-trash text-danger"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>

                        </table>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>


    
    <asp:HiddenField ID="hfIdEliminar" runat="server" />



   
    <div class="modal fade" id="modalConfirmarEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content bg-dark text-light">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmar Eliminación</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    ¿Seguro que desea eliminar esta especialidad?
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                    <asp:Button ID="btnConfirmarEliminar" runat="server"
                        CssClass="btn btn-danger"
                        Text="Eliminar"
                        OnClick="btnConfirmarEliminar_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalErrorEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content bg-dark text-light">
                <div class="modal-header">
                    <h5 class="modal-title text-danger">No se puede eliminar</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="modalErrorMensaje">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>


    <script>
        function abrirModal(id) {
            document.getElementById('<%= hfIdEliminar.ClientID %>').value = id;

            var modal = new bootstrap.Modal(document.getElementById('modalConfirmarEliminar'));
            modal.show();
        }
    </script>
</asp:Content>
