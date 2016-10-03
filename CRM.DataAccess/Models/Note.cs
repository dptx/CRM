using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataAccess.Models
{
    public class Note
    {
        public int NoteID { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public int AccountID { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Account Account { get; set; }
    }
}
