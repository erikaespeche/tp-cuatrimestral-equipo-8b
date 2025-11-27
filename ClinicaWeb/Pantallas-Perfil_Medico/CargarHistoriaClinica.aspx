<%@ Page Title="Crear Historia Clínica" Language="C#" MasterPageFile="~/PerfilMedico.Master"
    AutoEventWireup="true" CodeBehind="CargarHistoriaClinica.aspx.cs"
    Inherits="ClinicaWeb.Pantallas_Perfil_Medico.CargarHistoriaClinica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<link rel="stylesheet" href="Styles/PerfilMedicosCSS.css" />

<div id="pantalla-cargar-historia" class="hc-container">

    <h2 class="hc-titulo">Crear Historia Clínica</h2>
    <asp:HiddenField ID="hfIdPaciente" runat="server" />

    <!-- ===========================
         DATOS DEL PACIENTE (SOLO LECTURA)
         ============================ -->
    <div class="seccion">
        <h3>Datos del Paciente</h3>

        <div class="fila-f1">
            <div>
                <label>Nombre y Apellido</label>
                <span id="lblNombre" runat="server" class="dato-valor"></span>
            </div>
            <div>
                <label>DNI</label>
                <span id="lblDni" runat="server" class="dato-valor"></span>
            </div>
            <div>
                <label>Fecha de Nacimiento</label>
                <span id="lblFechaNac" runat="server" class="dato-valor"></span>
            </div>
            <div>
                <label>Grupo Sanguíneo</label>
                <span id="lblGrupo" runat="server" class="dato-valor"></span>
            </div>
        </div>

        <div class="fila-f2">
            <div>
                <label>Peso (kg)</label>
                <span id="lblPeso" runat="server" class="dato-valor"></span>
            </div>
            <div>
                <label>Altura (cm)</label>
                <span id="lblAltura" runat="server" class="dato-valor"></span>
            </div>
            <div>
                <label>Enfermedades Crónicas</label>
                <span id="lblCronicas" runat="server" class="dato-valor"></span>
            </div>
        </div>

        <div class="fila-f3">
            <label>Alergias</label>
            <span id="lblAlergias" runat="server" class="dato-valor"></span>
        </div>

        <div class="fila-f4">
            <label>Patologías</label>
            <span id="lblPatologias" runat="server" class="dato-valor"></span>
        </div>
    </div>

    <!-- ===========================
         CAMPOS EDITABLES DE LA CONSULTA
         ============================ -->
    <div class="seccion">
        <h3>Datos de la Consulta</h3>

        <div class="consulta-grid">
            <div>
                <label>Observaciones generales:</label>
                <textarea id="txtObservaciones" runat="server" class="input-textarea"
                    placeholder="Ingrese notas generales de la consulta..."></textarea>
            </div>
            <div>
                <label>Diagnóstico:</label>
                <textarea id="txtDiagnostico" runat="server" class="input-textarea"
                    placeholder="Ingrese el diagnóstico del profesional..."></textarea>
            </div>
        </div>

        <div class="consulta-grid">
            <div>
                <label>Tratamientos realizados:</label>
                <textarea id="txtTratamientos" runat="server" class="input-textarea"
                    placeholder="Detalle los procedimientos o tratamientos administrados..."></textarea>
            </div>
            <div>
                <label>Próximos pasos y recomendaciones:</label>
                <textarea id="txtProximosPasos" runat="server" class="input-textarea"
                    placeholder="Ingrese instrucciones futuras o recomendaciones..."></textarea>
            </div>
        </div>
    </div>

    <!-- ===========================
         ARCHIVOS ADJUNTOS
         ============================ -->
    <div class="seccion">
        <h3>Archivos Adjuntos</h3>

        <div class="file-drop" id="fileDrop">
            <p>Haga clic para cargar o arrastre y suelte archivos aquí</p>
            <asp:FileUpload ID="fileAdjuntos" runat="server" AllowMultiple="true" CssClass="file-input" />
        </div>

        <ul class="lista-archivos" id="listaArchivos"></ul>
    </div>

    <!-- ===========================
         BOTONES
         ============================ -->
    <div class="botones">
        <asp:Button ID="btnCancelar" runat="server" CssClass="btn-cancelar"
            Text="Cancelar" OnClick="btnCancelar_Click" />
        <asp:Button ID="btnGuardar" runat="server" CssClass="btn-guardar"
            Text="Guardar Historia Clínica" OnClick="btnGuardar_Click" />
    </div>

</div>

<script>
    const fileDrop = document.getElementById('fileDrop');
    const fileInput = document.getElementById('<%=fileAdjuntos.ClientID%>');
    const listaArchivos = document.getElementById('listaArchivos');

    // Abrir file input al hacer clic
    fileDrop.addEventListener('click', () => fileInput.click());

    // Mostrar archivos seleccionados
    fileInput.addEventListener('change', () => {
        listaArchivos.innerHTML = '';
        Array.from(fileInput.files).forEach(file => {
            const li = document.createElement('li');
            li.textContent = file.name;
            listaArchivos.appendChild(li);
        });
    });

    // Arrastrar y soltar archivos
    fileDrop.addEventListener('dragover', e => {
        e.preventDefault();
        fileDrop.style.borderColor = '#0d7ff2';
    });

    fileDrop.addEventListener('dragleave', e => {
        e.preventDefault();
        fileDrop.style.borderColor = '#314d68';
    });

    fileDrop.addEventListener('drop', e => {
        e.preventDefault();
        fileInput.files = e.dataTransfer.files;

        // Mostrar archivos
        listaArchivos.innerHTML = '';
        Array.from(fileInput.files).forEach(file => {
            const li = document.createElement('li');
            li.textContent = file.name;
            listaArchivos.appendChild(li);
        });
    });
</script>

</asp:Content>