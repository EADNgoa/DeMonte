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
            return View(base.BaseCreateEdit<BillDetail>(id, "Customer C inner JOIN Bill B ON C.CustomerID = B.CustomerID inner JOIN BillDetail bd ON B.BillID = bd.BillID where BillDetailID="+ id ));
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
