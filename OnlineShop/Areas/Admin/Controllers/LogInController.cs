using MyModel.DAO;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LogInController : Controller
    {
        // GET: Admin/LogIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.LogIn(model.UserName, model.Password);
                if (result>0)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var userSession = new UserLogin() { UserName = user.UserName, UserID = user.ID };
                    Session.Add(CommonConstants.USER_SESSION,userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    switch (result)
                    {
                        case 0: { ModelState.AddModelError("", "Tài khoản không tồn tại!!!"); break; }
                        case -1: { ModelState.AddModelError("", "Tài khoản đang bị khóa!!!"); break; }                       
                        default: { ModelState.AddModelError("", "Mật khẩu không đúng!!!"); break; }
                    }
                }
            }
            return View("Index");
        }
    }
}