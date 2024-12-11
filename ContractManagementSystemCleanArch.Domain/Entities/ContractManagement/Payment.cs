using System.ComponentModel.DataAnnotations.Schema;
using CMS.Domain.Entities.Contract;

namespace CMS.Domain.Entities.ContractManagement
{
    public enum PaymentStatus
    {
        Active,
        Expired,
        Terminated,
        Completed,
        Suspended,
        Cancelled,
        PendingApproval,
        InReview,
        RenewalDue,
        PaymentOverdue,
    }
    public enum Predefined_PaymentStructure
    {
        Monthly,
        Annually,
        Quarterly
        

    }

    public class Payment //Used to track payments made in relation to a contract.
                         //Each payment record includes details such as the payment amount,
                         //due date, payment method, and current status (e.g., pending, paid).

    {

        public int Id { get; set; }
        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public CMS.Domain.Entities.Contract.Contract? Contract { get; set; }
        public string? PaymentStructure { get; set; }
        public decimal Amount { get; set; }


        public PaymentStatus Status { get; set; } = PaymentStatus.Active;
    }
}
