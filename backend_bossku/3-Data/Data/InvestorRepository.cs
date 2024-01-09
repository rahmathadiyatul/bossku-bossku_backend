using backend_bossku._3_Data.Data.Interface;

namespace backend_bossku._3_Data.Data
{
    public class InvestorRepository : IInvestorRepository
    {
        public string SignUpInvestor()
        {
            var result = "INSERT INTO INVESTORS " +
                            "(fullName, email, companyName, phone, companyCat, companyLoc) values " +
                            "(@FullName, @Email, @CompanyName, @Phone, @CompanyCat, @CompanyLoc)";
            return result;
        }
    }
}
