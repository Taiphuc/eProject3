using eProject3.Areas.Admin.Models;
using eProject3.Model.Common;
using eProject3.Model.DAL;
using eProject3.Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private Project3DbContext db = new Project3DbContext();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserId = user.Id;
                    userSession.GroupId = user.GroupId;
                    userSession.UserName = user.UserName;
                    userSession.FirstName = user.FirstName;
                    userSession.LastName = user.LastName;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    //Phan quyen dua tren session_credential
                    var listCredentials = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    //Luu user info vao session
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/Admin/Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản không có quyền đăng nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login/Login");
        }
    }
}