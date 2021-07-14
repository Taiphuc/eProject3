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
    public class FeedBacksController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/FeedBack
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            var result = db.Feedback.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn);
            return View(result.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedBack feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
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
            FeedBack feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Email,Address,Content,Reply,CreatedOn,CreatedBy,IsDeleted")] FeedBack feedback)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(feedback);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                feedback.ModifiedBy = session.UserName;
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/Admin/FeedBacks");
            }
            return View(feedback);
        }

        public ActionResult Delete(Guid? id)
        {
            CountMessage();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedBack feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FeedBack feedback = db.Feedback.Find(id);
            feedback.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/FeedBacks");
        }
    }
}