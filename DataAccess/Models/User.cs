using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required,StringLength(50)]
        public string Email { get; set; }
        [Required,StringLength(50)]
        public string Password { get; set; }
        [Required,StringLength(50)]
        public string UserName { get; set; }
        [Required,StringLength(10)]
        public string Phone { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Order> Orders { get; set;}
    }
}
