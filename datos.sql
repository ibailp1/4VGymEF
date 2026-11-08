-- Datos de prueba para 4VGym (las tablas las crea la migración InitialCreate:  dotnet ef database update)
-- Se puede ejecutar varias veces: borra los datos y los vuelve a insertar con los mismos Id.
USE [4VGym];
GO

DELETE FROM ActivityInstructor;
DELETE FROM Activities;
DELETE FROM Instructors;
DELETE FROM ActivityTypes;
GO

SET IDENTITY_INSERT ActivityTypes ON;
INSERT INTO ActivityTypes (Id, Name, NumberMonitors) VALUES
    (10, N'BodyPump', 2),
    (20, N'Spinning', 1),
    (30, N'Pilates', 1),
    (40, N'CrossFit', 3);
SET IDENTITY_INSERT ActivityTypes OFF;
GO

SET IDENTITY_INSERT Instructors ON;
INSERT INTO Instructors (Id, Name, Email, PhotoUrl, Phone) VALUES
    (1, N'Ibai',   N'ibai@gmail.com',   N'https://4vgym.com/photos/1.png', N'600111222'),
    (2, N'Oihan',  N'oihan@gmail.com',  N'https://4vgym.com/photos/2.png', N'600333444'),
    (3, N'Miguel', N'miguel@gmail.com', N'https://4vgym.com/photos/3.png', N'600555666'),
    (4, N'Marta',  N'marta@gmail.com',  N'https://4vgym.com/photos/4.png', N'600777888');
SET IDENTITY_INSERT Instructors OFF;
GO

SET IDENTITY_INSERT Activities ON;
INSERT INTO Activities (Id, ActivityTypeId, MaxParticipants, DateStart, DateEnd) VALUES
    (1, 10, 20, '2027-01-12T18:00:00', '2027-01-12T19:00:00'),
    (2, 20, 15, '2027-01-13T09:00:00', '2027-01-13T10:00:00'),
    (3, 30, 12, '2027-01-14T11:00:00', '2027-01-14T12:00:00'),
    (4, 40, 10, '2027-01-15T19:00:00', '2027-01-15T20:30:00');
SET IDENTITY_INSERT Activities OFF;
GO

-- Relación N:M Activities - Instructors (los Id de columna son los que crea EF para la tabla intermedia)
INSERT INTO ActivityInstructor (ActivitiesId, InstructorsId) VALUES
    (1, 1), (1, 2),          -- BodyPump: 2 monitores
    (2, 3),                  -- Spinning: 1 monitor
    (3, 4),                  -- Pilates: 1 monitor
    (4, 1), (4, 2), (4, 3);  -- CrossFit: 3 monitores
GO
