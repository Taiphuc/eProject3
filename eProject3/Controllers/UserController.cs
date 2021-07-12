using BotDetect.Web.Mvc;
using eProject3.Common;
using eProject3.Model.Common;
using eProject3.Model.DAL;
using eProject3.Model.Dao;
using eProject3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Controllers
{
    public class UserController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Mã CAPTCHA không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại trong hệ thống");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại trong hệ thống");
                }
                else
                {
                    var user = new User();
                    user.Id = Guid.NewGuid();
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.ConfirmPassword = Encryptor.MD5Hash(model.ConfirmPassword);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.GroupId = "MEMBER";
                    user.Address = model.Address;
                    user.IsDeleted = false;
                    user.CreatedOn = DateTime.Now;

                    db.User.Add(user);
                    db.SaveChanges();
                    SetAlert("Đăng ký thành công", "success");

                    // Login
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserId = user.Id;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    userSession.FirstName = user.FirstName;
                    userSession.LastName = user.LastName;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
            }
            else
            {
                SetAlert("Đăng ký không thành công", "warning");
                MvcCaptcha.ResetCaptcha("ExampleCaptcha");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), false);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserId = user.Id;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    userSession.FirstName = user.FirstName;
                    userSession.LastName = user.LastName;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditProfile(Guid? id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/trang-chu");
            }
            else
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(User user)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(user);
                user.ModifiedBy = session.UserName;
                user.Password = Encryptor.MD5Hash(user.Password);
                user.ConfirmPassword = Encryptor.MD5Hash(user.ConfirmPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/trang-chu");
            }
            return View(user);
        }
    }
}