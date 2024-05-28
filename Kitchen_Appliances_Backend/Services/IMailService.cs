using Kitchen_Appliances_Backend.DTO.Mail;

namespace Kitchen_Appliances_Backend.Services
{
    public interface IMailService
    {
        void sendMail(CreateMailRequest request);
    }
}
