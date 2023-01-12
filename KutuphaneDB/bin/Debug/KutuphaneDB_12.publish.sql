﻿/*
Deployment script for KutuphaneDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "KutuphaneDB"
:setvar DefaultFilePrefix "KutuphaneDB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key cf39a43d-d87e-4ae2-9d53-d148bc84985a is skipped, element [dbo].[BookType].[type] (SqlSimpleColumn) will not be renamed to tur';


GO
PRINT N'Creating Table [dbo].[BookType]...';


GO
CREATE TABLE [dbo].[BookType] (
    [Id]  INT           IDENTITY (1, 1) NOT NULL,
    [tur] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'cf39a43d-d87e-4ae2-9d53-d148bc84985a')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('cf39a43d-d87e-4ae2-9d53-d148bc84985a')

GO

GO
PRINT N'Update complete.';


GO
