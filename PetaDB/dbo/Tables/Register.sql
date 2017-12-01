CREATE TABLE [dbo].[Register]
(
	[RegisterID] INT NOT NULL PRIMARY KEY, 
    [CustomerID] INT NULL, 
    [BillID] INT NULL, 
    [EncashmentCertificate] VARCHAR(350) NULL, 
    [Remarks] VARCHAR(350) NULL,
	   CONSTRAINT [FK_Register_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID]) ,
	   CONSTRAINT [FK_Register_BillID] FOREIGN KEY ([BillID]) REFERENCES [Bill]([BillID]) 
)
