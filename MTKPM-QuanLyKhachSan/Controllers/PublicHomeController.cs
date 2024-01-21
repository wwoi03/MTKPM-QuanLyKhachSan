using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicHomeController : Controller
    {
		private readonly IConfiguration _configuration;

		public PublicHomeController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		//Hàm SendMessage để xử lý chức năng gửi tin nhắn
		[HttpPost]
		public IActionResult SendMessage(string name, string email, string subject, string message)
		{
			// Xử lý logic để gửi email
			SendEmail(name, email, subject, message);

			// Có thể chuyển hướng hoặc trả về thông báo gửi thành công
			return RedirectToAction("Contact");
		}
		//Xử lý form thông tin
		private void SendEmail(string name, string email, string subject, string message)
		{
			Console.Write(name);
			var senderEmail = _configuration["thangdiensongovietnam@gmail.com"];
			var senderPassword = _configuration["thangdien0123456789"];
			var receiverEmail = _configuration["riotter2703@gmail.com"];

			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(name, email));
			emailMessage.To.Add(new MailboxAddress("Nhat Phi", "riotter2703@gmail.com"));
			emailMessage.Subject = subject;

			emailMessage.Body = new TextPart("plain")
			{
				Text = $"From: {email} \n{message}"
			};

			using (var client = new SmtpClient())
			{
				//client.Connect(_configuration["smtp.gmail.com"], int.Parse(_configuration["587"]), true);
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;
				client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
				client.Authenticate("thangdiensongovietnam@gmail.com", "ujyr rsoo ghrw qbkr");
				client.Send(emailMessage);
				client.Disconnect(true);
			}
		}
		public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }
		
		public IActionResult Contact()
		{ 
            return View();
        }
    }
}
