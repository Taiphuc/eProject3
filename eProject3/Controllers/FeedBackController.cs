using eProject3.Common;
using eProject3.Model.Common;
using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Controllers
{
    public class FeedBackController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: FeedBack
        [HttpGet]
        public ActionResult ContactFromCustomer()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                SetAlert("Bạn cần đăng nhập để thực hiện", "warning");
                return Redirect("/User/Login");
            }
        }

        [HttpPost]
        public ActionResult ContactFromCustomer(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                feedBack.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(feedBack);
                db.Feedback.Add(feedBack);
                db.SaveChanges();
                SetAlert("Cảm ơn bạn đã góp ý", "success");
                return Redirect("/FeedBack/ReplyFeedBack");
            }
            return View(feedBack);
        }

        public ActionResult ReplyFeedBack()
        {
            var result = db.Feedback.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList();
            return View(result);
        }
    }
}