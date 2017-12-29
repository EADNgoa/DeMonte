﻿CREATE TABLE [dbo].[Bill]
(
	[BillID] INT NOT NULL PRIMARY KEY Identity, 
    [BDate] DATE NOT NULL,
	[CustomerID] INT NULL, 
	[NoOfGuest] INT NULL, 
	[DateArrivalTime] DATETIME NULL, 
	[DateDepartureTime] DATETIME NULL, 
    [ChargesPerDay] DECIMAL(15, 2) NULL, 
	[RoomNo] varchar(10) NULL,
    [TotalDays] INT NULL, 
    [BillReceiptNo] INT NULL, 
    [PaidInForeignIndian] DECIMAL(15, 2) NULL,
	[EncashCertDetails] VARCHAR(100) NULL,
	[Remarks] VARCHAR(100) NULL,
    [CGST] DECIMAL(12, 2) NULL, 
    [SGST] DECIMAL(12, 2) NULL, 
    [IGST] DECIMAL(12, 2) NULL, 
    [Canceled] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Bill_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID]) 
  
   
)
