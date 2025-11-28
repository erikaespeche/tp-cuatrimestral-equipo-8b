using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic.Pantallas_Perfil_Medico
{
    public partial class DetallePaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPaciente();

            }
        }

        private void CargarPaciente()
        {
            // 1. Leo el DNI desde la URL
            string dni = Request.QueryString["dni"];

            if (string.IsNullOrEmpty(dni))
            {
                Response.Redirect("ListarPaciente.aspx");
                return;
            }

            // 2. Busco el paciente
            PacienteNegocio negocio = new PacienteNegocio();
            var paciente = negocio.BuscarPorDni(dni); 

            if (paciente == null)
            {
                Response.Redirect("ListarPaciente.aspx");
                return;
            }

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

            CargarHistoriaClinica(paciente.IdPaciente);
        }





        // ======================== BOTÓN GUARDAR ========================
        //protected void btnGuardarCambios_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // 1) VALIDAR ASP.NET
        //        if (!Page.IsValid)
        //        {
        //            ScriptManager.RegisterStartupScript(
        //                this, this.GetType(),
        //                "abrirModal",
        //                "abrirModalEditar();",
        //                true
        //            );
        //            return;
        //        }

        //        PacienteNegocio negocio = new PacienteNegocio();

        //        string dniQuery = Request.QueryString["dni"];
        //        var pacienteOriginal = negocio.BuscarPorDni(dniQuery);

        //        if (pacienteOriginal == null)
        //        {
        //            Response.Redirect("ListarPaciente.aspx?error=PacienteNoEncontrado");
        //            return;
        //        }

        //        // =============================
        //        // 2) CREAR OBJETO PACIENTE A MODIFICAR
        //        // =============================
        //        Paciente p = pacienteOriginal;   // <-- CLAVE: partimos del original

        //        // 3) Asignar campos editados
        //        p.Nombres = txtNombreEdit.Text.Trim();
        //        p.Apellidos = txtApellidoEdit.Text.Trim();
        //        p.Email = txtMailEdit.Text.Trim();
        //        p.Celular = txtCelEdit.Text.Trim();
        //        p.Telefono = txtTelEdit.Text.Trim();
        //        p.Direccion = txtDirEdit.Text.Trim();
        //        p.Ciudad = txtCiudadEdit.Text.Trim();
        //        p.Provincia = txtProvEdit.Text.Trim();
        //        p.CodigoPostal = txtCpEdit.Text.Trim();
        //        p.ObraSocial = txtObraEdit.Text.Trim();
        //        p.NumeroObraSocial = txtNumObraEdit.Text.Trim();

        //        if (int.TryParse(txtDniEdit.Text, out int dniParsed))
        //            p.DniPaciente = dniParsed;

        //        if (DateTime.TryParse(txtFechaEdit.Text, out DateTime fechaN))
        //            p.FechaNacimiento = fechaN;

        //        if (!string.IsNullOrEmpty(ddlSexoEdit.SelectedValue))
        //            p.Sexo = char.Parse(ddlSexoEdit.SelectedValue);

        //        // =============================
        //        // 4) GUARDAR CAMBIOS
        //        // =============================
        //        negocio.Modificar(p);

        //        // =============================
        //        // 5) MOSTRAR MODAL DE ÉXITO
        //        // =============================
        //        ScriptManager.RegisterStartupScript(
        //          this,
        //          GetType(),
        //          "ShowSuccessModal",
        //          "$('#modalExito').modal('show');",
        //           true
        //        );

        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMsg = "Error al guardar el paciente: " + ex.Message;
        //        errorMsg = errorMsg.Replace("'", "\\'").Replace("\r", "").Replace("\n", "<br>");
        //        ScriptManager.RegisterStartupScript(
        //         this,
        //         GetType(),
        //         "modalError",
        //         $"var m = new bootstrap.Modal(document.getElementById('modalError')); m.show(); document.getElementById('modalErrorBody').innerHTML = '{errorMsg}';",
        //         true
        //        );
        //    }
        //}

        private void CargarHistoriaClinica(int idPaciente)
        {
            HistoriaClinicaNegocio hcNeg = new HistoriaClinicaNegocio();
            var listaHC = hcNeg.ListarPorPaciente(idPaciente);

            if (listaHC.Count > 0)
            {
                var hc = listaHC[0]; // tomamos la última consulta
                lblGrupoSanguineo.Text = hc.GrupoFactorSanguineo;
                lblPeso.Text = hc.Peso + " kg";
                lblAltura.Text = hc.Altura + " m";
                lblAlergias.Text = hc.Alergias;
                lblEnfermedadesCronicas.Text = hc.EnfermedadesCronicas;
                lblPatologias.Text = hc.Patologias;
            }

            rptHistoriaClinica.DataSource = listaHC;
            rptHistoriaClinica.DataBind();
        }

        protected void rptHistoriaClinica_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerConsulta")
            {
                int idHC = Convert.ToInt32(e.CommandArgument);

                HistoriaClinicaNegocio negocio = new HistoriaClinicaNegocio();
                var consulta = negocio.BuscarPorId(idHC);

                if (consulta != null)
                {
                    lblVerFecha.Text = consulta.FechaConsulta.ToString("dd/MM/yyyy");
                    lblVerMedico.Text = consulta.NombreMedico;
                    lblVerEspecialidad.Text = consulta.NombreEspecialidad;

                    lblVerObservaciones.Text = consulta.Observaciones;
                    lblVerDiagnostico.Text = consulta.Diagnostico;
                    lblVerTratamientos.Text = consulta.Tratamientos;
                    lblVerRecomendaciones.Text = consulta.ProximosPasos;

                    // ABRIR MODAL
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "abrirModalConsulta",
                        "$('#modalVerConsulta').modal('show');",
                        true
                    );
                }
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

        public HistoriaClinica BuscarPorId(int id)
        {
            HistoriaClinica hc = null;

            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(@"
            SELECT HC.IdHistoriaClinica,
                   HC.FechaConsulta,
                   M.Nombre + ' ' + M.Apellido AS NombreMedico,
                   E.NombreEspecialidad,
                   HC.Observaciones,
                   HC.Diagnostico,
                   HC.Tratamientos,
                   HC.ProximosPasos
            FROM HISTORIA_CLINICA HC
            INNER JOIN MEDICOS M ON M.IdMedico = HC.IdMedico
            INNER JOIN ESPECIALIDADES E ON E.IdEspecialidad = HC.IdEspecialidad
            WHERE HC.IdHistoriaClinica = @id
        ");

                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    hc = new HistoriaClinica
                    {
                        IdHistoriaClinica = id,
                        FechaConsulta = (DateTime)datos.Lector["FechaConsulta"],
                        NombreMedico = datos.Lector["NombreMedico"].ToString(),
                        NombreEspecialidad = datos.Lector["NombreEspecialidad"].ToString(),
                        Observaciones = datos.Lector["Observaciones"].ToString(),
                        Diagnostico = datos.Lector["Diagnostico"].ToString(),
                        Tratamientos = datos.Lector["Tratamientos"].ToString(),
                        ProximosPasos = datos.Lector["ProximosPasos"].ToString()
                    };
                }

                return hc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}