<%@ Page Title="" Language="C#" MasterPageFile="~/PerfilAdministrador.Master" AutoEventWireup="true" CodeBehind="ListadoCitasAdm.aspx.cs" Inherits="ClinicaWeb.Pantasllas_Perfil_Administrador.ListadoCitasAdm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pantalla-citas-medicos" class="page-wrap d-flex align-items-start justify-content-center">

        <div class="citas-card contenedor-citas shadow-sm">

            <h1 class="titulo titlo-citas-diarias mb-3">Listado de Citas Diarias</h1>

            <div class="row g-4">
              
                <div class="col-12 col-lg-5">
                    <div class="calendar-box p-3">
                      

                        <asp:Calendar ID="Calendar1" runat="server"
                            CssClass="calendar"
                            DayHeaderStyle-CssClass="cal-day-header"
                            TitleStyle-CssClass="cal-title"
                            NextPrevStyle-CssClass="cal-nav"
                            WeekendDayStyle-CssClass="cal-weekend"
                            TodayDayStyle-CssClass="cal-today"
                            SelectedDayStyle-CssClass="cal-selected"
                            OtherMonthDayStyle-CssClass="cal-other-month"
                            OnSelectionChanged="Calendar1_SelectionChanged"
                            FirstDayOfWeek="Monday"
                            VisibleDate="2024-06-01"
                            SelectionMode="Day"></asp:Calendar>
                    </div>
                </div>

               
                <div class="col-12 col-lg-7">
                  
                    <div class="search-box d-flex gap-2 mb-3">
                        <asp:TextBox ID="SearchText" runat="server" CssClass="form-control search-input"
                            placeholder="Buscar por Nombre, Apellido, DNI"></asp:TextBox>
                        <asp:Button ID="SearchBtn" runat="server" CssClass="btn btn-primary btn-buscar"
                            Text="Buscar" OnClick="SearchBtn_Click" />
                    </div>

                    <div class="resumen d-flex gap-3 mb-3">
                        <div class="badge-resumen total">
                            <span class="label">Total:</span>
                            <asp:Label ID="TotalCountLabel" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="badge-resumen disponibles">
                            <span class="label">Disponibles:</span>
                            <asp:Label ID="AvailableCountLabel" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="badge-resumen fecha">
                            <span class="label">Fecha:</span>
                            <asp:Label ID="SelectedDateLabel" runat="server"></asp:Label>
                        </div>
                    </div>

                   
                    <div class="table-responsive tabla-citas">
                        <table class="table table-sm align-middle">
                            <thead>
                                <tr>
                                    <th>Hora</th>
                                    <th>Fecha</th>
                                    <th>Nombre</th>
                                    <th>Apellido</th>
                                    <th>DNI</th>
                                    <th>Obra Social</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="CitasRepeater" runat="server" OnItemCommand="CitasRepeater_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Hora") %></td>
                                            <td><%# ((DateTime)Eval("Fecha")).ToString("dd/MM/yyyy") %></td>
                                            <td><%# Eval("Nombre") %></td>
                                            <td><%# Eval("Apellido") %></td>
                                            <td><%# Eval("DNI") %></td>
                                            <td><%# Eval("ObraSocial") %></td>
                                            <td class="text-end">
                                                <asp:Button runat="server" Text="Ver Historia Clínica"
                                                    CssClass='btn btn-success btn-ver'
                                                    CommandName="ver"
                                                    CommandArgument='<%# Eval("DNI") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="table-row-alt">
                                            <td><%# Eval("Hora") %></td>
                                            <td><%# ((DateTime)Eval("Fecha")).ToString("dd/MM/yyyy") %></td>
                                            <td><%# Eval("Nombre") %></td>
                                            <td><%# Eval("Apellido") %></td>
                                            <td><%# Eval("DNI") %></td>
                                            <td><%# Eval("ObraSocial") %></td>
                                            <td class="text-end">
                                                <asp:Button runat="server" Text="Ver Historia Clínica"
                                                    CssClass='btn btn-success btn-ver'
                                                    CommandName="ver"
                                                    CommandArgument='<%# Eval("Id") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
