using Microsoft.VisualBasic;
using System;

namespace backend_bossku._1_Core.Entities
{
    public class Investor
    {
        public int InvestorId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string CompanyCat { get; set; }
        public string CompanyLoc { get; set; }

    }
}
