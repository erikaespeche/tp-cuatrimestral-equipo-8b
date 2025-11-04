<%@ Page Title="Perfil Administrador" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="perfilAdmin.aspx.cs" Inherits="Clinic.PerfilAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="admin-container">
        <h1 class="titulo">Panel del Administrador</h1>
        <p class="subtitulo">Gestione pacientes, agendas, especialidades, profesionales y usuarios.</p>

        
        <div class="card-grid">

            <div class="card">
                <i class="fa fa-user icono"></i>
                <h2>Pacientes</h2>
                <p>Administre los datos de los pacientes registrados.</p>
                <asp:Button ID="btnPacientes" runat="server" Text="Administrar" CssClass="btn" />
            </div>

            <div class="card">
                <i class="fa fa-calendar icono"></i>
                <h2>Agendas</h2>
                <p>Gestione los horarios y disponibilidad de atención.</p>
                <asp:Button ID="btnAgendas" runat="server" Text="Administrar" CssClass="btn" />
            </div>

            <div class="card">
                <i class="fa fa-stethoscope icono"></i>
                <h2>Especialidades</h2>
                <p>Controle las especialidades médicas disponibles.</p>
                <asp:Button ID="btnEspecialidades" runat="server" Text="Administrar" CssClass="btn" />
            </div>

            <div class="card">
                <i class="fa fa-user-md icono"></i>
                <h2>Profesionales</h2>
                <p>Gestione los médicos y sus asignaciones.</p>
                <asp:Button ID="btnProfesionales" runat="server" Text="Administrar" CssClass="btn" />
            </div>

            <div class="card">
                <i class="fa fa-users icono"></i>
                <h2>Usuarios</h2>
                <p>Administre las cuentas y roles de los usuarios del sistema.</p>
                <asp:Button ID="btnUsuarios" runat="server" Text="Administrar" CssClass="btn" OnClientClick="mostrarUsuarios(); return false;" />
            </div>
        </div>

        <div id="panelUsuarios" class="panel-usuarios" style="display: none;">

            <div class="panel-usuarios-header">
                <h2>Gestionar Usuarios</h2>
                <button class="btn-agregar">+ Agregar nuevo usuario</button>
            </div>

            <div class="buscador">
                <input type="text" class="input" placeholder="Nombre..." />
                <input type="text" class="input" placeholder="Apellido..." />
                <input type="text" class="input" placeholder="DNI..." />
                <input type="text" class="input" placeholder="Nombre de usuario..." />
                <button class="btn-buscar">Buscar</button>
            </div>

            <table class="tabla">
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Apellido</th>
                        <th>DNI</th>
                        <th>Usuario</th>
                        <th style="width: 160px;">Acciones</th>
                    </tr>
                </thead>
                
            </table>

        </div>


        <script>
            function mostrarUsuarios() {
                const panel = document.getElementById("panelUsuarios");
                panel.style.display = (panel.style.display === "none" || panel.style.display === "") ? "block" : "none";
                panel.scrollIntoView({ behavior: "smooth" });
            }
        </script>
</asp:Content>
