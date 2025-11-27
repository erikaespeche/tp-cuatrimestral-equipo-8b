<%@ Page Title="Gestión de Profesionales" Language="C#" MasterPageFile="~/PerfilRecepcionista.Master"
    AutoEventWireup="true" CodeBehind="GestionarProfesionales.aspx.cs"
    Inherits="Clinic.Pantallas_Perfil_Recepcionista.GestionarProfesionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pantalla-profesionales" class="container-fluid text-light py-4" style="max-width: 85%; margin: auto;">

        
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1">Gestión de Profesionales</h2>
                <p class="text-secondary">Busque, agregue, modifique o elimine profesionales de la clínica.</p>
            </div>
            <asp:Button ID="btnAgregar" runat="server" Text="+ Agregar Nuevo Profesional" CssClass="btn btn-primary fw-bold px-4 py-2" OnClick="btnAgregar_Click" />
        </div>

        
        <div class="card contenedor-filtro border-0 mb-4" style="border-radius: 10px;">
            <div class="card-body">
                <div class="row g-3 align-items-end">

                    <div class="col-md-4">
                        <label class="form-label text-white">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control bg-dark text-light border-secondary"
                            Placeholder="Buscar por Nombre" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label text-white">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server"
                            CssClass="form-control bg-dark text-light border-secondary"
                            Placeholder="Buscar por Apellido" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label text-white">Especialidad</label>
                        <div class="d-flex gap-2">
                            <asp:DropDownList ID="ddlEspecialidad" runat="server"
                                CssClass="form-select bg-dark text-light border-secondary">
                            </asp:DropDownList>

                            <asp:Button ID="btnBuscar" runat="server"
                                CssClass="btn btn-primary fw-bold px-4"
                                Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

        
        <div class="card border-0" style="border-radius: 10px;">
            <div class="table-responsive">
                <table class="custom-table align-middle w-100" style="border-radius: 10px;">
                    <thead class="border-bottom border-secondary">
                        <tr>
                            <th>NOMBRE</th>
                            <th>APELLIDO</th>
                            <th>DNI</th>
                            <th>TELÉFONO</th>
                            <th>EMAIL</th>
                            <th>ESPECIALIDADES</th>
                            <th>ACCIONES</th>
                        </tr>
                    </thead>

                    <tbody>
                        <asp:Repeater ID="repProfesionales" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Nombre") %></td>
                                    <td><%# Eval("Apellido") %></td>
                                    <td><%# Eval("Dni") %></td>
                                    <td><%# Eval("Telefono") %></td>
                                    <td><%# Eval("Email") %></td>


                                    <td>
                                        <%# string.Join(", ",
                                         ((dominio.Medico)Container.DataItem)
                                             .Especialidades.Select(esp => esp.Nombre)
                                         ) %>
                                    </td>

                                    <td>
                                        <!-- Ver -->
                                        <button type="button" class="btn btn-outline-info btn-sm me-1"
                                            data-bs-toggle="modal" data-bs-target="#modalVer"
                                            onclick='<%# "cargarDatosModal(\"" 
                                                + Eval("Nombre") + "\", \"" 
                                                + Eval("Apellido") + "\", \"" 
                                                + Eval("Dni") + "\", \"" 
                                                + Eval("Telefono") + "\", \"" 
                                                + Eval("Email") + "\", \"" 
                                                + string.Join(", ", ((dominio.Medico)Container.DataItem).Especialidades.Select(esp => esp.Nombre).ToArray()) 
                                                + "\")" %>'>
                                            <i class="bi bi-eye"></i>
                                        </button>

                                        <!-- Editar -->
                                        <button type="button" class="btn btn-outline-warning btn-sm me-1" 
                                                data-bs-toggle="modal" data-bs-target="#modalEditar"
                                                onclick='<%# "cargarDatosEditar(" + Eval("IdMedico") + ", \"" 
                                                             + Eval("Nombre") + "\", \"" 
                                                             + Eval("Apellido") + "\", \"" 
                                                             + Eval("Dni") + "\", \"" 
                                                             + Eval("Telefono") + "\", \"" 
                                                             + Eval("Email") + "\", \"" 
                                                             + string.Join(", ", ((dominio.Medico)Container.DataItem).Especialidades.Select(esp => esp.Nombre)) + "\")" %>'>
                                            <i class="bi bi-pencil"></i>
                                        </button>

                                        <!-- Eliminar -->
                                        <button type="button" class="btn btn-outline-danger btn-sm"
                                                data-bs-toggle="modal" data-bs-target="#modalEliminar"
                                                onclick='<%# "cargarDatosEliminar(" + Eval("IdMedico") + ", \"" 
                                                         + Eval("Nombre") + "\", \"" 
                                                         + Eval("Apellido") + "\")" %>'>
                                            <i class="bi bi-trash"></i>
                                        </button>


                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>

                </table>
            </div>
        </div>

    </div>

    <!-- Modal Ver -->
    <div class="modal fade" id="modalVer" tabindex="-1" aria-labelledby="modalVerLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content modal-turno-dark">

          <!-- Título -->
          <div class="modal-header border-secondary">
            <h5 class="modal-title fw-bold" id="modalVerLabel"> 
                <span class="material-symbols-outlined"
                      style="font-size: 26px; color:#007BFF; line-height:1; transform: translateY(4px);">
                    person
                </span>
                Detalles del Profesional</h5>
            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
          </div>

          <div class="modal-body">

            <!-- header -->
            <div class="d-flex align-items-center gap-3 pb-3 mb-3 border-bottom border-secondary">

              <!-- Logo + Nombre -->
              <div class="d-flex align-items-center justify-content-center"
                   style="width:64px; height:64px; border-radius:50%; background-color: rgba(0,123,255,0.15);">
                <span class="material-symbols-outlined d-flex align-items-center justify-content-center"
                      style="font-size:40px; color:#007BFF; line-height:1; transform: translate(1px, -6px);">
                  person
                </span>
              </div>

              <div>
                <p id="modalNombreCompleto" class="mb-1 fw-bold fs-5"></p>
              </div>

            </div>
             
            <!-- contenedor de datos -->
            <div id="contenedorDatos" class="px-2"></div>

            <!-- FOOTER --> 
            <div class="modal-footer border-secondary d-flex justify-content-end">
                <span id="modalEspecialidades" class="d-none"></span>
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
            </div>


        </div>
      </div>
    </div>
 </div>

    <!-- Modal Editar -->
    <div class="modal fade" id="modalEditar" tabindex="-1" aria-labelledby="modalEditarLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content modal-turno-dark">

          <!-- Header -->
          <div class="modal-header d-flex align-items-center gap-3 pb-3 mb-3 border-bottom border-secondary">
            <div class="d-flex align-items-center justify-content-center"
                 style="width:64px; height:64px; border-radius:50%; background-color: rgba(255,193,7,0.15);">
                <span class="material-symbols-outlined d-flex align-items-center justify-content-center"
                      style="font-size:40px; color:#FFC107; line-height:1; transform: translate(1px, -6px);">
                    edit
                </span>
            </div>

            <div>
                <h5 class="modal-title fw-bold mb-0" id="modalEditarLabel">Editar Profesional</h5>
            </div>

            <!-- Botón cerrar -->
            <button type="button" class="btn-close btn-close-white ms-auto" data-bs-dismiss="modal"></button>
          </div>

          <div class="modal-body">
            <div class="row mb-3">
              <div class="col-6">
                <label>Nombre</label>
                <input type="text" id="editNombre" runat="server" class="form-control bg-dark text-light border-secondary">
              </div>
              <div class="col-6">
                <label>Apellido</label>
                <input type="text" id="editApellido" runat="server" class="form-control bg-dark text-light border-secondary">
              </div>
            </div>
            <div class="row mb-3">
              <div class="col-6">
                <label>DNI</label>
                <input type="text" id="editDni" runat="server" class="form-control bg-dark text-light border-secondary">
              </div>
              <div class="col-6">
                <label>Teléfono</label>
                <input type="text" id="editTelefono" runat="server" class="form-control bg-dark text-light border-secondary">
              </div>
            </div>
            <div class="row mb-3">
              <div class="col-6">
                <label>Email</label>
                <input type="text" id="editEmail" runat="server" class="form-control bg-dark text-light border-secondary">
              </div>
              <div class="col-6">
                <label>Especialidades</label>
                <input type="text" id="editEspecialidades" runat="server" class="form-control bg-dark text-light border-secondary">
              </div>
            </div>
            <input type="hidden" id="editIdMedico" runat="server">
          </div>

          <div class="modal-footer border-secondary d-flex justify-content-end">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
            <asp:Button ID="btnGuardarCambios" runat="server" CssClass="btn btn-primary" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" />
          </div>
        </div>
      </div>
    </div>

