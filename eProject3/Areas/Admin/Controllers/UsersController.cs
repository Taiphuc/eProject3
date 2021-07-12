using eProject3.Common;
using eProject3.Model.Common;
using eProject3.Model.DAL;
using eProject3.Model.Dao;
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
    public class UsersController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/User
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Index(string sortOrder, string currentFilter, int? page, string search)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            var user = db.User.Include(x => x.UserGroup);

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
                user = user.Where(s => s.UserName.Contains(search) || s.Email.ToString().Contains(search)
                || s.UserGroup.Name.Contains(search) || s.Address.Contains(search));
            }
            switch (sortOrder)
            {
                case "username_desc":
                    user = user.OrderByDescending(s => s.UserName);
                    break;
                case "LastName":
                    user = user.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    user = user.OrderByDescending(s => s.LastName);
                    break;
                default:
                    user = user.OrderBy(s => s.UserName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = search;
            return View(user.ToPagedList(pageNumber, pageSize));
        }

        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name");
            return View();
        }

        [HasCredential(RoleId = "ADD_USER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckEmail(user.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại trong hệ thống");
                }
                else if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại trong hệ thống");
                }
                else
                {
                    user.Id = Guid.NewGuid();
                    AuditTable.InsertAuditFields(user);
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    user.CreatedBy = session.UserName;
                    user.Password = Encryptor.MD5Hash(user.Password);
                    user.ConfirmPassword = Encryptor.MD5Hash(user.ConfirmPassword);
                    db.User.Add(user);
                    db.SaveChanges();
                    SetAlert("Thêm mới thành công", "success");
                    return Redirect("/Admin/Users");
                }
            }
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name", user.GroupId);
            return View(user);
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
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name", user.GroupId);
            return View(user);
        }

        [HasCredential(RoleId = "EDIT_USER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(user);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                user.ModifiedBy = session.UserName;
                user.Password = Encryptor.MD5Hash(user.Password);
                user.ConfirmPassword = Encryptor.MD5Hash(user.ConfirmPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/Users");
            }
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name", user.GroupId);
            return View(user);
        }

        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HasCredential(RoleId = "DELETE_USER")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.User.Find(id);
            user.IsDeleted = true;
            db.SaveChanges();
            return Redirect("/Admin/Users");
        }

        public JsonResult ChangeStatus(Guid? id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}