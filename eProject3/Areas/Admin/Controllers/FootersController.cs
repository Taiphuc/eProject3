using eProject3.Common;
using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class FootersController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Footer
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.Footer.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList());
        }
    }
}