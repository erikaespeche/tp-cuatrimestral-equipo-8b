<%@ Page Title="Clinica - Buscar Paciente" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="ListarPacienteAdm.aspx.cs" Inherits="ClinicaWeb.Pantasllas_Perfil_Administrador.ListarPacienteAdm" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-listarPaciente" class="container py-5" style="margin-top:30px;">

        <!-- Buscador -->
        <div class="buscarPaciente card p-4 mb-4">
            <h2 class="titulo mb-3">Búsqueda de Pacientes</h2>
            <p class="subtitulo label-text mb-4">Ingrese los datos del paciente para realizar una búsqueda.</p>

            <div class="row mb-3">

                <div class="col-md-3">
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtDocumento" runat="server"  CssClass="form-control" Placeholder="Ingrese número de documento" />
                </div>

                <div class="col-md-3">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Ingrese nombre" />
                </div>

                <div class="col-md-3">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Placeholder="Ingrese apellido" />
                </div>

                <div class="col-md-3 contenedor-botonBuscar">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn boton-buscar btn-add w-100" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>

            </div>
        </div>

        <!-- Resultados -->
        <div class="contenedor-resultados card p-4">
            <h4 class="titulo titulo-resultado mb-3">Resultados de la Búsqueda</h4>

            <asp:Repeater ID="repPacientes" runat="server" OnItemCommand="RepPacientes_ItemCommand">
                <HeaderTemplate>
                    <table class="custom-table align-middle w-100">
                        <thead>
                            <tr class="resultado-paciente">
                                <th>Nombre</th>
                                <th>Documento</th>
                                <th>Obra Social</th>
                                <th>Número de Obra Social</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody style="text-align:center;">
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Nombres") %> <%# Eval("Apellidos") %></td>
                        <td><%# Eval("DniPaciente") %></td>
                        <td><%# Eval("ObraSocial") %></td>
                        <td><%# Eval("NumeroObraSocial") %></td>
                        <td>
                            <asp:LinkButton
                                ID="btnVer"
                                runat="server"
                                CssClass="btn btn-success btn-sm me-1"
                                CommandName="Ver"
                                CommandArgument='<%# Eval("DniPaciente") %>'>
                                <i class="bi bi-eye"></i>
                            </asp:LinkButton>

                            <%--<button class="btn btn-warning btn-sm"><i class="bi bi-pencil"></i></button>--%>
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

</asp:Content>