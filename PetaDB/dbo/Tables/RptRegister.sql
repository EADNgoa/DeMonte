﻿CREATE TABLE [dbo].[RptRegister]
(
    [BillID] INT NOT NULL PRIMARY KEY, 	
	[BillNo] varCHAR(10) NULL, 
	[Name] varCHAR(150) NULL, 
    [Address] varCHAR(350) NULL, 
    [PassportDet] varCHAR(100) NULL,     
	[NoOfGuest] INT NULL, 
	[NoOfRooms] INT NULL, 
	[DateArrivalTime] DATETIME NULL, 
	[DateDepartureTime] DATETIME NULL, 
    [ChargesPerDay] DECIMAL(15, 2) NULL, 
	[RoomNo] varchar(50) NULL,
    [TotalDays] INT NULL, 
	[TotalCharges] DECIMAL(15, 2) NULL,
    [PaidInForeignIndian] DECIMAL(15, 2) NULL,
    [BillReceiptNo] varchar(50) NULL, 
    [GST] DECIMAL(12, 2) NULL, 
	[EncashCertDetails] VARCHAR(100) NULL,
	[Remarks] VARCHAR(100) NULL,
	[RegisterNo] DECIMAL(10, 2) NULL, 
    )
