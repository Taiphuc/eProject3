using eProject3.Model.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Product
        public ActionResult Index(string sortOrder, string currentFilter , int? page, string search)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.PromotionPriceSortParm = sortOrder == "PromotionPrice" ? "promotion_price_desc" : "PromotionPrice";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            var product = db.Product.Include(x => x.ProductCategory).Where(x => x.IsDeleted == false);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(s => s.Name.Contains(search) || s.Price.ToString().Contains(search)
                || s.PromotionPrice.ToString().Contains(search) || s.ProductCategory.Name.Contains(search));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                case "PromotionPrice":
                    product = product.OrderBy(s => s.PromotionPrice);
                    break;
                case "promotion_price_desc":
                    product = product.OrderByDescending(s => s.PromotionPrice);
                    break;
                case "Quantity":
                    product = product.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    product = product.OrderByDescending(s => s.Quantity);
                    break;
                default:
                    product = product.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = search;
            return View(product.ToPagedList(pageNumber, pageSize));
        }
    }
}