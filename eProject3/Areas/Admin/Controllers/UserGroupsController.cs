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
    public class UserGroupsController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/UserGroups
        [HasCredential(RoleId = "VIEW_USER_GROUP")]
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.UserGroup.ToList());
        }

        [HasCredential(RoleId = "CREATE_USER_GROUP")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        [HasCredential(RoleId = "CREATE_USER_GROUP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                db.UserGroup.Add(userGroup);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/UserGroups");
            }
            return View(userGroup);
        }

        [HasCredential(RoleId = "VIEW_USER_GROUP")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        [HasCredential(RoleId = "EDIT_USER_GROUP")]
        public ActionResult Edit(string id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        [HasCredential(RoleId = "EDIT_USER_GROUP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGroup).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/UserGroups");
            }
            return View(userGroup);
        }

        [HasCredential(RoleId = "DELETE_USER_GROUP")]
        public ActionResult Delete(string id)
        {
            var existCredential = db.Credentials.Where(x => x.UserGroupId == id);
            var existUser = db.User.Where(x => x.GroupId == id);
            if (existCredential != null || existUser != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        [HasCredential(RoleId = "DELETE_USER_GROUP")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserGroup userGroup = db.UserGroup.Find(id);
            db.UserGroup.Remove(userGroup);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/UserGroups");
        }
    }
}