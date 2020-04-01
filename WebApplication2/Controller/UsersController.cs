using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using WebApplication2.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    public class UsersController : Controller
    {
        private Entities db = new Entities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
       
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,EmailID,DateOfBirth,Mobile,Password,ConfirmPassword")] User user)
        {
            using (Entities db = new Entities())
            {
                if (db.Users.Any(x => x.Username == user.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exist";
                    //return RedirectToAction("Create", "Users");
                }
                else
                {
                    if (ModelState.IsValid)
                        //{
                        //    if (db.Users.Any(x => x.Username == user.Username))
                        //        ViewBag.DuplicateMessage = "Username already exist";
                        //}
                        try
                        {
                            //ViewBag.SuccessMessage = "Registration successful";
                            db.Users.Add(user);
                            db.SaveChanges();
                            //ViewBag.Message = string.Format("Registration Successful");
                            MailMessage mail = new MailMessage(new MailAddress("healthexpendituresurvey@gmail.com"), new MailAddress(user.EmailID));
                            mail.Subject = "Email Confirmation";
                            string Body = "<br /><br />Please click the below link to activate your account"; 
                            Body += "<br /><br /> https://localhost:44380/Users/Edit/" + user.UserID;
                            Body += "<br /><br /><br/>Your username and password is,";
                            Body += "<br /><br />" + "Username " + " : " + user.Username;
                            Body += "<br /><br />" + "Password" + " : "+user.Password;

                            mail.Body = Body;
                            mail.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential("healthexpendituresurvey@gmail.com", "healthexpense"); // Enter seders User name and password  
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                            ViewBag.Message = string.Format("Registration successful,please check your e-mail to activate your account");
                            //return RedirectToAction("Index", "Home");

                        }

                        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                        {
                            Exception raise = dbEx;
                            foreach (var validationErrors in dbEx.EntityValidationErrors)
                            {
                                foreach (var validationError in validationErrors.ValidationErrors)
                                {
                                    string message = string.Format("{0}:{1}",
                                        validationErrors.Entry.Entity.ToString(),
                                        validationError.ErrorMessage);
                                    // raise a new exception nesting  
                                    // the current instance as InnerException  
                                    raise = new InvalidOperationException(message, raise);
                                }
                            }
                            throw raise;
                        }

                }
           
            return View();
        }
        }

        //Users Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
                using (Entities db = new Entities())
                {
                    var obj = db.Users.Where(a => a.Username.Equals(user.Username) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (obj != null && obj.ActivationStasus == true)
                {
                    Session["UserID"] = obj.UserID.ToString();
                    Session["Username"] = obj.Username.ToString();
                    return RedirectToAction("Index", "Home");

                }
                else 
                {
                    ViewBag.Message=string.Format("Invalid Username or Password");
                }
                //    if(!Membership.ValidateUser(obj.Username, obj.Password))
                //{
                //    ModelState.AddModelError(string.Empty, "The user name or password is incorrect");
                //    return View("Login","Users");
                //}
            }
            return View();
        }
         public ActionResult UserDashBoard()  
        {  
            if (Session["UserID"] != null)  
            {  
                return View();  
            } else  
            {  
                return RedirectToAction("Login");  
            }  
        }  
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            } else if(user.ActivationStasus == true)
            {
                ViewBag.SuccessMessage = string.Format("Account already activated");
            }
            user.ActivationStasus = true;
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,EmailID,DateOfBirth,Mobile,Password,ConfirmPassword,ActivationStasus")] User user)
        {
           if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = string.Format("Account activated successfully you can login now");
                //return RedirectToAction("Index","Home");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
