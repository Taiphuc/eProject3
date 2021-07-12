using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}