using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace MTKPM_QuanLyKhachSan.Controllers
{
    public class PublicHomeController : Controller
    {
		//Khai báo biến _configuration
		private readonly IConfiguration _configuration;
		//Tạo controller
		public PublicHomeController(IConfiguration configuration)
		{
			//Tạo dịch vụ configuration cho chức năng gửi email
			_configuration = configuration;
		}
		//Hàm SendMessage để xử lý chức năng gửi tin nhắn
		[HttpPost]
		public IActionResult SendMessage(string name, string email, string subject, string message)
		{
			// Xử lý logic để gửi email
			SendEmail(name, email, subject, message);

            // Đặt thông điệp vào TempData
            TempData["SuccessMessage"] = "Thông tin của bạn đã được gửi đến email của chúng tôi thành công!";

            // Có thể chuyển hướng hoặc trả về thông báo gửi thành công
            return RedirectToAction("Contact");
		}
		//Xử lý form thông tin
		private void SendEmail(string name, string email, string subject, string message)
		{
			Console.Write(name);
			//Tạo tài khoản trung gian để nhận dữ liệu người dùng nhập và gửi mail tới Email của khách sạn
			var senderEmail = _configuration["thangdiensongovietnam@gmail.com"];
			var senderPassword = _configuration["thangdien0123456789"];
			//Tạo tài khoản mặc định cho khách sạn sẽ nhận email từ người dùng
			var receiverEmail = _configuration["riotter2703@gmail.com"];
			//Sử dụng thư viện Mailkit và Mime để xây dựng chức năng Send Email
			var emailMessage = new MimeMessage();
			//Tên người gửi và địa chỉ email gửi thư
			emailMessage.From.Add(new MailboxAddress(name, email));
			//Tên người nhận và địa chỉ email nhận thư
			emailMessage.To.Add(new MailboxAddress("Nhat Phi", "riotter2703@gmail.com"));
			//Tên chủ đề của email
			emailMessage.Subject = subject;
			//Phần thân chứa nội dung của email
			emailMessage.Body = new TextPart("plain")
			{
				Text = $"From: {email} \n{message}"
			};

			//Tạo tài khoản mặc định sẽ nhận email từ khách hàng
			using (var client = new SmtpClient())
			{
				//Xây dựng cấu trúc chuẩn của thư viện MailKit như sau:
				//Kết nối Server để sử dụng dịch vụ của Gmail
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;
				client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
				//Xác thực và mặc định tài khoản sẽ là trung gian nhận thông tin từ form và gửi mail đến email của khách sạn
				client.Authenticate("thangdiensongovietnam@gmail.com", "ujyr rsoo ghrw qbkr");
				//Thực hiện chức năng gửi Email
				client.Send(emailMessage);
				//Kết thúc sau khi đã gửi Email thành công
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
		//Chức năng trang liên hệ
		public IActionResult Contact()
		{
            // Kiểm tra xem có thông điệp thành công không
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            return View();
        }
    }
}
