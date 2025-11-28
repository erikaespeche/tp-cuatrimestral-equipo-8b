using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using negocio;
using dominio;




namespace Clinic.Pantallas_Perfil_Recepcionista
{
    public partial class ConfigurarAgendaProfesional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProfesionales();
                CargarEspecialidades();

                ddlDuracion.AutoPostBack = true;

                // Evita FormatException si la duración no existe
                if (ddlDuracion.Items.FindByValue("30") != null)
                    ddlDuracion.SelectedValue = "30";
                else
                    ddlDuracion.SelectedIndex = 0;

                GenerarTabla(int.Parse(ddlDuracion.SelectedValue));

                DeshabilitarFechasPasadas();
                ActivarClickEnCeldas();
            }
        }

        // ===============================================
        // -------- CARGA DE PROFESIONALES / ESPEC. -------
        // ===============================================

        private void CargarProfesionales()
        {
            MedicoNegocio negocio = new MedicoNegocio();
            ddlProfesional.DataSource = negocio.Listar();
            ddlProfesional.DataTextField = "NombreCompleto";
            ddlProfesional.DataValueField = "IdMedico";
            ddlProfesional.DataBind();

            ddlProfesional.Items.Insert(0, new ListItem("Seleccione un profesional", ""));
        }

        protected void ddlProfesional_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarTabla(int.Parse(ddlDuracion.SelectedValue));
            ActivarClickEnCeldas();
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            ddlEspecialidad.DataSource = negocio.Listar();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una especialidad", ""));
        }

        // ===============================================
        // ----------- DESHABILITAR FECHAS PASADAS --------
        // ===============================================

        private void DeshabilitarFechasPasadas()
        {
            string hoy = DateTime.Now.ToString("yyyy-MM-dd");
            txtDesde.Attributes["min"] = hoy;
            txtHasta.Attributes["min"] = hoy;
        }

        // ===============================================
        // -------- EVENTO CAMBIO DE DURACIÓN -------------
        // ===============================================

        protected void ddlDuracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarTabla(int.Parse(ddlDuracion.SelectedValue));
            ActivarClickEnCeldas();
        }

        // ===============================================
        // -------- GENERACIÓN DE TABLA DINÁMICA ----------
        // ===============================================

        private void GenerarTabla(int duracionMinutos)
        {
            hfSeleccion.Value = "";   // ← LIMPIA SELECCIÓN
            tbodyAgenda.Controls.Clear();

            TimeSpan hora = new TimeSpan(6, 0, 0);
            TimeSpan fin = new TimeSpan(23, 0, 0);
            TimeSpan paso = TimeSpan.FromMinutes(duracionMinutos);

            while (hora <= fin)
            {
                HtmlTableRow fila = new HtmlTableRow();

                HtmlTableCell celHora = new HtmlTableCell();
                celHora.InnerText = hora.ToString(@"hh\:mm");
                celHora.Attributes["class"] = "agp-hora-cell";
                fila.Cells.Add(celHora);

                for (int dia = 1; dia <= 7; dia++)
                {
                    HtmlTableCell cel = new HtmlTableCell();
                    cel.Attributes["class"] = "agp-celda";
                    cel.Attributes["data-dia"] = dia.ToString();
                    cel.Attributes["data-hora"] = hora.ToString(@"hh\:mm");
                    fila.Cells.Add(cel);
                }

                tbodyAgenda.Controls.Add(fila);
                hora = hora.Add(paso);
            }
        }


        // ===============================================
        // ------------------- GUARDAR --------------------
        // ===============================================

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlProfesional.SelectedValue) ||
                string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
            {
                string mensaje = "Debe seleccionar profesional y especialidad.";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalSeleccion",
                    "document.getElementById('modalSeleccionMensaje').innerText = `" + mensaje + @"`;" +
                    "var myModal = new bootstrap.Modal(document.getElementById('modalSeleccionIncompleta'));" +
                    "myModal.show();",
                    true
                );

                return;
            }


            // Se arman las disponibilidades nuevas
            List<Disponibilidad> disponibilidades = ObtenerCeldasSeleccionadas();

            AgendaNegocio negocio = new AgendaNegocio();

            // 🔥 VALIDACIÓN DE AGENDA DUPLICADA

            bool existe = negocio.ExisteAgendaIgual(
              int.Parse(ddlProfesional.SelectedValue),
              int.Parse(ddlEspecialidad.SelectedValue),
              DateTime.Parse(txtDesde.Text),
              DateTime.Parse(txtHasta.Text),
              disponibilidades
            );

            if (existe)
            {
                string mensaje = "Ya existe una agenda idéntica para este profesional con la misma especialidad, días y horarios.";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "modalDuplicado",
                    "document.getElementById('modalErrorMensaje').innerText = `" + mensaje + @"`;
                     var myModal = new bootstrap.Modal(document.getElementById('modalError'));
                     myModal.show();",
                    true
                );

                return;
            }



            // ------------------------
            // SI NO ESTÁ DUPLICADA, SE CREA
            // ------------------------

            Agenda agenda = new Agenda
            {
                IdMedico = int.Parse(ddlProfesional.SelectedValue),
                IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue),
                DuracionTurno = int.Parse(ddlDuracion.SelectedValue),
                PacientesPorTurno = int.Parse(txtPacientes.Text),
                FechaDesde = DateTime.Parse(txtDesde.Text),
                FechaHasta = DateTime.Parse(txtHasta.Text),
                Disponibilidades = disponibilidades
            };

            int id = negocio.CrearAgenda(agenda);
            negocio.GuardarDisponibilidad(id, agenda.Disponibilidades);

            // Modal OK
            ScriptManager.RegisterStartupScript(this, GetType(), "ok", @"
             var myModal = new bootstrap.Modal(document.getElementById('modalOk'));
              myModal.show();
              ", true);
        }


        // ===============================================
        // ----- RECOLECTAR CELDAS SELECCIONADAS ----------
        // ===============================================

        private List<Disponibilidad> ObtenerCeldasSeleccionadas()
        {
            List<Disponibilidad> lista = new List<Disponibilidad>();

            if (string.IsNullOrEmpty(hfSeleccion.Value))
                return lista;

            string[] items = hfSeleccion.Value.Split(';');

            foreach (string item in items)
            {
                string[] partes = item.Split('-');

                lista.Add(new Disponibilidad
                {
                    DiaSemana = int.Parse(partes[0]),
                    Hora = TimeSpan.Parse(partes[1])
                });
            }

            return lista;
        }

        // ===============================================
        // -------- REACTIVAR JS TRAS POSTBACK -------------
        // ===============================================

        private void ActivarClickEnCeldas()
        {
            string script = @"
document.addEventListener('click', function(e){
    if(e.target.classList.contains('agp-celda')){
        e.target.classList.toggle('active');
        
        let dia = e.target.getAttribute('data-dia');
        let hora = e.target.getAttribute('data-hora');
        let key = dia + '-' + hora;

        let hidden = document.getElementById('" + hfSeleccion.ClientID + @"');
        let lista = hidden.value ? hidden.value.split(';') : [];

        if(e.target.classList.contains('active')){
            if(!lista.includes(key)) lista.push(key);
        } else {
            lista = lista.filter(x => x !== key);
        }

        hidden.value = lista.join(';');
    }
});
";

            ScriptManager.RegisterStartupScript(this, GetType(), "clickAgendas", script, true);
        }
    }
}

