using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string LoginAuthName { get; set; }
        public string OrderBook { get; set; }
        //relations
        public virtual ICollection< BookCustomerOrder> CustomerOrder { get; set; }

    }
}