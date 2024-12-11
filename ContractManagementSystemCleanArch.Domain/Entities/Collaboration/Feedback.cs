using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string Reviewer { get; set; }
    }
}
