using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace DeMonte.Controllers
{
    public class VoucherController : EAController
    {
        // GET: Clients
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<Voucher>(page, "Voucher where PayTo like '%" + PropName + "%'"));
        }



        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            return View(base.BaseCreateEdit<Voucher>(id, "VoucherID"));
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "VoucherID,TDate,PayTo,Amount,OnAccountOf,ChequeNo,DrawnOn")] Voucher voucher)
        {
            return base.BaseSave<Voucher>(voucher,voucher.VoucherID > 0);
        }

        // GET: Vouchers/Details
        public ActionResult Details(int? id)
        {

            return View(base.BaseCreateEdit<Voucher>(id, "VoucherID"));
        }

        // GET: Voucher/Details
        //      public ActionResult Details([Bind(Include = "VoucherID,TDate,PayTo,Amount,OnAccountOf,ChequeNo,DrawnOn")] Voucher voucher)
        //     {
        //        return base.B
        //     }

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
