using eProject3.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProject3.Common
{
    public class AuditTable
    {
        public static void InsertAuditFields(object obj)
        {
            dynamic item = obj;
            item.IsDeleted = false;
            item.CreatedOn = DateTime.Now;
        }
        public static void UpdateAuditFields(object obj)
        {
            var user = new User();
            dynamic item = obj;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = user.UserName;
        }
    }
}