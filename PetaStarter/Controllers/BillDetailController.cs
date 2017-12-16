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


        public ActionResult Details(int? id)
        {
            BillScreen vwdata = new BillScreen();
            vwdata.Bill = db.FirstOrDefault<Bill>("where BillID=@0", id);
            vwdata.Customer = db.FirstOrDefault<Customer>("where CustomerID=@0", vwdata.Bill.CustomerID);
            vwdata.BillDetail = db.Fetch<BillDetail>("where BillID =@0", vwdata.Bill.BillID);

            return View(vwdata);
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
            return base.BaseSave<BillDetail>(billdetail, billdetail.BillDetailID > 0);
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
