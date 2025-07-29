using Admin_KeToan.Model;
using Microsoft.EntityFrameworkCore;

namespace Admin_KeToan.Data
{
    public class KeToanDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Email> Emails { get; set; } // Thêm DbSet cho Email

        public KeToanDbContext() { }
        public KeToanDbContext(DbContextOptions<KeToanDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Configuration.Instance.GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString)
                              .EnableSensitiveDataLogging(); // Bật logging để debug
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Loan -> Bank: NoAction để tránh multiple cascade paths
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Bank)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BankId)
                .OnDelete(DeleteBehavior.NoAction);

            // Payment -> Loan: NoAction để tránh multiple cascade paths
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Loan)
                .WithMany(l => l.Payments)
                .HasForeignKey(p => p.LoanId)
                .OnDelete(DeleteBehavior.NoAction);

            // Payment -> Email: Mối quan hệ nhiều-nhiều
            modelBuilder.Entity<Payment>()
                .HasMany(p => p.NotificationEmails)
                .WithMany(e => e.Payments)
                .UsingEntity<Dictionary<string, object>>(
                    "PaymentEmail", // Tên bảng liên kết
                    j => j.HasOne<Email>().WithMany().HasForeignKey("EmailId"),
                    j => j.HasOne<Payment>().WithMany().HasForeignKey("PaymentId"),
                    j =>
                    {
                        j.HasKey("PaymentId", "EmailId"); // Khóa chính composite
                        j.ToTable("PaymentEmail"); // Tên bảng trong DB
                    });
        }
    }
}