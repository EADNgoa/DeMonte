CREATE PROCEDURE [dbo].[GetRegister]
	@StartDt Date,
	@EndDt Date
AS
	--Clear the tmp table
	Truncate table RptRegister

	--Fill in the part that can be done with a fast join
	insert Into RptRegister(BillID,BillNo, Name,Address,PassportDet,NoOfGuest,NoOfRooms,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,TotalDays, TotalCharges, PaidInForeignIndian,EncashCertDetails,Remarks,RegisterNo)
	select b.BillID, b.BillNo,c.Name, c.Address, CONCAT(c.PassportNo,' I: ',c.DateIssue,', E: ',c.DateExpiry) as PassportDet,
	b.NoOfGuest, 1 as NoOfRooms,b.DateArrivalTime,b.DateDepartureTime,COALESCE(b.ChargesPerDay,0),b.RoomNo,COALESCE(b.TotalDays,1), COALESCE((b.TotalDays*b.ChargesPerDay),0), b.PaidInForeignIndian, b.EncashCertDetails, b.Remarks, b.RegisterNo
	from Customer c 
	INNER Join Bill b ON c.CustomerID=b.CustomerID
	Where  b.RoomNo IS NOT NULL and b.Canceled=0 and b.DateArrivalTime between @StartDt and @EndDt

	--calc the GST
	Update r SET r.GST = 
	(SELECT SUM(GST) FROM BillDetail bd
		WHERE bd.BillID = r.BillID)
	FROM RptRegister r
	
	--calc the Accomodation charges
	Update r SET r.TotalCharges = r.TotalCharges +
	(SELECT COALESCE(SUM(ExtraPerson),0) FROM BillDetail bd
		WHERE bd.BillID = r.BillID)
	FROM RptRegister r
	
	--Fetch the receipts (if any)
	CREATE TABLE #Rcpts (
	BillNo INT NOT NULL PRIMARY KEY,
	Recpts VARCHAR(50)
	)
	
	Insert Into #Rcpts
	select BillNo, Recpts = STUFF
	(
		(
			SELECT DISTINCT ', ' + CAST(ReceiptID as VARCHAR(50))
			FROM Receipt r2
			WHERE r1.BillNo=r2.BillNo
			FOR XML PATH('')
		),1,1,''
	)
	FROM Receipt r1
	GROUP BY BillNo

	Update r SET r.BillReceiptNo= 
	(SELECT c.Recpts FROM #Rcpts c
		WHERE r.BillID=c.BillNo)
	FROM RptRegister r

	
