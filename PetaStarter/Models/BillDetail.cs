using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeMonte
{
    public class BillDetail
    {
        public int BillID { get; set; }
        public int CustomerID { get; set; }
        public int NoOfGuest { get; set; }
        public DateTime DateArrivalTime { get; set; }
        public DateTime DateDepartureTime { get; set; }
        public decimal ChargesPerDay { get; set; }
        public int RoomNo { get; set; }
        public int TotalDays { get; set; }
        public int PeriodToStay { get; set; }
        public int BillReceiptNo { get; set; }
        public decimal AmountGSTTax { get; set; }
        public decimal PaidInForeignIndian { get; set; }

    }
}