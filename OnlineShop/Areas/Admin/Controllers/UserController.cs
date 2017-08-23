using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTools;
using OnlineShop.Common;
using MyModel.DAO;
using MyModel.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        //public ActionResult Index(int page = 1, int pageSize = 10)
        //{
        //    var dao = new UserDao();
        //    var model = dao.ListAllPaging(page, pageSize);
        //    return View(model);
        //}
        public ActionResult Index()
        {
            var model = new UserDao().ListAll();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User entity)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var pwMD5 = (entity.UserName + entity.PassWord).Encrypt2MD5();
                entity.PassWord = pwMD5;
                //Cập nhật thời gian và người tạo
                entity.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                entity.CreatedBy = session.UserName;
                long id = dao.Insert(entity);
                if (id>0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo mới người dùng thất bại!!!");
                }
            }
            return RedirectToAction("Index","User");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            User model = new UserDao().GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            user.ModifiedDate = DateTime.Now;
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            user.ModifiedBy = session.UserName;
            if (new UserDao().Update(user))
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật thông tin người dùng thất bại!!!");
            }              
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (!new UserDao().Delete(id))
            {
                ModelState.AddModelError("", "Xóa người dùng thất bại");
            }
            return RedirectToAction("Index");
        }
    }
}