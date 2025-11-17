<%@ Page Title="Clinica - Detalle Paciente" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="DetallePaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.DetallePaciente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-4">

        <!-- Encabezado -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="titulo">Detalle de Paciente</h2>
            <button class="btn btn-outline-warning btn-sm me-1"><i class="bi bi-pencil"></i></button>

        </div>

        <!-- Datos del Paciente -->
        <div class="card p-4 mb-4">

            <!-- Row 1 -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Nombre</p>
                    <asp:Label ID="lblNombre" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Apellido</p>
                    <asp:Label ID="lblApellido" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Documento</p>
                    <asp:Label ID="lblDni" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Mail</p>
                    <asp:Label ID="lblMail" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

            <!-- Row 2 -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Celular</p>
                    <asp:Label ID="lblCelular" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Teléfono</p>
                    <asp:Label ID="lblTelefono" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Fecha de Nacimiento</p>
                    <asp:Label ID="lblFechaNacimiento" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Dirección</p>
                    <asp:Label ID="lblDireccion" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

            <!-- Row 3 -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Ciudad</p>
                    <asp:Label ID="lblCiudad" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Provincia</p>
                    <asp:Label ID="lblProvincia" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Obra Social</p>
                    <asp:Label ID="lblObraSocial" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Número de Obra Social</p>
                    <asp:Label ID="lblNroObraSocial" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

            <!-- Row 4 -->
            <div class="row">
                <div class="col-md-3">
                    <p class="label-text">Código Postal</p>
                    <asp:Label ID="lblCodigoPostal" runat="server" CssClass="value-text"></asp:Label>
                </div>

                <div class="col-md-3">
                    <p class="label-text">Sexo</p>
                    <asp:Label ID="lblSexo" runat="server" CssClass="value-text"></asp:Label>
                </div>
            </div>

        </div>

        <!-- Tabs -->
        <ul class="nav nav-tabs mb-3">
            <li class="nav-item"><a class="nav-link active" href="#">Citas</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Historia Clínica</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Etiquetas</a></li>
        </ul>

        <!-- SOLO MAQUETA, NO TOCAR -->
        <div class="card p-4">
            <div class="d-flex justify-content-end mb-3">
                <button type="button" class="btn btn-add">+ Agregar Nuevo Turno</button>
            </div>

            <table class="custom-table align-middle w-100">
                <thead>
                    <tr>
                        <th>FECHA</th>
                        <th>HORA</th>
                        <th>MÉDICO</th>
                        <th>TRATAMIENTO</th>
                        <th>ESTADO</th>
                        <th>ACCIONES</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>25/05/2024</td>
                        <td>10:00</td>
                        <td>Dr. Alan Grant</td>
                        <td>Consulta General</td>
                        <td><span class="badge badge-confirmado">Confirmado</span></td>
                        <td>
                            <button class="btn btn-primary btn-sm me-1">Enviar Recordatorio</button>
                            <button class="btn btn-warning btn-sm me-1 text-dark">Reprogramar</button>
                            <button class="btn btn-danger btn-sm">Cancelar</button>
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>

    </div>

</asp:Content>

