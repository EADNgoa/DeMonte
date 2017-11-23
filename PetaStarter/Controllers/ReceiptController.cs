using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMonte.Controllers
{
    public class ReceiptController : EAController
    {
        // GET: Clients
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<Receipt>(page, "Receipt where Name like '%" + PropName + "%'"));
        }



        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            return View(base.BaseCreateEdit<Receipt>(id, "ReceiptID"));
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "ReceiptID,TDate,Name,Amount,ChequeNo,ChqDate,DrawnOn,RoomNo,BillNo")] Receipt receipt)
        {
            return base.BaseSave<Receipt>(receipt,receipt.ReceiptID > 0);
        }

        public ActionResult Details(int? id)
        {
            return View(base.BaseCreateEdit<Receipt>(id, "ReceiptID"));
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
