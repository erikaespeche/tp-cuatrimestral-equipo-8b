using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class AgregarNuevoProfesional : System.Web.UI.Page
    {
        MedicoNegocio negocio = new MedicoNegocio();
        EspecialidadNegocio espNegocio = new EspecialidadNegocio();
        //TurnoTrabajoNegocio turnotrabajoNegocio = new TurnoTrabajoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarTurnos();
                CargarEspecialidades();

                if (Request.QueryString["nuevaEspecialidad"] == "1")
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "mensaje",
                        "alert('La especialidad fue creada correctamente.');",
                        true
                    );
                }
            }
        }

        //private void CargarTurnos()
        //{
        //    ddlTurnoTrabajo.DataSource = turnotrabajoNegocio.Listar();
        //    ddlTurnoTrabajo.DataTextField = "Nombre";
        //    ddlTurnoTrabajo.DataValueField = "IdTurnoTrabajo";
        //    ddlTurnoTrabajo.DataBind();

        //    ddlTurnoTrabajo.Items.Insert(0, new ListItem("Seleccione un turno", "0"));
        //}

        private void CargarEspecialidades()
        {
            ddlEspecialidad.DataSource = espNegocio.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una especialidad", "0"));
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarProfesionales.aspx");
        }

        protected void btnRegistrarProfesional_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreProfesional.Value.Trim();
            string apellido = txtApellidoProfesional.Value.Trim();
            string dni = txtDniProfesional.Value.Trim();
            string email = txtEmailProfesional.Value.Trim();
            string telefono = txtTelefonoProfesional.Value.Trim();

            bool valido = true;
            string mensajeError = "";

            // validaciones
            var valNombre = Validador.ValidarNombre(nombre);
            if (!valNombre.esValido) { mensajeError += valNombre.mensaje + "<br>"; valido = false; }

            var valApellido = Validador.ValidarApellido(apellido);
            if (!valApellido.esValido) { mensajeError += valApellido.mensaje + "<br>"; valido = false; }

            var valDni = Validador.ValidarDNI(dni);
            if (!valDni.esValido) { mensajeError += valDni.mensaje + "<br>"; valido = false; }

            if (!string.IsNullOrWhiteSpace(email))
            {
                var valEmail = Validador.ValidarEmail(email);
                if (!valEmail.esValido) { mensajeError += valEmail.mensaje + "<br>"; valido = false; }
            }

            if (!string.IsNullOrWhiteSpace(telefono))
            {
                var valTel = Validador.ValidarTelefono(telefono);
                if (!valTel.esValido) { mensajeError += valTel.mensaje + "<br>"; valido = false; }
            }

            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            if (idEspecialidad == 0)
            {
                mensajeError += "Debe seleccionar una especialidad.<br>";
                valido = false;
            }

            if (!valido)
            {
                mensajeError = mensajeError.Replace("'", "\\'")
                                           .Replace("\r", "")
                                           .Replace("\n", "<br>");

                ScriptManager.RegisterStartupScript(
                    this, GetType(), "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{mensajeError}';",
                    true
                );
                return;
            }

            try
            {
                Medico nuevoProfesional = new Medico
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Dni = dni,
                    Email = email,
                    Telefono = telefono,
                    Especialidades = new List<Especialidad>()
                };

                // 1️⃣ REGISTRA EL MÉDICO Y OBTIENE EL ID
                MedicoNegocio negocioMedico = new MedicoNegocio();
                int idMedico = negocioMedico.Agregar(nuevoProfesional);

                // 2️⃣ GUARDA LA ESPECIALIDAD EN LA TABLA PUENTE
                MedicoEspecialidadNegocio meNeg = new MedicoEspecialidadNegocio();
                meNeg.Agregar(new MedicoEspecialidad
                {
                    IdMedico = idMedico,
                    IdEspecialidad = idEspecialidad
                });

                // modal éxito
                ScriptManager.RegisterStartupScript(this, GetType(), "modalExito",
                    "var m = new bootstrap.Modal(document.getElementById('modalExito')); m.show();", true);
            }
            catch (Exception ex)
            {
                mensajeError = "Error al registrar el profesional: " + ex.Message;
                mensajeError = mensajeError.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");

                ScriptManager.RegisterStartupScript(this, GetType(), "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{mensajeError}';",
                    true);
            }
        }



        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarProfesionales.aspx");
        }

        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // No hace nada, solo cierra el modal
        }
    }
}
