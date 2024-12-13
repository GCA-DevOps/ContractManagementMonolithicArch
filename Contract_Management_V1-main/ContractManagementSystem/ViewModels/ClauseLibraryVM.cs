namespace ContractManagementSystem.ViewModels
{
    public class ClauseLibraryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClauseType { get; set; }

        public IFormFile File { get; set; } // For file upload
    }
}
