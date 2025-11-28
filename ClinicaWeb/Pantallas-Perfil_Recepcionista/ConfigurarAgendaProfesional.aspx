<%@ Page Title="Configurar Agenda Profesionales" 
    Language="C#" 
    MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" 
    CodeBehind="ConfigurarAgendaProfesional.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.ConfigurarAgendaProfesional" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <%-- Para que se ejcute el Modal de Exito o Error el formulario tiene que estar dentro de "asp:UpdatePanel" y ContentTemplate quedando los Modales de Exito o Error afuera --%>
  <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Conditional">
      <ContentTemplate>

    <div id="pantalla-agenda" class="agp-wrapper text-light">

        <h2 class="agp-titulo">Configurar Agenda</h2>

        <!-- Contenedor principal de selección y parámetros -->
        <div class="agp-card text-light mb-4">
            <div class="agp-card-body">

                <!-- Fila de selección de profesional y especialidad -->
                <div class="row seleccion-especialidad mb-4">
                    <div class="col-md-6">
                        <label class="form-label">Profesional</label>
                        <asp:DropDownList ID="ddlProfesional" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlProfesional_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Especialidad</label>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Parámetros -->
                <h5 class="fw-bold mb-3">Parámetros</h5>
                <div class="row g-3">
                    <div class="col-md-3">
                        <label class="form-label">Duración del Turno</label>
                        <asp:DropDownList ID="ddlDuracion" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlDuracion_SelectedIndexChanged">

                            <asp:ListItem Value="15">15 minutos</asp:ListItem>
                            <asp:ListItem Value="20">20 minutos</asp:ListItem>
                            <asp:ListItem Value="30">30 minutos</asp:ListItem>
                            <asp:ListItem Value="45">45 minutos</asp:ListItem>
                            <asp:ListItem Value="60">60 minutos</asp:ListItem>

                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Pacientes por Turno</label>
                        <asp:TextBox ID="txtPacientes" runat="server" CssClass="form-control" 
                                     TextMode="Number" Text="1" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Habilitar Desde</label>
                        <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Habilitar Hasta</label>
                        <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>

            </div>
        </div>

        <!-- Disponibilidad Semanal -->
        <div class="agp-card text-light mb-4">
            <div class="agp-card-body">
                <h5 class="fw-bold mb-3">Disponibilidad Semanal</h5>

                <div class="table-responsive">
                    <table class="table table-bordered table-dark text-center align-middle agp-disponibilidad">
                        <thead>
                            <tr>
                                <th>Hora</th>
                                <th>Lunes</th>
                                <th>Martes</th>
                                <th>Miércoles</th>
                                <th>Jueves</th>
                                <th>Viernes</th>
                                <th>Sábado</th>
                                <th>Domingo</th>
                            </tr>
                        </thead>

                        <!-- 🟩 Tbody dinánico generado desde el CodeBehind -->
                        <tbody id="tbodyAgenda" runat="server"></tbody>

                    </table>
                </div>
            </div>
        </div>

        <!-- Botones -->
        <div class="d-flex justify-content-end gap-3 mb-5">

            <asp:Button ID="btnEliminar" runat="server"
                Text="Eliminar Agenda" CssClass="btn btn-danger fw-bold px-4" />

            <asp:Button ID="btnGuardar" runat="server"
                Text="Guardar Cambios" CssClass="btn btn-success fw-bold px-4"
                OnClick="btnGuardar_Click" />

        </div>

        <!-- 🔥 HiddenField para guardar las celdas activas -->
        <asp:HiddenField ID="hfSeleccion" runat="server" />

    </div>
   </ContentTemplate>
</asp:UpdatePanel>




    
    <!-- MODAL EXITO -->
    <div class="modal fade" id="modalOk" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-success text-white">

                <div class="modal-header">
                    <h5 class="modal-title">Agenda guardada</h5>
                </div>

                <div class="modal-body">
                    Los cambios fueron guardados correctamente.
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-light" id="btnAceptarOk">
                        Aceptar
                    </button>
                </div>

            </div>
        </div>
    </div>


    <!-- MODAL ERROR -->
    <div class="modal fade" id="modalError" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-danger text-white">

                <div class="modal-header">
                    <h5 class="modal-title">No se pudo guardar la agenda</h5>
                </div>

                <div class="modal-body" id="modalErrorMensaje">
                    <!-- El mensaje se inserta desde C# -->
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-light" data-bs-dismiss="modal" id="btnCerrarOk">
                        Cerrar
                    </button>
                </div>

            </div>
        </div>
    </div>


    <!-- Modal Error Selección -->
    <div class="modal fade" id="modalSeleccionIncompleta" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Error</h5>
                    <button type="button" id="btnCerrarError1" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>

                <div class="modal-body" style="background-color: #e07b83">
                    <p id="modalSeleccionMensaje"></p>
                </div>

                <div class="modal-footer" style="background-color: #e07b83">
                    <button type="button" id="btnCerrarError2" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                </div>

            </div>
        </div>
    </div>






    <%-- JS --%>
    <script>
        document.getElementById('btnAceptarOk').addEventListener('click', function () {
            location.reload();
        });

        document.getElementById('btnCerrarOk').addEventListener('click', function () {
            location.reload();
        });

        document.getElementById('btnCerrarError1').addEventListener('click', function () {
            location.reload();
        });
        document.getElementById('btnCerrarError2').addEventListener('click', function () {
            location.reload();
        });

        // Cerrar modal ERROR sin recargar la página
        //document.getElementById('btnCerrarOk').addEventListener('click', function () {
        //    var modal = bootstrap.Modal.getInstance(document.getElementById('modalError'));
        //    modal.hide();
        //});

        //document.getElementById('btnCerrarError1').addEventListener('click', function () {
        //    var modal = bootstrap.Modal.getInstance(document.getElementById('modalSeleccionIncompleta'));
        //    modal.hide();
        //});

        //document.getElementById('btnCerrarError2').addEventListener('click', function () {
        //    var modal = bootstrap.Modal.getInstance(document.getElementById('modalSeleccionIncompleta'));
        //    modal.hide();
        //});




    </script>



</asp:Content>
