using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheFacebook.Models
{
    public class Friend
    {
        [Key][Column(Order = 0)]
        public int FirstPersonId { get; set; }
        [Key][Column(Order = 1)]
        public int SecondPersonId { get; set; }
    }
}