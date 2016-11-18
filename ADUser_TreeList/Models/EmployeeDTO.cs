using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADUser_TreeList.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public string TeamMember { get; set; }
        public string Manager { get; set; }
        public string cn { get; set; }
        public string ManagerCn { get; set; }
        public string Buerau { get; set; }
        public string Email { get; set; }

    }

    public class EmployeeDTOModel
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public string TeamMember { get; set; }
        public string Manager { get; set; }
        public string cn { get; set; }
        public string ManagerCn { get; set; }
        public string Buerau { get; set; }
        public string Email { get; set; }

    }
}