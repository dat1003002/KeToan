using System;
using System.Configuration;

namespace Admin_KeToan
{
    public class Configuration
    {
        private static Configuration _instance;
        private string _connectionString;

        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }
                return _instance;
            }
        }

        private Configuration()
        {
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["MyDb"]?.ConnectionString;
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new ConfigurationErrorsException("Chuỗi kết nối 'MyDb' không tồn tại trong app.config.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đọc chuỗi kết nối: " + ex.Message);
            }
        }

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Chuỗi kết nối chưa được tải.");
            }
            return _connectionString;
        }
    }
}