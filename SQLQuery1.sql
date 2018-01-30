select b.BillID, c.Name, c.Address, CONCAT(c.PassportNo,' I: ',c.DateIssue,', E: ',c.DateExpiry) as PassportDet,
	b.NoOfGuest, 1 as NoOfRooms,b.DateArrivalTime,b.DateDepartureTime,COALESCE(b.ChargesPerDay,0),b.RoomNo,COALESCE(b.TotalDays,1),
	COALESCE((b.TotalDays*b.ChargesPerDay),0)
	, b.PaidInForeignIndian, b.EncashCertDetails, b.Remarks, b.RegisterNo
	from Customer c 
	INNER Join Bill b ON c.CustomerID=b.CustomerID
	Where  b.RoomNo IS NOT NULL and b.Canceled=0 and b.DateArrivalTime between @StartDt and @EndDt
