﻿CREATE TABLE [dbo].[BookType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [tur] NVARCHAR(50) NOT NULL UNIQUE,
)
