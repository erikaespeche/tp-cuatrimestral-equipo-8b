<%@ Page Title="Clinica - Detalle Paciente" Language="C#" MasterPageFile="~/PerfilMedico.Master" AutoEventWireup="true" CodeBehind="DetallePaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Medico.DetallePaciente" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




<%-- Para que se ejcute el Modal de Exito o Error el formulario tiene que estar dentro de "asp:UpdatePanel" y ContentTemplate quedando los Modales de Exito o Error afuera --%>
  <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Always">
    <ContentTemplate> 

<div id="pantalla-detallepaciente"><!-- ⬅️ ID agregado -->

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
                    <p class="value-text">A+</p>
                    <!-- CAMBIAR POR Label ASP -->
                </div>

                <div class="col-md-4">
                    <p class="label-text">Peso</p>
                    <p class="value-text">80 kg</p>
                    <!-- CAMBIAR POR ASP -->
                </div>

                <div class="col-md-4">
                    <p class="label-text">Altura</p>
                    <p class="value-text">1.75 m</p>
                    <!-- CAMBIAR POR ASP -->
                </div>
            </div>

            <div class="filas-hc row mb-4">
                <div class="col-md-4">
                    <p class="label-text">Alergias</p>
                    <p class="value-text">Penicilina</p>
                </div>

                <div class="col-md-4">
                    <p class="label-text">Enfermedades Crónicas</p>
                    <p class="value-text">Hipertensión</p>
                </div>

                <div class="col-md-4">
                    <p class="label-text">Patologías</p>
                    <p class="value-text">Ninguna</p>
                </div>
            </div>

            <hr class="divisor">

            <!-- Consultas -->
            <h5 class="mb-3 seccion-titulo">Consultas</h5>

            <table class="custom-table align-middle w-100">
                <thead>
                    <tr>
                        <th>FECHA</th>
                        <th>MÉDICO</th>
                        <th>ESPECIALIDAD</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>10/05/2024</td>
                        <td>Dr. Carlos García</td>
                        <td>Cardiología</td>
                        <td>
                            <button class="btn btn-success btn-sm px-3">Ver</button>
                        </td>
                    </tr>

                    <tr>
                        <td>15/01/2024</td>
                        <td>Dr. Ana Martínez</td>
                        <td>Clínica Médica</td>
                        <td>
                            <button class="btn btn-success btn-sm px-3">Ver</button>
                        </td>
                    </tr>

                    <tr>
                        <td>02/11/2023</td>
                        <td>Dr. Luis Fernández</td>
                        <td>Traumatología</td>
                        <td>
                            <button class="btn btn-success btn-sm px-3">Ver</button>
                        </td>
                    </tr>
                </tbody>
            </table>

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

 






</div> <!-- ⬅️ cierre del div agregado -->
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







    


</asp:Content>

