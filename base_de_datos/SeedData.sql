

USE CLINICA_DB;
GO

INSERT INTO ESTADO_TURNO (NombreEstadoTurno, Uso) VALUES
('Pendiente', 'Admin'),
('Reprogramado', 'Admin'),
('Cancelado', 'Admin'),
('Ausente', 'Admin'),
('Presente', 'Admin'),

('En Sala', 'Medico'),
('Atendiéndose', 'Medico'),
('Atendido', 'Medico');


-- ----------------------------
-- PACIENTES MASIVOS
-- ----------------------------
INSERT INTO PACIENTES (TipoDocumento, DniPaciente, Nombres, Apellidos, FechaNacimiento, Sexo, Email, Telefono, Celular, Direccion, Ciudad, Provincia, CodigoPostal, ObraSocial, NumeroObraSocial)
VALUES
('DNI', 33333333, 'Carlos', 'Lopez', '1992-03-15', 'M', 'carlos@mail.com', '111111111', '222222222', 'Calle 1', 'Buenos Aires', 'BA', '1002', 'OSDE', '1111'),
('DNI', 44444444, 'María', 'Fernandez', '1988-07-22', 'F', 'maria@mail.com', '333333333', '444444444', 'Calle 2', 'Buenos Aires', 'BA', '1003', 'Swiss Medical', '2222'),
('DNI', 55555555, 'Pedro', 'Gimenez', '1975-12-01', 'M', 'pedro@mail.com', '555555555', '666666666', 'Calle 3', 'Buenos Aires', 'BA', '1004', 'OSDE', '3333'),
('DNI', 66666666, 'Lucía', 'Martinez', '1990-09-30', 'F', 'lucia@mail.com', '777777777', '888888888', 'Calle 4', 'Buenos Aires', 'BA', '1005', 'Galeno', '4444'),
('DNI', 77777777, 'Sofia', 'Ruiz', '1985-11-12', 'F', 'sofia@mail.com', '999999999', '101010101', 'Calle 5', 'Buenos Aires', 'BA', '1006', 'OSDE', '5555');

-- ----------------------------
-- MEDICOS MASIVOS
-- ----------------------------
INSERT INTO MEDICO (Nombre, Apellido, Dni, Telefono, Email)
VALUES
('Jorge', 'Ramirez', '33445566', '121212121', 'jorge@mail.com'),
('Valeria', 'Diaz', '55667788', '131313131', 'valeria@mail.com'),
('Martín', 'Alvarez', '66778899', '141414141', 'martin@mail.com');
-- ----------------------------
-- ESPECIALIDADES MASIVAS
-- ----------------------------
INSERT INTO ESPECIALIDAD (Nombre, Descripcion)
VALUES
('Pediatría', 'Especialidad de niños'),
('Cardiología', 'Especialidad de corazón'),
('Dermatología', 'Especialidad de piel');

-- ----------------------------
-- MEDICO_ESPECIALIDAD MASIVOS
-- ----------------------------
INSERT INTO MEDICO_ESPECIALIDAD (IdMedico, IdEspecialidad)
VALUES
(1, 1),(1,2),(2,2),(2,3),(3,1),(3,3);


-- ----------------------------
-- TURNOS MASIVOS
-- ----------------------------
-- Vamos a crear 15 turnos de ejemplo, distintos estados
INSERT INTO TURNO (IdPaciente, IdMedico, IdEspecialidad, Fecha, IdEstadoTurnoAdmin, IdEstadoTurnoMedico, Observaciones)
VALUES
(1,1,1,'2025-11-28 09:00',1,NULL,'Consulta pediatría'),
(2,1,2,'2025-11-28 09:30',1,3,'Consulta cardiología'),
(3,2,2,'2025-11-28 10:00',1,NULL,'Consulta cardiología'),
(4,2,3,'2025-11-28 10:30',1,4,'Consulta dermatología'),
(5,3,1,'2025-11-28 11:00',1,NULL,'Consulta pediatría'),
(1,3,3,'2025-11-28 11:30',1,3,'Consulta dermatología'),
(2,1,1,'2025-11-28 12:00',1,NULL,'Consulta pediatría'),
(3,2,2,'2025-11-28 12:30',1,4,'Consulta cardiología'),
(4,3,3,'2025-11-28 13:00',1,NULL,'Consulta dermatología'),
(5,1,1,'2025-11-28 13:30',1,3,'Consulta pediatría'),
(1,2,2,'2025-11-28 14:00',1,NULL,'Consulta cardiología'),
(2,3,3,'2025-11-28 14:30',1,4,'Consulta dermatología'),
(3,1,1,'2025-11-28 15:00',1,NULL,'Consulta pediatría'),
(4,2,2,'2025-11-28 15:30',1,3,'Consulta cardiología'),
(5,3,3,'2025-11-28 16:00',1,NULL,'Consulta dermatología');


