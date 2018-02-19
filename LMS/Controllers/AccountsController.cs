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
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registrationtbl reg)
        {
            string fileName = Path.GetFileNameWithoutExtension(reg.ImageFile.FileName);
            string extension = Path.GetExtension(reg.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            reg.ProfilePic = "~/Content/ProfileImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/ProfileImages/"), fileName);
            reg.ImageFile.SaveAs(fileName);

            using (LeaveManagementSystemEntities db = new LeaveManagementSystemEntities())
            {
                db.Registrationtbls.Add(reg);
                db.SaveChanges();
                ModelState.Clear();
                reg = null;
                ViewBag.Message = "Successfully Registered";
            }
            return View(reg);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Registrationtbl log)
        {
            return View();
        }
    }
}