<!-- Modal Eliminar -->
<div class="modal fade" id="modalEliminar" tabindex="-1" aria-labelledby="modalEliminarLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-lg">
    <div class="modal-content modal-turno-dark">

      <!-- Header tipo tarjeta -->
      <div class="modal-header d-flex align-items-center gap-3 pb-3 mb-3 border-bottom border-secondary">
        <!-- Logo circular rojo -->
        <div class="d-flex align-items-center justify-content-center"
             style="width:64px; height:64px; border-radius:50%; background-color: rgba(220,53,69,0.15);">
            <span class="material-symbols-outlined d-flex align-items-center justify-content-center"
                  style="font-size:40px; color:#DC3545; line-height:1; transform: translate(1px, -6px);">
                delete
            </span>
        </div>

        <!-- Título fijo -->
        <div>
            <h5 class="modal-title fw-bold mb-0" id="modalEliminarLabel">Eliminar Profesional</h5>
        </div>

        <!-- Botón cerrar -->
        <button type="button" class="btn-close btn-close-white ms-auto" data-bs-dismiss="modal"></button>
      </div>

      <!-- Cuerpo del modal -->
      <div class="modal-body">
        <p id="mensajeEliminar" class="fw-bold">¿Está seguro que desea eliminar al profesional?</p>

        <!-- Datos del profesional -->
        <div id="datosEliminarProfesional" class="px-2">
          <!-- Aquí se cargará dinámicamente el nombre y apellido -->
        </div>

        <p class="mt-3 text-danger">Todos los datos asociados a este profesional serán eliminados.</p>
        <input type="hidden" id="eliminarIdMedico" runat="server" />
      </div>

      <!-- Footer -->
      <div class="modal-footer border-secondary d-flex justify-content-end">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
        <asp:Button ID="btnConfirmarEliminar" runat="server" CssClass="btn btn-danger" Text="Confirmar Eliminación" OnClick="btnConfirmarEliminar_Click" />
      </div>

    </div>
  </div>
