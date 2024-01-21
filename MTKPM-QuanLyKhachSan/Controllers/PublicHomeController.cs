using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

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
			// Kiểm tra nếu name hoặc email là null
			if (name == null || email == null)
			{
				// Xử lý trường hợp name hoặc email là null
				// Ví dụ: Log lỗi hoặc thông báo người dùng
				return;
			}
			
			var senderEmail = _configuration["thangdiensongovietnam@gmail.com"];
			var senderPassword = _configuration["thangdien0123456789"];
			var receiverEmail = _configuration["riotter2703@gmail.com"];

			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(name, email));
			emailMessage.To.Add(new MailboxAddress("Nhật Phi", receiverEmail));
			emailMessage.Subject = subject;

			emailMessage.Body = new TextPart("plain")
			{
				Text = message
			};

			using (var client = new SmtpClient())
			{
				client.Connect(_configuration["smtp.gmail.com"], int.Parse(_configuration["587"]), true);
				client.Authenticate(senderEmail, senderPassword);
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
