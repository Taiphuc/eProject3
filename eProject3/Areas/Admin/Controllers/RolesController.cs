using eProject3.Common;
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
    public class RolesController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        [HasCredential(RoleId = "VIEW_ROLE")]
        // GET: Admin/Role
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Role.ToList());
        }

        [HasCredential(RoleId = "CREATE_ROLE")]
        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View();
        }

        [HasCredential(RoleId = "CREATE_ROLE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Role.Add(role);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/Roles");
            }

            return View(role);
        }

        [HasCredential(RoleId = "VIEW_ROLE")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HasCredential(RoleId = "EDIT_ROLE")]
        public ActionResult Edit(string id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HasCredential(RoleId = "EDIT_ROLE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/Roles");
            }
            return View(role);
        }

        [HasCredential(RoleId = "DELETE_ROLE")]
        public ActionResult Delete(string id)
        {
            var existCredential = db.Credentials.Where(x => x.RoleId == id).FirstOrDefault();
            if (existCredential != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HasCredential(RoleId = "DELETE_ROLE")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Role role = db.Role.Find(id);
            db.Role.Remove(role);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/Roles");
        }
    }
}