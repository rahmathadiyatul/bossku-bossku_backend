using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string name);
    }
}
