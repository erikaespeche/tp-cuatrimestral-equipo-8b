CREATE DATABASE CLINICA_DB;
GO

USE CLINICA_DB;
GO

--DROP DATABASE CLINICA_DB;


CREATE TABLE [dbo].[PACIENTES] (
    [IdPaciente] INT IDENTITY(1,1) NOT NULL,
    [TipoDocumento] VARCHAR(20) NOT NULL,   -- DNI / PASAPORTE / OTRO
    [DniPaciente] INT NOT NULL,
    [Nombres] VARCHAR(50) NOT NULL,
    [Apellidos] VARCHAR(50) NOT NULL,
    [FechaNacimiento] DATE NOT NULL,
    [Sexo] CHAR(1) NOT NULL,                -- F / M / O
    [Email] VARCHAR(100) NOT NULL,
    [Telefono] VARCHAR(20) NULL,
    [Celular] VARCHAR(20) NULL,
    [Direccion] VARCHAR(100) NOT NULL,
    [Ciudad] VARCHAR(100) NOT NULL,
    [Provincia] VARCHAR(100) NOT NULL,
    [CodigoPostal] VARCHAR(10) NOT NULL,
    [ObraSocial] VARCHAR(40) NULL,
    [NumeroObraSocial] VARCHAR(40) NULL,

    CONSTRAINT PK_PACIENTES PRIMARY KEY (IdPaciente),
    CONSTRAINT UQ_PACIENTES_DNI UNIQUE (DniPaciente),
    CONSTRAINT CK_PACIENTES_SEXO CHECK (Sexo IN ('F', 'M', 'O')),
    CONSTRAINT CK_PACIENTES_TIPODOC CHECK (TipoDocumento IN ('DNI', 'PASAPORTE', 'OTRO'))
);


GO


CREATE TABLE [dbo].[ESPECIALIDAD](
    [IdEspecialidad] INT IDENTITY(1,1) NOT NULL,
    [Nombre] VARCHAR(50) NOT NULL,
    [Descripcion] VARCHAR(200) NOT NULL,

    CONSTRAINT PK_ESPECIALIDAD PRIMARY KEY (IdEspecialidad ASC)
);
GO



CREATE TABLE [dbo].[MEDICO](
    [IdMedico] INT IDENTITY(1,1) NOT NULL,
    [Nombre] VARCHAR(50) NOT NULL,
    [Apellido] VARCHAR(50) NOT NULL,
    [Dni] VARCHAR(20) NOT NULL,
    [Telefono] VARCHAR(20),
    [Email] VARCHAR(100),
	Estado VARCHAR(10) NOT NULL DEFAULT 'Activo'

    CONSTRAINT PK_MEDICO PRIMARY KEY (IdMedico),
    
);
GO

CREATE TABLE [dbo].[MEDICO_ESPECIALIDAD] (
    IdMedico INT NOT NULL,
    IdEspecialidad INT NOT NULL,

    CONSTRAINT PK_MEDICO_ESPECIALIDAD PRIMARY KEY (IdMedico, IdEspecialidad),
    CONSTRAINT FK_ME_MEDICO FOREIGN KEY (IdMedico) REFERENCES MEDICO(IdMedico),
    CONSTRAINT FK_ME_ESPECIALIDAD FOREIGN KEY (IdEspecialidad) REFERENCES ESPECIALIDAD(IdEspecialidad));
GO

CREATE TABLE [dbo].[ESTADO_TURNO](
    IdEstadoTurno INT IDENTITY(1,1) NOT NULL,
    NombreEstadoTurno NVARCHAR(50) NOT NULL,
    Uso NVARCHAR(20) NOT NULL,
    CONSTRAINT PK_ESTADO_TURNO PRIMARY KEY (IdEstadoTurno),
    CONSTRAINT UQ_EstadoTurno_Nombre_Uso UNIQUE (NombreEstadoTurno, Uso)
);

CREATE TABLE [dbo].[ESTADO_NOTIFICACION](
    IdEstadoNotificacion INT IDENTITY(1,1) NOT NULL,
    NombreEstadoNotificacion NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_ESTADO_NOTIFICACION PRIMARY KEY (IdEstadoNotificacion),
    CONSTRAINT UQ_EstadoNotificacion_Nombre UNIQUE (NombreEstadoNotificacion)
);
GO


CREATE TABLE [dbo].[TURNO](
    IdTurno INT IDENTITY(1,1) NOT NULL,
    IdPaciente INT NOT NULL,
    IdMedico INT NOT NULL,
    IdEspecialidad INT NOT NULL,
    Fecha DATETIME NOT NULL,

    -- Manejo de estados
    IdEstadoTurnoAdmin INT NULL,
    IdEstadoTurnoMedico INT NULL,

    Observaciones NVARCHAR(500) NULL,

    CONSTRAINT PK_TURNO PRIMARY KEY (IdTurno),

    CONSTRAINT FK_TURNO_PACIENTE FOREIGN KEY (IdPaciente)
        REFERENCES PACIENTES(IdPaciente),

    CONSTRAINT FK_TURNO_MEDICO FOREIGN KEY (IdMedico)
        REFERENCES MEDICO(IdMedico),

    CONSTRAINT FK_TURNO_ESPECIALIDAD FOREIGN KEY (IdEspecialidad)
        REFERENCES ESPECIALIDAD(IdEspecialidad),

    CONSTRAINT FK_TURNO_ESTADO_ADMIN FOREIGN KEY (IdEstadoTurnoAdmin)
        REFERENCES ESTADO_TURNO(IdEstadoTurno),

    CONSTRAINT FK_TURNO_ESTADO_MEDICO FOREIGN KEY (IdEstadoTurnoMedico)
        REFERENCES ESTADO_TURNO(IdEstadoTurno)
);
GO

