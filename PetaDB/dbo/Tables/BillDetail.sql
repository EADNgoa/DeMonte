CREATE TABLE [dbo].[BillDetail]
(
	[BillDetailID] INT NOT NULL PRIMARY KEY, 
    [BillID] INT NULL, 
    [Date] DATE NULL, 
    [ExtraPerson] DECIMAL(12, 2) NULL, 
    [GST] DECIMAL(12, 2) NULL, 
    [Miscelleneous] DECIMAL(12, 2) NULL, 
    [Other1] DECIMAL(12, 2) NULL, 
    [Other2] DECIMAL(12, 2) NULL, 
    [Other3] DECIMAL(12, 2) NULL, 
    [Other4] DECIMAL(12, 2) NULL, 
    [Total] DECIMAL(12, 2) NULL, 
    CONSTRAINT [FK_BillDetail_Bill] FOREIGN KEY ([BillID]) REFERENCES [Bill]([BillID])
)
