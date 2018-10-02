CREATE TABLE [dbo].[Bill]
(
	[BillID] INT NOT NULL PRIMARY KEY Identity, 
    [BillNo] varchar(10) NULL,
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
	[RegisterNo] VARCHAR(15) NULL, 
    [NonResdCust] VARCHAR(150) NULL, 
    [GSTExport] BIT NULL DEFAULT 1, 
    [NRCgst] VARCHAR(30) NULL, 
    CONSTRAINT [FK_Bill_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID]) 
  
   
)
