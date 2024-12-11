using CMS.Domain.Entities.Approval;
using CMS.Domain.Entities.Authentication;
using CMS.Domain.Entities.Contract.ContractManagement;
using CMS.Domain.Entities.ContractManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CMS.Domain.Entities.Contract
{
    public enum ContractStatus
    {
        Active, Pending, Rejected, Expired, Draft
    }
    public enum Predefined_ContractTypes
    {
        [EnumMember(Value = "Joint_Venture")]
        Joint_Venture = 0,
        [EnumMember(Value = "Non_Disclosure")]
        Non_Disclosure,
        [EnumMember(Value = "Custom_Contract")]
        Custom_Contract,
        [EnumMember(Value = "Franchise")]
        Franchise,
        [EnumMember(Value = "Intellectual_Property")]
        Intellectual_Property,
        [EnumMember(Value = "Lease_Agreement")]
        Lease_Agreement,
        [EnumMember(Value = "licensing_Agreement")]
        licensing_Agreement,
        [EnumMember(Value = "Non_Complete")]
        Non_Complete,
        [EnumMember(Value = "Permanent_Employment")]
        Permanent_Employment,
        [EnumMember(Value = "Temporary_employment")]
        Temporary_employment,
        [EnumMember(Value = "Service")]
        Service,
        [EnumMember(Value = "Procurement")]
        Procurement,
        [EnumMember(Value = "Vendor")]
        Vendor,
    }

    public enum ApprovalProcessType
    {
        Sequential,
        Parallel
    }

    public class Contract
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        public ContractStatus? Status { get; set; } = ContractStatus.Draft;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        //public ICollection<Approver>? Approval { get; set; }
        public ICollection<ContractParty>? ContractParties { get; set; } // Navigation property
        public ICollection<Payment>? Payments { get; set; }
        //public  ICollection<ApplicationUser> Collaborators { get; set; }
        public TerminationClause? TerminationClause { get; set; }
        public List<Collaboration> Collaborators { get; set; } = new List<Collaboration>();
        public string? Title { get; set; }
        public string? Content { get; set; }
        //public AppUser? Creator { get; set; }
        public List<Approver>? Approvers { get; set; }
        public ApprovalProcessType? ApprovalProcessType { get; set; }

    }
}
