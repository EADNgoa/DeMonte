CREATE PROCEDURE [dbo].[ExpSalesxl]
	@Mon Int,
	@Yr Int
AS
	--Clear the tmp table
	Truncate table ExpSales

	--Fill in the part that can be done with a fast join
	insert Into ExpSales(BillID,TDate, GSTNo, Name,AddLineBreaks, TotalValue)
	select b.BillID,b.BDate,c.GSTNo, c.Name, c.Address, COALESCE((b.TotalDays*b.ChargesPerDay),0)
	from Customer c 
	INNER Join Bill b ON c.CustomerID=b.CustomerID
	Where b.Canceled=0 and MONTH(b.BDate)=@Mon and YEAR(b.BDate)=@Yr

	--calc the Accomodation charges
	Update r SET r.TotalValue= r.TotalValue +
	(SELECT COALESCE(SUM(ExtraPerson),0) FROM BillDetail bd
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
	