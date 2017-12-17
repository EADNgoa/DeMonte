using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc; for dynamic roles
//using Microsoft.AspNet.Identity;for dynamic roles
//using DeMonte.Models;for dynamic roles
//using Microsoft.AspNet.Identity.EntityFramework;for dynamic roles

namespace DeMonte
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }

    [MetadataType(typeof(VoucherMetadata))]
    public partial class Voucher
    {
    }

    [MetadataType(typeof(ReceiptMetadata))]
    public partial class Receipt
    {
    }

    [MetadataType(typeof(BillMetadata))]
    public partial class Bill
    {
    }

    [MetadataType(typeof(RegisterMetadata))]
    public partial class Register
    {
       
    }
    [MetadataType(typeof(BillDetailMetadata))]
    public partial class BillDetail
    {
        public decimal DayTotal { get { return (ExtraPerson ?? 0) + (Miscelleneous ?? 0) + (GST ?? 0) + (Other1 ?? 0) + (Other2 ?? 0) + (Other3 ?? 0) + (Other4 ?? 0);  } } 
    }

    [MetadataType(typeof(CustomerMetadata))]
    public class CustomerViewCls
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PassportNo { get; set; }
        public DateTime? DateIssue { get; set; }
        public DateTime? DateExpiry { get; set; }
        public string PhotographID { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
    }


    [MetadataType(typeof(RegisterMetadata))]
    public class RegisterViewCls
    {
        public int RegisterID { get; set; }
        public int CustomerID { get; set; }

        public int BillID { get; set; }
        public int EncashmentCash { get; set; }
        public int Remarks { get; set; }
    }
    public class BillScreen
    {
        public Customer Customer { get; set; }
        public Bill Bill { get; set; }
        public BillDetail FrmEdt { get; set; }
        public IEnumerable<BillDetail> BillDetails { get; set; }
    }

    public class BillIndex
    {
        public int BillID { get; set; }
        public DateTime BDate { get; set; }
        public string Customer { get; set; }
        public int NoOfGuests { get; set; }
        public DateTime ArrivalDt { get; set; }
        public int TotalDays { get; set; }
        public int RoomNo { get; set; }        
    }



    //[MetadataType(typeof(ConfigMetadata))]
    //public partial class Config
    //{
    //}


}