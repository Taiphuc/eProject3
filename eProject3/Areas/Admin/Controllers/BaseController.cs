using eProject3.Model.Common;
using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private Project3DbContext db = new Project3DbContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null || session.GroupId != CommonConstants.ADMIN_GROUP && session.GroupId != CommonConstants.MOD_GROUP)
            {
                filterContext.Result = Redirect("/Admin/Login/Login");
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        protected void CountMessage()
        {
            var feedbacks = db.Feedback.Where(x => x.IsDeleted == false && x.Reply == null);
            TempData["cms"] = feedbacks.Count().ToString();
        }
        protected void CountProduct()
        {
            var products = db.Product.Where(x => x.IsDeleted == false && x.Quantity < 5);
            TempData["cpd"] = products.Count().ToString();
        }
        protected void CountOrder()
        {
            var orders = db.Order.Where(x => x.Status == false && x.IsDeleted == false);
            TempData["ord"] = orders.Count().ToString();
        }
    }
}