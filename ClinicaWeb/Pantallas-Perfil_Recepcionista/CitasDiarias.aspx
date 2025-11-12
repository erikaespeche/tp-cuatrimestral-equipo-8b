<%@ Page Title="Citas Diarias" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" CodeBehind="CitasDiarias.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.CitasDiarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenedor-padre container-fluid py-5">
        <div class="row">
            <!-- Calendario Flatpickr -->
            <div class="col-md-3">
                <h5 class="citas-del-dia titulo mb-3">Citas del Día</h5>
                <div class="card calendar-card p-3 mb-4">
                    <input type="text" id="calendarioCitas" class="form-control calendario-inline" />
                </div>
            </div>

            <!-- Tabla de Citas -->
            <div class="contenedor-tabla-citas col-md-9">
                <div class="contenedor-citas card p-4">
                    <h4 class="tabla-citas titulo mb-4">
                        Gestiona y haz seguimiento de las citas programadas
                    </h4>

                    <!-- Filtros -->
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <input type="text" class="form-control" placeholder="Buscar por DNI del paciente..." />
                        </div>
                        <div class="col-md-2">
                            <select class="form-select">
                                <option>Estado</option>
                                <option>Confirmada</option>
                                <option>Pendiente</option>
                                <option>Cancelada</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select class="form-select">
                                <option>Médico</option>
                                <option>Dr. Juan Carlos</option>
                                <option>Dra. Laura Torres</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select class="form-select">
                                <option>Especialidad</option>
                                <option>Cardiología</option>
                                <option>Dermatología</option>
                            </select>
                        </div>
                    </div>

                    <!-- Tabla -->
                    <table class="custom-table align-middle w-100">
                        <thead>
                            <tr>
                                <th class="no-wrap">Fecha</th>
                                <th class="no-wrap">Nombre</th>
                                <th class="no-wrap">Paciente</th>
                                <th class="no-wrap">DNI</th>
                                <th class="no-wrap">Obra Social</th>
                                <th class="no-wrap">Médico</th>
                                <th class="no-wrap">Especialidad</th>
                                <th class="no-wrap">Estado</th>
                                <th class="no-wrap">Acción</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="no-wrap">15/10/24</td>
                                <td class="no-wrap">Laura Pérez</td>
                                <td class="no-wrap">001</td>
                                <td class="no-wrap">12345678</td>
                                <td class="no-wrap">OSDE</td>
                                <td class="no-wrap">Dr. Juan Carlos</td>
                                <td class="no-wrap">Cardiología</td>
                                <td><span class="badge badge-confirmado">Confirmada</span></td>
                                <td>
                                    <div class="d-flex flex-row gap-2">
                                        <button class="boton-citasdiarias btn btn-success btn-sm">Cobrar</button>
                                        <button class="boton-citasdiarias btn btn-warning btn-sm text-dark">Ausente</button>
                                        <button class="boton-citasdiarias btn btn-danger btn-sm">Cancelar</button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <!-- Flatpickr (Calendario Moderno) -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://npmcdn.com/flatpickr/dist/l10n/es.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            flatpickr("#calendarioCitas", {
                locale: "es",
                inline: true, // Muestra el calendario siempre visible
                dateFormat: "d/m/Y",
                defaultDate: new Date(),
                prevArrow: "<i class='bi bi-chevron-left'></i>",
                nextArrow: "<i class='bi bi-chevron-right'></i>"
            });
        });
    </script>
</asp:Content>
