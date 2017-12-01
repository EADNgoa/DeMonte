using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMonte.Controllers
{
    public class BillController : EAController
    {
        // GET: Clients
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<BillDetail>(page, "Bill where BillID like '%" + PropName + "%'"));        

        }

        public ActionResult ViewBill(int? page)
        {            
            return View("Index", db.Fetch<BillDetail>("Select * from Customer b inner join Bill c on b.customerID=c.customerID "));

        }

        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            return View(base.BaseCreateEdit<BillDetail>(id, "BillID"));
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillID,CustomerID,NoOfGuest,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,TotalDays,PeriodToStay,BillReceiptNo,AmountGSTTax,PaidInForeignIndian")] BillDetail bill)
        {
            return base.BaseSave<BillDetail>(bill, bill.BillID > 0);
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
