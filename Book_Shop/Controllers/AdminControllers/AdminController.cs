using Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Shop.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        //View Admin Panel
        public ActionResult Index()
        {
            //Admin Panel Anasayfada toplam kitap sayısı,yapılan yorumlar vb. sayılarını gösterme
            ViewData["bookCounts"] = db.Books.Count();
            ViewData["commentCount"] = db.Comments.Count();
            ViewData["ordersCount"] = db.Customers.Count();
            ViewData["contactCount"] = db.Contacts.Count();

           
            return View();
        }
    }
}