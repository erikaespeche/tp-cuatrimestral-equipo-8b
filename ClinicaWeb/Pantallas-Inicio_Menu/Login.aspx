<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Clinic.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Ingreso al Sistema - Hospital Clinic</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="/Content/Login.css" rel="stylesheet" />
</head>

<body class="login-page">
    <form runat="server">
        <div class="login-card">
            <i class="bi bi-hospital icon-hospital"></i>
            <h3>Gestion de turnos</h3>

            <%-- USUARIO --%>
            <div class="mb-3 text-start">
                <label for="txtUsuario" class="form-label">Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario"></asp:TextBox>
            </div>

            <%-- CONTRASEÑA --%>
            <div class="mb-3 text-start position-relative">
                <label for="txtPassword" class="form-label">Contraseña</label>

                <div class="password-wrapper">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"
                        CssClass="form-control input-pass" placeholder="Ingrese su contraseña"></asp:TextBox>

                    <!-- Ojito -->
                    <i class="bi bi-eye-fill toggle-pass" onclick="togglePassword('txtPassword', this)"></i>
                </div>
            </div>



    <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="d-block text-center mb-3"></asp:Label>

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />

            <p class="footer-text">© 2025 Hospital Clinico - Sistema de Turnos</p>
        </div>
    </form>



    <%-- JS --%>
    <script>
    function togglePassword(id, icon) {
        const input = document.getElementById(id);

        if (input.type === "password") {
            input.type = "text";
            icon.classList.remove("bi-eye-fill");
            icon.classList.add("bi-eye-slash-fill");
        } else {
            input.type = "password";
            icon.classList.remove("bi-eye-slash-fill");
            icon.classList.add("bi-eye-fill");
        }
    }
    </script>



</body>
</html>
