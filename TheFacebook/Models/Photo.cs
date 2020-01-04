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
        public string Image { get; set; } 
        public virtual ICollection<Comment> Comments { get; set; }
        public int GalleryId { get; set; }
        //public virtual Gallery Gallery { get; set; }
    }
}