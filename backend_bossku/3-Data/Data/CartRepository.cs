using backend_bossku._3_Data.Data.Interface;

namespace backend_bossku._3_Data.Data
{
    public class CartRepository : ICartRepository
    {
        public string CreateCartDB()
        {
            var result = "INSERT INTO dbo.cart " +
                            "(userId, courseId, schedule) values " +
                            "(@UserId, @CourseId, @Schedule)";
            return result;
        }
    }
}
