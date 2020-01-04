using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheFacebook.Models;

namespace TheFacebook.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comment
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult New(int photoId, string text)
        {
            Comment newComment = new Comment();
            newComment.Text = text;
            newComment.Date = DateTime.Now;
            newComment.UserId = User.Identity.GetUserId();
            newComment.PhotoId = photoId;
            db.Comments.Add(newComment);
            Photo photo = db.Photos.Find(photoId);
            photo.Comments.Add(newComment);
            db.SaveChanges();
            TempData["message"] = "Comentariul a fost adaugat!";


            return RedirectToAction("Show", "Photo", new { id = photoId });
        }
    }
}