using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheFacebook.Models;

namespace TheFacebook.Controllers
{
    public class MailBoxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MailBox
        public ActionResult Index()
        {
            if(TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View();
        }

        public ActionResult Show(int id)
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }

            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Mails = from mail in db.Messages where (mail.MailBoxId == id) select mail;
            MailBox mailBox = db.MailBoxes.Find(id);

            return View(mailBox);
        }
    }
}