using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMonte.Controllers
{
    [Authorize(Roles ="Boss,hr")]
    public class BillNonResdController : EAController
    {                
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;            
            return View("Index", base.BaseIndex<BillIndex>(page, "b.NonResdCust, b.BDate, b.BillID, b.BillNo, Canceled", "Bill b where CustomerId is NULL order by BillID desc"));
        }
        

        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            var vwdata = base.BaseCreateEdit<Bill>(id, "BillID");
            
            return View(vwdata);
        }
        
        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillID,BillNo,BDate,BillReceiptNo,PaidInForeignIndian,CGST,SGST,IGST,EncashCertDetails,Remarks,GSTExport, NonResdCust,NRCgst")] Bill bill)
        {
            using (var transaction = db.GetTransaction())
            {
                try
                {
                    //Save the Bill record
                    int b;
                    bill.TotalDays = 0;
                    if (bill.BillID > 0)
                    {
                        db.Update(bill);
                        b = bill.BillID;
                    }
                    else
                    {                        
                        b = (int)db.Insert(bill);                
                    }

                    //Insert Placeholder BillDetail records, but first delete the orphans (if any)            
                    var vwdata = db.Query<BillDetail>("where BillID=@0", b);
                    var killbill = vwdata.ToArray();
                    foreach (var item in killbill)
                    {
                        db.Delete<BillDetail>(item.BillDetailID);
                    }
                                
                    db.Insert(new BillDetail { BillID = b, Date = bill.BDate, GST = 0 });
                            

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

        

        /// <summary>
        /// Create BillDetail records
        /// </summary>
        /// <param name="id">Bill ID</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            var viewdata = new BillScreen();
            viewdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", id);            
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0  order by [Date]", viewdata.Bill.BillID);
            
            return View("BillandDetails", viewdata);
        }

        public ActionResult ManageDetails (int? id)
        {
            var viewdata = new BillScreen();
            viewdata.FrmEdt = BaseCreateEdit<BillDetail>(id, "BillDetailID");
            viewdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", viewdata.FrmEdt.BillID);            
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0", viewdata.Bill.BillID);

            return View("BillandDetails", viewdata);
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageDetails([Bind(Include = "BillDetailID,BillID,Date,Miscelleneous,Other1,Other2,Other3,Other4")] BillDetail billdetail)
        {            
            var Billrec = db.FirstOrDefault<Bill>("where BillID = @0", billdetail.BillID);
            var TaxableValue = (billdetail.Miscelleneous ?? 0) + (billdetail.Other1 ?? 0) + (billdetail.Other2 ?? 0) + (billdetail.Other3 ?? 0) + (billdetail.Other4 ?? 0);

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
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0  order by [Date]", viewdata.Bill.BillID);
            ViewBag.AdvanceAmt = db.Single<decimal>("Select coalesce(sum(Amount),0) from Receipt where BillNo = @0", id);

            return View("Print", viewdata);
        }

        public ActionResult Cancel(int? id)
        {
            var viewdata = new BillScreen();

            viewdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", id);
            
            viewdata.BillDetails = db.Fetch<BillDetail>("where BillID =@0  order by [Date]", viewdata.Bill.BillID);
            ViewBag.Receipts = db.Query<Receipt>("Select * from Receipt where BillNo = @0", id);

            return View("Cancel", viewdata);
        }

        [HttpPost]
        public ActionResult Cancel(int KillBill)
        {
            var kb = db.FirstOrDefault<Bill>("where BillID = @0", KillBill);
            kb.Canceled = true;
            db.Update(kb);
            return RedirectToAction("Index", "BillNonResd");
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
