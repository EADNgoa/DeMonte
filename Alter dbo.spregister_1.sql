spregister '12/03/17','19/03/17'

alter PROCEDURE spregister
@fromdate Datetime,
@todate Datetime
 As
 begin
	 Select * from Customer b inner join Bill c on b.customerID=c.customerID  where TransactionDate between @fromdate and @todate
 end
 select *from Bill, Customer