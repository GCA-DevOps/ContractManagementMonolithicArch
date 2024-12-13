using System;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class ClauseLibrary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClauseType { get; set; }
        public string Data { get; set; } // Base64 encoded file
      //  public string Content { get; set; } // Rich text content
        public string AppUserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateModified { get; set; }
    }
}

