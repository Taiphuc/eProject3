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
    public class ContactsController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Contact
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.Contact.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).Take(2).ToList());
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ObjectName,Address,Mobile,Website,Email,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                AuditTable.InsertAuditFields(contact);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                contact.CreatedBy = session.UserName;
                db.Contact.Add(contact);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/Contacts");
            }
            return View(contact);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(int? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(contact);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                contact.ModifiedBy = session.UserName;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/Contacts");
            }
            return View(contact);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contact.Find(id);
            contact.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/Contacts");
        }
    }
}