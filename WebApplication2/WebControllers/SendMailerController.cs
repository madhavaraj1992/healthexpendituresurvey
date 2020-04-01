using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.WebControllers
{
    public class SendMailerController : Controller
    {
        // GET: SendMailer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Index(WebApplication2.Models.MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {
                //MailMessage mail = new MailMessage();
                //mail.To.Add(_objModelMail.To);
                //mail.From = new MailAddress(_objModelMail.From);
                MailMessage mail = new MailMessage(new MailAddress("healthexpendituresurvey@gmail.com"), new MailAddress("healthexpendituresurvey@gmail.com"));
                mail.Subject = "Feedback Message from user";
                string Body = "<br /><br /> Feedback from user as follows,";
                Body += "<br /><br />" + "Username " + " : " + _objModelMail.To;
                Body += "<br /><br />" + "EmailID " + " : " + _objModelMail.Subject;
                Body += "<br /><br />" + "Subject " + " : " + _objModelMail.From;
                Body += "<br /><br />" + "Message " + " : " + _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("healthexpendituresurvey@gmail.com", "healthexpense"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Index", _objModelMail);
            }
            else
            {
                return View();
            }
        }
    }
}