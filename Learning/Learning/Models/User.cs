using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Major { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
    }
}
