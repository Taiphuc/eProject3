using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Home
        public ActionResult Index(CultureInfo cultures)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            ViewBag.CountUserGroup = db.UserGroup.Count();
            ViewBag.CountUser = db.User.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountRole = db.Role.Count();
            ViewBag.CountCredential = db.Credentials.Count();
            ViewBag.CountProduct = db.Product.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountProductCategory = db.ProductCategory.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountNews = db.News.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountNewsCategory = db.NewsCategory.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountAbout = db.About.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountMenu = db.Menu.Where(x => x.IsDeleted == false && x.ParentId == null).Count();
            ViewBag.CountSlide = db.Slide.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountContact = db.Contact.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountFooter = db.Footer.Where(x => x.IsDeleted == false).Count();
            ViewBag.CountOrder = db.Order.Where(x => x.IsDeleted == false).Count();
            ViewBag.Reven = db.OrderDetail.Where(x => x.IsDeleted == false).Sum(i => i.Price * i.Quantity);
            // tinh tong tien ban
            var result = from o in db.OrderDetail
                         join p in db.Product on o.ProductId equals p.Id
                         select new
                         {
                             Code = p.Code,
                             Price = o.Price,
                             Quantity = o.Quantity,
                             ProductId = p.Id
                         };
            var sumPrice = result.Sum(x => x.Quantity * x.Price);
            var sum = result.Sum(x => x.Quantity * x.Code);
            ViewBag.Benefit = sumPrice - sum;
            return View();
        }
    }
}