-- TURNOS SEMANA ANTERIOR
INSERT INTO TURNO (IdPaciente, IdMedico, IdEspecialidad, Fecha, IdEstadoTurnoAdmin, IdEstadoTurnoMedico, Observaciones)
VALUES
(1,1,1,'2025-11-21 09:00',1,NULL,'Consulta pediatría'),
(2,1,2,'2025-11-21 09:30',1,3,'Consulta cardiología'),
(3,2,2,'2025-11-21 10:00',1,NULL,'Consulta cardiología'),
(4,2,3,'2025-11-21 10:30',1,4,'Consulta dermatología'),
(5,3,1,'2025-11-21 11:00',1,NULL,'Consulta pediatría'),
(1,3,3,'2025-11-21 11:30',1,3,'Consulta dermatología'),
(2,1,1,'2025-11-21 12:00',1,NULL,'Consulta pediatría'),
(3,2,2,'2025-11-21 12:30',1,4,'Consulta cardiología'),
(4,3,3,'2025-11-21 13:00',1,NULL,'Consulta dermatología'),
(5,1,1,'2025-11-21 13:30',1,3,'Consulta pediatría'),
(1,2,2,'2025-11-21 14:00',1,NULL,'Consulta cardiología'),
(2,3,3,'2025-11-21 14:30',1,4,'Consulta dermatología'),
(3,1,1,'2025-11-21 15:00',1,NULL,'Consulta pediatría'),
(4,2,2,'2025-11-21 15:30',1,3,'Consulta cardiología'),
(5,3,3,'2025-11-21 16:00',1,NULL,'Consulta dermatología');

-- TURNOS SEMANA PRÓXIMA
INSERT INTO TURNO (IdPaciente, IdMedico, IdEspecialidad, Fecha, IdEstadoTurnoAdmin, IdEstadoTurnoMedico, Observaciones)
VALUES
(1,1,1,'2025-12-05 09:00',1,NULL,'Consulta pediatría'),
(2,1,2,'2025-12-05 09:30',1,3,'Consulta cardiología'),
(3,2,2,'2025-12-05 10:00',1,NULL,'Consulta cardiología'),
(4,2,3,'2025-12-05 10:30',1,4,'Consulta dermatología'),
(5,3,1,'2025-12-05 11:00',1,NULL,'Consulta pediatría'),
(1,3,3,'2025-12-05 11:30',1,3,'Consulta dermatología'),
(2,1,1,'2025-12-05 12:00',1,NULL,'Consulta pediatría'),
(3,2,2,'2025-12-05 12:30',1,4,'Consulta cardiología'),
(4,3,3,'2025-12-05 13:00',1,NULL,'Consulta dermatología'),
(5,1,1,'2025-12-05 13:30',1,3,'Consulta pediatría'),
(1,2,2,'2025-12-05 14:00',1,NULL,'Consulta cardiología'),
(2,3,3,'2025-12-05 14:30',1,4,'Consulta dermatología'),
(3,1,1,'2025-12-05 15:00',1,NULL,'Consulta pediatría'),
(4,2,2,'2025-12-05 15:30',1,3,'Consulta cardiología'),
(5,3,3,'2025-12-05 16:00',1,NULL,'Consulta dermatología');

