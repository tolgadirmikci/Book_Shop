using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class BookCustomerOrder
    {
        public int Id { get; set; }
        public virtual Customer CustomerOrder { get; set; }
        public virtual Book BookOrder { get; set; }
    }
}