-- Create Entishar Database and Users Table
-- Run this script in SQL Server Management Studio or Azure Data Studio

USE master;
GO

-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Entishar')
BEGIN
    CREATE DATABASE Entishar;
END
GO

USE Entishar;
GO

-- Create Users table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id NVARCHAR(36) NOT NULL PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL,
        Password NVARCHAR(100) NOT NULL,
        UserFullName NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        DateOfBirth DATE NOT NULL,
        CreationDate DATETIME2 NOT NULL DEFAULT GETDATE()
    );

    -- Create unique index on Username
    CREATE UNIQUE INDEX IX_Users_Username ON Users(Username);

    -- Insert admin user
    INSERT INTO Users (Id, Username, Password, UserFullName, IsActive, DateOfBirth, CreationDate)
    VALUES ('1', 'admin', 'admin123', 'Administrator', 1, '1990-01-01', '2026-01-01');
END
GO

-- Verify the data
SELECT * FROM Users;
GO
