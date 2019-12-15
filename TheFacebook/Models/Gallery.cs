using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Gallery
    {
        public int galleryID { get; set; }
        public string galleryName { get; set; }
        public int uploadedBy { get; set; }
        public virtual ICollection<Photo> photos { get; set; }
    }
}