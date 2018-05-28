using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace DeMonte.Controllers
{
    public class RegisterController : EAController
    {

        public ActionResult Index()
        {
           
            return View("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime fromdate, DateTime todate)
        {
            if (fromdate == null || todate == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                var exesp = db.Execute("exec GetRegister @0,@1", fromdate, todate);
                var vwData = db.Query<RptRegister>("Select * from RptRegister");
                return View("RegisterScreen", vwData);
            }
        }


        public ActionResult Details(int? BID)
        {

            var viewdata = base.BaseCreateEdit<RptRegister>(BID, "BillID");
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
