using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<RoleUser> EnRolledUser { get; set; }
    }
    public class RoleUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}