CREATE TABLE [dbo].[NOTIFICACION](
    IdNotificacion INT IDENTITY(1,1) NOT NULL,
    IdTurno INT NOT NULL,
    FechaEnvio DATETIME NOT NULL,
    Medio NVARCHAR(50) NOT NULL,           -- Email / WhatsApp / SMS
    IdEstadoNotificacion INT NOT NULL,     -- FK en vez de VARCHAR

    CONSTRAINT PK_NOTIFICACION PRIMARY KEY (IdNotificacion),

    CONSTRAINT FK_NOTIF_TURNO FOREIGN KEY (IdTurno) REFERENCES TURNO(IdTurno),

    CONSTRAINT FK_NOTIF_ESTADO FOREIGN KEY (IdEstadoNotificacion) REFERENCES ESTADO_NOTIFICACION(IdEstadoNotificacion)
);
GO




CREATE TABLE [dbo].[ROL](
    IdRol INT IDENTITY(1,1) NOT NULL,
    NombreRol VARCHAR(50) NOT NULL,  -- Administrador / Médico / Recepcionista
    CONSTRAINT PK_ROL PRIMARY KEY (IdRol),
    CONSTRAINT UQ_Rol_Nombre UNIQUE (NombreRol)
);
GO

CREATE TABLE [dbo].[USUARIOS](
    IdUsuario INT IDENTITY(1,1) NOT NULL,
    DniUsuario INT NOT NULL,
    Nombres VARCHAR(50) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    NombreUsuario VARCHAR(50) NOT NULL,
    Contrasena VARCHAR(200) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    IdRol INT NOT NULL,  -- FK a ROL

    CONSTRAINT PK_USUARIO PRIMARY KEY (IdUsuario),

    -- Único solo para NombreUsuario
    CONSTRAINT UQ_Usuario_NombreUsuario UNIQUE (NombreUsuario),

    -- Foreign Key
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (IdRol)
        REFERENCES ROL(IdRol)
);
GO

CREATE TABLE HistoriaClinica (
    IdHistoriaClinica INT IDENTITY PRIMARY KEY,
    IdPaciente INT NOT NULL,
    IdMedico INT NOT NULL,
    FechaConsulta DATETIME NOT NULL,
    Observaciones NVARCHAR(MAX),
    Diagnostico NVARCHAR(MAX),
    Tratamientos NVARCHAR(MAX),
    ProximosPasos NVARCHAR(MAX),
    ArchivosAdjuntos NVARCHAR(MAX),

    GrupoFactorSanguineo NVARCHAR(10),
    Peso DECIMAL(5,2),
    Altura DECIMAL(5,2),
    Alergias NVARCHAR(200),
    EnfermedadesCronicas NVARCHAR(200),
    Patologias NVARCHAR(200),

    FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente),
    FOREIGN KEY (IdMedico) REFERENCES Medico(IdMedico)
);
GO

CREATE TABLE AGENDA_PROFESIONAL (
    IdAgenda INT IDENTITY(1,1) PRIMARY KEY,
    IdMedico INT NOT NULL,
    IdEspecialidad INT NOT NULL,
    DuracionTurno INT NOT NULL,            -- minutos (30, 60, 90, etc.)
    PacientesPorTurno INT NOT NULL,
    FechaDesde DATE NOT NULL,
    FechaHasta DATE NOT NULL,

    CONSTRAINT FK_AP_MEDICO FOREIGN KEY (IdMedico) REFERENCES MEDICO(IdMedico),
    CONSTRAINT FK_AP_ESPECIALIDAD FOREIGN KEY (IdEspecialidad) REFERENCES ESPECIALIDAD(IdEspecialidad)
);
GO


CREATE TABLE AGENDA_DISPONIBILIDAD (
    IdDisponibilidad INT IDENTITY(1,1) PRIMARY KEY,
    IdAgenda INT NOT NULL,
    DiaSemana INT NOT NULL,     -- 1 = Lunes ... 7 = Domingo
    Hora TIME NOT NULL,         -- 06:00, 06:30, 07:00, etc.

    CONSTRAINT FK_AD_AGENDA FOREIGN KEY (IdAgenda) REFERENCES AGENDA_PROFESIONAL(IdAgenda)
);
GO
















------------
-- INSERT --

INSERT INTO ROL (NombreRol)
VALUES ('Administrador'), ('Medico'), ('Recepcionista');


DROP TABLE ROL