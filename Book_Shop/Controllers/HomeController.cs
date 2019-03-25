using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Book_Shop.Models;
using System.Net;
using PagedList;
using PagedList.Mvc;
namespace Book_Shop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int Page=1)
        {
            
            var books = db.Books.OrderByDescending(b => b.Id).ToPagedList(Page,8);//Paged List İle her sayfada 8 kitap listelenmesi...
            return View(books);
        }


        public ActionResult DetailsBook(int? id)
        {
         
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book==null)
            {
                return HttpNotFound();
            }
            ViewData["categories"] = db.Categories.ToList();//Category listesini gönderiyoruz
            return View(book);
        }

        //On Details Page , Comments Processes....
        [HttpPost]//DetailsBook GEt action'ı üzerinden alınan yorumların post action'ı
        public ActionResult DetailsBook(int id,string commentText,string commenterName)
        {

            Comment comments = new Comment();

            if (commentText!=null && commenterName!=null)//ikiside null olmaması lazım
            {
                comments.BookId = id;
                comments.CommenterName = commenterName;
                comments.CommentText = commentText;
                db.Comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }

        //Http-get CustomerOrder Form View
        public ActionResult CustomerOrder()
        {
            return View();
        }

        //Sipariş için kullanıcı bilgilerini alıyorum. ve bu arada details sayfası üzerindende id ile bookname'i almış oldum...
        [HttpPost]
        public ActionResult CustomerOrder(string Name,string Surname,string email,string addressLine1,string phoneNumber,int id)
        {
            Customer customers = new Customer();
           string bookName = (from b in db.Books
                           where b.Id == id
                           select b.BookName).First().ToString();
            
            customers.CustomerName = Name;
            customers.CustomerSurname = Surname;
            customers.CustomerEmail = email;
            customers.CustomerPhoneNumber = phoneNumber;
            customers.CustomerAddress = addressLine1;
            customers.LoginAuthName = null;
            customers.OrderBook = bookName;
            db.Customers.Add(customers);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        //Details Book Sayfasından Gelen ilgili id'ye göre kitap filtreleme Action
        public ActionResult BookCategoryFilter(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookFilter = from b in db.Books
                             where b.CategoryId == id
                             select b;
            if (bookFilter==null)
            {
                return HttpNotFound();
            }


            return View(bookFilter.ToList());
        }//Burayı yap

        //Contact User Form 
        public ActionResult ContactUser()
        {

            return View();
        }

 

     
    }
}