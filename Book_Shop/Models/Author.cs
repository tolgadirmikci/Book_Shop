using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name ="Author Name")]
        public string AuthorName { get; set; }
        public string AuthorInfo { get; set; }

        //relations
        public virtual ICollection<Book> Books { get; set; }

    }
}