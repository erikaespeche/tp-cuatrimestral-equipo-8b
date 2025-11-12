<%@ Page Title="Clinica - Agregar Paciente Nuevo" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="AgregarNuevoPaciente.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.AgregarNuevoPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Se agrega margen superior para que el título no quede tapado por el menú fijo -->
    <div class="container py-5" style="margin-top:50px;">
        <div class="card p-4 mb-4">
            <h2 id="registropaciente" class="titulo mb-4">Registro de Nuevo Paciente</h2>
            <p class="label-text mb-3">Complete los siguientes campos para registrar un nuevo paciente.</p>

            <!--Eliminado el <form> interno -->
            <div class="row mb-3">
                <div class="col-md-3">
                    <label class="label-text">Nombre</label>
                    <input type="text" class="form-control" placeholder="Ingrese el nombre" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Apellido</label>
                    <input type="text" class="form-control" placeholder="Ingrese el apellido" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Tipo de Documento</label>
                    <select class="form-select">
                        <option>DNI</option>
                        <option>Pasaporte</option>
                        <option>Otro</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label class="label-text">Número de Documento</label>
                    <input type="text" class="form-control" placeholder="Ingrese el número" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-3">
                    <label class="label-text">Fecha de Nacimiento</label>
                    <input type="date" class="form-control" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Sexo</label>
                    <select class="form-select">
                        <option>Seleccione el sexo</option>
                        <option>Masculino</option>
                        <option>Femenino</option>
                        <option>Otro</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label class="label-text">Dirección</label>
                    <input type="text" class="form-control" placeholder="Ingrese la dirección" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Código Postal</label>
                    <input type="text" class="form-control" placeholder="Ingrese el código postal" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-3">
                    <label class="label-text">Localidad</label>
                    <input type="text" class="form-control" placeholder="Ingrese la localidad" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Provincia</label>
                    <input type="text" class="form-control" placeholder="Ingrese la provincia" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Celular</label>
                    <input type="text" class="form-control" placeholder="Ingrese el número de celular" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Mail</label>
                    <input type="email" class="form-control" placeholder="Ingrese el mail" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-3">
                    <label class="label-text">Obra Social</label>
                    <input type="text" class="form-control" placeholder="Ingrese la obra social" />
                </div>
                <div class="col-md-3">
                    <label class="label-text">Número de Afiliado</label>
                    <input type="text" class="form-control" placeholder="Ingrese el número de afiliado" />
                </div>
            </div>

            <!-- Botones de acción -->
            <div class="d-flex justify-content-end gap-3">
                <button type="button" class="btn btn-danger">Cancelar</button>
                <asp:Button ID="btnRegistrarPaciente" runat="server" CssClass="btn btn-add" Text="Registrar Paciente" OnClick="btnRegistrarPaciente_Click" />
            </div>

        </div>
    </div>

</asp:Content>
