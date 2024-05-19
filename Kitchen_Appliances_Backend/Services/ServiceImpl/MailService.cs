using Kitchen_Appliances_Backend.Commons.Enums;
using Kitchen_Appliances_Backend.DependencyInjection.Options;
using Kitchen_Appliances_Backend.DTO.Mail;
using MailKit.Security;
using MimeKit;
using System.Globalization;

namespace Kitchen_Appliances_Backend.Services.ServiceImpl
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _mailTemplate;
        private readonly IOtpService _otpService;

        private const string EMAIL_TEMPLATE = "email-template";

        public MailService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IOtpService otpService)
        {
            _configuration = configuration;
            _mailTemplate = Path.Combine(webHostEnvironment.WebRootPath, EMAIL_TEMPLATE);
            _otpService = otpService;

        }

        private string GetMailContent(CreateMailRequest request)
        {
            var path = Path.Combine(_mailTemplate, request.Type);
            string body = string.Empty;
            using (StreamReader reader = new(path))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{name}", request.Name);
            body = body.Replace("{email}", request.Email);
            body = body.Replace("{title}", request.Title);
            if (request.Type != MAIL_TYPE.ORDER_CONFIRMATION.ToString())
            {
                //var otp = _tokenService.GenerateOTP(); đã có otp từ bên create mail request gửi qua
                body = body.Replace("{OTP}", request.OTP);
            }

            if (request.OrderConfirmationMail != null)
            {
                body = body.Replace("{email}", request.OrderConfirmationMail.Email);
                body = body.Replace("{receiver}", request.OrderConfirmationMail.Receiver);
                body = body.Replace("{phone}", request.OrderConfirmationMail.Phone);
                body = body.Replace("{address}", request.OrderConfirmationMail.Address);
                body = body.Replace("{paymentMethod}", request.OrderConfirmationMail.PaymentMethod);

                string totalPrice = request.OrderConfirmationMail.TotalPrice.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
                body = body.Replace("{totalPrice}", totalPrice + "đ");
            }
            return body;
        }

        public void sendMail(CreateMailRequest request)
        {
            try
            {
                //
                var mailOptions = new MailDefaultOptions();
                _configuration.GetSection(nameof(MailDefaultOptions)).Bind(mailOptions);
                var mailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(mailOptions.DisplayName, mailOptions.Mail)
                };

                mailMessage.From.Add(new MailboxAddress(mailOptions.DisplayName, mailOptions.Mail));
                mailMessage.To.Add(MailboxAddress.Parse(request.Email));

                mailMessage.Subject = request.Title;

                var builder = new BodyBuilder
                {
                    HtmlBody = GetMailContent(request)
                };

                mailMessage.Body = builder.ToMessageBody();

                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(mailOptions.Host, mailOptions.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailOptions.Mail, mailOptions.Password);
                _ = Task.Run(() => smtp.Send(mailMessage));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
