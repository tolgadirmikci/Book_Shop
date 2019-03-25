using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Shop.Models
{
    public class Book
    {
        public int Id { get; set; }

  
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Book Summary")]
        public string BookSummary { get; set; }


        [Display(Name = "ISBN No")]
        public int BookIsbnNo { get; set; }

    
        [Display(Name = "Photo")]
        public string BookPhotoPath { get; set; }

     
        [Display(Name = "Book Quantity")]
        public int BookQuantity { get; set; }

       
        [Display(Name = "Book Price")]
        public double BookPrice { get; set; }

   
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        [Display(Name = "Author Name")]
        public int AuthorId { get; set; }

        //Relations
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes{ get; set; }
        public  virtual ICollection<BookCustomerOrder> BookOrder { get; set; }
        //burada kaldım
    }
}