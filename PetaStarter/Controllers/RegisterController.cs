using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace DeMonte.Controllers
{
    [Authorize(Roles = "Boss,hr")]
    public class RegisterController : EAController
    {
        // GET: Clients
        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<RegisterViewCls>(page, "Register where CustomerID like '%" + PropName + "%'"));
        }

        public ActionResult Filter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewRegister(DateTime FromDt, DateTime ToDt)
        {
            db.Execute("exec GetRegister @0, @1", FromDt, ToDt);
            var vwData = db.Fetch<RegisterViewCls>("Select * from RptRegister");
            
            return View(vwData);
        }


        //// GET: Clients/Create
        //public ActionResult Manage(int? id)
        //{
        //    return View(base.BaseCreateEdit<RegisterViewCls>(id, "RegisterID"));
        //}

        //// POST: Customer/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Manage([Bind(Include = "RegisterID,CustomerID,BillID,EncashmentCertificate,Remarks")] RegisterViewCls register)
        //{
        //    return base.BaseSave<RegisterViewCls>(register,register.RegisterID > 0);
        //}

        //// GET: Vouchers/Details
        //public ActionResult Details(int? id)
        //{

        //    return View(base.BaseCreateEdit<Register>(id, "RegisterID"));
        //}

       

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
