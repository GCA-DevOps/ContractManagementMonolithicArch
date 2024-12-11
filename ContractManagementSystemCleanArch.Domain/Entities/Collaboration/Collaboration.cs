namespace CMS.Domain.Entities
{
    public class Collaboration
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentData { get; set; }
    }
}
