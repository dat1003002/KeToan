using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin_KeToan
{
    public partial class Login : Form
    {
        private Label lblError; // Khai báo lblError ở cấp độ class
        private ProgressBar progressBarLoading; // Khai báo ProgressBar ở cấp độ class
        private readonly string userPlaceholder = "Tên Đăng Nhập"; // Placeholder cho textuser
        private readonly string passPlaceholder = "Mật khẩu"; // Placeholder cho textpass

        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; 
            this.MaximizeBox = false; 
            this.MinimizeBox = false;

            // Thiết lập placeholder ban đầu
            textuser.Text = userPlaceholder;
            textuser.ForeColor = Color.Gray; // Màu xám cho placeholder
            textpass.Text = passPlaceholder;
            textpass.ForeColor = Color.Gray; // Màu xám cho placeholder
            textpass.PasswordChar = '\0'; // Tạm thời tắt PasswordChar để hiển thị placeholder

            // Thiết lập nút đăng nhập là nút mặc định khi nhấn Enter
            this.AcceptButton = btnlogin; // Nhấn Enter sẽ kích hoạt btnlogin_Click

            // Khởi tạo lblError
            lblError = new Label
            {
                Name = "lblError",
                Text = "",
                ForeColor = Color.Red,
                Location = new Point(50, textpass.Bottom + 10),
                AutoSize = true,
                Visible = false
            };
            this.Controls.Add(lblError);

            // Khởi tạo ProgressBar ở cuối form, dài hết chiều ngang
            progressBarLoading = new ProgressBar
            {
                Name = "progressBarLoading",
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 5, // Tăng tốc độ chạy nhanh hơn
                Location = new Point(0, this.ClientSize.Height - 40), // Bắt đầu từ mép trái, cách đáy 40px
                Size = new Size(this.ClientSize.Width, 20), // Chiều dài bằng chiều rộng form
                Visible = false // Ẩn ban đầu
            };
            this.Controls.Add(progressBarLoading);

            // Xử lý sự kiện khi form thay đổi kích thước để giữ ProgressBar dài hết form và ở dưới cùng
            this.Resize += (s, e) =>
            {
                progressBarLoading.Size = new Size(this.ClientSize.Width, 20);
                progressBarLoading.Location = new Point(0, this.ClientSize.Height - 40);
            };

            // Sự kiện Enter và Leave để xử lý placeholder
            textuser.Enter += Textuser_Enter;
            textuser.Leave += Textuser_Leave;
            textpass.Enter += Textpass_Enter;
            textpass.Leave += Textpass_Leave;

            // Sự kiện TextChanged để xóa thông báo lỗi
            textuser.TextChanged += textuser_TextChanged;
            textpass.TextChanged += textpass_TextChanged;

            // Đặt focus vào form để không tự động focus vào textuser hoặc textpass
            this.ActiveControl = null; // Hoặc this.ActiveControl = lblError; nếu muốn
        }

        private void Textuser_Enter(object sender, EventArgs e)
        {
            if (textuser.Text == userPlaceholder)
            {
                textuser.Text = "";
                textuser.ForeColor = Color.Black;
            }
        }

        private void Textuser_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textuser.Text))
            {
                textuser.Text = userPlaceholder;
                textuser.ForeColor = Color.Gray;
            }
        }

        private void Textpass_Enter(object sender, EventArgs e)
        {
            if (textpass.Text == passPlaceholder)
            {
                textpass.Text = "";
                textpass.ForeColor = Color.Black;
                textpass.PasswordChar = '*'; // Bật lại PasswordChar khi nhập
            }
        }

        private void Textpass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textpass.Text))
            {
                textpass.Text = passPlaceholder;
                textpass.ForeColor = Color.Gray;
                textpass.PasswordChar = '\0'; // Tắt PasswordChar để hiển thị placeholder
            }
        }

        private void textuser_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra và xóa thông báo lỗi nếu có
            if (textuser.Text != userPlaceholder && !string.IsNullOrEmpty(textuser.Text))
            {
                lblError.Visible = false;
            }
        }

        private void textpass_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra và xóa thông báo lỗi nếu có
            if (textpass.Text != passPlaceholder && !string.IsNullOrEmpty(textpass.Text))
            {
                lblError.Visible = false;
            }
        }

        private async void btnlogin_Click(object sender, EventArgs e)
        {
            // Xóa thông báo lỗi trước khi kiểm tra
            lblError.Visible = false;
            lblError.Text = "";

            // Hiển thị ProgressBar và vô hiệu hóa nút đăng nhập
            progressBarLoading.Visible = true;
            btnlogin.Enabled = false;

            // Giả lập tài khoản và mật khẩu (thay bằng kiểm tra cơ sở dữ liệu thực tế)
            string username = "admin";
            string password = "kt@1234";

            // Kiểm tra xem nội dung có phải là placeholder hay không
            string userInput = textuser.Text == userPlaceholder ? "" : textuser.Text;
            string passInput = textpass.Text == passPlaceholder ? "" : textpass.Text;

            // Kiểm tra nếu trường bị để trống
            if (string.IsNullOrWhiteSpace(userInput) || string.IsNullOrWhiteSpace(passInput))
            {
                lblError.Visible = true;
                lblError.Text = "Vui lòng nhập đầy đủ tên người dùng và mật khẩu!";
                progressBarLoading.Visible = false;
                btnlogin.Enabled = true;
                return; // Thoát khỏi hàm nếu có lỗi
            }

            // Giả lập thời gian xử lý đăng nhập (thay bằng truy vấn thực tế nếu cần)
            await Task.Delay(1000); // Chờ 2 giây để thấy hiệu ứng

            // Kiểm tra thông tin đăng nhập
            if (userInput == username && passInput == password)
            {
                // Đăng nhập thành công, mở form Home
                Home homeForm = new Home();
                homeForm.Show();
                this.Hide(); // Ẩn form đăng nhập
            }
            else
            {
                // Hiển thị thông báo lỗi
                lblError.Visible = true;
                lblError.Text = "Tài khoản hoặc mật khẩu không đúng!";
            }

            // Ẩn ProgressBar và bật lại nút đăng nhập
            progressBarLoading.Visible = false;
            btnlogin.Enabled = true;
        }
    }
}