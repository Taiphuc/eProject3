using eProject3.Areas.Admin.Models;
using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            ViewBag.CountUser = db.User.Count();
            ViewBag.CountRole = db.Role.Count();
            ViewBag.CountCredential = db.Credentials.Count();
            ViewBag.CountProduct = db.Product.Count();
            ViewBag.CountProductCategory = db.ProductCategory.Count();
            ViewBag.CountNews = db.News.Count();
            ViewBag.CountNewsCategory = db.NewsCategory.Count();
            ViewBag.CountAbout = db.About.Count();
            ViewBag.CountMenu = db.Menu.Count();
            ViewBag.CountSlide = db.Slide.Count();
            ViewBag.CountContact = db.Contact.Count();
            ViewBag.CountFooter = db.Footer.Count();
            ViewBag.CountOrder = db.Order.Count();
            ViewBag.Reven = db.OrderDetail.Sum(i => i.Price * i.Quantity);
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

        public ActionResult ExportToExcel()
        {
            var ExcelData = (from od in db.OrderDetail
                             join o in db.Order
                             on od.OrderId equals o.Id
                             join u in db.User
                             on o.UserId equals u.Id
                             join p in db.Product
                             on od.ProductId equals p.Id
                             select new ExcelModel
                             {
                                 Customer = u.FirstName + u.LastName,
                                 Email = u.Email,
                                 Phone = o.ShipPhone,
                                 ProductName = p.Name,
                                 CreatedOn = o.CreatedOn.Value,
                                 Price = od.Price.Value,
                                 Quantity = od.Quantity.Value,
                                 Total = od.Price.Value * od.Quantity.Value
                             }).ToList();
            var orderDetail = db.OrderDetail.Where(x => !x.IsDeleted).ToList();
            var gv = new GridView();
            gv.DataSource = ExcelData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();
        }

        public JsonResult Chart(int year)
        {
            List<decimal> price = new List<decimal>();
            List<decimal> stock = new List<decimal>();
            Dictionary<string, List<decimal>> result = new Dictionary<string, List<decimal>>();

            for (int month = 1; month <= 12; month++)
            {
                var priceByMonth = db.OrderDetail
                    .Where(x => !x.IsDeleted && x.CreatedOn.Value.Month == month && x.CreatedOn.Value.Year == year)
                    .Sum(x => x.Quantity * x.Price);

                price.Add(priceByMonth == null ? 0 : priceByMonth.Value);
                var stockByMonth = (from o in db.OrderDetail
                                    join p in db.Product
                                    on o.ProductId equals p.Id
                                    where o.CreatedOn.Value.Year == year && o.CreatedOn.Value.Month == month
                                    select new
                                    {
                                        Quantity = o.Quantity,
                                        Stock = p.Code
                                    }).Sum(x => x.Quantity * x.Stock);
                stock.Add(stockByMonth == null ? 0 : stockByMonth.Value);
            }
            result.Add("price", price);
            result.Add("stock", stock);
            return Json(new { data = result },
                JsonRequestBehavior.AllowGet);
        }
    }
}