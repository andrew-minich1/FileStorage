using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileStorage.Models.AccountViewModel;
using BLL_Interface.Entities;
using BLL_Interface.Services;
using FileStorage.Infrastructura;
using FileStorage.Infrastructura.Provider;
using System.Web.Security;

namespace FileStorage.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Index()
        {
            @ViewBag.Users = Membership.GetAllUsers();
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model )
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or login");
                }
            }
            return RedirectToAction("Index", "Home", model);
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["files"] = null;
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

     
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blluser = new UserEntity()
                {
                    Login = model.UserName,
                    Email = model.UserEmail,
                    Password = model.UserPassword,
                };
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(blluser);
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration");
                }
            }
            return View(model);
        }

    }
}
