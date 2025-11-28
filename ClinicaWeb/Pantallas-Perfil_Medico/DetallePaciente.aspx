<%@ Page Title="Clinica - Detalle Paciente" Language="C#" MasterPageFile="~/PerfilMedico.Master" AutoEventWireup="true" CodeBehind="DetallePaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Medico.DetallePaciente" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <%-- Para que se ejcute el Modal de Exito o Error el formulario tiene que estar dentro de "asp:UpdatePanel" y ContentTemplate quedando los Modales de Exito o Error afuera --%>
    <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <div id="pantalla-detallepaciente">
                <!-- ⬅️ ID agregado -->

                <div class="contenedor-detalle-paciente container py-4">

                    <!-- Encabezado -->
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <h2 class="titulo">Detalle de Paciente</h2>
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
                                <p class="label-text">Tipo de Documento</p>
                                <asp:Label ID="lblTipoDocumento" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Documento</p>
                                <asp:Label ID="lblDni" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <!-- Row 2 -->
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <p class="label-text">Mail</p>
                                <asp:Label ID="lblMail" runat="server" CssClass="value-text"></asp:Label>
                            </div>

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
                        </div>

                        <!-- Row 3 -->
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <p class="label-text">Dirección</p>
                                <asp:Label ID="lblDireccion" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Ciudad</p>
                                <asp:Label ID="lblCiudad" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Provincia</p>
                                <asp:Label ID="lblProvincia" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Código Postal</p>
                                <asp:Label ID="lblCodigoPostal" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <!-- Row 4 -->
                        <div class="row">
                            <div class="col-md-3">
                                <p class="label-text">Obra Social</p>
                                <asp:Label ID="lblObraSocial" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Número de Obra Social</p>
                                <asp:Label ID="lblNroObraSocial" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-3">
                                <p class="label-text">Sexo</p>
                                <asp:Label ID="lblSexo" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <!-- Tabs (solo maqueta) -->
                    <ul class="nav nav-tabs mb-3">
                        <li class="nav-item"><a class="nav-link active" href="#">Historia Clinica</a></li>

                    </ul>




                    <!-- ============================= -->
                    <!--     HISTORIA CLÍNICA UI      -->
                    <!-- ============================= -->

                    <div class="card p-4 mb-4 historia-clinica-card">

                        <!-- Título -->
                        <h5 class="mb-4 seccion-titulo">Datos Clínicos Importantes</h5>

                        <div class="filas-hc row mb-4">
                            <div class="col-md-4">
                                <p class="label-text">Grupo y Factor Sanguíneo</p>
                                <asp:Label ID="lblGrupoSanguineo" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-4">
                                <p class="label-text">Peso</p>
                                <asp:Label ID="lblPeso" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-4">
                                <p class="label-text">Altura</p>
                                <asp:Label ID="lblAltura" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <div class="filas-hc row mb-4">
                            <div class="col-md-4">
                                <p class="label-text">Alergias</p>
                                <asp:Label ID="lblAlergias" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-4">
                                <p class="label-text">Enfermedades Crónicas</p>
                                <asp:Label ID="lblEnfermedadesCronicas" runat="server" CssClass="value-text"></asp:Label>
                            </div>

                            <div class="col-md-4">
                                <p class="label-text">Patologías</p>
                                <asp:Label ID="lblPatologias" runat="server" CssClass="value-text"></asp:Label>
                            </div>
                        </div>

                        <hr class="divisor">
                        <div class="tabla-consultas-container">
                            <!-- Consultas -->
                            <h5 class="mb-3 seccion-titulo">Consultas</h5>

                            <table class="custom-table tabla-consultas align-middle w-100">
                                <thead>
                                    <tr>
                                        <th>FECHA</th>
                                        <th>MÉDICO</th>
                                        <th>ESPECIALIDAD</th>
                                        <th>ACCIONES</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptHistoriaClinica" runat="server" OnItemCommand="rptHistoriaClinica_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("FechaConsulta", "{0:dd/MM/yyyy}") %></td>
                                                <td><%# Eval("NombreMedico") %></td>
                                                <td><%# Eval("NombreEspecialidad") %></td>
                                                <td>
                                                    <asp:Button ID="btnVerConsulta" runat="server" Text="Ver"
                                                        CommandName="VerConsulta"
                                                        CommandArgument='<%# Eval("IdHistoriaClinica") %>'
                                                        CssClass="btn btn-success btn-sm px-3" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>







                    <%--           <table class="custom-table align-middle w-100">
                <thead>
                    <tr>
                        <th>FECHA</th>
                        <th>MÉDICO</th>
                        <th>ESPECIALIDAD</th>
                        <th>ACCIONES</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>25/05/2024</td>
                        <td>Dr. Alan Grant</td>
                        <td>Cardiologia</td>
                        <td>
                            <asp:LinkButton
                                ID="btnVer"
                                runat="server"
                                CssClass="btn btn-success btn-sm me-1"
                                CommandName="Ver"
                                CommandArgument='<%# Eval("DniPaciente") %>'>
                                <i class="bi bi-eye"></i>
                            </asp:LinkButton>

                            <%--<button class="btn btn-warning btn-sm"><i class="bi bi-pencil"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>--%>
                </div>

            </div>








            </div>
            <!-- ⬅️ cierre del div agregado -->
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--    <!-- MODAL — CAMBIOS GUARDADOS -->
    <div class="modal fade" id="modalGuardado" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-success text-white p-4 rounded">
                <h4 class="mb-3">Cambios guardados correctamente</h4>

                <div class="text-end">
                    <asp:Button ID="btnAceptarGuardado" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClick="btnAceptarGuardado_Click" />
                </div>
            </div>
        </div>
    </div>--%>



    <!-- ===================== -->
    <!--     MODAL ÉXITO       -->
    <!-- ===================== -->
    <div class="modal fade" id="modalExito" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-success text-white p-4 rounded">
                <h4 class="mb-3">Paciente registrado correctamente</h4>

                <div class="text-end">
                    <asp:Button ID="btnAceptarExito" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClientClick="location.reload(); return false;" />
                </div>
            </div>
        </div>
    </div>

    <!-- ===================== -->
    <!--      MODAL ERROR      -->
    <!-- ===================== -->
    <div class="modal fade" id="modalError" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content bg-danger text-white p-4 rounded">
                <h4 class="mb-3">Error al registrar el paciente</h4>

                <!-- Aquí se inyecta el mensaje -->
                <div id="modalErrorBody" class="mb-3"></div>

                <div class="text-end">
                    <asp:Button ID="btnAceptarError" runat="server"
                        CssClass="btn btn-light"
                        Text="Aceptar"
                        OnClientClick="location.reload(); return false;" />
                </div>
            </div>
        </div>
    </div>




    <!-- ===================== -->
    <!--   MODAL VER CONSULTA   -->
    <!-- ===================== -->
    <div class="modal fade" id="modalVerConsulta" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content p-4">

                <h4 class="mb-3">Detalle de Consulta</h4>

                <div class="row mb-2">
                    <div class="col-md-4">
                        <label>Fecha:</label>
                        <asp:Label ID="lblVerFecha" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Médico:</label>
                        <asp:Label ID="lblVerMedico" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Especialidad:</label>
                        <asp:Label ID="lblVerEspecialidad" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <hr />

                <div class="mb-3">
                    <label>Observaciones:</label>
                    <asp:Label ID="lblVerObservaciones" runat="server" CssClass="form-control-plaintext"></asp:Label>
                </div>

                <div class="mb-3">
                    <label>Diagnóstico:</label>
                    <asp:Label ID="lblVerDiagnostico" runat="server" CssClass="form-control-plaintext"></asp:Label>
                </div>

                <div class="mb-3">
                    <label>Tratamientos:</label>
                    <asp:Label ID="lblVerTratamientos" runat="server" CssClass="form-control-plaintext"></asp:Label>
                </div>

                <div class="mb-3">
                    <label>Recomendaciones:</label>
                    <asp:Label ID="lblVerRecomendaciones" runat="server" CssClass="form-control-plaintext"></asp:Label>
                </div>

                <div class="text-end">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>

            </div>
        </div>
    </div>






</asp:Content>

