CREATE TABLE [dbo].[RptRegister] (
    [BillID]              INT             NOT NULL,
    [Name]                VARCHAR (150)   NULL,
    [Address]             VARCHAR (350)   NULL,
    [PassportDet]         VARCHAR (100)   NULL,
    [NoOfGuest]           INT             NULL,
    [NoOfRooms]           INT             NULL,
    [DateArrivalTime]     DATETIME        NULL,
    [DateDepartureTime]   DATETIME        NULL,
    [ChargesPerDay]       DECIMAL (15, 2) NULL,
    [RoomNo]              VARCHAR (50)    NULL,
    [TotalDays]           INT             NULL,
    [TotalCharges]        DECIMAL (15, 2) NULL,
    [PaidInForeignIndian] DECIMAL (15, 2) NULL,
    [BillReceiptNo]       VARCHAR (50)    NULL,
    [GST]                 DECIMAL (12, 2) NULL,
    [EncashCertDetails]   VARCHAR (100)   NULL,
    [Remarks]             VARCHAR (100)   NULL,
    [RegisterNo]          INT             NULL,
    PRIMARY KEY CLUSTERED ([BillID] ASC)
);

