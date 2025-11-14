<%@ Page Title="Pacientes en Sala" Language="C#" MasterPageFile="~/PerfilMedico.Master" AutoEventWireup="true" CodeBehind="PacientesEnSala.aspx.cs" Inherits="ClinicaWeb.Medico.PacientesEnSala" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-medico">
        <div class="card-medico">
            <h2>Pacientes en Sala</h2>
            <hr />

           
            <asp:GridView ID="gvPacientesSala" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                    <asp:BoundField HeaderText="DNI" DataField="DniPaciente" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:BoundField HeaderText="Celular" DataField="Celular" />
                    <asp:BoundField HeaderText="Obra Social" DataField="ObraSocial" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnLlamar" runat="server" Text="Llamar" CssClass="btn btn-primary btn-sm" />
                            <asp:Button ID="btnEnConsulta" runat="server" Text="En Consulta" CssClass="btn btn-success btn-sm" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
