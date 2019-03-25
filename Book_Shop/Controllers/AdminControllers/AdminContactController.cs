using Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Book_Shop.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class AdminContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminContact
        public ActionResult Index()
        {

            
            return View(db.Contacts.ToList());
        }

        public ActionResult DeleteContact (int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contactdel = db.Contacts.Find(id);
            if (contactdel==null)
            {
                return HttpNotFound();
            }
            db.Contacts.Remove(contactdel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //ajax post ile kullacıdan dataları alıp post işlemi
        public ActionResult GetDatas(string p_name="",string p_email="",string p_phone="",string p_message="" )
        {
            Contact contactadd = new Contact();

            if (p_name!=null && p_email!=null && p_message!=null && p_phone!=null)
            {
              
                contactadd.ContactName = p_name;
                contactadd.ContactMessage = p_message;
                contactadd.ContactEmail = p_email;
                contactadd.ContactPhone = p_phone;
                db.Contacts.Add(contactadd);
                db.SaveChanges();
            }

            return View();
        }
    }
}