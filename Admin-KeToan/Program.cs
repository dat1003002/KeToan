using System;
using System.Windows.Forms;

namespace Admin_KeToan
{
    internal static class Program
    {
        private static EmailNotificationService _emailService;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            _emailService = new EmailNotificationService();
            _emailService.Start();
            try
            {
                Application.Run(new Login());
            }
            finally
            {
                _emailService.Stop(); // Dừng timer khi ứng dụng đóng
            }
        }
    }
}