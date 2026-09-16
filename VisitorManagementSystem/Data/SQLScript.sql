-- =============================================
-- Visitor Management System - SQL Script
-- Database: VisitorManagementDB
-- =============================================

USE [master]
GO

-- Create Database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'VisitorManagementDB')
BEGIN
    CREATE DATABASE [VisitorManagementDB]
END
GO

USE [VisitorManagementDB]
GO

-- =============================================
-- Table: Visitors
-- =============================================
IF OBJECT_ID('[dbo].[Visitors]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Visitors] (
        [VisitorId] INT IDENTITY(1,1) NOT NULL,
        [VisitorName] NVARCHAR(100) NOT NULL,
        [CompanyName] NVARCHAR(100) NOT NULL,
        [MobileNo] NVARCHAR(20) NOT NULL,
        [Email] NVARCHAR(100) NULL,
        [IDProof] NVARCHAR(50) NULL,
        CONSTRAINT [PK_Visitors] PRIMARY KEY CLUSTERED ([VisitorId] ASC)
    )
END
GO

-- =============================================
-- Table: Employees
-- =============================================
IF OBJECT_ID('[dbo].[Employees]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Employees] (
        [EmployeeId] INT IDENTITY(1,1) NOT NULL,
        [EmployeeName] NVARCHAR(100) NOT NULL,
        [Department] NVARCHAR(50) NOT NULL,
        [Designation] NVARCHAR(50) NOT NULL,
        [MobileNo] NVARCHAR(20) NOT NULL,
        [Email] NVARCHAR(100) NOT NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
    )
END
GO

-- =============================================
-- Table: Visits
-- =============================================
IF OBJECT_ID('[dbo].[Visits]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Visits] (
        [VisitId] INT IDENTITY(1,1) NOT NULL,
        [VisitorId] INT NOT NULL,
        [EmployeeId] INT NOT NULL,
        [Purpose] NVARCHAR(200) NOT NULL,
        [VisitDate] DATETIME2 NOT NULL,
        [CheckInTime] DATETIME2 NOT NULL,
        [CheckOutTime] DATETIME2 NULL,
        [Status] NVARCHAR(20) NOT NULL,
        CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED ([VisitId] ASC),
        CONSTRAINT [FK_Visits_Visitors] FOREIGN KEY ([VisitorId]) REFERENCES [dbo].[Visitors]([VisitorId]),
        CONSTRAINT [FK_Visits_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees]([EmployeeId])
    )
    
    -- Create indexes for better performance
    CREATE NONCLUSTERED INDEX [IX_Visits_VisitorId] ON [dbo].[Visits] ([VisitorId])
    CREATE NONCLUSTERED INDEX [IX_Visits_EmployeeId] ON [dbo].[Visits] ([EmployeeId])
    CREATE NONCLUSTERED INDEX [IX_Visits_VisitDate] ON [dbo].[Visits] ([VisitDate])
    CREATE NONCLUSTERED INDEX [IX_Visits_Status] ON [dbo].[Visits] ([Status])
END
GO

-- =============================================
-- Sample Data: Visitors
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Visitors])
BEGIN
    INSERT INTO [dbo].[Visitors] ([VisitorName], [CompanyName], [MobileNo], [Email], [IDProof]) VALUES
    ('John Smith', 'Tech Solutions Inc', '+1-555-0101', 'john.smith@techsolutions.com', 'Passport'),
    ('Sarah Johnson', 'Global Enterprises', '+1-555-0102', 'sarah.j@globalent.com', 'Driving License'),
    ('Michael Brown', 'Innovative Systems', '+1-555-0103', 'm.brown@innosys.com', 'Aadhar Card'),
    ('Emily Davis', 'Digital Dynamics', '+1-555-0104', 'emily.davis@digitaldyn.com', 'PAN Card'),
    ('Robert Wilson', 'Cloud Services Ltd', '+1-555-0105', 'r.wilson@cloudservices.com', 'Voter ID')
END
GO

-- =============================================
-- Sample Data: Employees
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Employees])
BEGIN
    INSERT INTO [dbo].[Employees] ([EmployeeName], [Department], [Designation], [MobileNo], [Email], [IsActive]) VALUES
    ('James Anderson', 'IT', 'Senior Developer', '+1-555-0201', 'james.anderson@company.com', 1),
    ('Lisa Martinez', 'HR', 'HR Manager', '+1-555-0202', 'lisa.martinez@company.com', 1),
    ('David Thompson', 'Sales', 'Sales Executive', '+1-555-0203', 'david.t@company.com', 1),
    ('Jennifer Garcia', 'Finance', 'Accountant', '+1-555-0204', 'jennifer.garcia@company.com', 1),
    ('William Rodriguez', 'Operations', 'Operations Manager', '+1-555-0205', 'william.r@company.com', 1),
    ('Patricia Lee', 'IT', 'System Administrator', '+1-555-0206', 'patricia.lee@company.com', 0)
END
GO

-- =============================================
-- Sample Data: Visits
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Visits])
BEGIN
    DECLARE @Today DATE = CAST(GETDATE() AS DATE)
    DECLARE @Yesterday DATE = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE)
    
    INSERT INTO [dbo].[Visits] ([VisitorId], [EmployeeId], [Purpose], [VisitDate], [CheckInTime], [CheckOutTime], [Status]) VALUES
    -- Today's visits
    (1, 1, 'Business meeting regarding new project', @Today, DATEADD(HOUR, 9, @Today), DATEADD(HOUR, 11, @Today), 'CheckedOut'),
    (2, 2, 'HR interview discussion', @Today, DATEADD(HOUR, 10, @Today), NULL, 'CheckedIn'),
    (3, 3, 'Product demonstration', @Today, DATEADD(HOUR, 14, @Today), NULL, 'CheckedIn'),
    -- Yesterday's visits
    (4, 4, 'Financial audit review', @Yesterday, DATEADD(HOUR, 11, @Yesterday), DATEADD(HOUR, 13, @Yesterday), 'CheckedOut'),
    (5, 5, 'Operations coordination meeting', @Yesterday, DATEADD(HOUR, 15, @Yesterday), DATEADD(HOUR, 17, @Yesterday), 'CheckedOut')
END
GO

-- =============================================
-- Verification Queries
-- =============================================
PRINT 'Database setup completed successfully!'
PRINT ''
PRINT 'Sample Data Summary:'
PRINT '-------------------'

SELECT 'Visitors' AS TableName, COUNT(*) AS RecordCount FROM [dbo].[Visitors]
UNION ALL
SELECT 'Employees', COUNT(*) FROM [dbo].[Employees]
UNION ALL
SELECT 'Visits', COUNT(*) FROM [dbo].[Visits]

PRINT ''
PRINT 'To verify the data, run these queries:'
PRINT 'SELECT * FROM [dbo].[Visitors]'
PRINT 'SELECT * FROM [dbo].[Employees]'
PRINT 'SELECT * FROM [dbo].[Visits]'
GO
