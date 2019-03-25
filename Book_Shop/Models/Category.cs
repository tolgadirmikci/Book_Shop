using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        //relations
        public virtual ICollection<Book> Books { get; set; }
    }
}