using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Controllers
{
    public class AboutController : Controller
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: About
        public ActionResult Index()
        {
            return View(db.About.Where(x => !x.IsDeleted).ToList());
        }
    }
}