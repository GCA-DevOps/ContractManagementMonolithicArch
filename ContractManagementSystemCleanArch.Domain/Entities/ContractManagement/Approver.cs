//using CMS.Domain.Entities;
//using CMS.Domain.Entities.Authentication;
//using System.ComponentModel.DataAnnotations;

//namespace CMS.Domain.Entities.ContractManagement
//{
//    public class Approver
//    {
//        [Key]
//        public int Id { get; set; }
//        public int StageId { get; set; } // Foreign key referencing Stage ID
//        public string UserId { get; set; } // Foreign key referencing User ID
//                                           // Other properties as needed

//        // Navigation properties
//        public Stage Stage { get; set; }
//        public AppUser AppUser { get; set; }


//    }
//}
