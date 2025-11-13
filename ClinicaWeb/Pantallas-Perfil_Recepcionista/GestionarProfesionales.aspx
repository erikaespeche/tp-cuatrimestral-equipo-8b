<%@ Page Title="Gestión de Profesionales" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"  
    AutoEventWireup="true" CodeBehind="GestionarProfesionales.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.GestionarProfesionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-profesionales" class="container-fluid text-light py-4" style="max-width: 85%; margin: auto;">

        <!-- Título y botón -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Gestión de Profesionales</h2>
                <p class="text-secondary">Busque, agregue, modifique o elimine profesionales de la clínica.</p>
            </div>
            <button class="btn boton-agregar btn-primary fw-bold px-4 py-2">
                + Agregar Nuevo Profesional
            </button>
        </div>

        <!-- Filtros -->
        <div class="card border-0 mb-4" style="border-radius: 10px;">
            <div class="card-body">
                <h5 class="fw-bold mb-3 text-white">Filtros</h5>
                <div class="row g-3 align-items-end">
                    <div class="col-md-4">
                        <label class="form-label text-white">Nombre</label>
                        <input type="text" class="form-control bg-dark text-light border-secondary" placeholder="Buscar por Nombre" />
                    </div>
                    <div class="col-md-4">
                        <label class="form-label text-white">Apellido</label>
                        <input type="text" class="form-control bg-dark text-light border-secondary" placeholder="Buscar por Apellido" />
                    </div>
                    <div class="col-md-4">
                        <label class="form-label text-white">Especialidad</label>
                        <div class="d-flex gap-2">
                            <select class="form-select bg-dark text-light border-secondary">
                                <option selected>Filtrar por Especialidad</option>
                                <option>Cardiología</option>
                                <option>Pediatría</option>
                                <option>Dermatología</option>
                            </select>
                            <button class="btn btn-primary fw-bold px-4">Buscar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tabla -->
        <div class="card border-0" style="border-radius: 10px;">
            <div class="table-responsive">
                <table class="custom-table align-middle w-100" style="border-radius: 10px;">
                    <thead class="border-bottom border-secondary">
                        <tr>
                            <th>NOMBRE</th>
                            <th>APELLIDO</th>
                            <th>MATRÍCULA</th>
                            <th>ESPECIALIDAD</th>
                            <th>TELÉFONO</th>
                            <th>CELULAR</th>
                            <th>EMAIL</th>
                            <th>ACCIONES</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Juan</td>
                            <td class="fw-bold">Pérez</td>
                            <td>MN12345</td>
                            <td>Cardiología</td>
                            <td>4501-9876</td>
                            <td>11-5555-1234</td>
                            <td>juan.perez@clinic.com</td>
                            <td>
                                <button class="btn btn-outline-info btn-sm me-1"><i class="bi bi-eye"></i></button>
                                <button class="btn btn-outline-warning btn-sm me-1"><i class="bi bi-pencil"></i></button>
                                <button class="btn btn-outline-danger btn-sm"><i class="bi bi-trash"></i></button>
                            </td>
                        </tr>
                        <tr>
                            <td>María</td>
                            <td class="fw-bold">González</td>
                            <td>MN67890</td>
                            <td>Dermatología</td>
                            <td>4788-1234</td>
                            <td>11-4444-5678</td>
                            <td>maria.gonzalez@clinic.com</td>
                            <td>
                                <button class="btn btn-outline-info btn-sm me-1"><i class="bi bi-eye"></i></button>
                                <button class="btn btn-outline-warning btn-sm me-1"><i class="bi bi-pencil"></i></button>
                                <button class="btn btn-outline-danger btn-sm"><i class="bi bi-trash"></i></button>
                            </td>
                        </tr>
                        <tr>
                            <td>Carlos</td>
                            <td class="fw-bold">Rodríguez</td>
                            <td>MN11223</td>
                            <td>Pediatría</td>
                            <td>4902-5432</td>
                            <td>11-3333-8765</td>
                            <td>carlos.r@clinic.com</td>
                            <td>
                                <button class="btn btn-outline-info btn-sm me-1"><i class="bi bi-eye"></i></button>
                                <button class="btn btn-outline-warning btn-sm me-1"><i class="bi bi-pencil"></i></button>
                                <button class="btn btn-outline-danger btn-sm"><i class="bi bi-trash"></i></button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

    </div>

</asp:Content>
