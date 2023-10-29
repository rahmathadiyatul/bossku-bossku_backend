using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._1_Core.Entities
{
    public class Invoice
    {
        public int IdInvoice { get; set; }
        public string InvNum { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public List<DateTime> Schedule { get; set; }
        public List<int> Course { get; set; }
        public int TotalCourse { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
