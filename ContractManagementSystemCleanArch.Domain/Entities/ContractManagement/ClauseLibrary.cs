using CMS.Domain.Entities;
using CMS.Domain.Entities.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CMS.Domain.Entities.ContractManagement
{
    public class ClauseLibrary
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClauseType {  get; set; }
        public string UserName { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime DateModified { get; set; }
        public string Data {  get; set; }

    }
}
