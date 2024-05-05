

using Bislerium_Coursework_Service.Model;

namespace Bislerium_Coursework_Service.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);

    }
}
