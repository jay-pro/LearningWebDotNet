using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDEnrollment { get; set; }
        public string IDStudent { get; set; }
        public string IDClass { get; set; }
        public int Progress { get; set; }
        public int Score { get; set; }
        [ForeignKey("IDStudent")]
        public Student Student { get; set; }
        [ForeignKey("IDClass")]
        public Class Class { get; set; }
    }
}
