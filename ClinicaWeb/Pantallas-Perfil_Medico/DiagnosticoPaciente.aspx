<%@ Page Title="Diagnóstico del Paciente" Language="C#" MasterPageFile="~/PerfilMedico.Master" AutoEventWireup="true" CodeBehind="DiagnosticoPaciente.aspx.cs" Inherits="ClinicaWeb.Medico.DiagnosticoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-medico">
        <div class="card-medico">
            <h2>Diagnóstico del Paciente</h2>
            <hr />

            
            <div class="row mb-3">
                <div class="col-md-4 col-sm-12 mb-2">
                    <label class="label-medico">Paciente</label>
                    <asp:TextBox ID="txtPaciente" runat="server" CssClass="form-control input-medico" ReadOnly="true" />
                </div>
                <div class="col-md-4 col-sm-12 mb-2">
                    <label class="label-medico">DNI</label>
                    <asp:TextBox ID="txtDni" runat="server" CssClass="form-control input-medico" ReadOnly="true" />
                </div>
                <div class="col-md-4 col-sm-12 mb-2">
                    <label class="label-medico">Fecha del Turno</label>
                    <asp:TextBox ID="txtFechaTurno" runat="server" CssClass="form-control input-medico" ReadOnly="true" />
                </div>
            </div>

            <div class="mb-3">
                <label class="label-medico">Motivo de Consulta</label>
                <asp:TextBox ID="txtMotivo" TextMode="MultiLine" Rows="3" CssClass="form-control input-medico" runat="server" ReadOnly="true" />
            </div>

            <div class="mb-3">
                <label class="label-medico">Diagnóstico</label>
                <asp:TextBox ID="txtDiagnostico" TextMode="MultiLine" Rows="4" CssClass="form-control input-medico" runat="server" />
            </div>

            <div class="mb-3">
                <label class="label-medico">Tratamiento / Observaciones</label>
                <asp:TextBox ID="txtTratamiento" TextMode="MultiLine" Rows="4" CssClass="form-control input-medico" runat="server" />
            </div>

            <asp:Button ID="btnGuardar" CssClass="btn-medico" runat="server" Text="Guardar Diagnóstico" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
