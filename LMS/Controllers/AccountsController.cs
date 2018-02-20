using LMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            Registrationtbl reg = new Registrationtbl();
            return View(reg);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registrationtbl reg, HttpPostedFileBase imageFile)
        {

            var _db = new LMSdb();
            if(imageFile != null)
            {
                reg.ProfilePic = new byte[imageFile.ContentLength];
                imageFile.InputStream.Read(reg.ProfilePic, 0, imageFile.ContentLength);
            }
                
                    _db.Registrationtbls.Add(reg);
                    _db.SaveChanges();
                    ModelState.Clear();
                    reg = null;
                    ViewBag.Message = "Successfully Registered";
                
            
            
            return View(reg);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Registrationtbl log)
        {
            LMSdb _db = new LMSdb();
            var users = _db.Registrationtbls.SingleOrDefault(model=>model.Email == model.Email && model.Password == model.Password);
            if(users != null)
            {
                Session["UserId"] = users.Id;
                Session["Name"] = users.Name;
                return RedirectToAction("Index", "Accounts");
            }
           
            return View();
        }
    }
}