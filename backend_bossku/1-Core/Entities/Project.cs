using Microsoft.VisualBasic;
using System;

namespace backend_bossku._1_Core.Entities
{
    public class Project
    {
        public int IdCart { get; set; }
        public int UserId { get; set; }
        public int[] CourseId { get; set; }
        public DateFormat Schedule { get; set; }
    }
}
