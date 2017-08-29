using MyModel.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var dao = new ProductDao();
            ViewBag.ListNewProducts = dao.GetFeaturedList(ProductDao.ProductFeature.New, 4);
            ViewBag.ListTopHotProducts = dao.GetFeaturedList(ProductDao.ProductFeature.TopHot, 4);
            ViewBag.Slides = new SlideDao().ListAll(true, true);
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListMenu(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListMenu(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}