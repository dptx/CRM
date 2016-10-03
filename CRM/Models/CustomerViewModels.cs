using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class CustomerViewModels
    {
    }

    public class CustomerViewModel
    {
        public CustomerModel Customer { get; set; }
        public List<CustomerModel> Customers { get; set; }
    }

    public class CustomerListModel
    {
        public List<CustomerModel> Customers { get; set; }
    }

    public class CustomerModel
    {
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Company { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }


}