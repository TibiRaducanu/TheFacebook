using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        //public ImageSource Portrait { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public int GalleryId { get; set; }
    }
}