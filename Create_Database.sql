CREATE SCHEMA app AUTHORIZATION dbo

GO

CREATE TABLE [app].[JobQueues]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2 NOT NULL,
	[CommandType] VARCHAR(255) NOT NULL,
	[CommandData] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_app_JobQueues_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE [app].[NotificationQueues]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2 NOT NULL,
	[MessageType] VARCHAR(255) NOT NULL,
	[MessageData] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_app_NotificationQueues_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE SCHEMA patient AUTHORIZATION dbo

GO

CREATE TABLE [patient].[Patients]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[FirstName] VARCHAR(150) NOT NULL,
	[LastName] VARCHAR(150) NOT NULL,
	[DateOfBirth] DATETIME2 NOT NULL,
	[EmailAddress] VARCHAR(25) NOT NULL,
	[TelephoneNumber] VARCHAR(25) NULL,
	[Address] VARCHAR(50) NULL,
	[PostCode] VARCHAR(8) NULL,
	[IsWelcomeEmailSent] BIT NOT NULL,
	[RegistrationDate] DATETIME2 NOT NULL,
	CONSTRAINT [PK_patient_Patients] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT UC_patient_Patients_EmailAddress UNIQUE ([EmailAddress])
)
GO

CREATE TABLE [patient].[Appointments]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[PatientId] INT NOT NULL,
	[EquipmentId] INT NOT NULL,
	[ReferenceCode] VARCHAR(6) NOT NULL,
	[AppointmentDate] DATETIME2 NOT NULL,
	[StartTime] TIME(7) NOT NULL,
	[EndTime] TIME(7) NOT NULL,
	[IsConfirmationEmailSent] BIT NOT NULL,
	[IsCancelled] BIT NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL,
	[LastModified] DATETIME2 NULL,
	
	CONSTRAINT [PK_patient_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Appointments_Patients] FOREIGN KEY ([PatientId]) REFERENCES [patient].[Patients] ([Id])
)
GO

CREATE VIEW [patient].[vAppointmentDetails]
AS
SELECT 
	patient.Appointments.Id, 
	patient.Appointments.ReferenceCode, 
	patient.Patients.FirstName, 
	patient.Patients.LastName, 
	patient.Appointments.AppointmentDate, 
	patient.Appointments.StartTime, 
	patient.Appointments.EndTime, 
	patient.Appointments.CreatedOn
FROM  
	patient.Patients INNER JOIN patient.Appointments ON patient.Patients.Id = patient.Appointments.PatientId
GO
