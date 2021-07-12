using eProject3.Common;
using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class SlidesController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Slide
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Slide.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).Take(3).ToList());
        }
    }
}