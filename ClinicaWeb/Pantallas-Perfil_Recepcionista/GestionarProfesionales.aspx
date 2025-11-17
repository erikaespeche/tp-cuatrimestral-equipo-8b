<%@ Page Title="Gestión de Profesionales" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" CodeBehind="GestionarProfesionales.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.GestionarProfesionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-profesionales" class="container-fluid text-light py-4" style="max-width: 85%; margin: auto;">

        <!-- Título y botón -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Gestión de Profesionales</h2>
                <p class="text-secondary">Busque, agregue, modifique o elimine profesionales de la clínica.</p>
            </div>
            <button class="btn boton-agregar btn-primary fw-bold px-4 py-2">
                + Agregar Nuevo Profesional
            </button>
        </div>

        <!-- Filtros -->
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

                    <div class="col-md-4">
                        <label class="form-label text-white">Especialidad</label>
                        <div class="d-flex gap-2">
                            <asp:DropDownList ID="ddlEspecialidad" runat="server"
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

        <!-- Tabla -->
        <div class="card border-0" style="border-radius: 10px;">
            <div class="table-responsive">
                <table class="custom-table align-middle w-100" style="border-radius: 10px;">
                    <thead class="border-bottom border-secondary">
                        <tr>
                            <th>NOMBRE</th>
                            <th>APELLIDO</th>
                            <th>DNI</th>
                            <th>TELÉFONO</th>
                            <th>EMAIL</th>
                            <th>ESPECIALIDADES</th>
                            <th>ACCIONES</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater ID="repProfesionales" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Apellido") %></td>
                                    <td><%# Eval("Dni") %></td>
                                    <td><%# Eval("Telefono") %></td>
                                    <td><%# Eval("Email") %></td>

                                    
                                    <td>
                                        <%# string.Join(", ",
                                         ((dominio.Medico)Container.DataItem)
                                             .Especialidades.Select(esp => esp.Nombre)
                                         ) %>
                                    </td>

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

    </div>

</asp:Content>
