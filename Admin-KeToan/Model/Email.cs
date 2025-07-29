using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_KeToan.Model
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
