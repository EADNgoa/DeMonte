CREATE TABLE [dbo].[ExpSales]
(
    [BillID] INT NOT NULL PRIMARY KEY, 	
	[BillNo] VARCHAR(10) NULL, 
	[TDate] DATE NULL, 
	[GSTNo] VARCHAR(30) NULL, 
	[Name] varCHAR(150) NULL, 
    [AddLineBreaks] varCHAR(350) NULL, 
    [TaxableSales] DECIMAL(15, 2) NULL,
    [IGST] DECIMAL(15, 2) NULL,
	[CGST] DECIMAL(15, 2) NULL,
	[SGST] DECIMAL(15, 2) NULL,
    [TotalValue] DECIMAL(15, 2) NULL,
    [GST] DECIMAL(12, 2) NULL 	
    )
