using eProject3.Model.DAL;
using eProject3.Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace eProject3.Controllers
{
    public class ProductController : Controller
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getNormalProduct1()
        {
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => x.IsNormalProduct1 == true && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Take(4).ToList();
            ViewBag.Count = products.Count();
            return PartialView(products);
        }

        public ActionResult getNormalProduct2()
        {
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => x.IsNormalProduct2 == true && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Take(4).ToList();
            ViewBag.Count = products.Count();
            return PartialView(products);
        }

        public ActionResult Detail(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Search(string keyword)
        {
            var model = new ProductDao().Search(keyword);
            ViewBag.Keyword = keyword;
            ViewBag.Count = model.Count();
            return View(model);
        }

        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllProduct(int? page)
        {
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList();
            ViewBag.Count = products.Count();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult getHotProduct()
        {
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => x.TopHot == true && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Take(2).ToList();
            ViewBag.Count = products.Count();
            return PartialView(products);
        }
        public ActionResult getNewProduct()
        {
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => x.IsNewProduct == true && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Take(3).ToList();
            ViewBag.Count = products.Count();
            return PartialView(products);
        }

        public ActionResult getInvolveProduct(Guid? id)
        {
            Product product = db.Product.Find(id);
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => x.ProductCategoryId == product.ProductCategoryId && x.Id != product.Id && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Take(3).ToList();
            ViewBag.Count = products.Count();
            return PartialView(products);
        }

        public ActionResult getAllCategory()
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            productCategories = db.ProductCategory.Where(x => !x.IsDeleted).ToList();
            var products = db.Product.ToList();
            ViewBag.Count = productCategories.Count();
            ViewBag.CountAllProduct = products.Count();
            return PartialView(productCategories);
        }

        public ActionResult categoryDetail(ProductCategory productCategory)
        {
            List<Product> products = new List<Product>();
            products = db.Product.Where(x => x.ProductCategoryId == productCategory.Id && !x.IsDeleted).ToList();
            ViewBag.Count = products.Count();
            return View(products);
        }
    }
}