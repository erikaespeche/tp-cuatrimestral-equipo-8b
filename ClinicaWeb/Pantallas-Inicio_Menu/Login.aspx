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
    <form id="form1" runat="server">
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

            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión"
                CssClass="btn btn-primary btn-login" OnClick="btnLogin_Click" />


            <a href="#" class="footer-text forgot-link" data-bs-toggle="modal" data-bs-target="#modalRecuperar">¿Olvidaste tu contraseña?
            </a>

        </div>

        <div class="modal fade" id="modalRecuperar" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="ventana-editar-paciente modal-content bg-dark text-light p-4"
                    style="background-color: #21364B; border: 1px solid gray; color: white;">

                    
                    <div class="modal-header border-0">
                        <h4 class="modal-title">Recuperar contraseña</h4>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>

                   
                    <div class="modal-body">

                        <label class="form-label">Ingresá tu email</label>

                        <asp:TextBox ID="txtRecuperarEmail" runat="server"
                            CssClass="form-control mb-3"
                            Style="background-color: #21364B; border: 1px solid gray; color: white;"
                            placeholder="ejemplo@correo.com">
                        </asp:TextBox>

                        <asp:Label ID="lblRecuperarError" runat="server"
                            CssClass="text-danger d-block mb-3">
                        </asp:Label>

                        <div class="text-end">
                            <asp:Button ID="btnEnviarRecupero" runat="server"
                                CssClass="btn btn-primary"
                                Text="Enviar"
                                OnClick="btnEnviarRecupero_Click" />
                        </div>
                    </div>

                </div>
            </div>
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

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>


</body>
</html>
