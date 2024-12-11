using CMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.DTOs.Request
{
    public class DepartmentDto
    {
        // public string Name {  get; set; }
        public Predefined_Departments SelectedDepartment { get; set; }
    }
}
