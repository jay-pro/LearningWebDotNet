using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDComment { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IDUser { get; set; }
        [ForeignKey("IDUser")]
        public User User { get; set; }
    }
}
