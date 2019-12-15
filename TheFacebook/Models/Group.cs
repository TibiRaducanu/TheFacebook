using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Group
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public virtual ICollection<Person> people { get; set; }
    }
}