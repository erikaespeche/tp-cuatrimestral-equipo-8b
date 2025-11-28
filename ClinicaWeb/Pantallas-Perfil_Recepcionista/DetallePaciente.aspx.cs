using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class DetallePaciente : System.Web.UI.Page
    {
        private TurnoNegocio turnoNegocio = new TurnoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Setear fecha máxima para el validator (hoy)
                valFechaRango.MaximumValue = DateTime.Now.ToString("yyyy-MM-dd");

                // Recuperar DNI desde querystring
                string dni = Request.QueryString["dni"];

                if (!string.IsNullOrEmpty(dni))
                {
                    CargarDatosPaciente(dni);
                    // Guardar el DNI en ViewState para usarlo después
                    ViewState["dni"] = dni;
                }
                else
                {
                    // Si no hay DNI, redirigir a la lista de pacientes
                    Response.Redirect("ListarPaciente.aspx?error=SinDNI");
                }

                CargarEspecialidades();
                CargarMedicos();
                CargarHorasDisponibles();
                CargarCitasPaciente();
            }
        }




        // ======================== CARGAR DATOS ========================
        private void CargarDatosPaciente(string dni)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            var paciente = negocio.BuscarPorDni(dni);

            if (paciente == null)
            {
                Response.Redirect("ListarPaciente.aspx?error=NoEncontrado");
                return;
            }

            ViewState["IdPaciente"] = paciente.IdPaciente;

            lblNombre.Text = paciente.Nombres;
            lblApellido.Text = paciente.Apellidos;
            lblTipoDocumento.Text = paciente.TipoDocumento;
            lblDni.Text = paciente.DniPaciente.ToString();
            lblMail.Text = paciente.Email;
            lblCelular.Text = paciente.Celular;
            lblTelefono.Text = paciente.Telefono;
            lblFechaNacimiento.Text = paciente.FechaNacimiento.ToString("dd/MM/yyyy");
            lblDireccion.Text = paciente.Direccion;
            lblCiudad.Text = paciente.Ciudad;
            lblProvincia.Text = paciente.Provincia;
            lblObraSocial.Text = paciente.ObraSocial;
            lblNroObraSocial.Text = paciente.NumeroObraSocial;
            lblCodigoPostal.Text = paciente.CodigoPostal;
            lblSexo.Text = paciente.Sexo.ToString();
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio espNeg = new EspecialidadNegocio();
            var lista = espNeg.Listar();

            ddlEspecialidad.DataSource = lista;
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccionar especialidad", "0"));
        }

        private void CargarMedicos()
        {
            MedicoNegocio medNeg = new MedicoNegocio();
            var lista = medNeg.Listar();

            ddlProfesional.DataSource = lista;
            ddlProfesional.DataTextField = "NombreCompleto";
            ddlProfesional.DataValueField = "IdMedico";
            ddlProfesional.DataBind();

            ddlProfesional.Items.Insert(0, new ListItem("Seleccionar profesional", "0"));
        }

        private void CargarHorasDisponibles()
        {
            ddlHoraTurno.Items.Clear(); // Limpiamos por si hay algo
            ddlHoraTurno.Items.Add(new ListItem("Seleccionar hora", "")); // Item inicial

            var horas = new List<string> { "08:00", "09:00", "10:00", "11:00" };
            foreach (var h in horas)
            {
                ddlHoraTurno.Items.Add(new ListItem(h, h));
            }
        }

        private void CargarCitasPaciente()
        {
            if (ViewState["IdPaciente"] == null)
                return;

            int idPaciente = (int)ViewState["IdPaciente"];

          
            var listaTurnos = turnoNegocio.ListarPorPaciente(idPaciente);

            // Filtrar: solo turnos activos o reprogramados y futuros
            var turnosVisibles = listaTurnos
                .Where(t => t.EstadoAdmin != "Cancelado" && t.Fecha >= DateTime.Now)
                .OrderBy(t => t.Fecha)
                .ToList();

            rptCitas.DataSource = turnosVisibles;
            rptCitas.DataBind();
        }


        // ======================== BOTÓN GUARDAR ========================
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) VALIDAR ASP.NET
                if (!Page.IsValid)
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "abrirModal",
                        "abrirModalEditar();",
                        true
                    );
                    return;
                }

                PacienteNegocio negocio = new PacienteNegocio();

                string dniQuery = Request.QueryString["dni"];
                var pacienteOriginal = negocio.BuscarPorDni(dniQuery);

                if (pacienteOriginal == null)
                {
                    Response.Redirect("ListarPaciente.aspx?error=PacienteNoEncontrado");
                    return;
                }

                // =============================
                // 2) CREAR OBJETO PACIENTE A MODIFICAR
                // =============================
                Paciente p = pacienteOriginal;   // <-- CLAVE: partimos del original

                // 3) Asignar campos editados
                p.Nombres = txtNombreEdit.Text.Trim();
                p.Apellidos = txtApellidoEdit.Text.Trim();
                p.Email = txtMailEdit.Text.Trim();
                p.Celular = txtCelEdit.Text.Trim();
                p.Telefono = txtTelEdit.Text.Trim();
                p.Direccion = txtDirEdit.Text.Trim();
                p.Ciudad = txtCiudadEdit.Text.Trim();
                p.Provincia = txtProvEdit.Text.Trim();
                p.CodigoPostal = txtCpEdit.Text.Trim();
                p.ObraSocial = txtObraEdit.Text.Trim();
                p.NumeroObraSocial = txtNumObraEdit.Text.Trim();

                if (int.TryParse(txtDniEdit.Text, out int dniParsed))
                    p.DniPaciente = dniParsed;

                if (DateTime.TryParse(txtFechaEdit.Text, out DateTime fechaN))
                    p.FechaNacimiento = fechaN;

                if (!string.IsNullOrEmpty(ddlSexoEdit.SelectedValue))
                    p.Sexo = char.Parse(ddlSexoEdit.SelectedValue);

                // =============================
                // 4) GUARDAR CAMBIOS
                // =============================
                negocio.Modificar(p);

                // =============================
                // 5) MOSTRAR MODAL DE ÉXITO
                // =============================
                ScriptManager.RegisterStartupScript(
                  this,
                  GetType(),
                  "ShowSuccessModal",
                  "$('#modalExito').modal('show');",
                   true
                );

            }
            catch (Exception ex)
            {
                string errorMsg = "Error al guardar el paciente: " + ex.Message;
                errorMsg = errorMsg.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
                ScriptManager.RegisterStartupScript(
                 this,
                 GetType(),
                 "modalError",
                 $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{errorMsg}';",
                 true
                );
            }
        }


        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            string dni = ViewState["dni"] as string;
            if (!string.IsNullOrEmpty(dni))
                Response.Redirect("DetallePaciente.aspx?dni=" + dni);
            else
                Response.Redirect("ListarPaciente.aspx?error=SinDNI");
        }




        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // Cierra el modal. Bootstrap ya lo maneja.
        }


        protected void btnAgendarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (ddlEspecialidad.SelectedValue == "0" || ddlProfesional.SelectedValue == "0"
                    || string.IsNullOrEmpty(txtFechaTurno.Text) || ddlHoraTurno.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "alert", "alert('Complete todos los campos antes de guardar el turno');", true);
                    return;
                }

                TurnoNegocio turnoNeg = new TurnoNegocio();

                DateTime fecha = DateTime.Parse(txtFechaTurno.Text);
                TimeSpan hora = TimeSpan.Parse(ddlHoraTurno.SelectedValue);
                DateTime fechaCompleta = fecha.Date + hora;

                int idPaciente = (int)ViewState["IdPaciente"];
                int idMedico = int.Parse(ddlProfesional.SelectedValue);

                // ================================
                // VALIDACIÓN 1: Médico libre
                // ================================
                if (!turnoNeg.MedicoEstaLibre(idMedico, fechaCompleta))
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "alert", "alert('El médico ya tiene un turno asignado en ese horario.');", true);
                    return;
                }

                // ================================
                // VALIDACIÓN 2: Paciente libre
                // ================================
                if (!turnoNeg.PacienteEstaLibre(idPaciente, fechaCompleta))
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "alert", "alert('El paciente ya tiene un turno asignado en ese horario.');", true);
                    return;
                }

                // Crear Turno
                Turno nuevoTurno = new Turno
                {
                    IdPaciente = idPaciente,
                    IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue),
                    IdMedico = idMedico,
                    Fecha = fechaCompleta,
                    Observaciones = txtObservaciones.Text
                };

                turnoNeg.Agregar(nuevoTurno);

                // Actualizar
                CargarCitasPaciente();

                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "ShowSuccessModal",
                    "$('#modalNuevoTurno').modal('hide'); alert('Turno agregado correctamente');",
                    true
                );

                ddlEspecialidad.SelectedIndex = 0;
                ddlProfesional.SelectedIndex = 0;
                txtFechaTurno.Text = "";
                ddlHoraTurno.SelectedIndex = 0;
                txtObservaciones.Text = "";
            }
            catch (Exception ex)
            {
                string errorMsg = "Error al guardar el turno: " + ex.Message;
                errorMsg = errorMsg.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{errorMsg}';",
                    true
                );
            }
        }




        // =======================================================
        // ===============   CARGAR DATOS DEL MODAL AGREGAR TURNO  ============
        // =======================================================
        protected void btnAbrirTurno_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "MostrarModalNuevoTurno",
                "var m = new bootstrap.Modal(document.getElementById('modalNuevoTurno')); m.show();",
                true
            );
        }

       
        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEsp = int.Parse(ddlEspecialidad.SelectedValue);
            MedicoNegocio medNeg = new MedicoNegocio();

            var medicos = idEsp > 0 ? medNeg.ListarPorEspecialidad(idEsp) : medNeg.Listar();

            ddlProfesional.DataSource = medicos;
            ddlProfesional.DataTextField = "NombreCompleto";
            ddlProfesional.DataValueField = "IdMedico";
            ddlProfesional.DataBind();
            ddlProfesional.Items.Insert(0, new ListItem("Seleccionar profesional", "0"));
        }


        protected void ddlProfesional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProfesional.SelectedValue == "0")
                return;

            int idMedico = int.Parse(ddlProfesional.SelectedValue);

            MedicoNegocio medNeg = new MedicoNegocio();
            Medico medico = medNeg.BuscarPorId(idMedico);

            if (medico != null && medico.Especialidades.Count > 0)
            {
                ddlEspecialidad.SelectedValue = medico.Especialidades[0].IdEspecialidad.ToString();

            }
        }

        // =======================================================
        // ===============   BOTONES DE ACCIÓN  ==================
        // =======================================================


        protected void rptCitas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Recordatorio")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);
                ViewState["IdTurnoRecordatorio"] = idTurno;

                TurnoNegocio turnoNeg = new TurnoNegocio();
                var turno = turnoNeg.ListarAgendaTodasLasFechas()
                                     .FirstOrDefault(t => t.IdTurno == idTurno);

                if (turno != null)
                {
                    lblMensajeRecordatorio.Text =
                        $"¿Querés enviar un recordatorio del turno del {turno.Fecha:dd/MM/yyyy HH:mm} con Dr./Dra. {turno.Medico} (Especialidad: {turno.Especialidad}) al mail {lblMail.Text}?";

                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "abrirModalRecordatorio",
                        "var m = new bootstrap.Modal(document.getElementById('modalRecordatorio')); m.show();",
                        true
                    );
                }
            }

            if (e.CommandName == "Cancelar")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);
                ViewState["IdTurnoAccion"] = idTurno;

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalCan",
                    "var modal = new bootstrap.Modal(document.getElementById('modalCancelar')); modal.show();",
                    true);
            }


            if (e.CommandName == "Reprogramar")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);
                ViewState["IdTurnoReprogramar"] = idTurno;

                TurnoNegocio turnoNeg = new TurnoNegocio();
                var turno = turnoNeg.ListarAgendaTodasLasFechas()
                                    .FirstOrDefault(t => t.IdTurno == idTurno);

                if (turno != null)
                {
                    txtFechaNuevoTurno.Text = turno.Fecha.ToString("yyyy-MM-dd");
                    ddlHoraNuevoTurno.SelectedValue = turno.Fecha.ToString("HH:mm");
                }

                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "modalReprogramar",
                    "var m = new bootstrap.Modal(document.getElementById('modalReprogramar')); m.show();",
                    true
                );
            }


        }


        protected void btnEnviarRecordatorio_Click(object sender, EventArgs e)
        {
            if (ViewState["IdTurnoRecordatorio"] == null) return;

            int idTurno = (int)ViewState["IdTurnoRecordatorio"];
            TurnoNegocio turnoNeg = new TurnoNegocio();
            var turno = turnoNeg.ListarAgendaTodasLasFechas()
                                .FirstOrDefault(t => t.IdTurno == idTurno);

            if (turno == null) return;

            string cuerpo = $@"<h1>Estimado/a, {lblNombre.Text} {lblApellido.Text}</h1>
                                <h2>Le recordamos su próximo turno en nuestra clínica:</h2>
                                <p>Fecha: {turno.Fecha:dd/MM/yyyy HH:mm}<br>
                                Especialidad: {turno.Especialidad}<br>
                                Profesional: {turno.Medico}<br>
                                <br>
                                Por favor, llegue con 10 minutos de antelación.<br>
                                <br>
                                Saludos cordiales, <br>
                                Clinica.</p>";

            try
            {
                EmailService emailService = new EmailService();
                emailService.armarCorreo(lblMail.Text, "Recordatorio de su turno en la clínica", cuerpo);
                emailService.enviarEmail();

                lblResultadoRecordatorio.Text = "Recordatorio enviado correctamente al correo: " + lblMail.Text;
            }
            catch (Exception ex)
            {
                lblResultadoRecordatorio.Text = "No se pudo enviar el recordatorio. Error: " + ex.Message;
            }

            // Abrir modal de resultado
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalResultado",
                "$('#modalResultadoRecordatorio').modal('show');", true);
        }


        protected void btnConfirmarCancelar_Click(object sender, EventArgs e)
        {
            int idTurno = (int)ViewState["IdTurnoAccion"];
            turnoNegocio.CancelarTurno(idTurno);

            CargarCitasPaciente();

          
            ScriptManager.RegisterStartupScript(this, GetType(), "cerrarModalCancelar",
                "$('#modalCancelar').modal('hide');", true);

          
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarExitoCancelar",
                "$('#modalExitoCancelar').modal('show');", true);
        }



        protected void btnConfirmarReprogramacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdTurnoReprogramar"] == null)
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "noTurnoSel",
                        "var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = 'No se encontró el turno a reprogramar.';", true);
                    return;
                }

                int idTurno = (int)ViewState["IdTurnoReprogramar"];

                
                if (string.IsNullOrEmpty(txtFechaNuevoTurno.Text) || string.IsNullOrEmpty(ddlHoraNuevoTurno.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "faltanCampos",
                        "var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = 'Complete fecha y hora antes de guardar.';", true);
                    return;
                }

                if (!DateTime.TryParse(txtFechaNuevoTurno.Text, out DateTime fecha))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "fechaInvalida",
                        "var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = 'Fecha inválida.';", true);
                    return;
                }

                if (!TimeSpan.TryParse(ddlHoraNuevoTurno.SelectedValue, out TimeSpan hora))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "horaInvalida",
                        "var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = 'Hora inválida.';", true);
                    return;
                }

                DateTime nuevaFechaHora = fecha.Date + hora;

               
                TurnoNegocio turnoNeg = new TurnoNegocio();

                
                turnoNeg.ModificarFechaHora(idTurno, nuevaFechaHora);

               
                CargarCitasPaciente();

               
                string script = @"
            var m = bootstrap.Modal.getInstance(document.getElementById('modalReprogramar'));
            if(m){ m.hide(); }
            var m2 = new bootstrap.Modal(document.getElementById('modalExito'));
            m2.show();
        ";

                ScriptManager.RegisterStartupScript(this, GetType(), "reprogOK", script, true);
            }
            catch (Exception ex)
            {
                string msg = "Error al reprogramar: " + ex.Message;
                msg = msg.Replace("'", "\\'").Replace("\r", "").Replace("\n", " ");
                ScriptManager.RegisterStartupScript(this, GetType(), "reprogErr",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{msg}';", true);
            }
        }


        private void CargarHorasDesdeAgendaSeleccionada()
        {
            // Validaciones rápidas
            if (ddlProfesional.SelectedValue == "0" || ddlEspecialidad.SelectedValue == "0" || string.IsNullOrEmpty(txtFechaTurno.Text))
            {
                ddlHoraTurno.Items.Clear();
                ddlHoraTurno.Items.Add(new ListItem("Seleccionar hora", ""));
                return;
            }

            int idMedico = int.Parse(ddlProfesional.SelectedValue);
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            if (!DateTime.TryParse(txtFechaTurno.Text, out DateTime fechaSel))
            {
                ddlHoraTurno.Items.Clear();
                ddlHoraTurno.Items.Add(new ListItem("Fecha inválida", ""));
                return;
            }

            TurnoNegocio turnoNeg = new TurnoNegocio();
            var horas = turnoNeg.HorasDisponiblesPorFecha(idMedico, idEspecialidad, fechaSel);

            ddlHoraTurno.Items.Clear();
            ddlHoraTurno.Items.Add(new ListItem("Seleccionar hora", ""));

            if (horas.Count == 0)
            {
                ddlHoraTurno.Items.Add(new ListItem("No hay horas disponibles", ""));
                return;
            }

            foreach (var h in horas)
            {
                ddlHoraTurno.Items.Add(new ListItem(h.HoraStr, h.HoraStr));
            }
        }

        protected void calTurno_SelectionChanged(object sender, EventArgs e)
        {
            // ejemplo si tienes un calendario: guardar la fecha en el textbox y recargar horas
            txtFechaTurno.Text = calTurno.SelectedDate.ToString("yyyy-MM-dd");
            CargarHorasDesdeAgendaSeleccionada();
        }



        // ---------------------- Pintar días del calendario ----------------------
        protected void calTurno_DayRender(object sender, DayRenderEventArgs e)
        {
            // Días del pasado: no seleccionables
            if (e.Day.Date < DateTime.Today)
            {
                e.Cell.CssClass = "dia-no-disponible";
                e.Day.IsSelectable = false;
                e.Cell.ToolTip = "Día pasado";
                return;
            }

            // Si no se seleccionó profesional o especialidad, mostrar neutro y no permitir selección
            if (ddlProfesional.SelectedValue == "0" || ddlEspecialidad.SelectedValue == "0")
            {
                e.Cell.CssClass = "dia-no-seleccionado";
                e.Day.IsSelectable = false;
                e.Cell.ToolTip = "Seleccione profesional y especialidad";
                return;
            }

            // Validar parsing de ids
            if (!int.TryParse(ddlProfesional.SelectedValue, out int idMedico) ||
                !int.TryParse(ddlEspecialidad.SelectedValue, out int idEspecialidad))
            {
                e.Cell.CssClass = "dia-no-seleccionado";
                e.Day.IsSelectable = false;
                return;
            }

            DateTime fecha = e.Day.Date;

            try
            {
                // 1) Buscar si existe una agenda activa para ese médico+especialidad en la fecha
                int? idAgenda = turnoNegocio.ObtenerIdAgendaActivo(idMedico, idEspecialidad, fecha);

                if (!idAgenda.HasValue)
                {
                    // No hay agenda -> marcar como no disponible (rojo)
                    e.Cell.CssClass = "dia-no-disponible";
                    e.Day.IsSelectable = false;
                    e.Cell.ToolTip = "Sin agenda para este profesional en esta fecha";
                    return;
                }

                // 2) Obtener horas disponibles (este método ya considera PacientesPorTurno y turnos ya tomados)
                var horasDisponibles = turnoNegocio.HorasDisponiblesPorFecha(idMedico, idEspecialidad, fecha);

                if (horasDisponibles != null && horasDisponibles.Any())
                {
                    // Día con al menos 1 hora libre -> disponible (verde)
                    e.Cell.CssClass = "dia-disponible";
                    e.Day.IsSelectable = true;
                    e.Cell.ToolTip = $"{horasDisponibles.Count} horario(s) disponibles";
                }
                else
                {
                    // Agenda existe pero sin horas libres -> no disponible (rojo)
                    e.Cell.CssClass = "dia-no-disponible";
                    e.Day.IsSelectable = false;
                    e.Cell.ToolTip = "No hay horarios disponibles (agenda completa)";
                }
            }
            catch (Exception ex)
            {
                // En caso de error, dejar neutro y mostrar tooltip con el error (útil para debugging)
                e.Cell.CssClass = "dia-no-seleccionado";
                e.Day.IsSelectable = false;
                e.Cell.ToolTip = "Error al calcular disponibilidad: " + ex.Message;
            }
        }


        protected void txtFechaTurno_TextChanged(object sender, EventArgs e)
        {
            CargarHorasDesdeAgendaSeleccionada();
        }







    }

}
