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
using System.Web.Security;
using Umbraco.Web.Mvc;

namespace Contact_Form.Controllers
{
    public class AuthController : SurfaceController
    {
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
        public ActionResult Login(LoginModelViewModel model)
        {
            TempData["success"] = false;

            if (ModelState.IsValid && string.IsNullOrWhiteSpace(model.HelperField))
            {
                var valid = Members.Login(model.Username, model.Password);

                if (valid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    TempData["success"] = true;
                    return RedirectToUmbracoPage(model.RedirectPage);
                }
            }

            return CurrentUmbracoPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Logout()
        {
            if (ModelState.IsValid)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }

            return Redirect("~/");
        }
    }
}