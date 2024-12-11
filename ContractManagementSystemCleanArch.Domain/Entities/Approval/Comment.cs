

namespace CMS.Domain.Entities.Approval
{
    public class Comment
    {
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public int ContractId { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
