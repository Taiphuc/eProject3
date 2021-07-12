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
    public class CredentialsController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Credential
        [HasCredential(RoleId = "VIEW_CREDENTIAL")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            var credentials = db.Credentials.Include(x => x.Role).Include(x => x.UserGroup);
            return View(credentials.ToList());
        }

        [HasCredential(RoleId = "CREATE_CREDENTIAL")]
        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            ViewBag.RoleId = new SelectList(db.Role, "Id", "Name");
            ViewBag.UserGroupId = new SelectList(db.UserGroup, "Id", "Name");
            return View();
        }

        [HasCredential(RoleId = "CREATE_CREDENTIAL")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserGroupId,RoleId")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Credentials.Add(credential);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/Credentials");
            }

            ViewBag.RoleId = new SelectList(db.Role, "Id", "Name", credential.RoleId);
            ViewBag.UserGroupId = new SelectList(db.UserGroup, "Id", "Name", credential.UserGroupId);
            return View(credential);
        }

        [HasCredential(RoleId = "VIEW_CREDENTIAL")]
        public ActionResult Details(string groupId, string roleId)
        {
            Credential credential = db.Credentials.Find(groupId, roleId);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        [HasCredential(RoleId = "DELETE_CREDENTIAL")]
        public ActionResult Delete(string groupId, string roleId)
        {
            if (groupId == null || roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(groupId, roleId);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        [HasCredential(RoleId = "DELETE_CREDENTIAL")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string groupId, string roleId)
        {
            Credential credential = db.Credentials.Find(groupId, roleId);
            db.Credentials.Remove(credential);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/Credentials");
        }
    }
}