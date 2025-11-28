using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace ClinicaWeb.Pantallas_Perfil_Medico
{
    public partial class CargarHistoriaClinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dni = Request.QueryString["dni"];
                if (string.IsNullOrEmpty(dni)) return;

                // 1) Traigo al paciente
                PacienteNegocio pacienteNeg = new PacienteNegocio();
                Paciente paciente = pacienteNeg.BuscarPorDni(dni);

                if (paciente != null)
                {
                    lblNombre.InnerText = paciente.Nombres + " " + paciente.Apellidos;
                    lblDni.InnerText = paciente.DniPaciente.ToString();
                    lblFechaNac.InnerText = paciente.FechaNacimiento.ToString("dd/MM/yyyy");

                    hfIdPaciente.Value = paciente.IdPaciente.ToString();
                }

                // 2) Traigo la última historia clínica
                HistoriaClinicaNegocio hcNeg = new HistoriaClinicaNegocio();
                var listaHC = hcNeg.ListarPorPaciente(paciente.IdPaciente);
                

                if (listaHC.Count > 0)
                {
                    var hc = listaHC[0]; // el registro más reciente
                    txtGrupo.Value = hc.GrupoFactorSanguineo.ToString();
                    txtPeso.Value = hc.Peso.ToString();
                    txtAltura.Value = hc.Altura.ToString("0.##");
                    txtCronicas.Value = hc.EnfermedadesCronicas;
                    txtAlergias.Value = hc.Alergias;
                    txtPatologias.Value = hc.Patologias;
                }
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PacienteEnSala.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idPaciente = int.Parse(hfIdPaciente.Value ?? "0");
                if (idPaciente == 0)
                {
                    Response.Write("<script>alert('Paciente no válido');</script>");
                    return;
                }

                int idMedico = 1;

                // 1) Traigo la última historia clínica
                HistoriaClinicaNegocio hcNeg = new HistoriaClinicaNegocio();
                var listaHC = hcNeg.ListarPorPaciente(idPaciente);

                string observaciones = txtObservaciones.Value;
                string diagnostico = txtDiagnostico.Value;
                string tratamientos = txtTratamientos.Value;
                string proximosPasos = txtProximosPasos.Value;

                if (listaHC.Count > 0)
                {
                    var ultimaHC = listaHC[0];

                    if (string.IsNullOrWhiteSpace(observaciones))
                        observaciones = ultimaHC.Observaciones;

                    if (string.IsNullOrWhiteSpace(diagnostico))
                        diagnostico = ultimaHC.Diagnostico;

                    if (string.IsNullOrWhiteSpace(tratamientos))
                        tratamientos = ultimaHC.Tratamientos;

                    if (string.IsNullOrWhiteSpace(proximosPasos))
                        proximosPasos = ultimaHC.ProximosPasos;
                }

                // 2) Guardar nueva historia clínica
                HistoriaClinica hc = new HistoriaClinica
                {
                    IdPaciente = idPaciente,
                    IdMedico = idMedico,
                    FechaConsulta = DateTime.Now,
                    Observaciones = observaciones,
                    Diagnostico = diagnostico,
                    Tratamientos = tratamientos,
                    ProximosPasos = proximosPasos,
                    ArchivosAdjuntos = GuardarArchivos(),

                    GrupoFactorSanguineo = txtGrupo.Value, 
                    Peso = decimal.Parse(txtPeso.Value),
                    Altura = decimal.Parse(txtAltura.Value, System.Globalization.CultureInfo.InvariantCulture),
                    Alergias = txtAlergias.Value,
                    EnfermedadesCronicas = txtCronicas.Value,
                    Patologias = txtPatologias.Value,
                };

                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta(@"
                                            INSERT INTO HistoriaClinica
                                            (IdPaciente, IdMedico, FechaConsulta, Observaciones, Diagnostico, Tratamientos, ProximosPasos, ArchivosAdjuntos,
                                             GrupoFactorSanguineo, Peso, Altura, Alergias, EnfermedadesCronicas, Patologias)
                                            VALUES
                                            (@IdPaciente, @IdMedico, @FechaConsulta, @Observaciones, @Diagnostico, @Tratamientos, @ProximosPasos, @ArchivosAdjuntos,
                                             @GrupoFactorSanguineo, @Peso, @Altura, @Alergias, @EnfermedadesCronicas, @Patologias)
                                        ");

                    datos.setearParametro("@IdPaciente", hc.IdPaciente);
                    datos.setearParametro("@IdMedico", hc.IdMedico);
                    datos.setearParametro("@FechaConsulta", hc.FechaConsulta);
                    datos.setearParametro("@Observaciones", hc.Observaciones);
                    datos.setearParametro("@Diagnostico", hc.Diagnostico);
                    datos.setearParametro("@Tratamientos", hc.Tratamientos);
                    datos.setearParametro("@ProximosPasos", hc.ProximosPasos);
                    datos.setearParametro("@ArchivosAdjuntos", hc.ArchivosAdjuntos);

                    datos.setearParametro("@GrupoFactorSanguineo", hc.GrupoFactorSanguineo);
                    datos.setearParametro("@Peso", hc.Peso);
                    datos.setearParametro("@Alergias", hc.Alergias);
                    datos.setearParametro("@EnfermedadesCronicas", hc.EnfermedadesCronicas);
                    datos.setearParametro("@Patologias", hc.Patologias);
                    datos.setearParametro("@Altura", hc.Altura);

                    datos.ejecutarAccion();
                }
                finally
                {
                    datos.cerrarConexion();
                }

                Response.Redirect("PacienteEnSala.aspx");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al guardar la historia clínica: {ex.Message}');</script>");
            }
        }

        private string GuardarArchivos()
        {
            if (!fileAdjuntos.HasFiles) return null;

            string rutaBase = Server.MapPath("~/Uploads/HistoriasClinicas/");
            if (!Directory.Exists(rutaBase))
                Directory.CreateDirectory(rutaBase);

            List<string> nombresArchivos = new List<string>();
            foreach (var file in fileAdjuntos.PostedFiles)
            {
                string path = Path.Combine(rutaBase, file.FileName);
                file.SaveAs(path);
                nombresArchivos.Add(file.FileName);
            }

            return string.Join(";", nombresArchivos);
        }

    }
}