</div>



    <script>
        function cargarDatosModal(nombre, apellido, dni, telefono, email, especialidades = "Sin especificar") {

            document.getElementById("modalNombreCompleto").innerText = `Dr. / Dra. ${nombre} ${apellido}`;
            document.getElementById("modalEspecialidades").innerText = especialidades;

            const contenedor = document.getElementById('contenedorDatos');
            contenedor.innerHTML = `
        
                <!-- Fila 1 -->
                <div class="row mb-3">
                    <div class="col-6">
                        <span>Nombre</span>
                        <p class="mb-1 fw-bold">${nombre}</p>
                    </div>
                    <div class="col-6">
                        <span>Apellido</span>
                        <p class="mb-1 fw-bold">${apellido}</p>
                    </div>
                </div>

                <!-- Fila 2 -->
                <div class="row mb-3">
                    <div class="col-6">
                        <span>DNI</span>
                        <p class="mb-1 fw-bold">${dni}</p>
                    </div>
                    <div class="col-6">
                        <span>Teléfono</span>
                        <p class="mb-1 fw-bold">${telefono}</p>
                    </div>
                </div>

                <!-- Fila 3 -->
                <div class="row mb-3">
                    <div class="col-6">
                        <span>Email</span>
                        <p class="mb-1 fw-bold">${email}</p>
                    </div>
                    <div class="col-6">
                        <span>Especialidad</span>
                        <p class="mb-1 fw-bold">${especialidades}</p>
                    </div>
                </div>


            `;
        }

        function cargarDatosModalEditar(nombre, apellido, dni, telefono, email, especialidades = "") {
            document.getElementById("modalNombreCompletoEditar").innerText = `Dr. / Dra. ${nombre} ${apellido}`;

            const contenedor = document.getElementById('contenedorDatosEditar');
            contenedor.innerHTML = `
            <div class="row mb-3">
                <div class="col-6">
                    <label>Nombre</label>
                    <input type="text" class="form-control bg-dark text-light border-secondary" id="inputNombre" value="${nombre}">
                </div>
                <div class="col-6">
                    <label>Apellido</label>
                    <input type="text" class="form-control bg-dark text-light border-secondary" id="inputApellido" value="${apellido}">
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-6">
                    <label>DNI</label>
                    <input type="text" class="form-control bg-dark text-light border-secondary" id="inputDni" value="${dni}">
                </div>
                <div class="col-6">
                    <label>Teléfono</label>
                    <input type="text" class="form-control bg-dark text-light border-secondary" id="inputTelefono" value="${telefono}">
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-6">
                    <label>Email</label>
                    <input type="email" class="form-control bg-dark text-light border-secondary" id="inputEmail" value="${email}">
                </div>
                <div class="col-6">
                    <label>Especialidades</label>
                    <input type="text" class="form-control bg-dark text-light border-secondary" id="inputEspecialidades" value="${especialidades}">
                </div>
            </div>
    `;
        }

        function guardarCambios() {
            const nombre = document.getElementById("inputNombre").value;
            const apellido = document.getElementById("inputApellido").value;
            const dni = document.getElementById("inputDni").value;
            const telefono = document.getElementById("inputTelefono").value;
            const email = document.getElementById("inputEmail").value;
            const especialidades = document.getElementById("inputEspecialidades").value;

            console.log({ nombre, apellido, dni, telefono, email, especialidades });

            // Cerrar modal
            const modal = bootstrap.Modal.getInstance(document.getElementById('modalEditar'));
            modal.hide();
        }

        function cargarDatosEditar(id, nombre, apellido, dni, telefono, email, especialidades) {
            document.getElementById('<%= editIdMedico.ClientID %>').value = id;
            document.getElementById('<%= editNombre.ClientID %>').value = nombre;
            document.getElementById('<%= editApellido.ClientID %>').value = apellido;
            document.getElementById('<%= editDni.ClientID %>').value = dni;
            document.getElementById('<%= editTelefono.ClientID %>').value = telefono;
            document.getElementById('<%= editEmail.ClientID %>').value = email;
            document.getElementById('<%= editEspecialidades.ClientID %>').value = especialidades;
        }


        function cargarDatosEliminar(id, nombre, apellido) {
            document.getElementById('<%= eliminarIdMedico.ClientID %>').value = id;
            document.getElementById('mensajeEliminar').innerText = `¿Está seguro que desea eliminar al profesional ${nombre} ${apellido}?`;
        }


    </script>



</asp:Content>
