using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VSOSCO.Controllers
{
    public class RunSharkController : Controller
    {
        // GET: RunShark
        public ActionResult Home()
        {
            return Redirect("~/index.html");
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public bool SendEmailContactForm(string name, string email, string phoneNum, string message)
        {
            bool isSuccess = true;
            try
            {
                MailMessage msgs = new MailMessage();
                msgs.To.Add(new MailAddress("octamux.info@gmail.com"));
                MailAddress address = new MailAddress("abc@domain.com");
                msgs.From = new MailAddress("octamux.info@gmail.com");
                msgs.Subject = "Hello!!! " + name + " Contacted you.";
                msgs.Sender = new MailAddress("octamux.info@gmail.com");

                var body = new StringBuilder();
                body.AppendLine("<H3>Dear Octamux,</H3>");
                body.AppendLine("<p>the user has contacted you and details are below.</p>");
                body.AppendLine("<p>Name : " + name + "</p>");
                body.AppendLine("<p>Phone Number : " + phoneNum + "</p>");
                body.AppendLine("<p>Email : " + email + "</p>");
                body.AppendLine("<p>Message : " + message + "</p>");
                body.AppendLine("<br>");
                body.AppendLine("<p>Thanks,<br>" + name + "</p>");

                msgs.Body = body.ToString();
                msgs.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();

                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;

                //client.Host = "smtp.gmail.com";
                //client.Port = 587;

                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("octamux.info@gmail.com", "octamux2018");
                //client.EnableSsl = true;

                //Send the msgs  
                client.Send(msgs);
            }
            catch (Exception ex)
            {
                isSuccess = false;

                // Models.Library.WriteLog("Error occured while sending an email from contact form ", ex);
            }
            ViewBag.send = isSuccess;

            return isSuccess;
            //return View("Octamux");
        }
    }
}