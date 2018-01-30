CREATE TABLE [dbo].[Bill]
(
	[BillID] INT NOT NULL PRIMARY KEY Identity, 
    [CustomerID] INT NULL, 
	[NoOfGuest] INT NULL, 
	[DateArrivalTime] DATETIME NULL, 
	[DateDepartureTime] DATETIME NULL, 
    [ChargesPerDay] DECIMAL(15, 2) NULL, 
	[RoomNo] INT NULL,
    [TotalDays] INT NULL, 
    [AdvReceiptNo] INT NULL, 
    [PaidInForeignIndian] DECIMAL(15, 2) NULL, 
    [CGST] DECIMAL(12, 2) NULL, 
    [SGST] DECIMAL(12, 2) NULL, 
    [IGST] DECIMAL(12, 2) NULL, 
    [TransactionDate] DATE NULL, 
    [EncashCertDetails] VARCHAR(350) NULL, 
    [Remarks] VARCHAR(350) NULL, 
    [RegisterNo] INT NULL, 
    [Canceled] BIT NULL, 
    CONSTRAINT [FK_Bill_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID]) 
  
   
)
