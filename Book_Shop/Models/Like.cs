using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int LikeCount { get; set; }
        public int BookId { get; set; }
        //relations
        public virtual Book Books { get; set; }

    }
}