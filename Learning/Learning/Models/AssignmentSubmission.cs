using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class AssignmentSubmission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDAssignmentSubmission { get; set; }
        public string FileName { get; set; }
        public string FileLink { get; set; }
        public string IDStudent { get; set; }
        public string IDAssignemnt { get; set; }
        [ForeignKey("IDStudent")]
        public Student Student { get; set; }
        [ForeignKey("IDAssignemnt")]
        public Assignment Assignment { get; set; }
    }
}
