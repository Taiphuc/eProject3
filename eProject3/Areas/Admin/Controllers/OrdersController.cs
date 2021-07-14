using eProject3.Model.DAL;
using eProject3.Model.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult CancelOrder(string sortOrder, int? page, string searchString, string currentFilter)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CreatedOnSortParm = sortOrder == "CreatedOn" ? "createdOn_desc" : "CreatedOn";
            var orders = db.Order.Where(x => x.IsDeleted == true);

            //Filter
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.ShipName.Contains(searchString) || s.ShipPhone.Contains(searchString) || s.ShipEmail.Contains(searchString) || s.ShipAddress.Contains(searchString));
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
            ViewBag.SearchString = searchString;
            return View(orders.OrderByDescending(x => x.CreatedOn).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = new OrderDetail();
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(db.OrderDetail.Where(x => x.OrderId == order.Id).ToList());
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Order order = db.Order.Find(id);
            order.IsDeleted = true;
            //Delete in order detail and Add turn quantity of product
            OrderDetail orderDetail = db.OrderDetail.FirstOrDefault(x => x.OrderId == order.Id);
            Product product = db.Product.FirstOrDefault(x => x.Id == orderDetail.ProductId);
            if (orderDetail != null)
            {
                orderDetail.IsDeleted = true;
            }
            if (product != null)
            {
                product.Quantity += orderDetail.Quantity;
            }
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/Admin/Orders");
        }

        public ActionResult UndoOrder(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("UndoOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult UndoOrderConfirmed(Guid? id)
        {
            Order order = db.Order.Find(id);
            order.IsDeleted = false;
            //Undo in order detail and Add turn quantity of product
            OrderDetail orderDetail = db.OrderDetail.FirstOrDefault(x => x.OrderId == order.Id);
            Product product = db.Product.FirstOrDefault(x => x.Id == orderDetail.ProductId);
            if (orderDetail != null)
            {
                orderDetail.IsDeleted = false;
            }
            if (product != null)
            {
                product.Quantity -= orderDetail.Quantity;
            }
            db.SaveChanges();
            SetAlert("Phục hồi đơn hàng thành công", "success");
            return Redirect("/Admin/CancelOrder");
        }

        public JsonResult ChangeStatus(Guid? id)
        {
            var result = new OrderDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}