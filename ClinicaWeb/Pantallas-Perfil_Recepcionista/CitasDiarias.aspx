<%@ Page Title="" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master" AutoEventWireup="true" CodeBehind="CitasDiarias.aspx.cs" Inherits="Clinic.Pantallas_Perfil_Recepcionista.CitasDiarias" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="contenedor-padre container-fluid py-5">
        <div class="row">

            <!-- Calendario -->
            <div class="col-md-3">
                <h5 class="citas-del-dia titulo mb-3">Citas del Día<br /></h5>
                <div class="card calendar-card p-3 mb-4">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <button class="calendar-nav-btn"><i class="bi bi-chevron-left"></i></button>
                        <h5 class="m-0 text-white">Octubre 2024</h5>
                        <button class="calendar-nav-btn"><i class="bi bi-chevron-right"></i></button>
                    </div>


                    <table class="custom-table tabla-calendario align-middle w-100" >
                        <thead>
                            <tr>
                                <th>D</th>
                                <th>L</th>
                                <th>M</th>
                                <th>X</th>
                                <th>J</th>
                                <th>V</th>
                                <th>S</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                            </tr>
                            <tr>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                                <td>9</td>
                                <td>10</td>
                                <td>11</td>
                                <td>12</td>
                            </tr>
                            <tr>
                                <td>13</td>
                                <td>14</td>
                                <td class="selected-day">15</td>
                                <td>16</td>
                                <td>17</td>
                                <td>18</td>
                                <td>19</td>
                            </tr>
                            <tr>
                                <td>20</td>
                                <td>21</td>
                                <td>22</td>
                                <td>23</td>
                                <td>24</td>
                                <td>25</td>
                                <td>26</td>
                            </tr>
                            <tr>
                                <td>27</td>
                                <td>28</td>
                                <td>29</td>
                                <td>30</td>
                                <td>31</td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <!-- Tabla de Citas -->
            <div class="contenedor-tabla-citas col-md-9">
                <div class="contenedor-citas card p-4">
                    <h4 class="tabla-citas titulo mb-4">Gestiona y haz seguimiento de las citas programadas</h4>

                    <!-- Filtros -->
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <input type="text" class="form-control" placeholder="Buscar por DNI del paciente..." />
                        </div>
                        <div class="col-md-2">
                            <select class="form-select">
                                <option>Estado</option>
                                <option>Confirmada</option>
                                <option>Pendiente</option>
                                <option>Cancelada</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select class="form-select">
                                <option>Médico</option>
                                <option>Dr. Juan Carlos</option>
                                <option>Dra. Laura Torres</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select class="form-select">
                                <option>Especialidad</option>
                                <option>Cardiología</option>
                                <option>Dermatología</option>
                            </select>
                        </div>
                    </div>

                    <!-- Tabla -->
                    <table class="custom-table align-middle w-100">
                        <thead>
                            <tr>
                                <th class="no-wrap">Fecha</th>
                                <th class="no-wrap">Nombre</th>
                                <th class="no-wrap">Paciente</th>
                                <th class="no-wrap">DNI</th>
                                <th class="no-wrap">Obra Social</th>
                                <th class="no-wrap">Médico</th>
                                <th class="no-wrap">Especialidad</th>
                                <th class="no-wrap">Estado</th>
                                <th class="no-wrap">Acción</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="no-wrap">15/10/24</td>
                                <td class="no-wrap">Laura Pérez</td>
                                <td class="no-wrap">001</td>
                                <td class="no-wrap">12345678</td>
                                <td class="no-wrap">OSDE</td>
                                <td class="no-wrap"">Dr. Juan Carlos</td>
                                <td class="no-wrap">Cardiología</td>
                                <td><span class="badge badge-confirmado">Confirmada</span></td>
                                <td>
                                    <div class="d-flex flex-row gap-2">
                                        <button class="boton-citasdiarias btn btn-success btn-sm">Cobrar</button>
                                        <button class="boton-citasdiarias btn btn-warning btn-sm text-dark">Ausente</button>
                                        <button class="boton-citasdiarias btn btn-danger btn-sm">Cancelar</button>
                                    </div>
                                </td>

                            </tr>
                            <!-- Repetir para otras citas -->
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>

 


</asp:Content>
