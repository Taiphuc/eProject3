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
    public class SlidesController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Slide
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Slide.Where(x => x.IsDeleted == false).OrderBy(x => x.DisplayOrder).ToList());
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create([Bind(Include = "Id,UrlImage,DisplayOrder,Link,Description,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.InsertAuditFields(slide);
                slide.CreatedBy = session.UserName;
                db.Slide.Add(slide);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/Slides");
            }
            return View(slide);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(int? id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit([Bind(Include = "Id,UrlImage,DisplayOrder,Link,Description,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.UpdateAuditFields(slide);
                slide.ModifiedBy = session.UserName;
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/Slides");
            }
            return View(slide);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        [HttpPost, ActionName("Delete")]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slide.Find(id);
            slide.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/Slides");
        }
    }
}