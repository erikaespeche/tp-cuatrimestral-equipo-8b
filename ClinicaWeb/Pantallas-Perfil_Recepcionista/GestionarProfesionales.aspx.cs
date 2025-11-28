using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class GestionarProfesionales : System.Web.UI.Page
    {
        MedicoNegocio negocio = new MedicoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarProfesionales();
                CargarEspecialidadesEditar();
            }
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio espNegocio = new EspecialidadNegocio();
            ddlEspecialidad.DataSource = espNegocio.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Filtrar por Especialidad", "0"));
        }
        private void CargarEspecialidadesEditar()
        {
            EspecialidadNegocio espNeg = new EspecialidadNegocio();
            editEspecialidades.DataSource = espNeg.Listar();
            editEspecialidades.DataTextField = "Nombre";
            editEspecialidades.DataValueField = "IdEspecialidad";
            editEspecialidades.DataBind();
        }


        private void CargarProfesionales()
        {
            repProfesionales.DataSource = negocio.Listar();
            repProfesionales.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var lista = negocio.Listar();
            string nom = Normalizar(txtNombre.Text);
            string ape = Normalizar(txtApellido.Text);


            int idEsp = int.Parse(ddlEspecialidad.SelectedValue);

            var filtrado = lista.Where(p =>
                Normalizar(p.Nombre).Contains(nom) &&
                Normalizar(p.Apellido).Contains(ape) &&
                (idEsp == 0 || p.Especialidades.Any(espI => espI.IdEspecialidad == idEsp))
            ).ToList();


            repProfesionales.DataSource = filtrado;
            repProfesionales.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarNuevoProfesional.aspx");
        }

        private string Normalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            string s = texto.ToLower();

            s = s.Replace("á", "a").Replace("é", "e").Replace("í", "i")
                 .Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n");

            return s.Trim();
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            int id = int.Parse(editIdMedico.Value);
            Medico medico = negocio.BuscarPorId(id);

            if (medico != null)
            {
                medico.Nombre = editNombre.Value;
                medico.Apellido = editApellido.Value;
                medico.Dni = editDni.Value;
                medico.Telefono = editTelefono.Value;
                medico.Email = editEmail.Value;

                // Limpiar especialidades previas
                medico.Especialidades = new List<Especialidad>();

                // Agregar las seleccionadas
                foreach (ListItem item in editEspecialidades.Items)
                {
                    if (item.Selected)
                    {
                        medico.Especialidades.Add(new Especialidad
                        {
                            IdEspecialidad = int.Parse(item.Value),
                            Nombre = item.Text
                        });
                    }
                }

                negocio.Modificar(medico);
                CargarProfesionales();
            }
        }


        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idMedico = int.Parse(eliminarIdMedico.Value);

            MedicoNegocio negocio = new MedicoNegocio();
            negocio.DarDeBaja(idMedico);

            // Recargar repeater
            CargarProfesionales();
        }


    }

}