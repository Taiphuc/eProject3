using eProject3.Model.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Areas.Admin.Controllers
{
    public class OrdersController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        // GET: Admin/Order
        public ActionResult Index(string sortOrder, string currentFilter, int? page, string search)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CreatedOnSortParm = sortOrder == "CreatedOn" ? "createdOn_desc" : "CreatedOn";
            var orders = db.Order.Where(x => x.IsDeleted == false);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            if (!String.IsNullOrEmpty(search))
            {
                orders = orders.Where(s => s.ShipName.Contains(search) || s.ShipPhone.Contains(search) || s.ShipEmail.Contains(search) || s.ShipAddress.Contains(search));
            }
            //Sort
            switch (sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderByDescending(s => s.ShipName);
                    break;
                case "CreatedOn":
                    orders = orders.OrderBy(s => s.CreatedOn);
                    break;
                case "createdOn_desc":
                    orders = orders.OrderByDescending(s => s.CreatedOn);
                    break;
                default:
                    orders = orders.OrderBy(s => s.ShipName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = search;
            return View(orders.ToPagedList(pageNumber, pageSize));
        }
    }
}