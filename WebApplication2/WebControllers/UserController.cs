using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;


namespace WebApplication2.WebControllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Index(int id=0)
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Index(User userModel)
        {
            
            using (var userModel1 = new test())
            {
            //    string cs = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
             //   userModel1.Database.Connection.ConnectionString = cs;
                //userModel1.User.Add(userModel);
                userModel1.SaveChanges();           
            }
            ModelState.Clear();
            return View("Index",userModel);
        }
    }
}