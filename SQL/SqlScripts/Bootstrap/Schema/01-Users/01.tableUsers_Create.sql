--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM sys.tables
    WHERE name = 'Users' AND SCHEMA_NAME(schema_id) = 'dbo')
    BEGIN
        DROP TABLE [dbo].[Users];
    END
GO
------------------------------------------------------------------------------
CREATE TABLE [dbo].[Users] (
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_Users PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [BirthDate] DATETIME NOT NULL,
    [Married] BIT NULL,
    [Phone] NVARCHAR(50) NULL,
    [Salary] DECIMAL(18, 2) NOT NULL
        CONSTRAINT CC_Users_Salary CHECK ([Salary] >= 0),
    [RegisteredDate] DATETIME NOT NULL
        CONSTRAINT DC_Users_RegisteredDate DEFAULT GETUTCDATE()
);
GO
--============================================================================