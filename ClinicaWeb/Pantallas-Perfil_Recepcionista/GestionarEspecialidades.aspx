<%@ Page Title="Gestionar Especialidades" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" 
    CodeBehind="GestionarEspecialidades.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.GestionarEspecialidades" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-especialidades" class="esp-wrapper container-fluid text-light">

        <!-- Título principal -->
        <div class="esp-header d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="esp-titulo fw-bold">Gestión de Especialidades</h2>
                <p class="esp-subtitulo text-secondary">Busque, agregue, modifique o elimine especialidades de la clínica.</p>
            </div>
            <asp:Button ID="btnAgregar" runat="server" Text="+ Agregar Nueva Especialidad" CssClass="btn btn-primary fw-bold px-4 py-2" />
        </div>

        <!-- Buscador -->
        <div class="esp-card bg-dark p-4 rounded mb-4 border border-secondary">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <label class="form-label text-light">Buscar por Especialidad</label>
                    <asp:TextBox ID="txtBuscarEspecialidad" runat="server" CssClass="form-control bg-dark text-light border-secondary" placeholder="Ej: Cardiología" />
                </div>
                <div class="boton-buscador col-md-4 d-flex align-items-end gap-2">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary fw-bold w-50" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtros" CssClass="btn btn-secondary fw-bold w-50" />
                </div>
            </div>
        </div>

        <!-- Lista de especialidades -->
        <div class="esp-card bg-dark p-4 rounded border border-secondary">
            <h5 class="fw-bold mb-3 text-light">Lista de Especialidades</h5>
            <div class="table-responsive">
                <table class="table table-dark table-borderless align-middle mb-0">
                    <thead class="esp-tabla-header">
                        <tr>
                            <th>Especialidad</th>
                            <th class="text-end">Acciones</th>
                        </tr>
                    </thead>
                    <tbody class="esp-tabla-body">
                        <tr>
                            <td>Cardiología</td>
                            <td class="text-end">
                                <i class="bi bi-pencil text-light me-3 esp-icono-editar" style="cursor:pointer;"></i>
                                <i class="bi bi-trash text-danger esp-icono-eliminar" style="cursor:pointer;"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Dermatología</td>
                            <td class="text-end">
                                <i class="bi bi-pencil text-light me-3 esp-icono-editar"></i>
                                <i class="bi bi-trash text-danger esp-icono-eliminar"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Ginecología y Obstetricia</td>
                            <td class="text-end">
                                <i class="bi bi-pencil text-light me-3 esp-icono-editar"></i>
                                <i class="bi bi-trash text-danger esp-icono-eliminar"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Pediatría</td>
                            <td class="text-end">
                                <i class="bi bi-pencil text-light me-3 esp-icono-editar"></i>
                                <i class="bi bi-trash text-danger esp-icono-eliminar"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Traumatología</td>
                            <td class="text-end">
                                <i class="bi bi-pencil text-light me-3 esp-icono-editar"></i>
                                <i class="bi bi-trash text-danger esp-icono-eliminar"></i>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

    </div>




</asp:Content>
