CREATE TABLE [dbo].[Bill]
(
	[BillID] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(350) NULL, 
    [Date] DATE NULL, 
    [Address] VARCHAR(350) NULL, 
    [RoomChargesPerDay] DECIMAL(15, 2) NULL, 
    [TotalDays] INT NULL, 
    [NoOfPerson] INT NULL, 
    [AdvReceiptNo] INT NULL, 
    [DateArrival] DATE NULL, 
    [CheckInTime] TIME NULL, 
    [RegisterNo] INT NULL, 
    [DateDeparture] DATE NULL, 
    [RoomNo] INT NULL
)
