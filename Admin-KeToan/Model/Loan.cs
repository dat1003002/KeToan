using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_KeToan.Model
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        public int BankId { get; set; }

        [Required]
        [StringLength(100)]
        public string LoanName { get; set; }

        [Required]
        public decimal Amount { get; set; } // Số tiền vay

        [Required]
        public int Duration { get; set; } // Số kỳ (tháng hoặc quý)

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Balance { get; set; } // Số dư

        public bool IsCompleted { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentPeriod { get; set; } // "Hàng Tháng" hoặc "Qúy"

        [Required]
        [StringLength(3)]
        public string Currency { get; set; } // "USD" hoặc "VND"

        [Required]
        public int StartPrincipalPaymentMonth { get; set; } // Tháng bắt đầu cắt gốc

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal PrincipalPaymentAmount { get; set; } // Số tiền gốc cắt mỗi kỳ

        [ForeignKey("BankId")]
        public Bank Bank { get; set; }

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}