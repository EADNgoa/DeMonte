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
        public ActionResult Index(int? page, int BillID)
        {
            page = 1;
            ViewBag.BillNo = BillID;
            return View("Index", base.BaseIndex<Receipt>(page, "Receipt where  BillNo =" + BillID  + " order by ReceiptID desc"));
        }



        // GET: Clients/Create
        public ActionResult Manage(int? id, int BillID)
        {
            ViewBag.ReceiptTypeID = new SelectList(db.Fetch<ReceiptType>("Select * from ReceiptTypes"), "ReceiptTypeID", "Type");
            var vwdata = base.BaseCreateEdit<Receipt>(id, "ReceiptID");
            ViewBag.BillNo = BillID;
            return View(vwdata);
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "ReceiptID,TDate,Name,Amount,ChequeNo,ChqDate,DrawnOn, BillNo,ReceiptTypeID")] Receipt receipt)
        {
            receipt.RoomNo = db.Single<string>("Select RoomNo from Bill Where BillID = @0", receipt.BillNo);

            var res = (receipt.ReceiptID>0) ? db.Update(receipt) : db.Insert(receipt);

            return RedirectToAction("Index",new { BillID = receipt.BillNo});
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
