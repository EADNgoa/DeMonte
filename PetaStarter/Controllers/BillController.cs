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
        public ActionResult Manage(int? id, int? CustID)
        {
            var vwdata = base.BaseCreateEdit<Bill>(id, "BillID");
            if (CustID.HasValue)                
                ViewBag.CustomerID = CustID;
            else            
                ViewBag.CustomerID = vwdata.CustomerID;

            return View(vwdata);
        }
        //public ActionResult Manage(int? id)

        //{
        //    return View(base.BaseCreateEdit<Bill>(id, "Bill"));
        //}
        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "BillID,CustomerID,NoOfGuest,DateArrivalTime,DateDepartureTime,ChargesPerDay,RoomNo,TotalDays,BillReceiptNo,PaidInForeignIndian,CGST,SGST,IGST")] Bill bill)
        {
        
            if (bill.DateDepartureTime.HasValue && bill.DateArrivalTime.HasValue)
                bill.TotalDays = ((DateTime)bill.DateDepartureTime - (DateTime)bill.DateArrivalTime).Days;
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
