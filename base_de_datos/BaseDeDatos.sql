USE master;
GO

CREATE DATABASE CLINICA_DB;
GO

USE CLINICA_DB;
GO

CREATE TABLE [dbo].[PACIENTES](
    [IdPaciente] INT IDENTITY(1,1) NOT NULL,
    [DniPaciente] INT NOT NULL,
    [Nombres] VARCHAR(50) NOT NULL,
    [Apellidos] VARCHAR(50) NOT NULL,
    [FechaNacimiento] DATE NOT NULL,
    [Sexo] CHAR(1) NOT NULL,
    [GrupoSanguineo] VARCHAR(8) NOT NULL,
    [Email] VARCHAR(1000) NOT NULL,
    [Telefono] VARCHAR(20) NOT NULL,
    [Celular] VARCHAR(20) NOT NULL,
    [Direccion] VARCHAR(100) NOT NULL,
    [Ciudad] VARCHAR(100) NOT NULL,
    [Provincia] VARCHAR(100) NOT NULL,
    [CodigoPostal] VARCHAR(10) NOT NULL,
    
	CONSTRAINT PK_PACIENTES PRIMARY KEY (IdPaciente));
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

CREATE TABLE [dbo].[USUARIO](
    IdUsuario INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Contraseña VARCHAR(200) NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,

    CONSTRAINT PK_USUARIO PRIMARY KEY (IdUsuario)
);
GO

------------
-- INSERT --

INSERT INTO [dbo].[PACIENTES] 
([DniPaciente], [Nombres], [Apellidos], [FechaNacimiento], [Sexo], [GrupoSanguineo], [Email], [Telefono], [Celular], [Direccion], [Ciudad], [Provincia], [CodigoPostal])
VALUES
(43858486, 'Maia', 'Perez', '2002-02-08', 'F', 'A+', 'maia@utn.com', '47153945', '1130958485', 'JoseMPaz', 'Olivos', 'Buenos Aires', 1642),
(43088086, 'Gianluca', 'Labarile', '1995-02-08', 'M', 'O+', 'gian@gmail.com', '47403945', '1166778899', 'Belgrano 456', 'San Isidro', 'Buenos Aires', 1642);
GO

INSERT INTO [dbo].[ESPECIALIDAD] (Nombre, Descripcion) VALUES
('Cardiología', 'Atención de enfermedades del corazón'),
('Dermatología', 'Tratamiento de afecciones de la piel'),
('Pediatría', 'Atención médica de niños'),
('Ginecología', 'Salud reproductiva femenina'),
('Traumatología', 'Lesiones óseas y musculares');

INSERT INTO [dbo].[TURNO_TRABAJO] (Nombre, HoraEntrada, HoraSalida) VALUES
('Mañana', '08:00', '14:00'),
('Tarde', '14:00', '20:00'),
('Noche', '20:00', '02:00');

INSERT INTO [dbo].[MEDICO] (Nombre, Apellido, Dni, Telefono, Email, IdTurnoTrabajo) VALUES
('Laura', 'Gonzalez', '32145698', '1155443322', 'laura.gonzalez@clinica.com', 1),
('Martín', 'Pacheco', '28900333', '1122334455', 'martin.pacheco@clinica.com', 2),
('Sofía', 'Martinez', '30455987', '1144556677', 'sofia.martinez@clinica.com', 1),
('Diego', 'Ocampo', '27123890', '1133221100', 'diego.ocampo@clinica.com', 3);

INSERT INTO [dbo].[MEDICO_ESPECIALIDAD] (IdMedico, IdEspecialidad) VALUES
(1, 1),
(1, 2),
(2, 3),
(3, 5),
(4, 1),
(4, 4);

INSERT INTO [dbo].[USUARIO] (Nombre, Contraseña, Rol, Email) VALUES
('admin', 'admin123', 'Administrador', 'admin@clinica.com'),
('recepcion', '1234', 'Recepcion', 'recepcion@clinica.com'),
('maia', 'claveSegura1', 'Usuario', 'maia@clinica.com');

INSERT INTO [dbo].[TURNO] (IdPaciente, IdMedico, IdEspecialidad, Fecha, Estado, Observaciones) VALUES
(1, 1, 1, '2025-02-20 10:00', 'Pendiente', 'Control de rutina'),
(2, 2, 3, '2025-02-21 15:30', 'Confirmado', 'Consulta pediátrica'),
(1, 3, 5, '2025-02-22 11:00', 'Pendiente', NULL),
(2, 4, 4, '2025-02-23 18:00', 'Cancelado', 'Paciente no asistió');

INSERT INTO [dbo].[NOTIFICACION] (IdTurno, FechaEnvio, Medio, Estado) VALUES
(1, '2025-02-19 09:00', 'Email', 'Enviado'),
(2, '2025-02-21 12:00', 'SMS', 'Enviado'),
(3, '2025-02-21 08:00', 'Email', 'Fallido'),
(4, '2025-02-23 09:30', 'WhatsApp', 'Enviado');

--Select * from pacientes

--SELECT IdPaciente, DniPaciente, Nombres, Apellidos, FechaNacimiento, Sexo, GrupoSanguineo, Email, Telefono, Celular, Direccion, Ciudad, Provincia, CodigoPostal FROM PACIENTES