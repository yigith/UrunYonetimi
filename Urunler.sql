USE master;
IF DB_ID('UrunYonetimiAdoDb') IS NULL
	CREATE DATABASE UrunYonetimiAdoDb;

GO

USE UrunYonetimiAdoDb;
IF OBJECT_ID(N'dbo.Urunler', N'U') IS NULL 
CREATE TABLE Urunler
(
	Id INT PRIMARY KEY IDENTITY,
	UrunAd NVARCHAR(100) NOT NULL,
	BirimFiyat DECIMAL(18,2) NOT NULL
);