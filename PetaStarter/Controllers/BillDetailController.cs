using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMonte.Controllers
{
    public class BillDetailController : EAController
    {
        // GET: Clients
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<BillDetail>(page, "BillDetail where BillDetailID like '%" + PropName + "%'"));
        }


        public ActionResult Details(int? BID)
        {

            BillScreen vwdata = new BillScreen();
            ViewBag.BillID = BID;
            vwdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", BID);
            vwdata.Customer = db.FirstOrDefault<Customer>("where CustomerID=@0", vwdata.Bill.CustomerID);
            vwdata.BillDetail = db.Fetch<BillDetail>("where BillID =@0", vwdata.Bill.BillID);

            return View(vwdata);
        }

        [HttpPost]
        public ActionResult Details([Bind(Include = "BillDetailID,BillID,Date,ExtraPerson,GST,Miscelleneous,Other1,Other2,Other3,Other4,Total")] BillDetail billdetail)
        {
            //BillScreen Data = new BillScreen();
            var GetItem = db.FirstOrDefault<BillDetail>("select * from BillDetail where BillDetailID=@0 and BillID=@0", billdetail.BillDetailID,billdetail.BillID);
             
            if (GetItem==null)
            {
                var Billrec = db.FirstOrDefault<Bill>("where BillID = @0", billdetail.BillID);
                var TaxableValue = Billrec.ChargesPerDay + billdetail.ExtraPerson;

                var igst = (TaxableValue * (Billrec.IGST ?? 0) / 100);
                var cgst = (TaxableValue * (Billrec.CGST ?? 0) / 100);
                var sgst = (TaxableValue * (Billrec.SGST ?? 0) / 100);
                billdetail.GST = igst + cgst + sgst;

                var other1 = billdetail.Other1;
                var other2 = billdetail.Other2;
                var other3 = billdetail.Other3;
                var other4 = billdetail.Other4;
                var gst = billdetail.GST;
                var miscelleneous =  billdetail.Miscelleneous;

                //billdetail.Total = billdetail.Other1 + billdetail.Other2+billdetail.Other3
                //    +billdetail.Other4+billdetail.GST+billdetail.Miscelleneous;
                var total = (other1 ?? 0)+ (other2 ?? 0)+ (other3??0) +( other4 ??0)+( gst ??0)+( miscelleneous ??0);
                billdetail.Total = total;
                //ViewBag.Total = db.Fetch<BillDetail>("");
                base.BaseSave<BillDetail>(billdetail, billdetail.BillDetailID > 0);
            }
            else
            {
                
                db.Update("BillDetail", "BillDetailID", new
                {

                    Date = billdetail.Date,
                    ExtraPerson = billdetail.ExtraPerson,
                    GST = billdetail.GST,
                    Miscelleneous = billdetail.Miscelleneous,
                    Other1 = billdetail.Other1,
                    Other2 = billdetail.Other2,
                    Other3 = billdetail.Other3,
                    Other4 = billdetail.Other4,
                     Total = billdetail.GST + billdetail.Miscelleneous + billdetail.Other1 + billdetail.Other2 + billdetail.Other3 +billdetail.Other4+billdetail.Total}, GetItem.BillDetailID);
                
            }
            return RedirectToAction("Details", new { BID = billdetail.BillID });
      
        }
        // GET: Clients/Create
        public ActionResult Manage(int? id, int? BID)
        {
            var vwdata = base.BaseCreateEdit<BillDetail>(id, "BillDetailID");
            if (BID.HasValue)
                ViewBag.BillID = BID;
            else
                ViewBag.BillID = vwdata.BillID;
            return View(vwdata);
        }
      

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillDetailID,BillID,Date,ExtraPerson,GST,Miscelleneous,Other1,Other2,Other3,Other4,Total")] BillDetail billdetail)
        {
            var Billrec = db.FirstOrDefault<Bill>("where BillID = @0", billdetail.BillID);
            var TaxableValue = Billrec.ChargesPerDay + billdetail.ExtraPerson;

            var igst = (TaxableValue * (Billrec.IGST ?? 0) / 100);
            var cgst = (TaxableValue * (Billrec.CGST ?? 0) / 100);
            var sgst = (TaxableValue * (Billrec.SGST ?? 0) / 100);
            billdetail.GST = igst + cgst + sgst;
           var total = billdetail.Other1 + billdetail.Other2 + billdetail.Other3
                    + billdetail.Other4 + billdetail.GST + billdetail.Miscelleneous ;
            billdetail.Total = total;
            return base.BaseSave<BillDetail>(billdetail, billdetail.BillDetailID > 0);
        }

        public ActionResult PrintDetail(int?PID)
        {

            BillScreen vwdata = new BillScreen();
            ViewBag.BillID = PID;
            vwdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", PID);
            vwdata.Customer = db.FirstOrDefault<Customer>("where CustomerID=@0", vwdata.Bill.CustomerID);
            vwdata.BillDetail = db.Fetch<BillDetail>("where BillID =@0", vwdata.Bill.BillID);
           

            return View(vwdata);
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
