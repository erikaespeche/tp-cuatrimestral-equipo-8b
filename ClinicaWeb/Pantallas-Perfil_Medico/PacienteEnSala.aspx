<%@ Page Title="Pacientes en Sala" Language="C#" MasterPageFile="~/PerfilMedico.Master"
    AutoEventWireup="true" CodeBehind="PacienteEnSala.aspx.cs"
    Inherits="ClinicaWeb.Medico.PacientesEnSala" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="pantalla-paciente-sala" class="pantalla-sala">

    <h2 class="titulo-paciente-sala">Gestión de Pacientes en Sala</h2>

    <!-- FILTROS -->
    <div class="filtros-box">
        <input type="text" placeholder="Ingrese DNI" />
        <input type="text" placeholder="Ingrese nombre" />
        <input type="text" placeholder="Ingrese apellido" />
        <button class="btn-buscar">🔍 Buscar</button>
    </div>

    <!-- TABS -->
    <ul class="nav nav-tabs nav-tabs-custom">
        <li class="nav-item">
            <a class="nav-link active" data-bs-toggle="tab" href="#espera">En Espera</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" data-bs-toggle="tab" href="#atendiendose">Atendiéndose</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" data-bs-toggle="tab" href="#atendido">Atendido</a>
        </li>
    </ul>

    <div class="tab-content">

        <!-- TAB EN ESPERA -->
        <div class="tab-pane fade show active" id="espera">
            <div class="tabla-sala">
                <asp:Repeater ID="rptTurnosEspera" runat="server" OnItemCommand="rptTurnosEspera_ItemCommand">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <tr class="titulos-pacientes-sala">
                                    <th>HORA TURNO</th>
                                    <th>NOMBRE</th>
                                    <th>DNI</th>
                                    <th>OBRA SOCIAL</th>
                                    <th>ACCIÓN</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Fecha", "{0:HH:mm}") %></td>
                            <td><%# Eval("Paciente") %></td>
                            <td><%# Eval("DNI") %></td>
                            <td><%# Eval("ObraSocial") %></td>
                            <td>
                                <asp:Button runat="server" Text="Atender" CssClass="btn-accion btn-atender" CommandName="Atender" CommandArgument='<%# Eval("IdTurno") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- TAB ATENDIENDOSE -->
        <div class="tab-pane fade" id="atendiendose">
            <div class="tabla-sala">
                <asp:Repeater ID="rptTurnosAtendiendose" runat="server" OnItemCommand="rptTurnosAtendiendose_ItemCommand">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <tr class="titulos-pacientes-sala">
                                    <th>HORA TURNO</th>
                                    <th>NOMBRE</th>
                                    <th>DNI</th>
                                    <th>OBRA SOCIAL</th>
                                    <th>ACCIÓN</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Fecha", "{0:HH:mm}") %></td>
                            <td><%# Eval("Paciente") %></td>
                            <td><%# Eval("DNI") %></td>
                            <td><%# Eval("ObraSocial") %></td>
                            <td>
                                <asp:Button runat="server" Text="Cargar Historia Clínica" 
                                    CssClass="btn-accion btn-cargarhistoriaclinica" 
                                    OnClientClick='<%# "window.location.href=\"CargarHistoriaClinica.aspx?dni=" + Eval("DNI") + "\"; return false;" %>' />
                                <asp:Button runat="server" Text="Finalizar" 
                                    CssClass="btn-accion btn-finalizar" 
                                    CommandName="Finalizar" CommandArgument='<%# Eval("IdTurno") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- TAB ATENDIDO -->
        <div class="tab-pane fade" id="atendido">
            <div class="tabla-sala">
                <asp:Repeater ID="rptTurnosAtendido" runat="server">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <tr class="titulos-pacientes-sala">
                                    <th>HORA TURNO</th>
                                    <th>NOMBRE</th>
                                    <th>DNI</th>
                                    <th>OBRA SOCIAL</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Fecha", "{0:HH:mm}") %></td>
                            <td><%# Eval("Paciente") %></td>
                            <td><%# Eval("DNI") %></td>
                            <td><%# Eval("ObraSocial") %></td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

    </div>
</div>

</asp:Content>
