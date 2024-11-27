using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Durargroup.Controllers
{
    public class ContactusController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult contactus()
        {
            string Msg = "";
            var httprequest = HttpContext.Request;
            try
            {
                string firstname = httprequest.Form["firstname"].ToString().Trim();
                string lastname = httprequest.Form["lastname"].ToString().Trim();
                string Type = httprequest.Form["type"].ToString().Trim();
                string company = httprequest.Form["company"].ToString().Trim();
                string phone = httprequest.Form["phone"].ToString().Trim();
                string email = httprequest.Form["email"].ToString().Trim();
                string subject = httprequest.Form["subject"].ToString().Trim();
                string message1 = httprequest.Form["message"].ToString().Trim();

                string Body = "<p>Dear Admin,</p> <p>We have a message from contact us page on durar group website. Details are given below</p><p style='margin-left:4%;line-height:19px'><b>Name: </b>" + firstname +" "+lastname+ "<br/><b>Phone: </b>" + phone + "<br/><b>Email: </b>" + email + "<br/><b>Company/Position: </b>" + company + "<br/><b>Category: </b>" + Type + "<br/><b>Message: </b>" + message1 + "<p>Regards, <br/> Durar Group</p>";

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("noreply@durargroup.com"));
                message.To.Add(new MailboxAddress("info@durargroup.com"));
                message.Subject = subject;

                
                ////html or plain
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = Body;
                message.Body = bodyBuilder.ToMessageBody();

                try
                {

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        //smtp Server, port, open or not ssl
                        client.Connect("smtp.emailsrvr.com", 465, true);
                        client.Authenticate("noreply@durargroup.com", "Change@123");
                        client.Send(message);
                        client.Disconnect(true);
                    }

                }
                catch (Exception ex)
                {

                }
                Msg = "Thank you for your message.";

            }
            catch (Exception ex)
            {
                Msg = "Your message is not send successfully.";
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);

        }

    }
}