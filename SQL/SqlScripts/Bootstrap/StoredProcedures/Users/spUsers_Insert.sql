--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spUsers_Insert'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE [dbo].[spUsers_Insert];
    END
GO
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spUsers_Insert]
    @Name NVARCHAR(100),
    @BirthDate DATETIME,
    @Married BIT,
    @Phone NVARCHAR(50),
    @Salary DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;

    --------------------------------------------------------------------------
    -- Validate:
    --------------------------------------------------------------------------
    IF @Name IS NULL
    BEGIN
        RAISERROR ('Must pass a @Name parameter.', 11, 1);
        RETURN -1;
    END

    IF @BirthDate IS NULL
    BEGIN
        RAISERROR ('Must pass a @BirthDate parameter.', 11, 2);
        RETURN -1;
    END

    IF @Salary IS NULL
    BEGIN
        RAISERROR ('Must pass a @Salary parameter.', 11, 3);
        RETURN -1;
    END

    --------------------------------------------------------------------------
    -- Insert:
    --------------------------------------------------------------------------
    INSERT INTO [dbo].[Users](
	    [Name],
	    [BirthDate],
	    [Married],
	    [Phone],
        [Salary])
	VALUES(@Name, @BirthDate, @Married, @Phone, @Salary);
	
    ------------------------------------------------------------------
    -- Return:
    ------------------------------------------------------------------
    SELECT CONVERT(INT, SCOPE_IDENTITY()) AS 'Id';
END
GO
--============================================================================