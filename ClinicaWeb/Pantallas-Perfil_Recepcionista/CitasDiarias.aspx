<%@ Page Title="Citas Diarias" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" CodeBehind="CitasDiarias.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.CitasDiarias" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet"
        href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />

    <div id="pantalla-citasDiarias" class="container-fluid px-4 py-4 pantalla-citas">

        <!-- ==========================================
             TITULO
             =========================================== -->
        <div class="contenedor-cita-dia">

            <!-- TÍTULOS CORRECTOS -->
            <h3 class="titulo-citas-dia mb-1">Citas del Día</h3>
            <span class="subfecha" id="fechaSeleccionada"></span>

            <h5 class="descripcion-title mb-4">Gestiona y haz seguimiento de las citas programadas.
            </h5>

        </div>

        <div class="row g-4">

            <!-- ==========================================
                 CALENDARIO (IZQUIERDA)
            =========================================== -->
            <div class="col-lg-3">
                <div class="card calendario-container p-3">
                    <input type="text" id="calendarioCitas" class="form-control calendario-inline" />
                </div>
            </div>




            <!-- ==========================================
                 TABLA + FILTROS + TÍTULOS (DERECHA)
            =========================================== -->
            <div class="col-lg-9">
                <div class="card tabla-citas-card p-4">


                    <!-- FILTROS -->
                    <div class="filtros-busqueda row mb-4 g-2">
                        <div class="col-md-5">
                            <input type="text" class="form-control input-search"
                                placeholder="Buscar por DNI del paciente..." />
                        </div>

                        <div class="col-md-2">
                            <select class="form-select select-filter">
                                <option>Estado</option>
                                <option>Confirmada</option>
                                <option>Pendiente</option>
                                <option>Cancelada</option>
                            </select>
                        </div>

                        <div class="col-md-3">
                            <select class="form-select select-filter">
                                <option>Médico</option>
                                <option>Dr. García</option>
                                <option>Dra. Martínez</option>
                                <option>Dr. López</option>
                            </select>
                        </div>

                        <div class="col-md-2">
                            <select class="form-select select-filter">
                                <option>Especialidad</option>
                                <option>Cardiología</option>
                                <option>Dermatología</option>
                                <option>Pediatría</option>
                            </select>
                        </div>
                    </div>

                    <!-- TABLA -->
                    <div class="table-responsive tabla-scroll">
                        <table class="custom-table align-middle w-100">
                            <thead>
                                <tr>
                                    <th>Fecha</th>
                                    <th>Hora</th>
                                    <th>Paciente</th>
                                    <th>DNI</th>
                                    <th>Obra Social</th>
                                    <th>Médico</th>
                                    <th>Especialidad</th>
                                    <th>Estado</th>
                                    <th>Acción</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr>
                                    <td>15/10/24</td>
                                    <td>09:00</td>
                                    <td>Juan Pérez</td>
                                    <td>12345678</td>
                                    <td>OSDE</td>
                                    <td>Dr. García</td>
                                    <td>Cardiología</td>
                                    <td><span class="estado confirmado">Confirmada</span></td>
                                    <td>
                                        <div class="acciones">
                                            <button class="btn-accion cobrar">Cobrar</button>
                                            <button class="btn-accion ausente">Ausente</button>
                                            <button class="btn-accion cancelar">Cancelar</button>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                    </div>

                </div>
            </div>

        </div>

    </div>

    <!-- FLATPICKR -->
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://npmcdn.com/flatpickr/dist/l10n/es.js"></script>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            flatpickr("#calendarioCitas", {
                locale: "es",
                inline: true,
                dateFormat: "d/m/Y",
                defaultDate: new Date(),
                prevArrow: "<i class='bi bi-chevron-left'></i>",
                nextArrow: "<i class='bi bi-chevron-right'></i>",
                onChange: function (selectedDates, dateStr) {
                    document.getElementById("fechaSeleccionada").innerHTML = dateStr;
                }
            });

            const hoy = new Date().toLocaleDateString("es-ES",
                { day: "numeric", month: "long", year: "numeric" });

            document.getElementById("fechaSeleccionada").innerHTML = hoy;
        });
    </script>

</asp:Content>
