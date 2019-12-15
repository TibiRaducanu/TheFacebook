using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Photo
    {
        public int photoId { get; set; }
        public string photoName { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        public int albumId { get; set; }
    }
}