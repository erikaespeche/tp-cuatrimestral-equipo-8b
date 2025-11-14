<%@ Page Title="Listado de Pacientes" Language="C#" MasterPageFile="~/PerfilMedico.Master" AutoEventWireup="true" CodeBehind="ListadoCitas.aspx.cs" Inherits="ClinicaWeb.Medico.ListadoCitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-medico">
        <div class="card-medico">
            <h2>Listado de Pacientes</h2>
            <hr />

            <asp:Label ID="lblCantidadPacientes" runat="server" Text="Total de pacientes: 0" CssClass="fw-bold mb-3 d-block"></asp:Label>

            <asp:GridView ID="gvListadoPacientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="IdPaciente" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombres" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellidos" />
                    <asp:BoundField HeaderText="DNI" DataField="DniPaciente" />
                    <asp:BoundField HeaderText="Fecha Nacimiento" DataField="FechaNacimiento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Obra Social" DataField="ObraSocial" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
