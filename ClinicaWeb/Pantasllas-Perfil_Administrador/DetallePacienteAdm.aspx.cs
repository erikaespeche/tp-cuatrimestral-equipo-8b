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
    public partial class DetallePacienteAdm : System.Web.UI.Page
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
                    Response.Redirect("ListarPacienteAdm.aspx");
                    return;
                }

                // 2. Busco el paciente
                PacienteNegocio negocio = new PacienteNegocio();
                var paciente = negocio.BuscarPorDni(dni);

                if (paciente == null)
                {
                    Response.Redirect("ListarPacienteAdm.aspx");
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
                    int idHistoriaClinica = Convert.ToInt32(e.CommandArgument);
                    // REDIRIGIR A PAG DETALLE -> HACERLA
                    Response.Redirect("DetalleConsulta.aspx?id=" + idHistoriaClinica);
                }
            }


            protected void btnAceptarExito_Click(object sender, EventArgs e)
            {
                string dni = ViewState["dni"] as string;
                if (!string.IsNullOrEmpty(dni))
                    Response.Redirect("DetallePacienteAdm.aspx?dni=" + dni);
                else
                    Response.Redirect("ListarPacienteAdm.aspx?error=SinDNI");
            }




            protected void btnAceptarError_Click(object sender, EventArgs e)
            {
                // Cierra el modal. Bootstrap ya lo maneja.
            }


        }
    }