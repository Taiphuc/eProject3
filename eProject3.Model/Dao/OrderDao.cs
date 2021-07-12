using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject3.Model.DAL;

namespace eProject3.Model.Dao
{
    public class OrderDao
    {
        private Project3DbContext db = new Project3DbContext();
        public Guid Insert(Order order)
        {
            order.Id = Guid.NewGuid();
            db.Order.Add(order);
            db.SaveChanges();
            return order.Id;
        }
        public bool ChangeStatus(Guid? id)
        {
            var order = db.Order.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }
    }
}
