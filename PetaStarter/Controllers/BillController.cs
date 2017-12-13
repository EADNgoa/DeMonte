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

        //public ActionResult Index(int? page, string PropName)
        //{
        //    if (PropName?.Length > 0) page = 1;
        //    return View("Index", base.BaseIndex<BillViewCls>(page, "Bill where BillID like '%" + PropName + "%'"));
        //}
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<Bill>(page, "Bill"));
        }
        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            return View(base.BaseCreateEdit<Bill>(id, "BillID"));
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillID,CustomerID,NoOfGuest,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,TotalDays,PeriodToStay,BillReceiptNo,PaidInForeignIndian,CGST,SGST,IGST")] Bill bill)
        {
          return  base.BaseSave<Bill>(bill, bill.BillID > 0);          
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
