<%@ Page Title="" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="CitasDiariasAdm.aspx.cs" Inherits="ClinicaWeb.Pantasllas_Perfil_Administrador.CitasDiariasAdm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet"
        href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />

    <div id="pantalla-citasDiarias" class="container-fluid px-4 py-4 pantalla-citas">


        <div class="contenedor-cita-dia">


            <h3 class="titulo-citas-dia mb-1">Citas del Día</h3>
            <asp:Label ID="lblFechaSeleccionada" runat="server" CssClass="subfecha"></asp:Label>

            <h5 class="descripcion-title mb-4">Gestiona y haz seguimiento de las citas programadas.
            </h5>

        </div>

        <div class="row g-4">


            <div class="col-lg-3">
                <div class="card calendario-container p-3">
                    <input type="text" id="calendarioCitas" class="form-control calendario-inline" />
                </div>
            </div>




            <div class="col-lg-9">
                <div class="card tabla-citas-card p-4">


                    <div class="filtros-busqueda row mb-4 g-2">
                        <div class="col-md-5">
                            <asp:TextBox ID="txtBuscarDNI"
                                runat="server"
                                CssClass="form-control input-search"
                                AutoPostBack="true"
                                OnTextChanged="txtBuscarDNI_TextChanged"
                                placeholder="Buscar por DNI del paciente..." />
                        </div>

                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlEstado"
                                runat="server"
                                CssClass="form-select select-filter"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                <asp:ListItem Value="">Estado</asp:ListItem>
                                <asp:ListItem Value="Confirmada">Confirmada</asp:ListItem>
                                <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                <asp:ListItem Value="Cancelado">Cancelada</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlMedico"
                                runat="server"
                                CssClass="form-select select-filter"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlEspecialidad"
                                runat="server"
                                CssClass="form-select select-filter"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                    </div>


                    <div class="table-responsive tabla-scroll">
                        <table class="custom-table align-middle w-100">
                            <thead>
                                <tr>
                                    <th>Fecha</th>
                                    <th>Hora</th>
                                    <th>Paciente</th>
                                    <th>DNI</th>
                                    <th>Obra Social</th>
                                    <th>Médico</th>
                                    <th>Especialidad</th>
                                    <th>Estado</th>
                                    <th>Acción</th>
                                </tr>
                            </thead>

                            <tbody>
                                <asp:Repeater ID="repTurnos" runat="server" OnItemCommand="repTurnos_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
                                            <td><%# Eval("Hora") %></td>
                                            <td><%# Eval("Paciente") %></td>
                                            <td><%# Eval("DNI") %></td>
                                            <td><%# Eval("ObraSocial") %></td>
                                            <td><%# Eval("Medico") %></td>
                                            <td><%# Eval("Especialidad") %></td>

                                            <td>
                                                <span class="estado <%# Eval("Estado").ToString().ToLower() %>">
                                                    <%# Eval("Estado") %>
                                                </span>
                                            </td>

                                            <td>
                                                <asp:LinkButton runat="server"
                                                    CssClass="btn-accion cobrar"
                                                    Text="Cobrar"
                                                    CommandName="CambiarEstado"
                                                    CommandArgument='<%# Eval("IdTurno") + "|Cobrado" %>' />

                                                <asp:LinkButton runat="server"
                                                    CssClass="btn-accion ausente"
                                                    Text="Ausente"
                                                    CommandName="CambiarEstado"
                                                    CommandArgument='<%# Eval("IdTurno") + "|Ausente" %>' />

                                                <asp:LinkButton runat="server"
                                                    CssClass="btn-accion cancelar"
                                                    Text="Cancelar"
                                                    CommandName="CambiarEstado"
                                                    CommandArgument='<%# Eval("IdTurno") + "|Cancelado" %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>

                        </table>
                    </div>

                </div>
            </div>

        </div>

    </div>

    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://npmcdn.com/flatpickr/dist/l10n/es.js"></script>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            flatpickr("#calendarioCitas", {
                locale: "es",
                inline: true,
                dateFormat: "d/m/Y",
                defaultDate: new Date(),
                prevArrow: "<i class='bi bi-chevron-left'></i>",
                nextArrow: "<i class='bi bi-chevron-right'></i>",


                onChange: function (selectedDates, dateStr) {

                    const form = document.forms[0];
                    const hidden = document.createElement("input");
                    hidden.type = "hidden";
                    hidden.name = "fecha";
                    hidden.value = dateStr;
                    form.appendChild(hidden);
                    form.submit();
                }
            });


        });
    </script>


</asp:Content>
