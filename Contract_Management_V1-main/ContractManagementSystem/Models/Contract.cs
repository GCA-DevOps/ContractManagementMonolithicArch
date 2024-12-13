using ContractManagementSystem.Models.ContractManagement;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models
{
    public enum ContractStatus
    {
        Active, Pending, Rejected, Expired, Draft
    }
    public enum Predefined_ContractTypes
    {
        Joint_Venture,
        Non_Disclosure,
        Custom_Contract,
        Franchise,
        Intellectual_Property,
        Lease_Agreement,
        licensing_Agreement,
        Non_Complete,
        Permanent_Employment,
        Temporary_employment,
        Service,
        Procurement,
        Vendor,
    }

    public class Contract
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [Required]

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        /* [Required]
         public required string Parties { get; set; }*/
        [Required]
        public string? RequesterName { get; set; }
        [Required]
        [Column("RequestedDepartment")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }



        [Required]
        public string? ContractType { get; set; }




        [Column("CreatorId")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public bool IsApprovalSent { get; set; } = false;
        [Required]
        public ContractStatus Status { get; set; } = ContractStatus.Draft;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ICollection<Approval>? Approval { get; set; }
        public ICollection<ContractParty>? ContractParties { get; set; } // Navigation property
        public ICollection<Payment>? Payments { get; set; }
        public TerminationClause? TerminationClause { get; set; }
        public decimal ContractValue { get;  set; }
        public string ContractParty { get; set; }
        









    }
}
