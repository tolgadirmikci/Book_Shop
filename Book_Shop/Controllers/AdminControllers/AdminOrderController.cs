using Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Book_Shop.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: AdminOrder
            //View orders-siparişleri görüntüleme
        public ActionResult OrderList()
        {
            var orderBookList=db.Customers.OrderByDescending(d => d.Id).ToList();

            return View(orderBookList);
        }

        //burada siparişlerden gelen onay id ile book adedini azaltma  ve bu gelen bilgileri customer'dan silme logic..
       
        public ActionResult ConfirmAndDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cust = db.Customers.Find(id);
            if (cust==null)
            {
                return HttpNotFound();
            }

            return View(cust);
        } //HTTPGET ConfirmAndDelete

        [HttpPost]
        public ActionResult ConfirmAndDelete(int? id,FormCollection collection)
        {

            Customer cust = db.Customers.Find(id);

            var findBookName = cust.OrderBook.ToString();//Book Name Customer'dan gelen

            int quantityBook = (from d in db.Books//customerdan gelen book name ile Book Table'deki book name aynı ise o book name'in  adet bilgisini getir
                                where d.BookName == findBookName
                                select d.BookQuantity).First();
            quantityBook -= 1;//adedi 1 azalt
            if (quantityBook>0)//adet sayısı 0 dan büyükse yani stokta kitap varsa...
            {

                int bookId = (from d in db.Books//kitap id'sini al 
                              where d.BookName == findBookName
                              select d.Id).First();
                var findBookID = db.Books.Find(bookId);//bu id'ye göre db'de ilgili propertyleri al
                findBookID.BookQuantity = quantityBook;//1 azaltılmış olan kitabı update et eskisi ile 
                
                
                db.SaveChanges();//yapılan db değişiklerini kaydet....
            }
            else//stokta kitap yok ilgili karşılama sayfasına git....
            {
                return RedirectToAction("NoBookPage", "Home");
            }
          
            db.Customers.Remove(cust);//AdminOrder/OrderList/+id kaydı sil db'den 
            db.SaveChanges();
            return RedirectToAction("OrderList", "AdminOrder");
            
        }//ConfirmAndDelete HttpPOST


        //Stokta Kitap yoksa Görüntülenecek Page Action -->
        public ActionResult NoBookPage()
        {
            return View();
        }






    }//AdminOrder Controller....
}
