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


        public ActionResult Index(int? page)
        {
            return View(base.BaseIndex<BillViewCls>(page, "Customer b inner join Bill c on b.CustomerID=c.CustomerID "));

        }


        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            return View(base.BaseCreateEdit<BillViewCls>(id, "BillID"));
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillID,CustomerID,NoOfGuest,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,TotalDays,PeriodToStay,BillReceiptNo,PaidInForeignIndian,CheckInTime,CheckOutTime,CGST,SGST,IGST")] BillViewCls bill)
        {
          return  base.BaseSave<BillViewCls>(bill, bill.BillID > 0);          
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
