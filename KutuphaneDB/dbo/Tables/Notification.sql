CREATE TABLE [dbo].[Notification]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [MemberId] INT NOT NULL, 
    [Bildirim] NCHAR(100) NULL,
    [isActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FKmid] FOREIGN KEY (MemberID) REFERENCES dbo.member(ID)
)
