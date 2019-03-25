using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommenterName  { get; set; }
        public string CommentText { get; set; }
        public int BookId { get; set; }
        //relations
        public virtual Book Books { get; set; }
    }
}