using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._1_Core.Entities
{
    public class DetailInvoice
    {
        public int IdInvoice { get; set; }
        public string InvNum { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public DateTime Schedule { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
