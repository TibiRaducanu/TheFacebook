using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Person
    {
        public int personId { get; set; }
        public string username { get; set; }
        public string mail { get; set; }
        public virtual ICollection<Gallery> galleries { get; set; }
        public virtual ICollection<Person> friends { get; set; }
        public virtual ICollection<Group> groups { get; set; }
        public bool privateUser { get; set; }
    }
}