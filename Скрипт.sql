CREATE DATABASE ShopProductExpiration
GO

USE ShopProductExpiration
GO

CREATE TABLE Products(
	Id INT Identity Primary KEY,
	ProductName NVARCHAR(150) NOT NULL,
	ProductManufacturingDate DATETIME NOT NULL,
	ProductPackagingDate DATETIME,
	ShelfLife SMALLINT,
	TimeUnits NVARCHAR(100),
	SellBy DATETIME,
	ProductCount SMALLINT,
	CountUnits NVARCHAR(100),
	ShopDepartment NVARCHAR(100),
	DepartmentHeadFIO NVARCHAR(150),
	RowNumber SMALLINT,
	ShelvingNumber SMALLINT,
	ShelfNumber SMALLINT,
);
