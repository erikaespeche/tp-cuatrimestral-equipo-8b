<%@ Page Title="Mi Agenda" Language="C#" MasterPageFile="~/PerfilMedico.Master"
    AutoEventWireup="true" CodeBehind="Calendario.aspx.cs"
    Inherits="ClinicaWeb.Medico.Calendario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="~/Content/PerfilMedicoCSS.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pantalla-agenda" class="agp-wrapper">

        <h2 class="agp-titulo">Mi Agenda</h2>

       
        <div class="agp-card mb-4">
            <div class="agp-card-body">
                <h5 class="fw-bold mb-3">Disponibilidad Semanal</h5>
                <div class="table-responsive">
                    <table class="table table-bordered table-light text-center align-middle agp-disponibilidad">
                        <thead class="table-dark">
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
                            <% for (int hora = 8; hora <= 19; hora++)
                                { %>
                            <tr>
                                <td><%= hora.ToString("00") %>:00 - <%= (hora + 1).ToString("00") %>:00</td>
                                <% for (int dia = 1; dia <= 7; dia++)
                                    { %>
                                <td class="agp-celda">
                                   
                                    <% if (hora == 10 && dia == 1)
                                    { %>
                                    <asp:LinkButton runat="server" PostBackUrl="DiagnosticoPaciente.aspx?TurnoID=1" CssClass="btn btn-sm btn-primary w-100">Juan Pérez</asp:LinkButton>
                                    <% } %>
                                    <% if (hora == 11 && dia == 2)
                                    { %>
                                    <asp:LinkButton runat="server" PostBackUrl="DiagnosticoPaciente.aspx?TurnoID=2" CssClass="btn btn-sm btn-success w-100">María Gómez</asp:LinkButton>
                                    <% } %>
                                </td>
                                <% } %>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                </div>
            </div>
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
