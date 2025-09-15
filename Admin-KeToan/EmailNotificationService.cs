using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Timers;
using Admin_KeToan.Data;
using Microsoft.EntityFrameworkCore;

namespace Admin_KeToan
{
    public class EmailNotificationService
    {
        private readonly System.Timers.Timer _emailTimer;
        private List<Model.Email> _cachedEmails;

        public EmailNotificationService()
        {
            _emailTimer = new System.Timers.Timer();
            _emailTimer.Elapsed += EmailTimer_Elapsed;
            _emailTimer.AutoReset = false; // Chạy một lần, sau đó lập lịch lại
            ScheduleDailyCheck(10, 00); // Lập lịch kiểm tra lúc 10:00 sáng
        }

        public void Start() => _emailTimer.Start();
        public void Stop() => _emailTimer.Stop();

        private void ScheduleDailyCheck(int hour, int minute)
        {
            var now = DateTime.Now;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            if (scheduledTime <= now)
                scheduledTime = scheduledTime.AddDays(1);
            var interval = (scheduledTime - now).TotalMilliseconds;
            _emailTimer.Interval = interval;
            Console.WriteLine($"[{DateTime.Now}] Scheduled email check for {scheduledTime:dd/MM/yyyy HH:mm:ss}");
        }

        private void EmailTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckAndSendEmails();
            ScheduleDailyCheck(10, 00); // Lập lịch lại cho 10:00 sáng ngày tiếp theo
        }

        private void CheckAndSendEmails()
        {
            try
            {
                using var context = new KeToanDbContext();
                var payments = context.Payments
                    .Include(p => p.Loan)
                    .ThenInclude(l => l.Bank)
                    .Include(p => p.NotificationEmails)
                    .Where(p => !p.IsConfirmed && p.PaymentDate.HasValue && p.PaymentDate.Value.Date == DateTime.Today.AddDays(3))
                    .ToList();
                Console.WriteLine($"[{DateTime.Now}] Payments found: {payments.Count}");
                if (!payments.Any()) return;

                _cachedEmails = context.Emails.ToList();
                Console.WriteLine($"[{DateTime.Now}] Emails found: {_cachedEmails.Count}");
                if (!_cachedEmails.Any())
                {
                    LogError("Không có email nào được cấu hình.");
                    return;
                }

                foreach (var payment in payments)
                {
                    var loan = payment.Loan;
                    var bank = loan?.Bank;
                    if (loan == null || bank == null)
                    {
                        Console.WriteLine($"[{DateTime.Now}] Bỏ qua thanh toán {payment.PaymentId}: Loan hoặc Bank là null");
                        continue;
                    }

                    // Kiểm tra xem đây có phải là khoản thanh toán cuối cùng
                    bool isFinalPayment = context.Payments
                        .Where(p => p.LoanId == loan.LoanId && !p.IsConfirmed && p.EndDate > payment.EndDate)
                        .Count() == 0 && (loan.Balance - payment.PrincipalPaid <= 0 || loan.IsCompleted);

                    var emailsToNotify = payment.NotificationEmails.Any() ? payment.NotificationEmails : _cachedEmails;
                    foreach (var email in emailsToNotify)
                    {
                        SendEmailNotification(email.EmailAddress, bank.BankName, loan.LoanName,
                            payment.PaymentDate.Value, payment.InterestPaid, payment.PrincipalPaid, isFinalPayment, loan.Balance);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Lỗi gửi email: {ex.Message}");
            }
        }
        private void SendEmailNotification(string emailAddress, string bankName, string loanName,
            DateTime paymentDate, decimal interestPaid, decimal principalPaid, bool isFinalPayment, decimal loanBalance)
        {
            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["SmtpEmail"],
                    "Hệ thống quản lý lãi suất DRB");
                var toAddress = new MailAddress(emailAddress);
                string fromPassword = ConfigurationManager.AppSettings["SmtpPassword"];
                const string subject = "Thông báo đến hạn thanh toán lãi";
                string body = $"Kính gửi bộ phận kế toán,\n\n" +
                             $"Khoản vay '{loanName}' từ ngân hàng {bankName} sẽ đến hạn thanh toán vào ngày {paymentDate:dd/MM/yyyy}.\n" +
                             $"Chi tiết:\n" +
                             $"- Tiền lãi: {interestPaid:N2} \n" +
                             (isFinalPayment
                                 ? $"- Tiền gốc: {principalPaid:N0} (Toàn bộ số dư {loanBalance:N0} sẽ được trả hết)\n"
                                 : $"- Tiền gốc: {principalPaid:N0} \n") +
                             $"Vui lòng chuẩn bị thanh toán đúng hạn.\n\n" +
                             $"Trân trọng,\nHệ thống Kế Toán";

                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["SmtpHost"],
                    Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]),
                    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };
                smtp.Send(message);
                Console.WriteLine($"[{DateTime.Now}] Gửi email thành công tới {emailAddress}");
            }
            catch (Exception ex)
            {
                LogError($"Lỗi gửi email đến {emailAddress}: {ex.Message}");
            }
        }

        private void LogError(string message)
        {
            string error = $"{DateTime.Now}: {message}";
            Console.WriteLine(error);
            File.AppendAllText("error.log", error + "\n");
        }
    }
}