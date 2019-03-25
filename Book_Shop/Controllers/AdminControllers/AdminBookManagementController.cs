using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shop.Models;
using System.Data.Entity;
using System.Web.Helpers;
using System.IO;
using System.Net;
using PagedList.Mvc;
using PagedList;
namespace Book_Shop.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBookManagementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminBookManagement
        public ActionResult Index(int Page=1 )
        {
            var bookList = db.Books.OrderBy(x => x.Id).ToPagedList(Page, 4);
            return View(bookList);
        }

        //Http Get--Create a new book on admin panel by admin
        public ActionResult CreateBook()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");

            return View();
        }

        //Http Post--Create a new book on admin panel by admin
        [HttpPost]
        public ActionResult CreateBook(Book book,HttpPostedFileBase BookPhotoPath)
        {
            if (ModelState.IsValid)//is my Book model valid?
            {
                if (BookPhotoPath != null)// Photo process...
                {
                    WebImage img = new WebImage(BookPhotoPath.InputStream);
                    FileInfo photoInfo = new FileInfo(BookPhotoPath.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Resize(280, 240);
                    img.Save("~/Uploads/BookPhoto/" + newPhoto);
                    book.BookPhotoPath = "/Uploads/BookPhoto/" + newPhoto;
                }
                db.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName", book.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", book.CategoryId);

            return View(book);
        }

        //Details for My Books on Admin Panel
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
            return View(book);
        }

        //Http Get--Edit book on admin panel
        public ActionResult EditBook(int id)
        {
          
            Book book = db.Books.Find(id);
            if (book==null)
            {
                return HttpNotFound();
            }
         
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName", book.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName",book.CategoryId);

            return View(book);
        }

        //Http post--Edit book
        [HttpPost]
        public ActionResult EditBook(int id,HttpPostedFileBase BookPhotoPath,Book book)
        {
            try
            {
             

                var books = db.Books.Find(id);
                if (BookPhotoPath != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(books.BookPhotoPath)))
                    {
                        System.IO.File.Delete(Server.MapPath(books.BookPhotoPath));
                    }
                    WebImage img = new WebImage(BookPhotoPath.InputStream);
                    FileInfo photoInfo = new FileInfo(BookPhotoPath.FileName);
                    string newPhoto = Guid.NewGuid().ToString() + photoInfo.Extension;
                    img.Resize(280, 240);
                    img.Save("~/Uploads/BookPhoto/" + newPhoto);
                    books.BookPhotoPath = "/Uploads/BookPhoto/" + newPhoto;

                    books.BookName = book.BookName;
                    books.BookIsbnNo = book.BookIsbnNo;
                    books.BookPrice = book.BookPrice;
                    books.BookSummary = book.BookSummary;
                    books.BookQuantity = book.BookQuantity;
                    books.CategoryId = book.CategoryId;
                    books.AuthorId = book.AuthorId;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName", book.AuthorId);
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", book.CategoryId);
                return View(book);
            } 
        }

        //http-get delete book all with relations 
        public ActionResult DeleteBook(int? id)
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
            return View(book);
        }

        //http post delete book
        [HttpPost]
        public ActionResult DeleteBook(int? id,FormCollection collection)
        {
            try
            {
                Book books = db.Books.Find(id);
                if (books == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(books.BookPhotoPath)))
                {
                    System.IO.File.Delete(Server.MapPath(books.BookPhotoPath));
                }
                foreach (var item in books.Comments.ToList())
                {
                    db.Comments.Remove(item);
                }
                foreach (var item in books.Likes)
                {
                    db.Likes.Remove(item);
                }

                db.Books.Remove(books);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch 
            {

                return View();
            }
        }//Delete Book HTTP POST



















    }//Controller Ending Bracket
}//Namespace Ending Bracket