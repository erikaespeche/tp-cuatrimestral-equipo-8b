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

CREATE TABLE [dbo].[TURNO_TRABAJO] (
    IdTurnoTrabajo INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    HoraEntrada TIME NOT NULL,
    HoraSalida TIME NOT NULL,

	CONSTRAINT PK_TURNO_TRABAJO PRIMARY KEY (IdTurnoTrabajo));
GO


CREATE TABLE [dbo].[MEDICO](
    [IdMedico] INT IDENTITY(1,1) NOT NULL,
    [Nombre] VARCHAR(50) NOT NULL,
    [Apellido] VARCHAR(50) NOT NULL,
    [Dni] VARCHAR(20) NOT NULL,
    [Telefono] VARCHAR(20),
    [Email] VARCHAR(100),
    [IdTurnoTrabajo] INT NULL,

    CONSTRAINT PK_MEDICO PRIMARY KEY (IdMedico),
    CONSTRAINT FK_MEDICO_TURNO_TRABAJO FOREIGN KEY (IdTurnoTrabajo)
        REFERENCES TURNO_TRABAJO(IdTurnoTrabajo)
);
GO

CREATE TABLE [dbo].[MEDICO_ESPECIALIDAD] (
    IdMedico INT NOT NULL,
    IdEspecialidad INT NOT NULL,

    CONSTRAINT PK_MEDICO_ESPECIALIDAD PRIMARY KEY (IdMedico, IdEspecialidad),
    CONSTRAINT FK_ME_MEDICO FOREIGN KEY (IdMedico) REFERENCES MEDICO(IdMedico),
    CONSTRAINT FK_ME_ESPECIALIDAD FOREIGN KEY (IdEspecialidad) REFERENCES ESPECIALIDAD(IdEspecialidad));
GO

CREATE TABLE [dbo].[TURNO](
    IdTurno INT IDENTITY(1,1) NOT NULL,
    IdPaciente INT NOT NULL,
    IdMedico INT NOT NULL,
    IdEspecialidad INT NOT NULL,
    Fecha DATETIME NOT NULL,
    Estado VARCHAR(20) NOT NULL,
    Observaciones VARCHAR(500) NULL,

    CONSTRAINT PK_TURNO PRIMARY KEY (IdTurno),
    CONSTRAINT FK_TURNO_PACIENTE FOREIGN KEY (IdPaciente) REFERENCES PACIENTES(IdPaciente),
    CONSTRAINT FK_TURNO_MEDICO FOREIGN KEY (IdMedico) REFERENCES MEDICO(IdMedico),
    CONSTRAINT FK_TURNO_ESPECIALIDAD FOREIGN KEY (IdEspecialidad) REFERENCES ESPECIALIDAD(IdEspecialidad)
);
GO

CREATE TABLE [dbo].[NOTIFICACION] (
    IdNotificacion INT IDENTITY(1,1) NOT NULL,
    IdTurno INT NOT NULL,
    FechaEnvio DATETIME NOT NULL,
    Medio VARCHAR(50) NOT NULL,
    Estado VARCHAR(20) NOT NULL,

    CONSTRAINT PK_NOTIFICACION PRIMARY KEY (IdNotificacion),
    CONSTRAINT FK_NOTIF_TURNO FOREIGN KEY (IdTurno) REFERENCES TURNO(IdTurno)
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
    IdHistoriaClinica INT IDENTITY(1,1) PRIMARY KEY,
    IdPaciente INT NOT NULL,
    IdMedico INT NOT NULL,
    FechaConsulta DATETIME NOT NULL,
    Observaciones NVARCHAR(MAX),
    Diagnostico NVARCHAR(MAX),
    Tratamientos NVARCHAR(MAX),
    ProximosPasos NVARCHAR(MAX),
    ArchivosAdjuntos NVARCHAR(MAX) NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'En Espera',

    FOREIGN KEY (IdPaciente) REFERENCES PACIENTES(IdPaciente),
    FOREIGN KEY (IdMedico) REFERENCES MEDICO(IdMedico)
);

CREATE TABLE AgendaMedico (
    IdAgenda INT IDENTITY(1,1) PRIMARY KEY,
    IdMedico INT NOT NULL,
    DiaSemana INT NOT NULL,   
    Hora TIME NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Disponible',  -- Disponible / Bloqueado / Ocupado
    IdTurno INT NULL,      

    CONSTRAINT FK_AgendaMedico_Medico FOREIGN KEY (IdMedico) REFERENCES MEDICO(IdMedico),
    CONSTRAINT FK_AgendaMedico_Turno FOREIGN KEY (IdTurno) REFERENCES TURNO(IdTurno)
);


------------
-- INSERT --

INSERT INTO ROL (NombreRol)
VALUES ('Administrador'), ('Medico'), ('Recepcionista');


DROP TABLE ROL