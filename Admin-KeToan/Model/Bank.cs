using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin_KeToan.Model
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }

        [Required]
        public string BankName { get; set; }

        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}