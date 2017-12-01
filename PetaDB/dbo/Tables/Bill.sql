﻿CREATE TABLE [dbo].[Bill]
(
	[BillID] INT NOT NULL PRIMARY KEY Identity, 
    [CustomerID] INT NULL, 
	[NoOfGuest] INT NULL, 
	[DateArrivalTime] DATETIME NULL, 
	[DateDepartureTime] DATE NULL, 
    [ChargesPerDay] DECIMAL(15, 2) NULL, 
	[RoomNo] INT NULL,
    [TotalDays] INT NULL, 
	[PeriodToStay] INT NULL,
    [BillReceiptNo] INT NULL, 
	[AmountGSTTax] DECIMAL(15, 2) NULL, 
    [PaidInForeignIndian] DECIMAL(15, 2) NULL, 
    CONSTRAINT [FK_Bill_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID]) 
  
   
)
