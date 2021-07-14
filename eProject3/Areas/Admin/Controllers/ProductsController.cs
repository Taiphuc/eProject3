using eProject3.Common;
using eProject3.Model.Common;
using eProject3.Model.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var product = db.Product.Include(x => x.ProductCategory);

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

        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(product);
                product.CreatedBy = session.UserName;
                db.Product.Add(product);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/Products");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        public ActionResult Details(Guid? id)
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

        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(product);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.ModifiedBy = session.UserName;
                product.ModifiedOn = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/Products");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        public ActionResult Delete(Guid? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Product.Find(id);
            product.IsDeleted = true;
            AuditTable.UpdateAuditFields(product);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/Products");
        }

        public ActionResult Restore(Guid? id)
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

        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public ActionResult RestoreConfirmed(Guid id)
        {
            Product product = db.Product.Find(id);
            ProductCategory productCategory = db.ProductCategory.Where(x => x.Id == product.ProductCategory.Id).FirstOrDefault();
            if (productCategory.IsDeleted)
            {
                SetAlert("Bạn phải khôi phục hãng " + productCategory.Name + " trước", "warning");
                return Redirect("/Admin/Products");
            }
            else
            {
                product.IsDeleted = false;
                AuditTable.UpdateAuditFields(product);
                db.SaveChanges();
                SetAlert("Khôi phục thành công thành công", "success");
                return Redirect("/Admin/Products");
            }
        }
    }
}