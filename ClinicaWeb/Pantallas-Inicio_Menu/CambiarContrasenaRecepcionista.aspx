<%@ Page Title="Clinica | Cambiar Contraseña" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" 
    AutoEventWireup="true" CodeBehind="CambiarContrasenaRecepcionista.aspx.cs" Inherits="Clinic.Pantallas_Inicio_Menu.CambiarContrasenaRecepcionista" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="pantalla-perfilRecepcionista-cambiar-contrasena">

    <div class="container cambiar-pass-container" style="width:50%">

        <h2 class="titulo-cambiar" style="margin-top:90px; width:50%">Cambiar Contraseña</h2>
        <p class="subtitulo-cambiar">Ingrese su contraseña actual y la nueva contraseña para actualizar sus datos.</p>

        <div class="formulario-cambiar shadow-lg p-4 rounded-4">

            <!-- CONTRASEÑA ACTUAL -->
            <div class="mb-3 position-relative">
                <label class="form-label text-white">Contraseña actual</label>
                <div class="input-group">
                    <asp:TextBox ID="txtActual" runat="server" CssClass="form-control input-dark" TextMode="Password" />
                    <span class="input-group-text toggle-pass"><i class="bi bi-eye-fill"></i></span>
                </div>
            </div>

            <!-- NUEVA CONTRASEÑA -->
            <div class="mb-3 position-relative">
                <label class="form-label text-white">Nueva contraseña</label>
                <div class="input-group">
                    <asp:TextBox ID="txtNueva" runat="server" CssClass="form-control input-dark" TextMode="Password" />
                    <span class="input-group-text toggle-pass"><i class="bi bi-eye-fill"></i></span>
                </div>
            </div>

            <!-- REPETIR CONTRASEÑA -->
            <div class="mb-4 position-relative">
                <label class="form-label text-white">Repetir nueva contraseña</label>
                <div class="input-group">
                    <asp:TextBox ID="txtRepetir" runat="server" CssClass="form-control input-dark" TextMode="Password" />
                    <span class="input-group-text toggle-pass"><i class="bi bi-eye-fill"></i></span>
                </div>
            </div>

            <!-- BOTONES ALINEADOS A LA DERECHA -->
            <div class="d-flex justify-content-end gap-2">
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                    CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" />

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios"
                    CssClass="btn btn-guardar-azul" OnClick="btnGuardar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje-resultado mt-3"></asp:Label>

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


