using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Admin_KeToan.Data;
using Admin_KeToan.Model;
using System.IO;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace Admin_KeToan
{
    public partial class KhoanVay : Form
    {
        private readonly int _bankId;
        private readonly string _paymentPeriod;
        private int _selectedLoanId = 0;
        private RadioButton radioUSD;
        private RadioButton radioVND;
        private DateTimePicker dateEndDate;

        public KhoanVay(int bankId, string paymentPeriod)
        {
            InitializeComponent();
            _bankId = bankId;
            _paymentPeriod = paymentPeriod;
            Load += KhoanVay_Load;
            inputAmount.TextChanged += inputAmount_TextChanged;
            inputDuration.TextChanged += inputDuration_TextChanged;

            radioUSD = new RadioButton { Name = "radioUSD", Text = "USD" };
            radioVND = new RadioButton { Name = "radioVND", Text = "VND" };

            dateEndDate = new DateTimePicker
            {
                Name = "dateEndDate",
                Format = DateTimePickerFormat.Short,
                Enabled = false
            };

            ExcelPackage.License.SetNonCommercialPersonal("Your Name");
        }

        private void KhoanVay_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupComboBoxes();
            SetupRadioButtons();
            SetupDateEndDate();
            LoadLoans();
            dataLoan.Width = ClientSize.Width;
            dataLoan.Location = new Point(0, dataLoan.Location.Y);
        }

        private void SetupComboBoxes()
        {
            cbchuky.Items.Clear();
            cbchuky.Items.AddRange(new string[] { "Tháng", "Quý" });

            try
            {
                using var context = new KeToanDbContext();
                var banks = context.Banks
                    .Select(b => new { b.BankId, b.BankName })
                    .ToList();
                cbnganhang.DataSource = banks;
                cbnganhang.DisplayMember = "BankName";
                cbnganhang.ValueMember = "BankId";
                cbnganhang.SelectedValue = _bankId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách ngân hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupRadioButtons()
        {
            radioUSD.Location = new Point(cbnganhang.Location.X, cbnganhang.Location.Y + cbnganhang.Height + 10);
            radioUSD.Size = new Size(80, 20);
            radioUSD.Checked = true;
            Controls.Add(radioUSD);

            radioVND.Location = new Point(radioUSD.Location.X + 100, radioUSD.Location.Y);
            radioVND.Size = new Size(80, 20);
            radioVND.Checked = false;
            Controls.Add(radioVND);
        }

        private void SetupDateEndDate()
        {
            dateEndDate.Location = new Point(dateStartDate.Location.X, dateStartDate.Location.Y + dateStartDate.Height + 10);
            dateEndDate.Size = new Size(200, 20);
            Controls.Add(dateEndDate);
        }

        private void SetupDataGridView()
        {
            dataLoan.AutoGenerateColumns = false;
            dataLoan.Columns.Clear();
            dataLoan.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "LoanId", HeaderText = "ID", Visible = false, ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "LoanName", HeaderText = "Tên khoản vay", ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "Amount", HeaderText = "Số tiền vay", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }, ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "Currency", HeaderText = "Tiền tệ", ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "Duration", HeaderText = "Thời hạn vay", ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Ngày bắt đầu", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "Ngày kết thúc", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "PaymentPeriod", HeaderText = "Chu kỳ thanh toán", ReadOnly = true },
                new DataGridViewTextBoxColumn { DataPropertyName = "Balance", HeaderText = "Số dư", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }, ReadOnly = true },
                new DataGridViewCheckBoxColumn { Name = "Select", HeaderText = "Chọn", Width = 50, ReadOnly = false }
            );
            dataLoan.ColumnHeadersDefaultCellStyle.Font = new Font(dataLoan.Font, FontStyle.Bold);
            dataLoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataLoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataLoan.ReadOnly = false;
            dataLoan.CellFormatting += DataLoan_CellFormatting;
            dataLoan.CellContentClick += dataLoan_CellContentClick;
            dataLoan.CellValueChanged += DataLoan_CellValueChanged;
        }

        private void DataLoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int loanId = (int)dataLoan.Rows[e.RowIndex].Cells[0].Value;
                using var context = new KeToanDbContext();
                var earliestUnconfirmedPayment = context.Payments
                    .Where(p => p.LoanId == loanId && !p.IsConfirmed)
                    .OrderBy(p => p.EndDate)
                    .FirstOrDefault();

                if (earliestUnconfirmedPayment != null && earliestUnconfirmedPayment.EndDate.Date <= DateTime.Today.AddDays(3))
                {
                    dataLoan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    dataLoan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi định dạng dòng khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataLoan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataLoan.Columns[e.ColumnIndex].Name == "Select")
            {
                // Không làm gì, chỉ cho phép chọn/bỏ chọn
            }
        }

        private void dataLoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                _selectedLoanId = (int)dataLoan.Rows[e.RowIndex].Cells[0].Value;

                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _selectedLoanId);

                if (loan == null)
                {
                    MessageBox.Show($"Không tìm thấy khoản vay với LoanId: {_selectedLoanId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Populate form fields
                inputLoanName.Text = loan.LoanName;
                inputAmount.Text = loan.Amount.ToString("N0");
                dateStartDate.Value = loan.StartDate;
                inputDuration.Text = loan.Duration.ToString();
                dateEndDate.Value = loan.EndDate;

                string paymentPeriod = loan.PaymentPeriod?.Trim();
                if (!string.IsNullOrEmpty(paymentPeriod))
                {
                    paymentPeriod = paymentPeriod.Replace("\u00A0", " ")
                        .Replace("\u200B", "")
                        .Replace("\u202F", " ")
                        .Replace("\u2003", " ")
                        .Replace("\u2002", " ")
                        .Replace("\t", "")
                        .Trim()
                        .Normalize(System.Text.NormalizationForm.FormC);
                }

                string thangNormalized = "Tháng".Normalize(System.Text.NormalizationForm.FormC);
                string quyNormalized = "Quý".Normalize(System.Text.NormalizationForm.FormC);

                if (string.IsNullOrEmpty(paymentPeriod))
                {
                    cbchuky.SelectedIndex = -1;
                }
                else if (string.Equals(paymentPeriod, thangNormalized, StringComparison.OrdinalIgnoreCase))
                {
                    cbchuky.SelectedIndex = 0;
                }
                else if (string.Equals(paymentPeriod, quyNormalized, StringComparison.OrdinalIgnoreCase))
                {
                    cbchuky.SelectedIndex = 1;
                }
                else
                {
                    cbchuky.SelectedIndex = -1;
                    MessageBox.Show($"Giá trị PaymentPeriod '{paymentPeriod}' không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                cbnganhang.SelectedValue = loan.BankId;

                if (loan.Currency == "USD")
                {
                    radioUSD.Checked = true;
                    radioVND.Checked = false;
                }
                else if (loan.Currency == "VND")
                {
                    radioUSD.Checked = false;
                    radioVND.Checked = true;
                }
                else
                {
                    radioUSD.Checked = false;
                    radioVND.Checked = false;
                    MessageBox.Show($"Giá trị Currency '{loan.Currency}' không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLoans()
        {
            try
            {
                using var context = new KeToanDbContext();
                var loans = context.Loans
                    .Where(l => l.BankId == _bankId && l.PaymentPeriod == _paymentPeriod && !l.IsCompleted)
                    .Select(l => new
                    {
                        l.LoanId,
                        l.LoanName,
                        l.Amount,
                        l.Currency,
                        l.Duration,
                        l.StartDate,
                        l.EndDate,
                        l.PaymentPeriod,
                        l.Balance,
                        EarliestUnconfirmedPaymentDate = context.Payments
                            .Where(p => p.LoanId == l.LoanId && !p.IsConfirmed && p.InterestPaid > 0)
                            .OrderBy(p => p.EndDate)
                            .Select(p => p.EndDate)
                            .FirstOrDefault()
                    })
                    .OrderBy(l => l.EarliestUnconfirmedPaymentDate == default(DateTime) ? DateTime.MaxValue : l.EarliestUnconfirmedPaymentDate)
                    .ThenBy(l => l.LoanName)
                    .Select(l => new
                    {
                        l.LoanId,
                        l.LoanName,
                        l.Amount,
                        l.Currency,
                        l.Duration,
                        l.StartDate,
                        l.EndDate,
                        l.PaymentPeriod,
                        l.Balance
                    })
                    .ToList();

                dataLoan.DataSource = null;
                dataLoan.DataSource = loans;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inputDuration_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(inputDuration.Text, out int duration) && duration > 0)
            {
                string selectedPaymentPeriod = cbchuky.SelectedItem?.ToString();
                DateTime startDate = dateStartDate.Value;

                if (selectedPaymentPeriod == "Tháng")
                {
                    dateEndDate.Value = startDate.AddMonths(duration);
                }
                else if (selectedPaymentPeriod == "Quý")
                {
                    dateEndDate.Value = startDate.AddMonths(duration * 3);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedLoanId == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoản vay để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _selectedLoanId);

                if (loan == null)
                {
                    MessageBox.Show($"Không tìm thấy khoản vay với LoanId: {_selectedLoanId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                loan.LoanName = inputLoanName.Text;
                if (long.TryParse(inputAmount.Text.Replace(",", ""), out long amount))
                    loan.Amount = amount;
                else
                {
                    MessageBox.Show("Số tiền vay không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(inputDuration.Text, out int duration) || duration <= 0)
                {
                    MessageBox.Show("Thời hạn vay không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                loan.Duration = duration;
                loan.StartDate = dateStartDate.Value;
                loan.EndDate = dateEndDate.Value;

                string selectedPaymentPeriod = cbchuky.SelectedItem?.ToString();
                if (selectedPaymentPeriod != "Tháng" && selectedPaymentPeriod != "Quý")
                {
                    MessageBox.Show("Chu kỳ thanh toán không hợp lệ. Vui lòng chọn 'Tháng' hoặc 'Quý'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                loan.PaymentPeriod = selectedPaymentPeriod;

                loan.BankId = (int)cbnganhang.SelectedValue;

                loan.Currency = radioUSD.Checked ? "USD" : "VND";

                if (string.IsNullOrEmpty(loan.LoanName) || string.IsNullOrEmpty(loan.PaymentPeriod) || string.IsNullOrEmpty(loan.Currency))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                context.SaveChanges();
                LoadLoans();
                _selectedLoanId = 0;
                ClearInputs();
                MessageBox.Show("Chỉnh sửa khoản vay thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi chỉnh sửa khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            inputLoanName.Text = string.Empty;
            inputAmount.Text = string.Empty;
            dateStartDate.Value = DateTime.Now;
            inputDuration.Text = string.Empty;
            dateEndDate.Value = DateTime.Now;
            cbchuky.SelectedIndex = -1;
            cbnganhang.SelectedValue = _bankId;
            radioUSD.Checked = true;
            radioVND.Checked = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (_selectedLoanId == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoản vay để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _selectedLoanId);

                if (loan == null)
                {
                    MessageBox.Show($"Không tìm thấy khoản vay với LoanId: {_selectedLoanId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var hasPayments = context.Payments.Any(p => p.LoanId == _selectedLoanId);
                if (hasPayments)
                {
                    MessageBox.Show("Không thể xóa khoản vay vì đã có các thanh toán liên quan.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa khoản vay '{loan.LoanName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                context.Loans.Remove(loan);
                context.SaveChanges();

                LoadLoans();
                _selectedLoanId = 0;
                ClearInputs();
                MessageBox.Show("Xóa khoản vay thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inputAmount_TextChanged(object sender, EventArgs e)
        {
            string text = inputAmount.Text.Replace(",", "");
            if (string.IsNullOrEmpty(text))
            {
                inputAmount.Text = "";
                return;
            }

            if (!long.TryParse(text, out long number) || number < 0)
            {
                inputAmount.Text = inputAmount.Text.Remove(inputAmount.Text.Length - 1);
                inputAmount.SelectionStart = inputAmount.Text.Length;
                return;
            }

            inputAmount.TextChanged -= inputAmount_TextChanged;
            inputAmount.Text = string.Format("{0:N0}", number);
            inputAmount.SelectionStart = inputAmount.Text.Length;
            inputAmount.TextChanged += inputAmount_TextChanged;
        }

        private void btnchitiet_Click(object sender, EventArgs e)
        {
            if (_selectedLoanId == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoản vay để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _selectedLoanId);
                if (loan == null)
                {
                    MessageBox.Show($"Không tìm thấy khoản vay với LoanId: {_selectedLoanId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var chiTietForm = new ChiTiet(_selectedLoanId, loan.PaymentPeriod);
                chiTietForm.ShowDialog();
                LoadLoans();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            var selectedLoanIds = dataLoan.Rows.Cast<DataGridViewRow>()
                .Where(row => (bool?)row.Cells["Select"].Value == true)
                .Select(row => (int)row.Cells[0].Value)
                .ToList();

            if (!selectedLoanIds.Any())
            {
                MessageBox.Show("Vui lòng chọn ít nhất một khoản vay để xuất Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Chọn nơi lưu file Excel",
                    FileName = $"DanhSachKhoanVay_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using var package = new ExcelPackage();
                    using var context = new KeToanDbContext();

                    foreach (var loanId in selectedLoanIds)
                    {
                        var loan = context.Loans.FirstOrDefault(l => l.LoanId == loanId);
                        if (loan == null)
                        {
                            MessageBox.Show($"Không tìm thấy khoản vay với LoanId: {loanId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        var payments = context.Payments
                            .Where(p => p.LoanId == loanId)
                            .Select(p => new
                            {
                                p.StartDate,
                                p.EndDate,
                                p.NumberOfDays,
                                p.InterestRate,
                                p.InterestPaid,
                                p.PrincipalPaid,
                                p.CumulativeInterestPaid,
                                p.EstimatedInterestPaid,
                                p.EstimatedPrincipalPaid,
                                p.DayCountConvention,
                                p.IsConfirmed
                            })
                            .ToList();

                        // Tạo worksheet mới cho mỗi khoản vay
                        var worksheet = package.Workbook.Worksheets.Add($"KhoanVay_{loanId}_{loan.LoanName.Replace(" ", "_")}");

                        // Loan Details Section
                        worksheet.Cells[1, 1].Value = $"Thông tin khoản vay - {loan.LoanName}";
                        worksheet.Cells[1, 1, 1, 9].Merge = true;
                        worksheet.Cells[1, 1].Style.Font.Bold = true;

                        // Loan Headers
                        string[] loanHeaders = new[]
                        {
                            "Tên khoản vay", "Số tiền vay", "Tiền tệ", "Thời hạn vay",
                            "Ngày bắt đầu", "Ngày kết thúc", "Chu kỳ thanh toán",
                            "Số tiền tính lãi", "Số tiền gốc trả mỗi kỳ"
                        };
                        for (int i = 0; i < loanHeaders.Length; i++)
                        {
                            worksheet.Cells[2, i + 1].Value = loanHeaders[i];
                            worksheet.Cells[2, i + 1].Style.Font.Bold = true;
                        }

                        // Loan Data
                        worksheet.Cells[3, 1].Value = loan.LoanName;
                        worksheet.Cells[3, 2].Value = loan.Amount;
                        worksheet.Cells[3, 2].Style.Numberformat.Format = "#,##0";
                        worksheet.Cells[3, 3].Value = loan.Currency;
                        worksheet.Cells[3, 4].Value = loan.Duration;
                        worksheet.Cells[3, 5].Value = loan.StartDate;
                        worksheet.Cells[3, 5].Style.Numberformat.Format = "dd/mm/yyyy";
                        worksheet.Cells[3, 6].Value = loan.EndDate;
                        worksheet.Cells[3, 6].Style.Numberformat.Format = "dd/mm/yyyy";
                        worksheet.Cells[3, 7].Value = loan.PaymentPeriod;
                        worksheet.Cells[3, 8].Value = loan.Balance;
                        worksheet.Cells[3, 8].Style.Numberformat.Format = "#,##0";
                        worksheet.Cells[3, 9].Value = loan.PrincipalPaymentAmount;
                        worksheet.Cells[3, 9].Style.Numberformat.Format = "#,##0";

                        // Payments Section
                        worksheet.Cells[5, 1].Value = $"Thông tin thanh toán - {loan.LoanName}";
                        worksheet.Cells[5, 1, 5, 11].Merge = true;
                        worksheet.Cells[5, 1].Style.Font.Bold = true;

                        // Payment Headers
                        string[] paymentHeaders = new[]
                        {
                            "Ngày bắt đầu", "Ngày kết thúc", "Số ngày", "Lãi suất",
                            "Tiền lãi đã trả", "Tiền gốc đã trả", "Tổng lãi đã trả",
                            "Tiền lãi dự tính", "Tiền gốc dự tính", "Phương thức tính ngày",
                            "Trạng thái thanh toán"
                        };
                        for (int i = 0; i < paymentHeaders.Length; i++)
                        {
                            worksheet.Cells[6, i + 1].Value = paymentHeaders[i];
                            worksheet.Cells[6, i + 1].Style.Font.Bold = true;
                        }

                        // Payment Data
                        for (int i = 0; i < payments.Count; i++)
                        {
                            var payment = payments[i];
                            worksheet.Cells[i + 7, 1].Value = payment.StartDate;
                            worksheet.Cells[i + 7, 1].Style.Numberformat.Format = "dd/mm/yyyy";
                            worksheet.Cells[i + 7, 2].Value = payment.EndDate;
                            worksheet.Cells[i + 7, 2].Style.Numberformat.Format = "dd/mm/yyyy";
                            worksheet.Cells[i + 7, 3].Value = payment.NumberOfDays;
                            worksheet.Cells[i + 7, 4].Value = payment.InterestRate;
                            worksheet.Cells[i + 7, 5].Value = payment.InterestPaid;
                            worksheet.Cells[i + 7, 5].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[i + 7, 6].Value = payment.PrincipalPaid;
                            worksheet.Cells[i + 7, 6].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[i + 7, 7].Value = payment.CumulativeInterestPaid;
                            worksheet.Cells[i + 7, 7].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[i + 7, 8].Value = payment.EstimatedInterestPaid;
                            worksheet.Cells[i + 7, 8].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[i + 7, 9].Value = payment.EstimatedPrincipalPaid;
                            worksheet.Cells[i + 7, 9].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[i + 7, 10].Value = payment.DayCountConvention;
                            worksheet.Cells[i + 7, 11].Value = payment.IsConfirmed ? "Đã thanh toán" : "Chưa thanh toán";
                        }

                        // Auto-fit columns for this worksheet
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    }

                    // Lưu file Excel
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    MessageBox.Show($"Xuất file thành công: {saveFileDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}