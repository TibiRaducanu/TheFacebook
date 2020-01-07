using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class MailBox
    {
        [Key]
        public int MailBoxId { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}