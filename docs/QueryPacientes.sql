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
    CONSTRAINT [PK_PACIENTES] PRIMARY KEY CLUSTERED 
    (
        [IdPaciente] ASC
    ) WITH (
        PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
        ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY];
GO

INSERT INTO [dbo].[PACIENTES] 
([DniPaciente], [Nombres], [Apellidos], [FechaNacimiento], [Sexo], [GrupoSanguineo], [Email], [Telefono], [Celular], [Direccion], [Ciudad], [Provincia], [CodigoPostal])
VALUES
(43858486, 'Maia', 'Perez', '2002-02-08', 'F', 'A+', 'maia@utn.com', '47153945', '1130958485', 'JoseMPaz', 'Olivos', 'Buenos Aires', 1642),
(43088086, 'Gianluca', 'Labarile', '1995-02-08', 'M', 'O+', 'gian@gmail.com', '47403945', '1166778899', 'Belgrano 456', 'San Isidro', 'Buenos Aires', 1642);
GO


--Select * from pacientes

--SELECT IdPaciente, DniPaciente, Nombres, Apellidos, FechaNacimiento, Sexo, GrupoSanguineo, Email, Telefono, Celular, Direccion, Ciudad, Provincia, CodigoPostal FROM PACIENTES