using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class StudentFeedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ClassId { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt = DateTime.Now;
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Class Class { get; set; }

    }
}
