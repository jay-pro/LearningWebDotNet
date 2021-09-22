using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDStudent { get; set; }
        public string IDUser { get; set; }
        [ForeignKey("IDUser")]
        public int isChecked { get; set; }
        public User User { get; set;  }
    }
}
