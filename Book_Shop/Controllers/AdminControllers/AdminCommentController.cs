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
    public class AdminCommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminComment
        public ActionResult Index()
        {
           
            return View(db.Comments.ToList());
        }

        //Kötü içerikli yapılan yorumları sil....
        public ActionResult Delete(int? id)
        {

            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment delComment = db.Comments.Find(id);
            if (delComment==null)
            {
                return HttpNotFound();
            }

            db.Comments.Remove(delComment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}