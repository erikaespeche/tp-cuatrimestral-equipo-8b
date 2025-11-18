<%@ Page Title="Gestionar Especialidades" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true"
    CodeBehind="GestionarEspecialidades.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.GestionarEspecialidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-especialidades" class="esp-wrapper container-fluid text-light">

        
        <div class="esp-header d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="esp-titulo fw-bold">Gestión de Especialidades</h2>
                <p class="esp-subtitulo text-secondary">Busque, agregue, modifique o elimine especialidades de la clínica.</p>
            </div>
            <asp:Button ID="btnAgregar" runat="server" Text="+ Agregar Nueva Especialidad" CssClass="btn btn-primary fw-bold px-4 py-2" />
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

            <div class="table-responsive">
                <table class="table table-dark table-borderless align-middle mb-0">
                    <thead class="esp-tabla-header">
                        <tr>
                            <th class="titulo-especialidad">Especialidad</th>
                            <th class="text-end" style="width: 120px;">Acciones</th>
                        </tr>
                    </thead>

                    <tbody class="esp-tabla-body">
                        <asp:Repeater ID="repEspecialidades" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("Nombre") %>
                                    </td>
                                    <!-- Columna derecha ACCIONES -->
                                    <td class="text-end">
                                        <i class="bi bi-pencil text-light me-3 esp-icono-editar" style="cursor: pointer;"></i>
                                        <i class="bi bi-trash text-danger esp-icono-eliminar" style="cursor: pointer;"></i>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>

                </table>
            </div>
        </div>

    </div>

</asp:Content>
