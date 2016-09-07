using Contact_Form.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Contact_Form.Controllers
{
    public class ContactController : SurfaceController
    {
        private SmtpClient smtp;

        public ContactController()
        {
            smtp = new SmtpClient();
        }

        [ChildActionOnly]
        public ActionResult ContactForm()
        {
            // In case you need it...
            var currentNode = Umbraco.TypedContent(UmbracoContext.PageId.GetValueOrDefault());

            var model = new ContactViewModel();
            return PartialView("_ContactForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactForm(ContactViewModel model)
        {
            if (ModelState.IsValid && string.IsNullOrWhiteSpace(model.HelperField))
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<strong>Kontakt oplysninger</strong>");
                sb.AppendFormat("Navn: {0}<br />", model.Name);
                sb.AppendFormat("Firmanavn: {0}<br />", model.CompanyName);
                sb.AppendFormat("E-mail: {0}<br />", model.Email);
                sb.AppendFormat("Telefon: {0}<br /><br />", model.Phone);
                sb.AppendFormat("Emne: {0}<br />", model.Subject);
                sb.AppendFormat("<p><strong>Besked:</strong> <br />{0}</p>", model.Message);

                MailMessage message = new MailMessage();

                message.To.Add(new MailAddress(ConfigurationManager.AppSettings["ContactAddress"]));
                message.Sender = new MailAddress(model.Email);
                message.From = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
                message.Body = sb.ToString();
                message.Subject = string.Format("KimDamDev Kontaktform - {0}", model.Name);
                message.IsBodyHtml = true;

                TempData["success"] = true;

                try
                {
                    smtp.Send(message);

                }
                catch (SmtpException smtpEx)
                {
                    TempData["success"] = false;
                }
            }

            return CurrentUmbracoPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookAppointment(BookAppointmentViewModel model)
        {
            if (ModelState.IsValid && string.IsNullOrWhiteSpace(model.HelperField))
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<strong>Kontakt oplysninger</strong>");
                sb.AppendFormat("Navn: {0}<br />", model.Name);
                sb.AppendFormat("Adresse: {0}, {1} {2}<br />", model.Address, model.Zip, model.City);
                sb.AppendFormat("E-mail: {0}<br />", model.Email);
                sb.AppendFormat("Telefon: {0}<br /><br />", model.Phone);
                sb.AppendFormat("Fjernsupport: {0}<br />", model.IsRemoteSupport ? "Ja" : "Nej");
                sb.AppendFormat("Beskriv problem: {0}<br />", model.Message);
                sb.AppendFormat("<p><strong>Besked:</strong> <br />{0}</p>", model.Message);

                MailMessage message = new MailMessage();

                message.To.Add(new MailAddress(ConfigurationManager.AppSettings["ContactAddress"]));
                message.Sender = new MailAddress(model.Email);
                message.From = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
                message.Body = sb.ToString();
                message.Subject = string.Format("KimDamDev ring op - {0}", model.Name);
                message.IsBodyHtml = true;

                TempData["success"] = true;

                try
                {
                    smtp.Send(message);

                }
                catch (SmtpException smtpEx)
                {
                    TempData["success"] = false;
                }
            }

            return CurrentUmbracoPage();
        }
    }
}