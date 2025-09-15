using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_KeToan.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public int LoanId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime? PaymentDate { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,4)")]
        public decimal InterestPaid { get; set; }
        [Required]
        [Column(TypeName = "decimal(15,4)")]
        public decimal PrincipalPaid { get; set; }
        [Column(TypeName = "decimal(15,4)")]
        public decimal CumulativeInterestPaid { get; set; }
        [Column(TypeName = "decimal(15,4)")]
        public decimal EstimatedInterestPaid { get; set; }
        [Column(TypeName = "decimal(15,4)")]
        public decimal EstimatedPrincipalPaid { get; set; }
        [Required]
        public int DayCountConvention { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        [Required]
        public bool IsEmailSent { get; set; }
        [ForeignKey("LoanId")]
        public Loan Loan { get; set; }
        public List<Email> NotificationEmails { get; set; } = new List<Email>();
    }
}