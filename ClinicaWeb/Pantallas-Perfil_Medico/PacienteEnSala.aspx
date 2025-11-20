<%@ Page Title="Pacientes en Sala" Language="C#" MasterPageFile="~/PerfilMedico.Master" AutoEventWireup="true" CodeBehind="PacienteEnSala.aspx.cs" Inherits="ClinicaWeb.Medico.PacientesEnSala" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">




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
            <a class="nav-link" data-bs-toggle="tab" href="#atendido">Atendido</a>
        </li>
    </ul>

    <div class="tab-content">

        <!-- TAB EN ESPERA -->
        <div class="tab-pane fade show active" id="espera">

            <div class="tabla-sala">
                <table>
                    <thead>
                        <tr>
                            <th>HORA TURNO</th>
                            <th>NOMBRE</th>
                            <th>APELLIDO</th>
                            <th>DNI</th>
                            <th>OBRA SOCIAL</th>
                            <th>ACCIÓN</th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr>
                            <td>09:00</td>
                            <td>Juan</td>
                            <td>Pérez</td>
                            <td>12.345.678</td>
                            <td>OSDE</td>
                            <td>
                                <button class="btn-accion btn-ver">Ver</button>
                                <button class="btn-accion btn-atendido">Atendido</button>
                            </td>
                        </tr>

                        <tr>
                            <td>09:15</td>
                            <td>María</td>
                            <td>González</td>
                            <td>23.456.789</td>
                            <td>Swiss Medical</td>
                            <td>
                                <button class="btn-accion btn-ver">Ver</button>
                                <button class="btn-accion btn-atendido">Atendido</button>
                            </td>
                        </tr>

                        <tr>
                            <td>09:30</td>
                            <td>Carlos</td>
                            <td>Rodríguez</td>
                            <td>34.567.890</td>
                            <td>Galeno</td>
                            <td>
                                <button class="btn-accion btn-ver">Ver</button>
                                <button class="btn-accion btn-atendido">Atendido</button>
                            </td>
                        </tr>
                    </tbody>

                </table>
            </div>

        </div>

        <!-- TAB ATENDIDO -->
        <div class="tab-pane fade" id="atendido">
            <p class="text-muted">No hay pacientes atendidos.</p>
        </div>

    </div>

</div>




    
</asp:Content>

