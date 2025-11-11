<%@ Page Title="Clinica - Detalle Paciente" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="DetallePaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.DetallePaciente" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <!-- Contenido principal -->
    <div class="container py-4">
        <!-- Encabezado -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="titulo">Detalle de Paciente</h2>
            <button type="button" class="btn btn-edit">Editar</button>
        </div>

        <!-- Datos del Paciente -->
        <div class="card p-4 mb-4">
            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Nombre</p>
                    <p class="value-text">Juan</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Apellido</p>
                    <p class="value-text">Pérez</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Documento</p>
                    <p class="value-text">12.345.678</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Mail</p>
                    <p class="value-text">juan.perez@example.com</p>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Celular</p>
                    <p class="value-text">11 2345-6789</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Teléfono</p>
                    <p class="value-text">011 4567-8901</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Fecha de Nacimiento</p>
                    <p class="value-text">15/08/1985</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Dirección</p>
                    <p class="value-text">Av. Siempreviva 742</p>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-3">
                    <p class="label-text">Localidad</p>
                    <p class="value-text">Springfield</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Provincia</p>
                    <p class="value-text">Buenos Aires</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Obra Social</p>
                    <p class="value-text">OSDE</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Número de Obra Social</p>
                    <p class="value-text">210-12345678-01</p>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <p class="label-text">Código Postal</p>
                    <p class="value-text">B1636</p>
                </div>
                <div class="col-md-3">
                    <p class="label-text">Sexo</p>
                    <p class="value-text">Masculino</p>
                </div>
            </div>
        </div>

        <!-- Tabs -->
        <ul class="nav nav-tabs mb-3">
            <li class="nav-item"><a class="nav-link active" href="#">Citas</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Historia Clínica</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Etiquetas</a></li>
        </ul>

        <!-- Tabla de Turnos con estilo personalizado -->
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
                    <tr>
                        <td>15/06/2024</td>
                        <td>15:30</td>
                        <td>Dra. Ellie Sattler</td>
                        <td>Control</td>
                        <td><span class="badge badge-pendiente">Pendiente</span></td>
                        <td>
                            <button class="btn btn-primary btn-sm me-1">Enviar Recordatorio</button>
                            <button class="btn btn-warning btn-sm me-1 text-dark">Reprogramar</button>
                            <button class="btn btn-danger btn-sm">Cancelar</button>
                        </td>
                    </tr>
                    <tr>
                        <td>10/07/2024</td>
                        <td>08:00</td>
                        <td>Dr. Ian Malcolm</td>
                        <td>Estudios</td>
                        <td><span class="badge badge-cancelado">Cancelado</span></td>
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
