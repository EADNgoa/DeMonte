using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace DeMonte.Controllers
{
    [Authorize(Roles = "Boss,hr")]
    public class ExportController : EAController
    {
        /// <summary>
        /// Used to form a generic request screen for reports that need only mon and yr
        /// </summary>        
        private void MakeMonYrRq()
        {
            ViewBag.MonthBox = DeMonte.MyExtensions.MonthList();
            List<SelectListItem> slyr = new List<SelectListItem>();
            for (int i = 0; i < 10; i++)
            {
                slyr.Add(new SelectListItem { Value = DateTime.Today.AddYears(-i).Year.ToString(), Text = DateTime.Today.AddYears(-i).Year.ToString() });
            }
            ViewBag.YearBox = slyr;

        }

        /// <summary>
        /// Used for export to Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="output"></param>
        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }


        public ActionResult PayRegRq()
        {
            MakeMonYrRq();
            ViewBag.Title = "Export GST Sales";
            ViewBag.action = "PayRegRq";

            return View("PayRegRq");
        }


        // POST: redirects to Index to show Pay Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult PayRegRq(FormCollection fm)
        {
            if (ModelState.IsValid)            
                return RedirectToAction("SalesGST", new { mon = fm["PayMonth"], yr = fm["PayYear"] });
            
            return View();
        }

        public ActionResult SalesGST(int? mon, int? yr)
        {
            if (mon == null || yr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string monyr = MyExtensions.MonthFromInt((int)mon) + ' ' + yr;
            db.Execute("exec ExpSalesxl @0, @1", mon, yr);
            var ExpData = db.Query<ExpSale>("Select * from ExpSales");            

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=GST Sales " + monyr + ".xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            WriteTsv(ExpData.Select(r => new { r.TDate,r.GSTNo,r.Name,r.Address,r.BillNo,r.TaxableSales,r.IGST,r.CGST,r.SGST,r.TotalValue,r.GST }), Response.Output);
            Response.End();

            return RedirectToAction("Index", "Home");
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
