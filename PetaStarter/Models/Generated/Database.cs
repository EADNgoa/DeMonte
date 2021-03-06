




















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `DefaultConnection`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=(localdb)\ProjectsV13;Initial Catalog=DeMonte;Integrated Security=True;`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace DeMonte
{

	public partial class Repository : Database
	{
		public Repository() 
			: base("DefaultConnection")
		{
			CommonConstruct();
		}

		public Repository(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			Repository GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static Repository GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new Repository();
        }

		[ThreadStatic] static Repository _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

	}
	



    

	[TableName("dbo.__MigrationHistory")]



	[PrimaryKey("MigrationId", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class __MigrationHistory  
    {



		[Column] public string MigrationId { get; set; }





		[Column] public string ContextKey { get; set; }





		[Column] public byte[] Model { get; set; }





		[Column] public string ProductVersion { get; set; }



	}

    

	[TableName("dbo.__RefactorLog")]



	[PrimaryKey("OperationKey", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class __RefactorLog  
    {



		[Column] public Guid OperationKey { get; set; }



	}

    

	[TableName("dbo.AspNetRoles")]



	[PrimaryKey("Id", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class AspNetRole  
    {



		[Column] public string Id { get; set; }





		[Column] public string Name { get; set; }



	}

    

	[TableName("dbo.AspNetUserClaims")]



	[PrimaryKey("Id")]




	[ExplicitColumns]

    public partial class AspNetUserClaim  
    {



		[Column] public int Id { get; set; }





		[Column] public string UserId { get; set; }





		[Column] public string ClaimType { get; set; }





		[Column] public string ClaimValue { get; set; }



	}

    

	[TableName("dbo.AspNetUserLogins")]



	[PrimaryKey("LoginProvider", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class AspNetUserLogin  
    {



		[Column] public string LoginProvider { get; set; }





		[Column] public string ProviderKey { get; set; }





		[Column] public string UserId { get; set; }



	}

    

	[TableName("dbo.AspNetUserRoles")]



	[PrimaryKey("UserId", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class AspNetUserRole  
    {



		[Column] public string UserId { get; set; }





		[Column] public string RoleId { get; set; }



	}

    

	[TableName("dbo.AspNetUsers")]



	[PrimaryKey("Id", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class AspNetUser  
    {



		[Column] public string Id { get; set; }





		[Column] public string Email { get; set; }





		[Column] public bool EmailConfirmed { get; set; }





		[Column] public string PasswordHash { get; set; }





		[Column] public string SecurityStamp { get; set; }





		[Column] public string PhoneNumber { get; set; }





		[Column] public bool PhoneNumberConfirmed { get; set; }





		[Column] public bool TwoFactorEnabled { get; set; }





		[Column] public DateTime? LockoutEndDateUtc { get; set; }





		[Column] public bool LockoutEnabled { get; set; }





		[Column] public int AccessFailedCount { get; set; }





		[Column] public string UserName { get; set; }





		[Column] public DateTime DateCreated { get; set; }





		[Column] public bool Disabled { get; set; }





		[Column] public DateTime? LastLogin { get; set; }



	}

    

	[TableName("dbo.Bill")]



	[PrimaryKey("BillID")]




	[ExplicitColumns]

    public partial class Bill  
    {



		[Column] public int BillID { get; set; }





		[Column] public int? CustomerID { get; set; }





		[Column] public int? NoOfGuest { get; set; }





		[Column] public DateTime? DateArrivalTime { get; set; }





		[Column] public DateTime? DateDepartureTime { get; set; }





		[Column] public decimal? ChargesPerDay { get; set; }





		[Column] public int? RoomNo { get; set; }





		[Column] public int? TotalDays { get; set; }





		[Column] public int? BillReceiptNo { get; set; }

		[Column] public decimal? PaidInForeignIndian { get; set; }

		[Column] public decimal? CGST { get; set; }





		[Column] public decimal? SGST { get; set; }





		[Column] public decimal? IGST { get; set; }



	}

    

	[TableName("dbo.BillDetail")]

	[PrimaryKey("BillDetailID")]

	[ExplicitColumns]

    public partial class BillDetail  
    {



		[Column] public int BillDetailID { get; set; }





		[Column] public int? BillID { get; set; }





		[Column] public DateTime? Date { get; set; }





		[Column] public decimal? ExtraPerson { get; set; }





		[Column] public decimal? GST { get; set; }





		[Column] public decimal? Miscelleneous { get; set; }





		[Column] public decimal? Other1 { get; set; }





		[Column] public decimal? Other2 { get; set; }





		[Column] public decimal? Other3 { get; set; }





		[Column] public decimal? Other4 { get; set; }





		[Column] public decimal? Total { get; set; }



	}

    

	[TableName("dbo.Customer")]



	[PrimaryKey("CustomerID")]




	[ExplicitColumns]

    public partial class Customer  
    {



		[Column] public int CustomerID { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string PassportNo { get; set; }





		[Column] public DateTime? DateIssue { get; set; }





		[Column] public DateTime? DateExpiry { get; set; }





		[Column] public string PhotograghID { get; set; }



	}

    

	[TableName("dbo.Receipt")]



	[PrimaryKey("ReceiptID")]




	[ExplicitColumns]

    public partial class Receipt  
    {



		[Column] public int ReceiptID { get; set; }





		[Column] public DateTime? TDate { get; set; }





		[Column] public string Name { get; set; }





		[Column] public decimal? Amount { get; set; }





		[Column] public string ChequeNo { get; set; }





		[Column] public DateTime? ChqDate { get; set; }





		[Column] public string DrawnOn { get; set; }





		[Column] public string RoomNo { get; set; }





		[Column] public int? BillNo { get; set; }





		[Column] public int ReceiptTypeID { get; set; }



	}

    

	[TableName("dbo.ReceiptTypes")]



	[PrimaryKey("ReceiptTypeID")]




	[ExplicitColumns]

    public partial class ReceiptType  
    {



		[Column] public int ReceiptTypeID { get; set; }





		[Column] public string Type { get; set; }



	}

    

	[TableName("dbo.Register")]



	[PrimaryKey("RegisterID", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class Register  
    {



		[Column] public int RegisterID { get; set; }





		[Column] public int? CustomerID { get; set; }





		[Column] public int? BillID { get; set; }





		[Column] public string EncashmentCertificate { get; set; }





		[Column] public string Remarks { get; set; }



	}

    

	[TableName("dbo.Voucher")]



	[PrimaryKey("VoucherID")]




	[ExplicitColumns]

    public partial class Voucher  
    {



		[Column] public int VoucherID { get; set; }





		[Column] public DateTime? TDate { get; set; }





		[Column] public string PayTo { get; set; }





		[Column] public decimal? Amount { get; set; }





		[Column] public string OnAccountOf { get; set; }





		[Column] public string ChequeNo { get; set; }





		[Column] public string DrawnOn { get; set; }



	}


}
