using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class NoteViewModels
    {
    }

    public class NoteModel
    {
        public int NoteID { get; set; }

        [Required(ErrorMessage = "Please enter a note")]
        public string Body { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public int AccountID { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; }
    }

    public class CustomerNotesViewModel
    {
        public CustomerModel Customer { get; set; }
        public List<NoteModel> Notes { get; set; }
    }
}