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
            ViewBag.ReceiptTypeID = new SelectList(db.Fetch<ReceiptType>("Select * from ReceiptTypes"), "ReceiptTypeID", "Type");
            return View(base.BaseCreateEdit<Receipt>(id, "ReceiptID"));
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "ReceiptID,TDate,Name,Amount,ChequeNo,ChqDate,DrawnOn,RoomNo,BillNo, ReceiptTypeID")] Receipt receipt)
        {
            return base.BaseSave<Receipt>(receipt,receipt.ReceiptID > 0);
        }

        public ActionResult Details(int? id)
        {
            var viewdata = base.BaseCreateEdit<Receipt>(id, "ReceiptID");
            ChangeNumbersToWords cntw = new ChangeNumbersToWords();
            ViewBag.recptAmt = cntw.changeToWords(viewdata.Amount.ToString());
            ViewBag.ReceiptTypeName = db.FirstOrDefault<string>("Select top 1 Type from ReceiptTypes where ReceiptTypeId = @0", viewdata.ReceiptTypeID);
            return View(viewdata);
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
