using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Reply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDReply { get; set; }
        public string Content { get; set; } 
        public string IDUser { get; set; }
        public DateTime CretatedAt { get; set; }
        public string IDComment { get; set; }
        [ForeignKey("IDUser")]
        public User User { get; set; }
        [ForeignKey("IDComment")]
        public Comment Comment { get; set; }
    }
}
