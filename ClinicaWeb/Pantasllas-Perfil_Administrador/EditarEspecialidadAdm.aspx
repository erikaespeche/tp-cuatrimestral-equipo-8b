<%@ Page Title="" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="EditarEspecialidadAdm.aspx.cs" Inherits="ClinicaWeb.Pantasllas_Perfil_Administrador.EditarEspecialidadAdm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4 text-light">
        <h2>Editar Especialidad</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mb-2"></asp:Label>

        <div class="mb-3">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click"/>
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary ms-2" OnClick="btnCancelar_Click"/>
    </div>
</asp:Content>
