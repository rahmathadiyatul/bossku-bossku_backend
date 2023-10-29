using System;

namespace backend_bossku._1_Core.Entities.SubEntities
{
    public class CartContent
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime Schedule { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }

    }
}
