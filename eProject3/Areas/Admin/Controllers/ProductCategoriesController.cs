using eProject3.Common;
using eProject3.Model.Common;
using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class ProductCategoriesController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.ProductCategory.OrderBy(x => x.DisplayOrder).ToList());
        }

        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(productCategory);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                productCategory.CreatedBy = session.UserName;
                var lastProduct = db.ProductCategory.Where(x => !x.IsDeleted).OrderByDescending(x => x.DisplayOrder).FirstOrDefault();
                if (lastProduct == null)
                {
                    productCategory.DisplayOrder = 1;
                }
                else
                {
                    productCategory.DisplayOrder = lastProduct.DisplayOrder + 1;
                }
                AuditTable.InsertAuditFields(productCategory);
                db.ProductCategory.Add(productCategory);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/ProductCategories");
            }

            return View(productCategory);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(productCategory);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                productCategory.ModifiedBy = session.UserName;
                productCategory.ModifiedOn = DateTime.Now;
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/ProductCategories");
            }
            return View(productCategory);
        }

        public ActionResult Delete(Guid? id)
        {
            var existProduct = db.Product.Where(x => x.ProductCategoryId == id);
            ViewBag.Count = existProduct.Count();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var existProduct = db.Product.Where(x => x.ProductCategoryId == id && x.IsDeleted == false).ToList();
            if (existProduct.Any())
            {
                existProduct.ForEach(x => x.IsDeleted = true);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            productCategory.IsDeleted = true;
            AuditTable.UpdateAuditFields(productCategory);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/ProductCategories");
        }

        public ActionResult Restore(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public ActionResult RestoreConfirmed(Guid id)
        {
            ProductCategory productCategory = db.ProductCategory.Find(id);
            productCategory.IsDeleted = false;
            AuditTable.UpdateAuditFields(productCategory);
            db.SaveChanges();
            SetAlert("Khôi phục thành công", "success");
            return Redirect("/Admin/ProductCategories");
        }
    }
}