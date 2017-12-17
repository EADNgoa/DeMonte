using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMonte.Controllers
{
    public class BillController : EAController
    {                
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<BillIndex>(page, "b.BillID, c.Name as Customer,b.NoOfGuest, b.DateArrivalTime as ArrivalDt, TotalDays, RoomNo", "Bill b, Customer c where b.customerID=c.CustomerID order by BillID desc"));
        }
     
        // GET: Clients/Create
        public ActionResult Manage(int? id, int? CustID)
        {
            var vwdata = base.BaseCreateEdit<Bill>(id, "BillID");
            if (CustID.HasValue)                
                ViewBag.CustomerID = CustID;
            else            
                ViewBag.CustomerID = vwdata.CustomerID;

            return View(vwdata);
        }
        
        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillID,BDate,CustomerID,NoOfGuest,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,BillReceiptNo,PaidInForeignIndian,CGST,SGST,IGST,EncashCertDetails,Remarks")] Bill bill)
        {
            using (var transaction = db.GetTransaction())
            {
                try
                {
                    //Save the Bill record
                    int b;
                    bill.TotalDays = ((DateTime)bill.DateDepartureTime.Value.Date - (DateTime)bill.DateArrivalTime.Value.Date).Days;
                    if (bill.BillID > 0)
                    {
                        db.Update(bill);
                        b = bill.BillID;
                    }
                    else
                    {
                        bill.BDate = DateTime.Today.Date;
                        b = (int)db.Insert(bill);                
                    }

                    //Insert Placeholder BillDetail records, but first delete the orphans (if any)            
                    var vwdata = db.Query<BillDetail>("where BillID=@0 and ((date< @1) or (date >= @2))", b, bill.DateArrivalTime.Value.Date, bill.DateDepartureTime.Value.Date);
                    var killbill = vwdata.ToArray();
                    foreach (var item in killbill)
                    {
                        db.Delete<BillDetail>(item.BillDetailID);
                    }
                                
                    if (bill.DateDepartureTime.HasValue && bill.DateArrivalTime.HasValue)
                    {
                        var existingDays = db.Query<BillDetail>("where BillID=@0 and ((date>= @1) and (date <= @2))", b, bill.DateArrivalTime.Value.Date, bill.DateDepartureTime.Value.Date);
                        DateTime dt = (DateTime)bill.DateArrivalTime.Value.Date;
                        for (int i = 0; i < bill.TotalDays; i++)
                        {
                            if (!existingDays.Any(bd => bd.Date==dt))//Insert the placeholders only if they dont exist already
                                db.Insert(new BillDetail { BillID = b, Date = dt });
                            dt = dt.AddDays(1);
                        }
                    }

                    transaction.Complete();
                    return RedirectToAction("Details", new { id = b });
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                    throw ex;
                }
            }

        }


        public ActionResult FetchEndangeredBillDets(int? BillID, DateTime? NewStart, DateTime? NewStop)
        {
            if (BillID.HasValue && NewStart.HasValue && NewStop.HasValue)
            {
                var vwdata = db.Query<BillDetail>("where BillID=@0 and ((date< @1) or (date > @2)) order by [Date]", BillID, NewStart.Value.Date, NewStop.Value.Date);
                return PartialView("DeleConfirm", vwdata);
            }
            return null;
        }


        /// <summary>
        /// Create BillDetail records
        /// </summary>
        /// <param name="id">Bill ID</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            var viewdata = new BillScreen();
            viewdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", id);
            viewdata.Customer = db.FirstOrDefault<Customer>("where CustomerID=@0", viewdata.Bill.CustomerID);
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0  order by [Date]", viewdata.Bill.BillID);
            
            return View("BillandDetails", viewdata);
        }

        public ActionResult ManageDetails (int? id)
        {
            var viewdata = new BillScreen();
            viewdata.FrmEdt = BaseCreateEdit<BillDetail>(id, "BillDetailID");
            viewdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", viewdata.FrmEdt.BillID);
            viewdata.Customer = db.FirstOrDefault<Customer>("where CustomerID=@0", viewdata.Bill.CustomerID);
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0", viewdata.Bill.BillID);

            return View("BillandDetails", viewdata);
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageDetails([Bind(Include = "BillDetailID,BillID,Date,ExtraPerson,Miscelleneous,Other1,Other2,Other3,Other4")] BillDetail billdetail)
        {
            
            var Billrec = db.FirstOrDefault<Bill>("where BillID = @0", billdetail.BillID);
            var TaxableValue = Billrec.ChargesPerDay + (billdetail.ExtraPerson??0);

            var igst = (TaxableValue * (Billrec.IGST ?? 0) / 100);
            var cgst = (TaxableValue * (Billrec.CGST ?? 0) / 100);
            var sgst = (TaxableValue * (Billrec.SGST ?? 0) / 100);
            billdetail.GST = igst + cgst + sgst;
            db.Update(billdetail);
            return RedirectToAction("Details", new { id = billdetail.BillID });
        }

        public ActionResult Print(int? id)
        {
            var viewdata = new BillScreen();
                        
            viewdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", id);
            viewdata.Customer = db.FirstOrDefault<Customer>("where CustomerID=@0", viewdata.Bill.CustomerID);
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0  order by [Date]", viewdata.Bill.BillID);
            ViewBag.AdvanceAmt = db.Single<decimal>("Select sum(Amount) from Receipt where BillNo = @0", id);

            return View("Print", viewdata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
