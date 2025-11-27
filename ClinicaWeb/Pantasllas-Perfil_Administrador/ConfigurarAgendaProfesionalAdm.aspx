<%@ Page Title="" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="ConfigurarAgendaProfesionalAdm.aspx.cs" Inherits="ClinicaWeb.Pantasllas_Perfil_Administrador.ConfigurarAgendaProfesionalAdm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-agenda" class="agp-wrapper text-light">

        <h2 class="agp-titulo">Configurar Agenda</h2>

        <!-- Contenedor principal de selección y parámetros -->
        <div class="agp-card text-light mb-4">
            <div class="agp-card-body">

                <!-- Fila de selección de profesional y especialidad -->
                <div class="row seleccion-especialidad mb-4">
                    <div class="col-md-6">
                        <label class="form-label">Profesional</label>
                        <asp:DropDownList ID="ddlProfesional" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione un profesional" Value="" />
                            <asp:ListItem Text="Dr. Pérez" Value="1" />
                            <asp:ListItem Text="Dra. García" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Especialidad</label>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione una especialidad" Value="" />
                            <asp:ListItem Text="Cardiología" Value="1" />
                            <asp:ListItem Text="Pediatría" Value="2" />
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Parámetros -->
                <h5 class="fw-bold mb-3">Parámetros</h5>
                <div class="row g-3">
                    <div class="col-md-3">
                        <label class="form-label">Duración del Turno</label>
                        <asp:DropDownList ID="ddlDuracion" runat="server" CssClass="form-select">
                            <asp:ListItem Text="30 minutos" Value="30" />
                            <asp:ListItem Text="1 hora" Value="60" />
                            <asp:ListItem Text="1 hora 30 min" Value="90" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Pacientes por Turno</label>
                        <asp:TextBox ID="txtPacientes" runat="server" CssClass="form-control" TextMode="Number" Text="1" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Habilitar Desde</label>
                        <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-3">
                        <label class="form-label">Habilitar Hasta</label>
                        <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>

            </div>
        </div>

        <!-- Disponibilidad Semanal -->
        <div class="agp-card text-light mb-4">
            <div class="agp-card-body">
                <h5 class="fw-bold mb-3">Disponibilidad Semanal</h5>
                <div class="table-responsive">
                    <table class="table table-bordered table-dark text-center align-middle agp-disponibilidad">
                        <thead>
                            <tr>
                                <th>Hora</th>
                                <th>Lunes</th>
                                <th>Martes</th>
                                <th>Miércoles</th>
                                <th>Jueves</th>
                                <th>Viernes</th>
                                <th>Sábado</th>
                                <th>Domingo</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% for (int hora = 6; hora <= 23; hora++)
                                { %>
                            <tr>
                                <td><%= hora.ToString("00") %>:00</td>
                                <% for (int dia = 1; dia <= 7; dia++)
                                    { %>
                                <td class="agp-celda"></td>
                                <% } %>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <!-- Botones -->
        <div class="d-flex justify-content-end gap-3 mb-5">
            <%--<asp:Button ID="btnModificar" runat="server" Text="Modificar Agenda" CssClass="btn btn-warning text-dark fw-bold px-4" />--%>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Agenda" CssClass="btn btn-danger fw-bold px-4" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-success fw-bold px-4" />
        </div>

    </div>

    <script>
        document.addEventListener('DOMContentLoaded', () => {
            document.querySelectorAll('#pantalla-agenda .agp-celda').forEach(cell => {
                cell.addEventListener('click', () => {
                    cell.classList.toggle('active');
                });
            });
        });
    </script>

</asp:Content>
