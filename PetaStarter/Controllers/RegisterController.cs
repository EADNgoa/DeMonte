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
            var vwData = db.Fetch<RegisterViewCls>("Select * from RptRegister order by RegisterNo");
            
            return View(vwData);
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
