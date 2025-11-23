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
                CargarDatosPaciente();
            }
        }

        private void CargarDatosPaciente()
        {
            int idPaciente = int.Parse(Request.QueryString["IdPaciente"] ?? "0");
            if (idPaciente == 0) return;

            string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT Nombres, Apellidos, DniPaciente, FechaNacimiento, ObraSocial,
                   GrupoSanguineo, Peso, Altura, EnfermedadesCronicas, Alergias, Patologias
            FROM PACIENTES
            WHERE IdPaciente = @IdPaciente", conexion);

                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblNombre.InnerText = dr["Nombres"].ToString() + " " + dr["Apellidos"].ToString();
                    lblDni.InnerText = dr["DniPaciente"].ToString();
                    lblFechaNac.InnerText = Convert.ToDateTime(dr["FechaNacimiento"]).ToString("dd/MM/yyyy");
                    lblGrupo.InnerText = dr["GrupoSanguineo"].ToString();
                    lblPeso.InnerText = dr["Peso"].ToString();
                    lblAltura.InnerText = dr["Altura"].ToString();
                    lblCronicas.InnerText = dr["EnfermedadesCronicas"].ToString();
                    lblAlergias.InnerText = dr["Alergias"].ToString();
                    lblPatologias.InnerText = dr["Patologias"].ToString();
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
                int idPaciente = int.Parse(Request.QueryString["IdPaciente"] ?? "0");
                if (idPaciente == 0)
                {
                    Response.Write("<script>alert('Paciente no válido');</script>");
                    return;
                }

                int idMedico = 1; // Aquí podés tomarlo del login del médico

                // ===========================
                // GUARDAR ARCHIVOS ADJUNTOS
                // ===========================
                string archivosConcatenados = null;
                string rutaBase = Server.MapPath("~/Uploads/HistoriasClinicas/");
                if (!Directory.Exists(rutaBase))
                    Directory.CreateDirectory(rutaBase);

                if (fileAdjuntos.HasFiles)
                {
                    List<string> nombresArchivos = new List<string>();
                    foreach (var file in fileAdjuntos.PostedFiles)
                    {
                        string path = Path.Combine(rutaBase, file.FileName);
                        file.SaveAs(path);
                        nombresArchivos.Add(file.FileName);
                    }
                    archivosConcatenados = string.Join(";", nombresArchivos);
                }

                // ===========================
                // CREAR OBJETO HISTORIA CLÍNICA
                // ===========================
                HistoriaClinica hc = new HistoriaClinica
                {
                    IdPaciente = idPaciente,
                    IdMedico = idMedico,
                    FechaConsulta = DateTime.Now,
                    Observaciones = txtObservaciones.Value,
                    Diagnostico = txtDiagnostico.Value,
                    Tratamientos = txtTratamientos.Value,
                    ProximosPasos = txtProximosPasos.Value,
                    ArchivosAdjuntos = archivosConcatenados
                };

                // ===========================
                // GUARDAR EN BASE DE DATOS CON ACCESODATOS
                // ===========================
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta(@"
                INSERT INTO HistoriaClinica
                (IdPaciente, IdMedico, FechaConsulta, Observaciones, Diagnostico, Tratamientos, ProximosPasos, ArchivosAdjuntos)
                VALUES
                (@IdPaciente, @IdMedico, @FechaConsulta, @Observaciones, @Diagnostico, @Tratamientos, @ProximosPasos, @ArchivosAdjuntos)
            ");

                    datos.setearParametro("@IdPaciente", hc.IdPaciente);
                    datos.setearParametro("@IdMedico", hc.IdMedico);
                    datos.setearParametro("@FechaConsulta", hc.FechaConsulta);
                    datos.setearParametro("@Observaciones", hc.Observaciones);
                    datos.setearParametro("@Diagnostico", hc.Diagnostico);
                    datos.setearParametro("@Tratamientos", hc.Tratamientos);
                    datos.setearParametro("@ProximosPasos", hc.ProximosPasos);
                    datos.setearParametro("@ArchivosAdjuntos", hc.ArchivosAdjuntos);

                    datos.ejecutarAccion();
                }
                finally
                {
                    datos.cerrarConexion();
                }

                Response.Redirect("PacientesEnSala.aspx");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al guardar la historia clínica: {ex.Message}');</script>");
            }
        }
    }
}
