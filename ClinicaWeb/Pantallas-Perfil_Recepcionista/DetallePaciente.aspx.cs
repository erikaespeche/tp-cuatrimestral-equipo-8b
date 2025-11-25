using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class DetallePaciente : System.Web.UI.Page
    {
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

        private void CargarMedicos(int idEspecialidad)
        {
            MedicoNegocio medNeg = new MedicoNegocio();
            var lista = medNeg.Listar();

            ddlProfesional.DataSource = lista;
            ddlProfesional.DataTextField = "NombreCompleto"; 
            ddlProfesional.DataValueField = "IdMedico";
            ddlProfesional.DataBind();

            ddlProfesional.Items.Insert(0, new ListItem("Seleccionar profesional", "0"));
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
            ddlHora.Items.Clear(); // Limpiamos por si hay algo
            ddlHora.Items.Add(new ListItem("Seleccionar hora", "")); // Item inicial

            var horas = new List<string> { "08:00", "09:00", "10:00", "11:00" };
            foreach (var h in horas)
            {
                ddlHora.Items.Add(new ListItem(h, h));
            }
        }

        private void CargarCitasPaciente()
        {
            // Obtenés el Id del paciente desde el ViewState
            if (ViewState["IdPaciente"] == null)
                return;

            int idPaciente = (int)ViewState["IdPaciente"];

            TurnoNegocio turnoNeg = new TurnoNegocio();
            var listaTurnos = turnoNeg.ListarPorPaciente(idPaciente); 

            rptCitas.DataSource = listaTurnos;
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
                    || string.IsNullOrEmpty(txtFechaTurno.Text) || ddlHora.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "alert", "alert('Complete todos los campos antes de guardar el turno');", true);
                    return;
                }

                TurnoNegocio turnoNeg = new TurnoNegocio();

                // Convertir fecha 
                DateTime fecha = DateTime.Parse(txtFechaTurno.Text);
                TimeSpan hora = TimeSpan.Parse(ddlHora.SelectedValue);

                // Combinar fecha + hora
                DateTime fechaCompleta = fecha.Date + hora;

                // Crear objeto Turno
                Turno nuevoTurno = new Turno
                {
                    IdPaciente = (int)ViewState["IdPaciente"],
                    IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue),
                    IdMedico = int.Parse(ddlProfesional.SelectedValue),
                    Fecha = fechaCompleta, 
                    Observaciones = txtObservaciones.Text
                };


                // Guardar en DB
                turnoNeg.Agregar(nuevoTurno);

                //actualizar citas
                CargarCitasPaciente();

                // Cerrar modal y mostrar mensaje de éxito
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "ShowSuccessModal",
                    "$('#modalNuevoTurno').modal('hide'); alert('Turno agregado correctamente');",
                    true
                );

                // Limpiar campos
                ddlEspecialidad.SelectedIndex = 0;
                ddlProfesional.SelectedIndex = 0;
                txtFechaTurno.Text = "";
                ddlHora.SelectedIndex = 0;
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

        protected void calTurno_SelectionChanged(object sender, EventArgs e)
        {
            // Guardamos la fecha seleccionada en el HiddenField
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEsp = int.Parse(ddlEspecialidad.SelectedValue);

            MedicoNegocio medNeg = new MedicoNegocio();

            if (idEsp == 0)
            {
                CargarMedicos();
            }
            else
            {
                var lista = medNeg.ListarPorEspecialidad(idEsp);

                ddlProfesional.DataSource = lista;
                ddlProfesional.DataTextField = "NombreCompleto";
                ddlProfesional.DataValueField = "IdMedico";
                ddlProfesional.DataBind();

                ddlProfesional.Items.Insert(0, new ListItem("Seleccionar profesional", "0"));
            }
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



    }
}
