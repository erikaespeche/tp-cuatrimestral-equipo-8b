<%@ Page Title="Clinica | Cambiar Contraseña" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" 
    AutoEventWireup="true" CodeBehind="CambiarContrasenaRecepcionista.aspx.cs" Inherits="Clinic.Pantallas_Inicio_Menu.CambiarContrasenaRecepcionista" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Para que se ejcute el Modal de Exito o Error el formulario tiene que estar dentro de "asp:UpdatePanel" y ContentTemplate quedando los Modales de Exito o Error afuera --%>
  <asp:UpdatePanel ID="updForm" runat="server" UpdateMode="Always">
    <ContentTemplate> 

<div id="pantalla-perfilRecepcionista-cambiar-contrasena">

    <div class="container cambiar-pass-container" style="width:50%; margin-top:40px">

        <h2 class="titulo-cambiar" style="margin-top:40px; width:50%">Cambiar Contraseña</h2>
        <%--<h2 class="text-center mb-4" style="margin-top:40px; width:50%">Cambiar Contraseña
            <br />
            <small class="text-primary">
                <asp:Label ID="lblUsuarioTitulo" runat="server"></asp:Label>
            </small>
        </h2>--%>

        <p class="subtitulo-cambiar">Ingrese su contraseña actual y la nueva contraseña para actualizar sus datos.</p>

        <div class="formulario-cambiar shadow-lg p-4 rounded-4">

            <!-- CONTRASEÑA ACTUAL -->
            <div class="mb-3 position-relative">
                <label class="form-label text-white">Contraseña actual</label>
                <div class="input-group">
                    <asp:TextBox ID="txtActual" runat="server" CssClass="form-control input-dark" TextMode="Password" />
                    <span class="input-group-text toggle-pass"><i class="bi bi-eye-fill"></i></span>
                </div>
                <asp:Label ID="lblErrorActual" runat="server" CssClass="text-danger d-block"></asp:Label>
              

            </div>

            <!-- NUEVA CONTRASEÑA -->
            <div class="mb-3 position-relative">
                <label class="form-label text-white">Nueva contraseña</label>
                <div class="input-group">
                    <asp:TextBox ID="txtNueva" runat="server" CssClass="form-control input-dark" TextMode="Password" />
                    <span class="input-group-text toggle-pass"><i class="bi bi-eye-fill"></i></span>
                </div>
                <asp:Label ID="lblErrorNueva" runat="server" CssClass="text-danger d-block"></asp:Label>
                

            </div>

            <!-- REPETIR CONTRASEÑA -->
            <div class="mb-4 position-relative" style="padding-top:6px">
                <label class="form-label text-white">Repetir nueva contraseña</label>
                <div class="input-group">
                    <asp:TextBox ID="txtRepetir" runat="server" CssClass="form-control input-dark" TextMode="Password" />
                    <span class="input-group-text toggle-pass"><i class="bi bi-eye-fill"></i></span>
                </div>
                <asp:Label ID="lblErrorRepetir" runat="server" CssClass="text-danger d-block"></asp:Label>

            </div>

            <!-- BOTONES ALINEADOS A LA DERECHA -->
            <div class="d-flex justify-content-end gap-2">
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                    CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" />

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                    CssClass="btn btn-guardar-azul" OnClick="btnGuardar_Click" />
            </div>

            <%--<asp:label id="lblMensaje" runat="server" cssclass="mensaje-resultado mt-3"></asp:label>--%>
           


        </div>
    </div>

</div>

  </ContentTemplate>
</asp:UpdatePanel>



    <!-- MODAL ÉXITO -->
    <div class="modal fade" id="modalExito" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content bg-success text-white">
                <div class="modal-header">
                    <h5 class="modal-title">Éxito</h5>
                    
                </div>
                <div class="modal-body">
                    La contraseña se actualizó correctamente.
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnAceptarExito"
                        runat="server"
                        Text="Aceptar"
                        CssClass="btn btn-light"
                        OnClick="btnAceptarExito_Click" />
                </div>
            </div>
        </div>
    </div>


    <!-- MODAL ERROR -->
    <div class="modal fade" id="modalError" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content bg-danger text-white">
                <div class="modal-header">
                    <h5 class="modal-title">Error</h5>
                    
                </div>
                <div class="modal-body">
                    Ocurrió un error al guardar los cambios.
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnAceptarError"
                        runat="server"
                        Text="Aceptar"
                        CssClass="btn btn-light"
                        OnClick="btnAceptarError_Click" />
                </div>
            </div>
        </div>
    </div>



 








    <%-- JS --%>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const toggles = document.querySelectorAll(".toggle-pass");

            toggles.forEach((toggle) => {
                toggle.addEventListener("click", function () {
                    let input = this.parentElement.querySelector("input");

                    if (input.type === "password") {
                        input.type = "text";
                        this.innerHTML = '<i class="bi bi-eye-slash-fill"></i>';
                    } else {
                        input.type = "password";
                        this.innerHTML = '<i class="bi bi-eye-fill"></i>';
                    }
                });
            });
        });
    </script>





</asp:Content>


