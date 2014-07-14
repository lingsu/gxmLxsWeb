using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lxs.Admin.Models.Customers
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }


        //customer roles
        public string CustomerRoleNames { get; set; }
        //public List<CustomerRoleModel> AvailableCustomerRoles { get; set; }
        public int[] SelectedCustomerRoleIds { get; set; }
    }
}