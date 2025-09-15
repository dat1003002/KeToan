using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admin_KeToan.Data;
using Microsoft.EntityFrameworkCore;

namespace Admin_KeToan
{
    public partial class CtLichsu : Form
    {
        private readonly int _loanId;
        private int currentPage = 1; // Trang hiện tại
        private readonly int pageSize = 20; // Số bản ghi mỗi trang
        private List<object> allPayments; // Danh sách tất cả thanh toán
        private Label lblPageInfo; // Nhãn hiển thị thông tin phân trang
        private Button btnPrevious, btnNext; // Nút chuyển trang trước/sau

        public CtLichsu(int loanId)
        {
            _loanId = loanId;
            InitializeComponent();
            InitializeDataGridView();
            InitializeControls();
            InitializeEvents();
            LoadPaymentsAsync();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        private void InitializeDataGridView()
        {
            // Thiết lập DataGridView
            datactLichsu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datactLichsu.RowHeadersVisible = true;
            datactLichsu.RowHeadersWidth = 60;
            // Định dạng cột
            datactLichsu.ColumnAdded += (s, e) =>
            {
                var column = e.Column;
                switch (column.Name)
                {
                    case "StartDate":
                        column.HeaderText = "Ngày bắt đầu";
                        column.DefaultCellStyle.Format = "dd/MM/yyyy";
                        break;
                    case "EndDate":
                        column.HeaderText = "Ngày kết thúc";
                        column.DefaultCellStyle.Format = "dd/MM/yyyy";
                        break;
                    case "NumberOfDays":
                        column.HeaderText = "Số ngày";
                        break;
                    case "InterestRate":
                        column.HeaderText = "Lãi suất";
                        column.DefaultCellStyle.Format = "0.00\\%"; // Hiển thị lãi suất với 2 chữ số thập phân và ký hiệu %
                        break;
                    case "InterestPaid":
                        column.HeaderText = "Tiền lãi đã trả";
                        column.DefaultCellStyle.Format = "#,##0"; // Định dạng cho long (không thập phân)
                        break;
                    case "PrincipalPaid":
                        column.HeaderText = "Tiền cắt gốc";
                        column.DefaultCellStyle.Format = "#,##0"; // Định dạng cho long (không thập phân)
                        break;
                    case "DayCountConvention":
                        column.HeaderText = "Phương thức tính ngày";
                        break;
                    case "IsConfirmed":
                        column.HeaderText = "Trạng thái";
                        break;
                    default:
                        column.Visible = false; // Ẩn các cột không được định nghĩa
                        break;
                }
                // Bôi đậm tiêu đề cột
                column.HeaderCell.Style.Font = new Font(datactLichsu.Font.FontFamily, 12, FontStyle.Bold);
            };
        }

        private void InitializeControls()
        {
            // Tạo các điều khiển phân trang
            btnPrevious = new Button { Text = "Previous", Size = new Size(100, 30), Location = new Point(datactLichsu.Left, datactLichsu.Bottom + 10) };
            btnNext = new Button { Text = "Next", Size = new Size(100, 30), Location = new Point(datactLichsu.Right - 100, datactLichsu.Bottom + 10) };
            lblPageInfo = new Label { Text = "Trang 1", AutoSize = true, Location = new Point((datactLichsu.Left + datactLichsu.Right - 100) / 2, datactLichsu.Bottom + 10) };
            // Thêm các điều khiển vào form
            this.Controls.Add(btnPrevious);
            this.Controls.Add(btnNext);
            this.Controls.Add(lblPageInfo);
        }

        private void InitializeEvents()
        {
            btnPrevious.Click += (s, e) => ChangePage(-1);
            btnNext.Click += (s, e) => ChangePage(1);
            this.Load += (s, e) => CenterDataGridView();
            this.Resize += (s, e) => CenterDataGridView();
        }

        private async Task LoadPaymentsAsync()
        {
            try
            {
                using (var context = new KeToanDbContext())
                {
                    // Truy vấn danh sách thanh toán cho khoản vay
                    allPayments = await context.Payments
                        .Where(p => p.LoanId == _loanId)
                        .Select(p => new
                        {
                            p.StartDate,
                            p.EndDate,
                            p.NumberOfDays,
                            p.InterestRate,
                            InterestPaid = (long)p.InterestPaid, // Ép kiểu về long để đảm bảo tương thích
                            PrincipalPaid = (long)p.PrincipalPaid, // Ép kiểu về long để đảm bảo tương thích
                            p.DayCountConvention,
                            IsConfirmed = p.IsConfirmed ? "Đã thanh toán" : "Chưa thanh toán"
                        })
                        .OrderBy(p => p.StartDate)
                        .AsNoTracking()
                        .ToListAsync<object>();
                    currentPage = 1;
                    DisplayPage(currentPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayPage(int page)
        {
            if (allPayments == null || !allPayments.Any())
            {
                datactLichsu.DataSource = null;
                lblPageInfo.Text = "Không tìm thấy kết quả";
                currentPage = 1;
                return;
            }
            int totalPages = (int)Math.Ceiling((double)allPayments.Count / pageSize);
            currentPage = Math.Clamp(page, 1, totalPages);
            var pageData = allPayments.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            datactLichsu.DataSource = pageData;
            // Thêm số thứ tự (STT) vào header của dòng
            for (int i = 0; i < pageData.Count; i++)
            {
                int stt = (currentPage - 1) * pageSize + i + 1;
                datactLichsu.Rows[i].HeaderCell.Value = stt.ToString();
            }
            UpdatePaginationInfo();
        }

        private void CenterDataGridView()
        {
            int verticalSpace = 45;
            datactLichsu.Location = new Point(
                (this.ClientSize.Width - datactLichsu.Width) / 2,
                this.ClientSize.Height - datactLichsu.Height - verticalSpace
            );
            int controlY = datactLichsu.Bottom + 10;
            btnPrevious.Location = new Point(datactLichsu.Left, controlY);
            btnNext.Location = new Point(datactLichsu.Right - btnNext.Width, controlY);
            lblPageInfo.Location = new Point(
                (datactLichsu.Left + datactLichsu.Right - lblPageInfo.Width) / 2,
                controlY
            );
        }

        private void ChangePage(int direction)
        {
            int totalPages = (int)Math.Ceiling((double)allPayments.Count / pageSize);
            currentPage = Math.Clamp(currentPage + direction, 1, totalPages > 0 ? totalPages : 1);
            DisplayPage(currentPage);
        }

        private void UpdatePaginationInfo()
        {
            int totalPages = (int)Math.Ceiling((double)allPayments.Count / pageSize);
            lblPageInfo.Text = allPayments.Any() ? $"Trang {currentPage}/{totalPages}" : "Không tìm thấy kết quả";
        }

        private void datactLichsu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể thêm logic xử lý khi nhấn vào ô trong datactLichsu nếu cần
        }
    }
}