-- Hospital Appointment Management Database Setup

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HospitalAppointmentDB')
BEGIN
    CREATE DATABASE HospitalAppointmentDB;
END
GO

USE HospitalAppointmentDB;
GO

-- Create Appointments Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Appointments')
BEGIN
    CREATE TABLE Appointments (
        Id INT PRIMARY KEY IDENTITY(1,1),
        PatientName NVARCHAR(100) NOT NULL,
        DoctorName NVARCHAR(100) NOT NULL,
        AppointmentDate DATETIME NOT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT 'Scheduled',
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- Insert Sample Data
INSERT INTO Appointments (PatientName, DoctorName, AppointmentDate, Status)
VALUES 
    ('John Doe', 'Dr. Smith', DATEADD(DAY, 5, GETDATE()), 'Scheduled'),
    ('Jane Smith', 'Dr. Wilson', DATEADD(DAY, 7, GETDATE()), 'Scheduled'),
    ('Robert Johnson', 'Dr. Brown', DATEADD(DAY, 10, GETDATE()), 'Scheduled'),
    ('Mary Davis', 'Dr. Martinez', DATEADD(DAY, 3, GETDATE()), 'Scheduled');
GO

-- Verify Data
SELECT * FROM Appointments;
GO
