using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public string commentText { get; set; }
        public int createdBy { get; set; }
    }
}