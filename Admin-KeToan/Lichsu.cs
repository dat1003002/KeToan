using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admin_KeToan.Data;
using Admin_KeToan.Model;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;

namespace Admin_KeToan
{
    public partial class Lichsu : Form
    {
        private int currentPage = 1; // Trang hiện tại
        private readonly int pageSize = 20; // Số bản ghi mỗi trang
        private List<object> allLoans; // Danh sách tất cả khoản vay
        private List<object> filteredLoans; // Danh sách khoản vay sau khi lọc
        private Label lblPageInfo; // Nhãn hiển thị thông tin phân trang
        private Button btnPrevious, btnNext; // Nút chuyển trang trước/sau

        public Lichsu()
        {
            InitializeComponent();
            InitializeControls();
            InitializeEvents();
            LoadBanksAsync(); // Tải danh sách ngân hàng
            LoadCompletedLoansAsync();
            // Tắt chức năng giãn form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        private void InitializeControls()
        {
            // Thiết lập DataGridView (đã tạo sẵn trong Designer)
            datalisu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datalisu.RowHeadersVisible = true;
            datalisu.RowHeadersWidth = 60;
            // Tạo các điều khiển phân trang
            btnPrevious = new Button { Text = "Previous", Size = new Size(100, 30), Location = new Point(datalisu.Left, datalisu.Bottom + 10) };
            btnNext = new Button { Text = "Next", Size = new Size(100, 30), Location = new Point(datalisu.Right - 100, datalisu.Bottom + 10) };
            lblPageInfo = new Label { Text = "Trang 1", AutoSize = true, Location = new Point((datalisu.Left + datalisu.Right - 100) / 2, datalisu.Bottom + 10) };
            // Thêm các điều khiển vào form
            this.Controls.Add(btnPrevious);
            this.Controls.Add(btnNext);
            this.Controls.Add(lblPageInfo);
        }

        private void InitializeEvents()
        {
            btnPrevious.Click += (s, e) => ChangePage(-1);
            btnNext.Click += (s, e) => ChangePage(1);
            cbloc.SelectedIndexChanged += cbloc_SelectedIndexChanged;
            this.Load += (s, e) => CenterDataGridView();
            this.Resize += (s, e) => CenterDataGridView();
        }

        private async Task LoadBanksAsync()
        {
            try
            {
                using (var context = new KeToanDbContext())
                {
                    // Truy vấn danh sách ngân hàng từ cơ sở dữ liệu
                    var banks = await context.Banks
                        .Select(b => new { b.BankId, b.BankName })
                        .AsNoTracking()
                        .ToListAsync();
                    // Thêm mục "Tất cả" để hiển thị tất cả khoản vay
                    var bankList = new List<object> { new { BankId = 0, BankName = "Tất cả" } };
                    bankList.AddRange(banks);
                    // Gán danh sách ngân hàng vào ComboBox
                    cbloc.DataSource = null; // Reset DataSource để tránh lỗi
                    cbloc.DataSource = bankList;
                    cbloc.DisplayMember = "BankName";
                    cbloc.ValueMember = "BankId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách ngân hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCompletedLoansAsync()
        {
            try
            {
                using (var context = new KeToanDbContext())
                {
                    // Truy vấn tất cả khoản vay đã hoàn thành
                    allLoans = await context.Loans
                        .Where(l => l.IsCompleted)
                        .Include(l => l.Bank)
                        .Select(l => new
                        {
                            l.LoanId,
                            l.BankId,
                            l.LoanName,
                            BankName = l.Bank.BankName,
                            l.Amount,
                            l.Currency,
                            l.StartDate,
                            l.EndDate,
                            Status = l.IsCompleted ? "Đã Hoàn thành" : "Chưa hoàn thành"
                        })
                        .AsNoTracking()
                        .ToListAsync<object>();
                    filteredLoans = allLoans;
                    currentPage = 1;
                    DisplayPage(currentPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbloc.SelectedItem != null)
                {
                    dynamic selectedItem = cbloc.SelectedItem;
                    string selectedBankName = selectedItem.BankName;
                    // Lọc danh sách khoản vay theo BankName
                    if (selectedBankName == "Tất cả")
                    {
                        filteredLoans = allLoans;
                    }
                    else
                    {
                        filteredLoans = allLoans.Where(l => l.GetType().GetProperty("BankName")?.GetValue(l)?.ToString() == selectedBankName).ToList();
                        if (!filteredLoans.Any())
                        {
                            filteredLoans = new List<object>(); // Trả về danh sách trống nếu không tìm thấy
                        }
                    }
                    currentPage = 1; // Reset về trang đầu
                    DisplayPage(currentPage);
                    // Làm mới DataGridView để đảm bảo hiển thị đúng
                    datalisu.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc danh sách khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayPage(int page)
        {
            if (filteredLoans == null || !filteredLoans.Any())
            {
                datalisu.DataSource = null;
                lblPageInfo.Text = "Không tìm thấy kết quả";
                currentPage = 1;
                return;
            }
            int totalPages = (int)Math.Ceiling((double)filteredLoans.Count / pageSize);
            currentPage = Math.Clamp(page, 1, totalPages);
            var pageData = filteredLoans.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            datalisu.DataSource = pageData;
            // Thêm số thứ tự (STT) vào header của dòng
            for (int i = 0; i < pageData.Count; i++)
            {
                int stt = (currentPage - 1) * pageSize + i + 1;
                datalisu.Rows[i].HeaderCell.Value = stt.ToString();
            }
            // Đảm bảo cột BankName luôn hiển thị
            if (!datalisu.Columns.Contains("BankName") && pageData.Any())
            {
                datalisu.Columns.Add("BankName", "Ngân hàng");
            }
            if (datalisu.Columns.Contains("BankName"))
            {
                datalisu.Columns["BankName"].Visible = true;
                datalisu.Columns["BankName"].HeaderText = "Ngân hàng";
                // Gán giá trị BankName từ dữ liệu
                for (int i = 0; i < pageData.Count; i++)
                {
                    var bankName = pageData[i].GetType().GetProperty("BankName")?.GetValue(pageData[i])?.ToString() ?? "Chưa xác định";
                    datalisu.Rows[i].Cells["BankName"].Value = bankName;
                }
            }
            // Tùy chỉnh tiêu đề cột
            if (datalisu.Columns.Contains("LoanId")) datalisu.Columns["LoanId"].Visible = false; // Ẩn cột LoanId
            if (datalisu.Columns.Contains("LoanName")) datalisu.Columns["LoanName"].HeaderText = "Tên khoản vay";
            if (datalisu.Columns.Contains("Amount")) datalisu.Columns["Amount"].HeaderText = "Số tiền vay";
            if (datalisu.Columns.Contains("Currency")) datalisu.Columns["Currency"].HeaderText = "Tiền tệ";
            if (datalisu.Columns.Contains("StartDate")) datalisu.Columns["StartDate"].HeaderText = "Ngày bắt đầu";
            if (datalisu.Columns.Contains("EndDate")) datalisu.Columns["EndDate"].HeaderText = "Ngày kết thúc";
            if (datalisu.Columns.Contains("Status")) datalisu.Columns["Status"].HeaderText = "Trạng thái";
            // Thêm cột Checkbox sau cột Status
            if (!datalisu.Columns.Contains("SelectLoan"))
            {
                var checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "SelectLoan",
                    HeaderText = "Chọn",
                    Width = 60,
                    FalseValue = false,
                    TrueValue = true,
                    IndeterminateValue = false
                };
                datalisu.Columns.Add(checkBoxColumn);
                // Đặt vị trí cột Checkbox ngay sau cột Status
                if (datalisu.Columns.Contains("Status"))
                {
                    datalisu.Columns["SelectLoan"].DisplayIndex = datalisu.Columns["Status"].DisplayIndex + 1;
                }
            }
            // Ẩn cột BankId nếu có
            if (datalisu.Columns.Contains("BankId")) datalisu.Columns["BankId"].Visible = false;
            // Định dạng cột số tiền (dùng "N0" cho long để hiển thị số nguyên lớn với dấu phẩy)
            if (datalisu.Columns.Contains("Amount"))
                datalisu.Columns["Amount"].DefaultCellStyle.Format = "N0";
            // Định dạng cột ngày
            if (datalisu.Columns.Contains("StartDate"))
                datalisu.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (datalisu.Columns.Contains("EndDate"))
                datalisu.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            // Bôi đậm tiêu đề cột
            Font headerFont = new Font(datalisu.Font.FontFamily, 12, FontStyle.Bold);
            foreach (DataGridViewColumn column in datalisu.Columns)
            {
                if (column.Visible)
                {
                    column.HeaderCell.Style.Font = headerFont;
                }
            }
            UpdatePaginationInfo();
        }

        private void CenterDataGridView()
        {
            int verticalSpace = 45;
            datalisu.Location = new Point(
                (this.ClientSize.Width - datalisu.Width) / 2,
                this.ClientSize.Height - datalisu.Height - verticalSpace
            );
            int controlY = datalisu.Bottom + 10;
            btnPrevious.Location = new Point(datalisu.Left, controlY);
            btnNext.Location = new Point(datalisu.Right - btnNext.Width, controlY);
            lblPageInfo.Location = new Point(
                (datalisu.Left + datalisu.Right - lblPageInfo.Width) / 2,
                controlY
            );
        }

        private void ChangePage(int direction)
        {
            int totalPages = (int)Math.Ceiling((double)filteredLoans.Count / pageSize);
            currentPage = Math.Clamp(currentPage + direction, 1, totalPages > 0 ? totalPages : 1);
            DisplayPage(currentPage);
        }

        private void UpdatePaginationInfo()
        {
            int totalPages = (int)Math.Ceiling((double)filteredLoans.Count / pageSize);
            lblPageInfo.Text = filteredLoans.Any() ? $"Trang {currentPage}/{totalPages}" : "Không tìm thấy kết quả";
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Thiết lập giấy phép cho EPPlus (sửa đúng cách cho license non-commercial)
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Lấy danh sách các khoản vay được chọn
                var selectedLoans = datalisu.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells["SelectLoan"].Value != null && (bool)row.Cells["SelectLoan"].Value)
                    .Select(row => new
                    {
                        LoanId = (int)row.Cells["LoanId"].Value,
                        LoanName = row.Cells["LoanName"].Value?.ToString(),
                        BankName = row.Cells["BankName"].Value?.ToString(),
                        Amount = row.Cells["Amount"].Value != null ? Convert.ToInt64(row.Cells["Amount"].Value) : 0L, // Sử dụng long (0L) để phù hợp
                        Currency = row.Cells["Currency"].Value?.ToString(),
                        StartDate = row.Cells["StartDate"].Value != null ? Convert.ToDateTime(row.Cells["StartDate"].Value) : DateTime.MinValue,
                        EndDate = row.Cells["EndDate"].Value != null ? Convert.ToDateTime(row.Cells["EndDate"].Value) : DateTime.MinValue,
                        Status = row.Cells["Status"].Value?.ToString()
                    })
                    .ToList();
                if (!selectedLoans.Any())
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một khoản vay để xuất Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Mở SaveFileDialog để người dùng chọn vị trí lưu file
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Chọn nơi lưu file Excel",
                    FileName = $"LichSuKhoanVay_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                })
                {
                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                        return;
                    // Kiểm tra quyền truy cập thư mục
                    string directory = Path.GetDirectoryName(saveFileDialog.FileName);
                    if (!Directory.Exists(directory) || !new DirectoryInfo(directory).GetAccessControl().AreAccessRulesProtected) // Sửa để kiểm tra quyền đúng hơn (AreAccessRulesProtected thay vì AreAccessRulesCanonical)
                    {
                        MessageBox.Show("Không có quyền truy cập thư mục đích. Vui lòng chọn thư mục khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    using (var package = new ExcelPackage(new FileInfo(saveFileDialog.FileName)))
                    {
                        using (var context = new KeToanDbContext())
                        {
                            // Nhóm các khoản vay theo BankName
                            var groupedLoans = selectedLoans.GroupBy(loan => loan.BankName).ToList();
                            int worksheetIndex = 1;
                            foreach (var group in groupedLoans)
                            {
                                string bankName = group.Key ?? "Unknown";
                                // Tạo tên worksheet hợp lệ
                                string worksheetName = $"Bank_{worksheetIndex++}_{bankName.Replace(" ", "_").Replace("/", "_").Replace("\\", "_")}";
                                if (worksheetName.Length > 31) worksheetName = worksheetName.Substring(0, 31);
                                var worksheet = package.Workbook.Worksheets.Add(worksheetName);
                                int currentRow = 1;
                                // Tiêu đề chính cho ngân hàng
                                worksheet.Cells[currentRow, 1].Value = $"Thông tin khoản vay - Ngân hàng {bankName}";
                                worksheet.Cells[currentRow, 1, currentRow, 7].Merge = true; // Merge 7 cột theo yêu cầu
                                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                                currentRow++;
                                // Tiêu đề cột cho khoản vay (giữ 7 cột theo yêu cầu)
                                string[] loanHeaders = new[] { "Mã khoản vay", "Tên khoản vay", "Số tiền vay", "Tiền tệ", "Ngày bắt đầu", "Ngày kết thúc", "Trạng thái" };
                                for (int i = 0; i < loanHeaders.Length; i++)
                                {
                                    worksheet.Cells[currentRow, i + 1].Value = loanHeaders[i];
                                    worksheet.Cells[currentRow, i + 1].Style.Font.Bold = true;
                                }
                                currentRow++;
                                // Dữ liệu khoản vay
                                foreach (var loan in group)
                                {
                                    worksheet.Cells[currentRow, 1].Value = loan.LoanId;
                                    worksheet.Cells[currentRow, 2].Value = loan.LoanName;
                                    worksheet.Cells[currentRow, 3].Value = loan.Amount;
                                    worksheet.Cells[currentRow, 3].Style.Numberformat.Format = "#,##0"; // Phù hợp cho long (số nguyên lớn)
                                    worksheet.Cells[currentRow, 4].Value = loan.Currency;
                                    worksheet.Cells[currentRow, 5].Value = loan.StartDate;
                                    worksheet.Cells[currentRow, 5].Style.Numberformat.Format = "dd/mm/yyyy";
                                    worksheet.Cells[currentRow, 6].Value = loan.EndDate;
                                    worksheet.Cells[currentRow, 6].Style.Numberformat.Format = "dd/mm/yyyy";
                                    worksheet.Cells[currentRow, 7].Value = loan.Status;
                                    currentRow++;
                                    // Thông tin thanh toán cho khoản vay
                                    var dbLoan = context.Loans
                                        .Include(l => l.Payments)
                                        .FirstOrDefault(l => l.LoanId == loan.LoanId);
                                    if (dbLoan == null)
                                    {
                                        MessageBox.Show($"Không tìm thấy khoản vay với LoanId: {loan.LoanId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }
                                    // Tiêu đề phần thanh toán
                                    worksheet.Cells[currentRow, 1].Value = $"Thông tin thanh toán - {loan.LoanName}";
                                    worksheet.Cells[currentRow, 1, currentRow, 11].Merge = true;
                                    worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                                    currentRow++;
                                    // Tiêu đề cột thanh toán
                                    string[] paymentHeaders = new[]
                                    {
                                        "Ngày bắt đầu", "Ngày kết thúc", "Số ngày", "Lãi suất",
                                        "Tiền lãi đã trả", "Tiền gốc đã trả", "Tổng lãi đã trả",
                                        "Tiền lãi dự tính", "Tiền gốc dự tính", "Phương thức tính ngày",
                                        "Trạng thái thanh toán"
                                    };
                                    for (int i = 0; i < paymentHeaders.Length; i++)
                                    {
                                        worksheet.Cells[currentRow, i + 1].Value = paymentHeaders[i];
                                        worksheet.Cells[currentRow, i + 1].Style.Font.Bold = true;
                                    }
                                    currentRow++;
                                    // Dữ liệu thanh toán
                                    foreach (var payment in dbLoan.Payments.OrderBy(p => p.StartDate))
                                    {
                                        worksheet.Cells[currentRow, 1].Value = payment.StartDate;
                                        worksheet.Cells[currentRow, 1].Style.Numberformat.Format = "dd/mm/yyyy";
                                        worksheet.Cells[currentRow, 2].Value = payment.EndDate;
                                        worksheet.Cells[currentRow, 2].Style.Numberformat.Format = "dd/mm/yyyy";
                                        worksheet.Cells[currentRow, 3].Value = payment.NumberOfDays;
                                        worksheet.Cells[currentRow, 4].Value = payment.InterestRate;
                                        worksheet.Cells[currentRow, 5].Value = payment.InterestPaid;
                                        worksheet.Cells[currentRow, 5].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[currentRow, 6].Value = payment.PrincipalPaid;
                                        worksheet.Cells[currentRow, 6].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[currentRow, 7].Value = payment.CumulativeInterestPaid;
                                        worksheet.Cells[currentRow, 7].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[currentRow, 8].Value = payment.EstimatedInterestPaid;
                                        worksheet.Cells[currentRow, 8].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[currentRow, 9].Value = payment.EstimatedPrincipalPaid;
                                        worksheet.Cells[currentRow, 9].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[currentRow, 10].Value = payment.DayCountConvention;
                                        worksheet.Cells[currentRow, 11].Value = payment.IsConfirmed ? "Đã thanh toán" : "Chưa thanh toán";
                                        currentRow++;
                                    }
                                    currentRow++;
                                }
                                // Tự động điều chỉnh độ rộng cột
                                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                            }
                            // Lưu file Excel
                            package.Save();
                            MessageBox.Show($"Xuất file thành công: {saveFileDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnchitet_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dòng được chọn trong DataGridView
                var selectedRows = datalisu.Rows.Cast<DataGridViewRow>()
                    .Where(row => row.Cells["SelectLoan"].Value != null && (bool)row.Cells["SelectLoan"].Value)
                    .ToList();
                if (selectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn đúng một khoản vay để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy LoanId từ dòng được chọn
                int selectedLoanId = (int)selectedRows[0].Cells["LoanId"].Value;
                // Mở form CtLichsu và truyền LoanId
                var ctLichsuForm = new CtLichsu(selectedLoanId);
                ctLichsuForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}