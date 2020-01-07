using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime Date { get; set; }
        public int MailBoxId { get; set; }
    }
}