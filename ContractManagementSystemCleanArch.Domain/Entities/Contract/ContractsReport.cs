namespace CMS.Domain.Entities
{
    public class ContractsReport
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public decimal Value { get; set; }
        public string? Parties { get; set; }
        public string? Department { get; set; }

    }

}
