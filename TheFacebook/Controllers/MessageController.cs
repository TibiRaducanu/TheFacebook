using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheFacebook.Models;

namespace TheFacebook.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Message
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult New(string from, int mailBoxId, string to, string message)
        {
            Message newMessage = new Message();
            newMessage.Text = message;
            newMessage.From = from;
            newMessage.To = (from person in db.People where (person.Username == to) select person.UserId).FirstOrDefault();
            newMessage.MailBoxId = mailBoxId;
            newMessage.Date = DateTime.Now;
            db.Messages.Add(newMessage);
            MailBox mailbox = (from mail in db.MailBoxes where (mail.UserId == newMessage.To) select mail).FirstOrDefault();
            mailbox.Messages.Add(newMessage);
            db.SaveChanges();
            TempData["message"] = "Mesajul a fost trimis cu succes!";

            return RedirectToAction("Show", "MailBox", new { id = mailBoxId });
        }
    }
}