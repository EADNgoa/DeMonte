CREATE PROCEDURE [dbo].[ExpSalesxl]
	@Mon Int,
	@Yr Int
AS
	--Clear the tmp table
	Truncate table ExpSales

	--Fill in the part that can be done with a fast join: for resident custoemrs
	insert Into ExpSales(BillID,BillNo,TDate, GSTNo, Name,AddLineBreaks, TotalValue)
	select b.BillID, b.BillNo,b.BDate,c.GSTNo, c.Name, c.Address, COALESCE((b.TotalDays*b.ChargesPerDay),0)
	from Customer c 
	INNER Join Bill b ON c.CustomerID=b.CustomerID
	Where b.GSTExport = 1 and b.Canceled=0 and MONTH(b.BDate)=@Mon and YEAR(b.BDate)=@Yr

	--Fill in the part that can be done with a fast join: for Non-resident custoemrs
	insert Into ExpSales(BillID,BillNo, TDate, GSTNo, Name,AddLineBreaks, TotalValue)
	select b.BillID,b.BillNo,b.BDate, b.NRCgst,b.NonResdCust,'',  0
	from Bill b 
	Where b.GSTExport = 1 and b.Canceled=0 and MONTH(b.BDate)=@Mon and YEAR(b.BDate)=@Yr and CustomerID IS NULL

	--calc the Accomodation charges
	Update r SET r.TotalValue= r.TotalValue +
	(SELECT COALESCE(SUM(ExtraPerson),0) FROM BillDetail bd
		WHERE bd.BillID = r.BillID)
	FROM ExpSales r

	--calc the Non-resident charges
	Update r SET r.TotalValue= r.TotalValue +
	(SELECT COALESCE(SUM(bd.Miscelleneous),0)+COALESCE(SUM(bd.Other1),0) +COALESCE(SUM(bd.Other2),0)+COALESCE(SUM(bd.Other3),0)+COALESCE(SUM(bd.Other4),0) FROM BillDetail bd
		WHERE bd.BillID = r.BillID)
	FROM ExpSales r

	--calc each type of GST
	Update r SET r.CGST= r.TotalValue * 
	(SELECT COALESCE(b.CGST,0)/100 FROM Bill b WHERE b.BillID=r.BillID )
	FROM ExpSales r
	
	Update r SET r.IGST= r.TotalValue * 
	(SELECT COALESCE(b.IGST,0)/100 FROM Bill b WHERE b.BillID=r.BillID )
	FROM ExpSales r

	Update r SET r.SGST= r.TotalValue * 
	(SELECT COALESCE(b.SGST,0)/100 FROM Bill b WHERE b.BillID=r.BillID )
	FROM ExpSales r

	--calc the total GST seperately as a counter-check
	Update r SET r.GST = 
	(SELECT SUM(GST) FROM BillDetail bd
		WHERE bd.BillID = r.BillID)
	FROM ExpSales r
	
	Update ExpSales SET TaxableSales = TotalValue-CGST-SGST-IGST
	