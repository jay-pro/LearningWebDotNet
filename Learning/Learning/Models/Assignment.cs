using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDAssignemnt { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public string IDClass { get; set; }
        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public ICollection<AssignmentSubmission> AssignmentSubmission { get; set; }
        
    }
}
