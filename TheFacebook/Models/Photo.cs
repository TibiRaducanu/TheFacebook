using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Drawing;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TheFacebook.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        [DisplayName("Upload file")] 
        //public ImageSource Portrait { get; set; }
        public string ImagePath { get; set; }
        //public HttpPostedFileBase ImageFile { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public int GalleryId { get; set; }
    }
}