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
    public class MenusController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Menu
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Menu.Where(x => x.IsDeleted == false).ToList());
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
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.InsertAuditFields(menu);
                menu.CreatedBy = session.UserName;
                db.Menu.Add(menu);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/Admin/Menus");
            }

            return View(menu);
        }

        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
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
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.UpdateAuditFields(menu);
                menu.ModifiedBy = session.UserName;
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/Menus");
            }
            return View(menu);
        }
    }
}