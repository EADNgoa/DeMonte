USE [DeMonte]
GO

/****** Object: SqlProcedure [dbo].[spregister] Script Date: 01/22/18 9:41:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE spregister
@fromdate DateTime,
@todate DateTime

 As
 begin
 delete  from Register
 INSERT INTO Register(Name,Address,PassportNo,DateIssue,DateExpiry,NoOfGuest,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,TotalDays,PaidInForeignIndian,AdvReceiptNo)
	 Select c.Name,c.Address,c.PassportNo,c.DateIssue,c.DateExpiry,b.NoOfGuest,b.DateArrivalTime,b.DateDepartureTime,b.ChargesPerDay,b.RoomNo,b.TotalDays,b.PaidInForeignIndian,b.AdvReceiptNo
	 from Customer c 
	 inner join Bill b on c.customerID=b.customerID 
	 inner join BillDetail bd on bd.BillID=B.BillID
	 where b.TransactionDate between @fromdate and @todate


 DECLARE @Total decimal(10,2) 
set  @Total = (select sum(ExtraPerson) from BillDetail) 
 update Register set TotalAccomodamation=ChargesPerDay*TotalAccomodamation*(@Total) 
  end