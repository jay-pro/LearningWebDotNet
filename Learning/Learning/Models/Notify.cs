using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Notify
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IDNotify { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CretatedAt { get; set; }
        public string IDUser { get; set; }
        [ForeignKey("IDUser")]

        public ICollection<User> User{ get; set; }
    }
}
