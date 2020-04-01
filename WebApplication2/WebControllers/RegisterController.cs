using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.WebControllers
{
    public class RegisterController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Register(User user)
        {
            using (Entities db = new Entities())
            { 
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                // Business Logic
                ViewBag.Message = "Sucess or Failure Message";
                ModelState.Clear();
                return PartialView("_Register");
            }
            return PartialView("_Register", user);
        }
    }
}
}