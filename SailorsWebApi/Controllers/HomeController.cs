using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SailorsWebApi.BLL;
using SailorsWebApi.BLL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.Controllers
{
    public class HomeController : Controller
    {
        private IUsersServices usersServices;

        public HomeController(IUsersServices userServices)
        {
            usersServices = userServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users objUser)
        {
            if (ModelState.IsValid)
            {
                using (HowDatabaseEntities db = new HowDatabaseEntities())
                {
                    var obj = db.Users.FirstOrDefault(a => a.userName.Equals(objUser.userName) && a.passwordHash.Equals(objUser.passwordHash));
                    if (obj != null)
                    {
                        Session["UserID"] = obj.userId.ToString();
                        Session["UserName"] = obj.userName;
                        Session["Function"] = obj.functionId.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users objUser)
        {
            if (ModelState.IsValid)
            {
                using (HowDatabaseEntities db = new HowDatabaseEntities())
                {
                    var obj = db.Users.FirstOrDefault(a => a.userName.Equals(objUser.userName) && a.passwordHash.Equals(objUser.passwordHash));
                    if (obj == null)
                    {
                        usersServices.AddUser(obj);
                    }
                    else
                    {
                        
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Logout()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(object obj)
        {
            if (ModelState.IsValid)
            {
                if(obj != null) Session.Abandon();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}