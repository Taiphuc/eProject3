﻿using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class AboutsController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/About
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.About.Where(x => x.IsDeleted == false).ToList());
        }
    }
}