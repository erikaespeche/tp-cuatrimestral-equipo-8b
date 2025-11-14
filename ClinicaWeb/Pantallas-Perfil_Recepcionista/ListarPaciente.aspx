<%@ Page Title="Clinica - Buscar Paciente" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="ListarPaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.ListarPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Agrego margen superior para evitar que lo tape el menu fijo -->
    <div id="pantalla-listarPaciente" class="container py-5" style="margin-top:30px;">

        <!-- Buscador -->
        <div class="buscarPaciente card p-4 mb-4">
            <h2 class="titulo mb-3">Búsqueda de Pacientes</h2>
            <p class="label-text mb-4">Ingrese los datos del paciente para realizar una búsqueda.</p>

            <!-- Ingresar datos del paciente -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <input type="text" class="form-control" placeholder="Ingrese número de documento" />
                </div>
                <div class="col-md-3">
                    <input type="text" class="form-control" placeholder="Ingrese nombre" />
                </div>
                <div class="col-md-3">
                    <input type="text" class="form-control" placeholder="Ingrese apellido" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-add w-100" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>

        <!-- Resultados -->
        <div class="card p-4">
            <h4 class="titulo mb-3">Resultados de la Búsqueda</h4>

            <table class="custom-table align-middle w-100">
                <thead>
                    <tr>
                        <th class="listarPaciente-resultado">Nombre</th>
                        <th class="listarPaciente-resultado">Teléfono Celular</th>
                        <th class="listarPaciente-resultado">Obra Social</th>
                        <th class="listarPaciente-resultado">Número de Obra Social</th>
                        <th class="listarPaciente-resultado">Acciones</th>
                    </tr>
                </thead>
                <tbody style="text-align:center;">
                    <tr>
                        <td>María García</td>
                        <td>11 9876-5432</td>
                        <td>Swiss Medical</td>
                        <td>SMG-98765432-02</td>
                        <td>
                            <button class="btn btn-success btn-sm me-1"><i class="bi bi-eye"></i></button>
                            <button class="btn btn-warning btn-sm"><i class="bi bi-pencil"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

    </div>

</asp:Content>

