using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Major { get; set; }
        public string Email { get; set; }
    }
}
