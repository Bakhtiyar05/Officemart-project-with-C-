using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OfficeMart.UI.Controllers
{
    public class AboutController : Controller
    {
        [Route("Haqqımızda")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(string name, string phone, string email, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                return NotFound();
            }
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("officemartbaku@gmail.com", "OfficeMart2020");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("officemartbaku@gmail.com", "OfficeMart");
                mail.To.Add("info@officemart.az");
                mail.Subject = "Əlaqə üçün müraciət";
                mail.IsBodyHtml = true;
                mail.Body = String.Format(
                    $"<h3> Ad: <span style =\"color:green;font-size:20px;\">{name}</span></h3>" +
                    $"<h3> Nömrə: <span style =\"color:green;font-size:20px;\">{phone}</span></h3>" +
                    $"<h3> Email: <span style =\"color:green;font-size:20px;\">{email}</span></h3>" +
                    $"<h3> Mesaj: <span style =\"color:green;font-size:20px;\">{message}</span></h3>");
                sc.Send(mail);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            return Redirect("/Home/Index");

        }

    }
}
