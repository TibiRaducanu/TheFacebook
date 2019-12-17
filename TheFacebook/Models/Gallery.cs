using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Gallery
    {   
        [Key]
        public int GalleryID { get; set; }
        [Required(ErrorMessage = "Numele galeriei este obligatoriu!")]
        [StringLength(20, ErrorMessage = "Numele galeriei nu poate avea mai mult de 20 de caractere")]
        public string GalleryName { get; set; }
        public int UploadedBy { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}