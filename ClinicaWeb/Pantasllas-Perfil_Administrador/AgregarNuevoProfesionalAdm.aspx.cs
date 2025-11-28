using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaWeb.Pantasllas_Perfil_Administrador
{
    public partial class AgregarNuevoProfesionalAdm : System.Web.UI.Page
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
            Response.Redirect("GestionarProfesionalesAdm.aspx");
        }

        protected void btnRegistrarProfesional_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreProfesional.Value.Trim();
            string apellido = txtApellidoProfesional.Value.Trim();
            string dni = txtDniProfesional.Value.Trim();
            string email = txtEmailProfesional.Value.Trim();
            string telefono = txtTelefonoProfesional.Value.Trim();
            int idTurno = int.Parse(ddlTurnoTrabajo.SelectedValue);

            // uso la clase validador 
            bool valido = true;
            string mensajeError = "";

            // nombre
            var valNombre = Validador.ValidarNombre(nombre);
            if (!valNombre.esValido) { mensajeError += valNombre.mensaje + "<br>"; valido = false; }

            // apellido
            var valApellido = Validador.ValidarApellido(apellido);
            if (!valApellido.esValido) { mensajeError += valApellido.mensaje + "<br>"; valido = false; }

            //  DNI
            var valDni = Validador.ValidarDNI(dni);
            if (!valDni.esValido) { mensajeError += valDni.mensaje + "<br>"; valido = false; }

            //  email
            if (!string.IsNullOrWhiteSpace(email))
            {
                var valEmail = Validador.ValidarEmail(email);
                if (!valEmail.esValido) { mensajeError += valEmail.mensaje + "<br>"; valido = false; }
            }

            //  telefono
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                var valTel = Validador.ValidarTelefono(telefono);
                if (!valTel.esValido) { mensajeError += valTel.mensaje + "<br>"; valido = false; }
            }

            // turno
            if (idTurno == 0)
            {
                mensajeError += "Debe seleccionar un turno.<br>";
                valido = false;
            }

            // especialidades
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
                dominio.Medico nuevoProfesional = new dominio.Medico
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Dni = dni,
                    Email = email,
                    Telefono = telefono,
                    IdTurnoTrabajo = idTurno,
                    Especialidades = new List<Especialidad>()
                };

                string nombreEspecialidad = ddlEspecialidad.SelectedItem.Text;

                nuevoProfesional.Especialidades.Add(new Especialidad
                {
                    IdEspecialidad = idEspecialidad,
                    Nombre = nombreEspecialidad
                });


                negocio.Agregar(nuevoProfesional);

                // Mostrar modal de éxito
                ScriptManager.RegisterStartupScript(this, GetType(), "modalExito",
                    "var m = new bootstrap.Modal(document.getElementById('modalExito')); m.show();", true);
            }
            catch (Exception ex)
            {
                mensajeError = "Error al registrar el profesional: " + ex.Message;
                mensajeError = mensajeError.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
                ScriptManager.RegisterStartupScript(this, GetType(), "modalError",
                    $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{mensajeError}';", true);
            }
        }

        protected void btnAceptarExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionarProfesionalesAdm.aspx");
        }

        protected void btnAceptarError_Click(object sender, EventArgs e)
        {
            // No hace nada, solo cierra el modal
        }
    }
}
