using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{

    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Username-ul este obligatoriu!")]
        [StringLength(20, ErrorMessage = "Username-ul nu poate avea mai mult de 20 de caractere!")]
        public string Username { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Person> Requests { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public bool PrivateUser { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public MailBox Mail { get; set; }
    }
}