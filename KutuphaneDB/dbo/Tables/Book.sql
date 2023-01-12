CREATE TABLE [dbo].[Book]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [isim] NVARCHAR(50) NOT NULL, 
    [yazar] NVARCHAR(50) NOT NULL, 
    [tur] NVARCHAR(50) NOT NULL, 
    [sayfa] INT NOT NULL , 
    [durum] NCHAR(10) NULL DEFAULT 'kutuphane',
    CONSTRAINT [FKbooktur] FOREIGN KEY (tur) REFERENCES dbo.BookType(tur)
)
