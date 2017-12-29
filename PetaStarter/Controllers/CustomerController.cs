using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMonte.Controllers
{
    [Authorize(Roles = "Boss,hr")]
    public class CustomerController : EAController
    {
        // GET: Clients

        public ActionResult Index(int? page, string PropName)
        {
            if (PropName?.Length > 0) page = 1;
            return View("Index", base.BaseIndex<CustomerViewCls>(page, "Customer where Name like '%" + PropName + "%' order by CustomerID desc"));
        }



        // GET: Clients/Create
        public ActionResult Manage(int? id)
        {
            var customer = base.BaseCreateEdit<Customer>(id, "CustomerID");

            if (id != null) {
                CustomerViewCls res = new CustomerViewCls
                {
                    CustomerID = customer.CustomerID,
                    Name = customer.Name,
                    Address = customer.Address,
                    GSTNo =customer.GSTNo,
                    PassportNo = customer.PassportNo,
                    DateIssue = customer.DateIssue,
                    PhotographID = customer.PhotograghID,
                    DateExpiry = customer.DateExpiry
                };
                return View(res);
            } else
            {
                return View();
            }
        }


        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Include = "CustomerID,Name,Address, PassportNo, DateIssue,DateExpiry,PhotographID,GSTNo, UploadedFile")] CustomerViewCls customer)
        {
                Customer res = new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = customer.Name,
                    Address= customer.Address,
                    GSTNo=customer.GSTNo,
                    PassportNo= customer.PassportNo,
                    DateIssue= customer.DateIssue,
                    DateExpiry= customer.DateExpiry
                };

            if (customer.UploadedFile != null || customer.CustomerID > 0)
            {
                if (customer.UploadedFile != null)
                {
                    string fn = customer.UploadedFile.FileName.Substring(customer.UploadedFile.FileName.LastIndexOf('\\') + 1);
                    fn = customer.CustomerID + "_" + fn;
                    //string SavePath = System.IO.Path.Combine(Server.MapPath("~/IDs"), fn);
                    //customer.UploadedFile.SaveAs(SavePath);

                    System.Drawing.Bitmap upimg = new System.Drawing.Bitmap(customer.UploadedFile.InputStream);
                    System.Drawing.Bitmap svimg = MyExtensions.CropUnwantedBackground(upimg);
                    svimg.Save(System.IO.Path.Combine(Server.MapPath("~/IDs"), fn));

                    res.PhotograghID = fn;
                }
                else
                {
                    res.PhotograghID = customer.PhotographID;
                }                                
            }
            base.BaseSave<Customer>(res, res.CustomerID > 0);

            return RedirectToAction("Index");

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
