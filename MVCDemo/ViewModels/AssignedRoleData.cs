using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.ViewModels
{
    public class AssignedRoleData
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Assigned { get; set; }
    }
}