-- ----------------------------
-- NOTIFICACIONES MASIVAS
-- ----------------------------
INSERT INTO NOTIFICACION (IdTurno, FechaEnvio, Medio, IdEstadoNotificacion)
VALUES
(1,GETDATE(),'Email',1),(2,GETDATE(),'WhatsApp',1),(3,GETDATE(),'SMS',2),
(4,GETDATE(),'Email',1),(5,GETDATE(),'Email',2),(6,GETDATE(),'WhatsApp',1),
(7,GETDATE(),'SMS',1),(8,GETDATE(),'Email',2),(9,GETDATE(),'WhatsApp',1),
(10,GETDATE(),'Email',1),(11,GETDATE(),'SMS',2),(12,GETDATE(),'Email',1),
(13,GETDATE(),'WhatsApp',1),(14,GETDATE(),'SMS',2),(15,GETDATE(),'Email',1);

-- ----------------------------
-- HISTORIA CLINICA MASIVA
-- ----------------------------
INSERT INTO HistoriaClinica (IdPaciente, IdMedico, FechaConsulta, Observaciones, Diagnostico, Tratamientos, ProximosPasos, GrupoFactorSanguineo, Peso, Altura)
VALUES
(1,1,'2025-11-28 09:00','Control pediatría','Gripe','Paracetamol','Revisar en 7 días','A+',70,1.70),
(2,1,'2025-11-28 09:30','Chequeo cardiología','Hipertensión','Atenolol','Revisar en 15 días','O+',80,1.75),
(3,2,'2025-11-28 10:00','Control cardiología','Arritmia','Amiodarona','Revisar en 10 días','B+',75,1.72),
(4,2,'2025-11-28 10:30','Dermatología','Eczema','Crema tópica','Revisar en 10 días','AB+',68,1.65),
(5,3,'2025-11-28 11:00','Consulta pediatría','Varicela','Reposo','Control en 1 semana','O-',72,1.68);

-- ----------------------------
-- AGENDAS PROFESIONALES
-- ----------------------------
INSERT INTO AGENDA_PROFESIONAL (IdMedico, IdEspecialidad, DuracionTurno, PacientesPorTurno, FechaDesde, FechaHasta)
VALUES
(1,1,30,1,'2025-11-28','2025-12-31'),
(2,2,30,1,'2025-11-28','2025-12-31'),
(3,3,60,1,'2025-11-28','2025-12-31');

-- ----------------------------
-- DISPONIBILIDAD AGENDA
-- ----------------------------
INSERT INTO AGENDA_DISPONIBILIDAD (IdAgenda, DiaSemana, Hora)
VALUES
(1,1,'09:00'),(1,1,'09:30'),(1,1,'10:00'),
(2,1,'10:00'),(2,1,'10:30'),(2,1,'11:00'),
(3,1,'11:00'),(3,1,'11:30'),(3,1,'12:00');
GO

-- ----------------------------
-- ROLES
-- ----------------------------
INSERT INTO ROL (NombreRol)
VALUES
('Administrador'),
('Medico'),
('Recepcionista');


-- ----------------------------
-- USUARIOS
-- ----------------------------
INSERT INTO USUARIOS (DniUsuario, Nombres, Apellidos, NombreUsuario, Contrasena, Email, IdRol)
VALUES
(43858485, 'Maia', 'Perez Rodriguez', 'mperez', '1234', 'maia@mail.com', 1), -- Admin
(30111222, 'Federico', 'Alonso', 'falonso', '1234', 'federico@mail.com', 2), -- Médico
(40122334, 'Eri', 'Espeche', 'espeche', '1234', 'espeche@mail.com', 3),       -- Recepcionista
(50133445, 'Lucas', 'Martinez', 'lmartinez', '1234', 'lucas@mail.com', 2),
(60144556, 'Valentina', 'Lopez', 'vlopez', '1234', 'valentina@mail.com', 2),
(70155667, 'Martin', 'Diaz', 'mdiaz', '1234', 'martin@mail.com', 3),
(80166778, 'Sofia', 'Rojas', 'srojas', '1234', 'sofia@mail.com', 1),
(90177889, 'Diego', 'Fernandez', 'dfernandez', '1234', 'diego@mail.com', 2),
(10188990, 'Camila', 'Torres', 'ctorres', '1234', 'camila@mail.com', 3),
(11199001, 'Nicolas', 'Vega', 'nvega', '1234', 'nicolas@mail.com', 2);


select * from usuarios