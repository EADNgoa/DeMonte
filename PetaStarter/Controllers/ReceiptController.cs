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
      
        public ActionResult Manage(int? id, int? RID)
        {

            ViewBag.ReceiptTypeID = new SelectList(db.Fetch<ReceiptType>("Select * from ReceiptTypes"), "ReceiptTypeID", "Type");
            var vwdata = base.BaseCreateEdit<Receipt>(id, "ReceiptID");
           
            
            if (RID.HasValue)
                ViewBag.BillID = RID;
            else
                ViewBag.BillID = vwdata.BillID;
            return View(vwdata);
        }


        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "ReceiptID,TDate,Name,Amount,ChequeNo,ChqDate,DrawnOn,RoomNo,BillID, ReceiptTypeID")] Receipt receipt)
        {
         
            return base.BaseSave<Receipt>(receipt,receipt.ReceiptID > 0);
        }

        public ActionResult Details(int? RID)
        {
           
            var viewdata = base.BaseCreateEdit<Receipt>(RID, "ReceiptID");

            ViewBag.RoomNo = db.FirstOrDefault<int>("select RoomNo from Bill where BillID=@0", viewdata.BillID);
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
