using eProject3.Model.Common;
using eProject3.Model.DAL;
using eProject3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eProject3.Controllers
{
    public class HomeController : BaseController
    {
        private Project3DbContext db = new Project3DbContext();
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            List<Menu> menu = new List<Menu>();
            menu = db.Menu.Where(x => x.ParentId == null && !x.IsDeleted).OrderBy(x => x.DisplayOrder).ToList();
            return PartialView(menu);
        }

        [ChildActionOnly]
        public ActionResult SubMenu(int parentId)
        {
            List<Menu> sMenu = new List<Menu>();
            sMenu = db.Menu.Where(x => x.ParentId == parentId && !x.IsDeleted).OrderBy(x => x.DisplayOrder).ToList();
            ViewBag.Count = sMenu.Count();
            return PartialView(sMenu);
        }

        //[ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult Contact()
        {
            List<Contact> contact = new List<Contact>();
            contact = db.Contact.Where(x => !x.IsDeleted).ToList();
            ViewBag.Count = contact.Count();
            return PartialView(contact);
        }

        [ChildActionOnly]
        public PartialViewResult Slide()
        {
            List<Slide> slide = new List<Slide>();
            slide = db.Slide.Where(x => !x.IsDeleted).ToList();
            ViewBag.Count = slide.Count();
            return PartialView(slide);
        }

        public ActionResult HistoryPayment()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                return View(db.Order.Where(x => x.UserId == session.UserId && !x.IsDeleted).ToList());
            }
            else
            {
                SetAlert("Bạn cần đăng nhập để thực hiện", "warning");
                return Redirect("/User/Login");
            }
        }

        public ActionResult HistoryPaymentDetail(Guid? id)
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
            return View(db.OrderDetail.Where(x => x.OrderId == order.Id).OrderByDescending(x => x.CreatedOn).ToList());
        }

        public ActionResult CancelOrderView()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                return View(db.Order.Where(x => x.UserId == session.UserId && x.IsDeleted).ToList());
            }
            else
            {
                SetAlert("Bạn cần đăng nhập để thực hiện", "warning");
                return Redirect("/User/Login");
            }
        }

        public ActionResult CancelOrder(Guid? id)
        {
            Order order = db.Order.Find(id);
            if (order.Status == true)
            {
                SetAlert("Đơn hàng đã được vận chuyển, bạn không thể hủy!", "warning");
            }
            else
            {
                order.IsDeleted = true;
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
                SetAlert("Bạn vừa hủy một đơn hàng", "warning");
            }
            return Redirect("/Home/HistoryPayment");
        }

        public ActionResult UndoOrder(Guid? id)
        {
            Order order = db.Order.Find(id);
            order.IsDeleted = false;
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
            return Redirect("/Home/HistoryPayment");
        }
    }
}