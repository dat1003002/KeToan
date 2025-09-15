using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Admin_KeToan.Data;
using Admin_KeToan.Model;

namespace Admin_KeToan
{
    public partial class ChiTiet : Form
    {
        private readonly int _loanId;
        private readonly string _paymentPeriod;
        private bool _isEditing;
        private int _editingPaymentId;
        private readonly System.Windows.Forms.Timer _blinkTimer;
        private bool _blinkState;
        private Payment _tempPayment;

        public ChiTiet(int selectedLoanId, string paymentPeriod)
        {
            InitializeComponent();
            _loanId = selectedLoanId;
            _paymentPeriod = paymentPeriod;
            _blinkTimer = new System.Windows.Forms.Timer { Interval = 500 };
            Load += ChiTiet_Load;
            radio360.CheckedChanged += radio360_CheckedChanged;
            radio365.CheckedChanged += radio365_CheckedChanged;
            dateEndDate.ValueChanged += dateEndDate_ValueChanged;
            StartDate.ValueChanged += StartDate_ValueChanged;
            txtlaisuat.TextChanged += txtlaisuat_TextChanged;
            txtcatgoc.TextChanged += txtcatgoc_TextChanged;
            txtsongay.TextChanged += txtsongay_TextChanged;
        }

        private void ChiTiet_Load(object sender, EventArgs e)
        {
            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan == null)
                {
                    ShowErrorMessage($"Không tìm thấy khoản vay với LoanId: {_loanId}");
                    Close();
                    return;
                }
                UpdateLoanDisplays(loan);
                SetupDataGridView();
                LoadPayments();
                UpdateEstimatedInterestDisplay();
                _blinkTimer.Tick += BlinkTimer_Tick;
                _blinkTimer.Start();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi tải thông tin khoản vay: {ex.Message}");
                Close();
            }
        }

        private void LoadPayments()
        {
            try
            {
                using var context = new KeToanDbContext();
                var payments = context.Payments
                    .Where(p => p.LoanId == _loanId)
                    .Select(p => new
                    {
                        p.PaymentId,
                        p.StartDate,
                        p.EndDate,
                        p.PaymentDate,
                        p.NumberOfDays,
                        p.InterestRate,
                        p.EstimatedInterestPaid,
                        p.EstimatedPrincipalPaid,
                        p.InterestPaid,
                        p.PrincipalPaid,
                        p.DayCountConvention,
                        p.IsConfirmed
                    })
                    .OrderByDescending(p => p.StartDate)
                    .ToList();
                dataPayment.DataSource = payments;
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan != null)
                {
                    UpdateLoanBalance(loan, 0, context);
                    context.SaveChanges();
                    UpdateLoanDisplays(loan);
                }
                UpdateEstimatedInterestDisplay();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi tải danh sách thanh toán: {ex.Message}");
            }
        }

        private void SetupDataGridView()
        {
            dataPayment.AutoGenerateColumns = false;
            dataPayment.Columns.Clear();
            dataPayment.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "PaymentId", HeaderText = "ID", Visible = false },
                new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Từ Ngày", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "Đến Ngày", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "PaymentDate", HeaderText = "Ngày Trả", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "NumberOfDays", HeaderText = "Số ngày" },
                new DataGridViewTextBoxColumn { DataPropertyName = "InterestRate", HeaderText = "Lãi suất (%/năm)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "EstimatedInterestPaid", HeaderText = "Tiền lãi dự tính", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "EstimatedPrincipalPaid", HeaderText = "Tiền cắt gốc dự tính", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "InterestPaid", HeaderText = "Tiền lãi thực tế", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "PrincipalPaid", HeaderText = "Tiền cắt gốc thực tế", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } },
                new DataGridViewTextBoxColumn { DataPropertyName = "DayCountConvention", HeaderText = "Quy ước ngày" },
                new DataGridViewButtonColumn { Name = "ConfirmButton", HeaderText = "Xác nhận", Text = "Xác nhận", UseColumnTextForButtonValue = false },
                new DataGridViewButtonColumn { Name = "EditButton", HeaderText = "Chỉnh sửa", Text = "Chỉnh sửa", UseColumnTextForButtonValue = false }
            );
            dataPayment.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataPayment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataPayment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataPayment.ReadOnly = true;
            dataPayment.CellFormatting += DataPayment_CellFormatting;
            dataPayment.CellContentClick += dataPayment_CellContentClick;
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            _blinkState = !_blinkState;
            UpdatePaymentDateDisplay();
        }

        private void UpdateLoanDisplays(Loan loan)
        {
            lbcatgoc.Text = $"{GetLabelTitle(lbcatgoc.Text)} {loan.Balance:N2}";
            lbEndDate.Text = $"{GetLabelTitle(lbEndDate.Text)} {(loan.EndDate != DateTime.MinValue ? loan.EndDate.ToString("dd/MM/yyyy") : "Chưa xác định")}";
            UpdateInterestPaidDisplay();
            UpdateCumulativeInterestPaidDisplay();
            if (loan.IsCompleted)
            {
                lbcatgoc.Text = $"{GetLabelTitle(lbcatgoc.Text)} 0.00 (Đã hoàn thành)";
                MessageBox.Show("Khoản vay đã được hoàn thành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateInterestPaidDisplay()
        {
            try
            {
                using var context = new KeToanDbContext();
                var earliestUnconfirmedPayment = context.Payments
                    .Where(p => p.LoanId == _loanId && !p.IsConfirmed)
                    .OrderBy(p => p.StartDate)
                    .FirstOrDefault();
                lblai.Text = $"{GetLabelTitle(lblai.Text)} {(earliestUnconfirmedPayment?.EstimatedInterestPaid.ToString("N2") ?? "0.00")}";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi hiển thị tiền lãi dự tính: {ex.Message}");
            }
        }

        private void UpdateCumulativeInterestPaidDisplay()
        {
            try
            {
                using var context = new KeToanDbContext();
                var payments = context.Payments
                    .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                    .ToList();
                decimal totalPaid = payments.Sum(p => p.PrincipalPaid + p.InterestPaid);
                lbCumulativeInterestPaid.Text = $"{GetLabelTitle(lbCumulativeInterestPaid.Text)} {totalPaid:N2}";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi hiển thị tổng tiền: {ex.Message}");
            }
        }

        private void UpdateEstimatedInterestDisplay()
        {
            try
            {
                using var context = new KeToanDbContext();
                var totalEstimatedInterest = context.Payments
                    .Where(p => p.LoanId == _loanId)
                    .Sum(p => p.EstimatedInterestPaid);
                lbdutinh.Text = $"{GetLabelTitle(lbdutinh.Text)} {totalEstimatedInterest:N2}";
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi hiển thị tiền lãi dự tính: {ex.Message}");
            }
        }

        private string GetLabelTitle(string text) => text.Contains(":") ? text.Substring(0, text.IndexOf(':') + 1).Trim() : text;

        private void DataPayment_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                using var context = new KeToanDbContext();
                int paymentId = (int)dataPayment.Rows[e.RowIndex].Cells[0].Value;
                var payment = context.Payments.Find(paymentId);
                if (payment == null) return;
                var earliestUnconfirmedPayment = context.Payments
                    .Where(p => p.LoanId == _loanId && !p.IsConfirmed)
                    .OrderBy(p => p.StartDate)
                    .FirstOrDefault();
                var latestConfirmedPayment = context.Payments
                    .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                    .OrderByDescending(p => p.StartDate)
                    .FirstOrDefault();
                bool isOverdue = !payment.IsConfirmed && payment.EndDate.Date < DateTime.Today;
                bool isDueSoon = !payment.IsConfirmed && payment.EndDate.Date <= DateTime.Today.AddDays(3) && payment.EndDate.Date >= DateTime.Today;
                dataPayment.Rows[e.RowIndex].DefaultCellStyle.BackColor = isOverdue ? Color.Red :
                    isDueSoon ? Color.Red :
                    earliestUnconfirmedPayment != null && payment.PaymentId == earliestUnconfirmedPayment.PaymentId ? Color.LightGreen :
                    payment.IsConfirmed ? Color.LightGray : Color.White;
                if (e.ColumnIndex == dataPayment.Columns["ConfirmButton"].Index)
                {
                    var cell = dataPayment.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = (earliestUnconfirmedPayment != null && payment.PaymentId == earliestUnconfirmedPayment.PaymentId) ? "Xác nhận" : "";
                    cell.ReadOnly = payment.PaymentId != earliestUnconfirmedPayment?.PaymentId;
                }
                else if (e.ColumnIndex == dataPayment.Columns["EditButton"].Index)
                {
                    var cell = dataPayment.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Value = (earliestUnconfirmedPayment != null && payment.PaymentId == earliestUnconfirmedPayment.PaymentId) ||
                                 (latestConfirmedPayment != null && payment.PaymentId == latestConfirmedPayment.PaymentId) ?
                                 (_isEditing && _editingPaymentId == payment.PaymentId ? "Đang chỉnh sửa" : "Chỉnh sửa") : "";
                    cell.ReadOnly = payment.PaymentId != earliestUnconfirmedPayment?.PaymentId && payment.PaymentId != latestConfirmedPayment?.PaymentId;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi định dạng cột: {ex.Message}");
            }
        }

        private void dataPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                int paymentId = (int)dataPayment.Rows[e.RowIndex].Cells[0].Value;
                using var context = new KeToanDbContext();
                var payment = context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
                if (payment == null)
                {
                    ShowErrorMessage("Không tìm thấy thanh toán.");
                    return;
                }
                if (e.ColumnIndex == dataPayment.Columns["EditButton"].Index)
                    HandleEditPayment(payment);
                else if (e.ColumnIndex == dataPayment.Columns["ConfirmButton"].Index)
                    HandleConfirmPayment(payment, context);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi xử lý hành động: {ex.Message}");
            }
        }

        private void HandleEditPayment(Payment payment)
        {
            if (_isEditing)
            {
                ShowWarningMessage("Vui lòng lưu hoặc hủy chỉnh sửa hiện tại trước khi chỉnh sửa thanh toán khác.");
                return;
            }
            using var context = new KeToanDbContext();
            var earliestUnconfirmedPayment = context.Payments
                .Where(p => p.LoanId == _loanId && !p.IsConfirmed)
                .OrderBy(p => p.StartDate)
                .FirstOrDefault();
            var latestConfirmedPayment = context.Payments
                .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefault();
            if (payment.PaymentId != earliestUnconfirmedPayment?.PaymentId && payment.PaymentId != latestConfirmedPayment?.PaymentId)
            {
                ShowWarningMessage("Chỉ có thể chỉnh sửa thanh toán chưa xác nhận sớm nhất hoặc thanh toán đã xác nhận mới nhất.");
                return;
            }
            _isEditing = true;
            _editingPaymentId = payment.PaymentId;
            StartDate.Value = payment.StartDate;
            dateEndDate.Value = payment.EndDate;
            txtlaisuat.Text = payment.InterestRate.ToString("F2");
            txtcatgoc.Text = payment.EstimatedPrincipalPaid.ToString("N2");
            txtsongay.Text = payment.NumberOfDays.ToString();
            radio365.Checked = payment.DayCountConvention == 365;
            radio360.Checked = payment.DayCountConvention == 360;
            CalculatePayment(); // Tính lại các giá trị dựa trên dữ liệu hiện tại
            dataPayment.Invalidate();
        }

        private void HandleConfirmPayment(Payment payment, KeToanDbContext context)
        {
            if (payment.IsConfirmed)
            {
                ShowWarningMessage("Thanh toán này đã được xác nhận.");
                return;
            }
            if (payment.InterestRate == 0)
            {
                MessageBox.Show("Không thể xác nhận với lãi suất bằng 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var earliestUnconfirmedPayment = context.Payments
                .Where(p => p.LoanId == _loanId && !p.IsConfirmed)
                .OrderBy(p => p.StartDate)
                .FirstOrDefault();
            var latestConfirmedPayment = context.Payments
                .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefault();
            if (payment.PaymentId != earliestUnconfirmedPayment?.PaymentId && payment.PaymentId != latestConfirmedPayment?.PaymentId)
            {
                ShowWarningMessage("Chỉ có thể xác nhận thanh toán chưa xác nhận sớm nhất hoặc thanh toán đã xác nhận mới nhất.");
                return;
            }
            var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
            if (loan == null)
            {
                ShowErrorMessage("Không tìm thấy khoản vay.");
                return;
            }
            bool isLastPayment = !context.Payments.Any(p => p.LoanId == _loanId && !p.IsConfirmed && p.StartDate > payment.StartDate);
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (isLastPayment || payment.EstimatedPrincipalPaid >= loan.Balance)
                    {
                        if (MessageBox.Show($"Trả hết số dư gốc ({loan.Balance:N2}) và lãi ({payment.EstimatedInterestPaid:N2}) từ {payment.StartDate:dd/MM/yyyy}? Khoản vay sẽ hoàn thành.", "Xác nhận trả hết", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            return;
                        payment.PrincipalPaid = loan.Balance;
                        payment.InterestPaid = payment.EstimatedInterestPaid;
                        payment.IsConfirmed = true;
                        loan.Balance = 0;
                        loan.IsCompleted = true;
                        loan.EndDate = payment.EndDate;
                        var futurePayments = context.Payments
                            .Where(p => p.LoanId == _loanId && !p.IsConfirmed && p.StartDate > payment.StartDate)
                            .ToList();
                        context.Payments.RemoveRange(futurePayments);
                    }
                    else
                    {
                        if (MessageBox.Show($"Xác nhận thanh toán từ {payment.StartDate:dd/MM/yyyy} với gốc ({payment.EstimatedPrincipalPaid:N2}) và lãi ({payment.EstimatedInterestPaid:N2})?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            return;
                        payment.IsConfirmed = true;
                        payment.PrincipalPaid = payment.EstimatedPrincipalPaid;
                        payment.InterestPaid = payment.EstimatedInterestPaid;
                        payment.CumulativeInterestPaid = context.Payments
                            .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                            .Sum(p => p.InterestPaid);
                        UpdateLoanBalance(loan, payment.PrincipalPaid, context);
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    LoadPayments();
                    UpdatePaymentDateDisplay();
                    UpdateCumulativeInterestPaidDisplay();
                    UpdateEstimatedInterestDisplay();
                    dataPayment.Invalidate();
                    MessageBox.Show("Xác nhận thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowErrorMessage($"Lỗi xử lý giao dịch: {ex.Message}");
                }
            }
        }

        private void radio360_CheckedChanged(object sender, EventArgs e)
        {
            if (radio360.Checked) CalculatePayment();
        }

        private void radio365_CheckedChanged(object sender, EventArgs e)
        {
            if (radio365.Checked) CalculatePayment();
        }

        private void dateEndDate_ValueChanged(object sender, EventArgs e)
        {
            CalculatePayment();
        }

        private int DayCountConvention => radio365.Checked ? 365 : radio360.Checked ? 360 : 0;

        private void txtlaisuat_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtlaisuat.Text)) return;
            if (!decimal.TryParse(txtlaisuat.Text, out decimal interestRate) || interestRate < 0)
            {
                ShowWarningMessage("Lãi suất không hợp lệ. Vui lòng nhập số dương.");
                return;
            }
            CalculatePayment();
        }

        private void txtcatgoc_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcatgoc.Text)) return;
            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan == null)
                {
                    ShowErrorMessage("Không tìm thấy khoản vay.");
                    return;
                }
                if (!decimal.TryParse(txtcatgoc.Text, out decimal principalPaid) || principalPaid < 0)
                {
                    ShowWarningMessage("Số tiền cắt gốc không hợp lệ.");
                    txtcatgoc.Text = "";
                    return;
                }
                if (principalPaid > loan.Balance)
                {
                    ShowWarningMessage($"Số tiền cắt gốc không được vượt quá số dư: {loan.Balance:N2}.");
                    txtcatgoc.Text = loan.Balance.ToString();
                }
                CalculatePayment();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi kiểm tra số tiền cắt gốc: {ex.Message}");
            }
        }

        private void txtsongay_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtsongay.Text)) return;
            if (!int.TryParse(txtsongay.Text, out int numberOfDays) || numberOfDays <= 0)
            {
                ShowWarningMessage("Số ngày không hợp lệ.");
                txtsongay.Text = "";
                return;
            }
            CalculatePayment();
        }

        private void CalculatePayment()
        {
            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan == null) return;

                DateTime startDate = StartDate.Value;
                DateTime endDate = dateEndDate.Value;

                // Tính lại số ngày dựa trên StartDate và EndDate
                int numberOfDays = endDate >= startDate ? (endDate - startDate).Days : 0;
                txtsongay.Text = numberOfDays.ToString(); // Cập nhật giao diện

                // Tính PaymentDate dựa trên EndDate, chuyển sang thứ Hai nếu cần
                DateTime paymentDate = endDate;
                if (endDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    paymentDate = endDate.AddDays(2); // Chuyển sang thứ Hai
                }
                else if (endDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    paymentDate = endDate.AddDays(1); // Chuyển sang thứ Hai
                }

                if (!decimal.TryParse(txtlaisuat.Text, out decimal interestRate) || interestRate < 0) return;
                if (DayCountConvention == 0) return;

                decimal estimatedPrincipalPaid = string.IsNullOrWhiteSpace(txtcatgoc.Text) ? 0m : decimal.Parse(txtcatgoc.Text);
                decimal remainingBalance = loan.Balance;

                decimal estimatedInterestPaid = Math.Round(remainingBalance * (interestRate / 100) * numberOfDays / DayCountConvention, 4, MidpointRounding.AwayFromZero);

                _tempPayment = new Payment
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    PaymentDate = paymentDate,
                    NumberOfDays = numberOfDays,
                    InterestRate = interestRate,
                    EstimatedInterestPaid = estimatedInterestPaid,
                    EstimatedPrincipalPaid = estimatedPrincipalPaid,
                    InterestPaid = 0m,
                    PrincipalPaid = 0m,
                    DayCountConvention = DayCountConvention
                };

                UpdatePaymentDateDisplay();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi tính toán thanh toán: {ex.Message}");
            }
        }
        private void addpayment_Click(object sender, EventArgs e)
        {
            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan == null)
                {
                    ShowErrorMessage("Không tìm thấy khoản vay.");
                    return;
                }
                if (!ValidatePaymentInput(loan, context)) return;
                decimal estimatedPrincipalPaid = string.IsNullOrWhiteSpace(txtcatgoc.Text) ? 0m : decimal.Parse(txtcatgoc.Text);
                DateTime adjustedEndDate = dateEndDate.Value;
                if (_isEditing)
                {
                    UpdateExistingPayment(loan, estimatedPrincipalPaid, context, adjustedEndDate);
                    _isEditing = false;
                    _editingPaymentId = 0;
                    ResetInputFields();
                }
                else
                {
                    AddNewPayment(loan, estimatedPrincipalPaid, context, adjustedEndDate);
                }
                LoadPayments();
                dataPayment.Invalidate();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi xử lý thanh toán: {ex.Message}");
            }
        }

        private void btnthemmoi_Click(object sender, EventArgs e)
        {
            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan == null)
                {
                    ShowErrorMessage("Không tìm thấy khoản vay.");
                    return;
                }

                // Kiểm tra nếu đang ở chế độ chỉnh sửa
                if (_isEditing)
                {
                    ShowWarningMessage("Vui lòng hoàn tất hoặc hủy chỉnh sửa hiện tại trước khi thêm mới.");
                    return;
                }

                // Gọi CalculatePayment để cập nhật _tempPayment
                CalculatePayment();

                // Kiểm tra dữ liệu đầu vào
                if (!ValidatePaymentInput(loan, context))
                {
                    return;
                }

                // Lấy giá trị từ các ô nhập
                DateTime startDate = StartDate.Value;
                DateTime endDate = dateEndDate.Value;
                int numberOfDays = string.IsNullOrWhiteSpace(txtsongay.Text)
                    ? (endDate >= startDate ? (endDate - startDate).Days : 0)
                    : int.Parse(txtsongay.Text);

                // Kiểm tra và lấy DayCountConvention
                if (!radio360.Checked && !radio365.Checked)
                {
                    ShowWarningMessage("Vui lòng chọn quy ước ngày (360 hoặc 365).");
                    return;
                }
                int dayCountConvention = radio365.Checked ? 365 : 360;

                // Kiểm tra lãi suất
                if (!decimal.TryParse(txtlaisuat.Text, out decimal interestRate) || interestRate < 0)
                {
                    ShowWarningMessage("Vui lòng nhập lãi suất hợp lệ.");
                    return;
                }

                // Lấy số tiền cắt gốc (nếu có)
                decimal estimatedPrincipalPaid = string.IsNullOrWhiteSpace(txtcatgoc.Text)
                    ? 0m
                    : decimal.Parse(txtcatgoc.Text);

                // Kiểm tra số tiền cắt gốc
                if (estimatedPrincipalPaid > loan.Balance)
                {
                    ShowWarningMessage($"Số tiền cắt gốc không được vượt quá số dư: {loan.Balance:N2}.");
                    return;
                }

                // Tính toán EstimatedInterestPaid
                decimal estimatedInterestPaid = Math.Round(
                    loan.Balance * (interestRate / 100) * numberOfDays / dayCountConvention,
                    4,
                    MidpointRounding.AwayFromZero
                );

                // Điều chỉnh PaymentDate nếu EndDate rơi vào thứ Bảy hoặc Chủ Nhật
                DateTime paymentDate = endDate;
                if (endDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    paymentDate = endDate.AddDays(2); // Chuyển sang thứ Hai
                }
                else if (endDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    paymentDate = endDate.AddDays(1); // Chuyển sang thứ Hai
                }

                // Tạo đối tượng Payment mới
                _tempPayment = new Payment
                {
                    LoanId = _loanId,
                    StartDate = startDate,
                    EndDate = endDate,
                    PaymentDate = paymentDate,
                    NumberOfDays = numberOfDays,
                    InterestRate = interestRate,
                    EstimatedInterestPaid = estimatedInterestPaid,
                    EstimatedPrincipalPaid = estimatedPrincipalPaid,
                    InterestPaid = 0m,
                    PrincipalPaid = 0m,
                    DayCountConvention = dayCountConvention,
                    IsConfirmed = false,
                    IsEmailSent = false
                };

                // Thêm thanh toán mới vào cơ sở dữ liệu
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Payments.Add(_tempPayment);
                        UpdateLoanBalance(loan, 0m, context);
                        context.SaveChanges();
                        transaction.Commit();

                        // Cập nhật giao diện
                        LoadPayments();
                        UpdatePaymentDateDisplay();
                        UpdateEstimatedInterestDisplay();
                        dataPayment.Invalidate();

                        // Reset các ô nhập
                        ResetInputFields();

                        MessageBox.Show("Thêm thanh toán mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ShowErrorMessage($"Lỗi khi thêm thanh toán mới: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi xử lý sự kiện thêm mới: {ex.Message}");
            }
        }

        private void ResetInputFields()
        {
            StartDate.Value = DateTime.Today;
            dateEndDate.Value = StartDate.Value.AddMonths(1);
            txtlaisuat.Text = "";
            txtcatgoc.Text = "";
            txtsongay.Text = "";
            radio365.Checked = false;
            radio360.Checked = false;
        }

        private bool ValidatePaymentInput(Loan loan, KeToanDbContext context)
        {
            // Loại bỏ kiểm tra về thanh toán mới nhất chưa xác nhận theo yêu cầu
            /*
            if (!_isEditing)
            {
                var latestPayment = context.Payments
                    .Where(p => p.LoanId == _loanId)
                    .OrderByDescending(p => p.PaymentId)
                    .FirstOrDefault();
                if (latestPayment != null && !latestPayment.IsConfirmed)
                {
                    ShowWarningMessage("Thanh toán mới nhất chưa được xác nhận.");
                    return false;
                }
            }
            */

            if (_tempPayment == null)
            {
                ShowWarningMessage("Vui lòng nhập đầy đủ thông tin.");
                return false;
            }
            if (_tempPayment.NumberOfDays <= 0)
            {
                ShowWarningMessage("Số ngày không hợp lệ.");
                return false;
            }
            if (!radio365.Checked && !radio360.Checked)
            {
                ShowWarningMessage("Vui lòng chọn quy ước ngày (365 hoặc 360).");
                return false;
            }
            if (!decimal.TryParse(txtlaisuat.Text, out decimal interestRate) || interestRate < 0)
            {
                ShowWarningMessage("Vui lòng nhập lãi suất hợp lệ.");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtcatgoc.Text) && (!decimal.TryParse(txtcatgoc.Text, out decimal principalPaid) || principalPaid < 0))
            {
                ShowWarningMessage("Vui lòng nhập số tiền cắt gốc hợp lệ.");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtcatgoc.Text) && decimal.Parse(txtcatgoc.Text) > loan.Balance)
            {
                ShowWarningMessage($"Số tiền cắt gốc không được vượt quá số dư: {loan.Balance:N2}.");
                return false;
            }
            return true;
        }

        private void UpdatePaymentDateDisplay()
        {
            try
            {
                using var context = new KeToanDbContext();
                var earliestUnconfirmedPayment = context.Payments
                    .Where(p => p.LoanId == _loanId && !p.IsConfirmed)
                    .OrderBy(p => p.StartDate)
                    .FirstOrDefault();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan == null)
                {
                    Console.WriteLine("Loan not found.");
                    return;
                }
                string endDateText = earliestUnconfirmedPayment != null
                    ? earliestUnconfirmedPayment.PaymentDate?.ToString("dd/MM/yyyy") ?? earliestUnconfirmedPayment.EndDate.ToString("dd/MM/yyyy")
                    : loan.EndDate != DateTime.MinValue ? loan.EndDate.ToString("dd/MM/yyyy") : "Chưa xác định";
                lbngaytralai.Text = $"{GetLabelTitle(lbngaytralai.Text)} {endDateText}";
                if (earliestUnconfirmedPayment != null)
                {
                    Console.WriteLine($"PaymentDate: {earliestUnconfirmedPayment.PaymentDate}, IsConfirmed: {earliestUnconfirmedPayment.IsConfirmed}");
                }
                else
                {
                    Console.WriteLine("No unconfirmed payment found.");
                }
                lbngaytralai.ForeColor = earliestUnconfirmedPayment != null && !earliestUnconfirmedPayment.IsConfirmed &&
                    earliestUnconfirmedPayment.PaymentDate.HasValue &&
                    (earliestUnconfirmedPayment.PaymentDate.Value.Date < DateTime.Today ||
                     (earliestUnconfirmedPayment.PaymentDate.Value.Date <= DateTime.Today.AddDays(3) &&
                      earliestUnconfirmedPayment.PaymentDate.Value.Date >= DateTime.Today))
                    ? (_blinkState ? Color.Red : Color.White) : Color.Black;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi hiển thị ngày trả lãi: {ex.Message}");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        private void UpdateExistingPayment(Loan loan, decimal estimatedPrincipalPaid, KeToanDbContext context, DateTime adjustedEndDate)
        {
            var payment = context.Payments.FirstOrDefault(p => p.PaymentId == _editingPaymentId);
            if (payment == null || payment.IsConfirmed)
            {
                ShowErrorMessage("Không tìm thấy hoặc không thể chỉnh sửa thanh toán đã xác nhận.");
                return;
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Cập nhật khoản thanh toán hiện tại
                    payment.StartDate = _tempPayment.StartDate;
                    payment.EndDate = adjustedEndDate;
                    payment.NumberOfDays = adjustedEndDate >= _tempPayment.StartDate ? (adjustedEndDate - _tempPayment.StartDate).Days : 0;
                    payment.InterestRate = _tempPayment.InterestRate;
                    payment.EstimatedPrincipalPaid = estimatedPrincipalPaid;
                    payment.InterestPaid = 0m;
                    payment.PrincipalPaid = 0m;
                    payment.DayCountConvention = _tempPayment.DayCountConvention;

                    // Điều chỉnh PaymentDate dựa trên EndDate
                    DateTime paymentDate = adjustedEndDate;
                    if (adjustedEndDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        paymentDate = adjustedEndDate.AddDays(2); // Chuyển sang thứ Hai
                    }
                    else if (adjustedEndDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        paymentDate = adjustedEndDate.AddDays(1); // Chuyển sang thứ Hai
                    }
                    payment.PaymentDate = paymentDate;

                    // Tính toán số dư còn lại trước khi cập nhật các thanh toán tiếp theo
                    var priorPayments = context.Payments
                        .Where(p => p.LoanId == _loanId && p.StartDate < payment.StartDate)
                        .OrderBy(p => p.StartDate)
                        .ToList();
                    decimal totalPriorPrincipalPaid = priorPayments.Sum(p => p.EstimatedPrincipalPaid);
                    decimal currentBalance = loan.Amount - totalPriorPrincipalPaid;
                    payment.EstimatedInterestPaid = Math.Round(currentBalance * (payment.InterestRate / 100) * payment.NumberOfDays / payment.DayCountConvention, 2, MidpointRounding.AwayFromZero);

                    // Cập nhật các thanh toán tiếp theo
                    var subsequentPayments = context.Payments
                        .Where(p => p.LoanId == _loanId && !p.IsConfirmed && p.StartDate > payment.StartDate)
                        .OrderBy(p => p.StartDate)
                        .ToList();
                    DateTime previousEndDate = payment.EndDate;
                    currentBalance -= payment.EstimatedPrincipalPaid;

                    foreach (var subsequentPayment in subsequentPayments)
                    {
                        // Cập nhật StartDate và EndDate
                        subsequentPayment.StartDate = previousEndDate;
                        subsequentPayment.EndDate = previousEndDate.AddMonths(1); // Giữ logic thêm 1 tháng
                        subsequentPayment.NumberOfDays = subsequentPayment.EndDate >= subsequentPayment.StartDate ? (subsequentPayment.EndDate - subsequentPayment.StartDate).Days : 0;
                        subsequentPayment.InterestRate = _tempPayment.InterestRate;
                        subsequentPayment.EstimatedInterestPaid = Math.Round(currentBalance * (subsequentPayment.InterestRate / 100) * subsequentPayment.NumberOfDays / subsequentPayment.DayCountConvention, 2, MidpointRounding.AwayFromZero);
                        subsequentPayment.EstimatedPrincipalPaid = subsequentPayment.EstimatedPrincipalPaid; // Giữ nguyên số tiền cắt gốc dự tính
                        subsequentPayment.InterestPaid = 0m;
                        subsequentPayment.PrincipalPaid = 0m;

                        // Điều chỉnh PaymentDate cho subsequentPayment
                        paymentDate = subsequentPayment.EndDate;
                        if (subsequentPayment.EndDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            paymentDate = subsequentPayment.EndDate.AddDays(2); // Chuyển sang thứ Hai
                        }
                        else if (subsequentPayment.EndDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            paymentDate = subsequentPayment.EndDate.AddDays(1); // Chuyển sang thứ Hai
                        }
                        subsequentPayment.PaymentDate = paymentDate;

                        currentBalance -= subsequentPayment.EstimatedPrincipalPaid;
                        previousEndDate = subsequentPayment.EndDate;
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    MessageBox.Show("Chỉnh sửa thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateEstimatedInterestDisplay();
                    LoadPayments(); // Tải lại danh sách thanh toán để cập nhật giao diện
                    dataPayment.Invalidate();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowErrorMessage($"Lỗi chỉnh sửa thanh toán: {ex.Message}");
                }
            }
        }

        private void AddNewPayment(Loan loan, decimal estimatedPrincipalPaid, KeToanDbContext context, DateTime adjustedEndDate)
        {
            if (_tempPayment == null)
            {
                ShowErrorMessage("Dữ liệu thanh toán chưa được tính toán.");
                return;
            }
            // Điều chỉnh PaymentDate nếu EndDate rơi vào thứ Bảy hoặc Chủ Nhật
            DateTime paymentDate = adjustedEndDate;
            if (adjustedEndDate.DayOfWeek == DayOfWeek.Saturday)
            {
                paymentDate = adjustedEndDate.AddDays(2); // Chuyển sang thứ Hai
            }
            else if (adjustedEndDate.DayOfWeek == DayOfWeek.Sunday)
            {
                paymentDate = adjustedEndDate.AddDays(1); // Chuyển sang thứ Hai
            }

            var payment = new Payment
            {
                LoanId = _loanId,
                StartDate = _tempPayment.StartDate,
                EndDate = adjustedEndDate,
                PaymentDate = paymentDate,
                NumberOfDays = _tempPayment.NumberOfDays,
                InterestRate = _tempPayment.InterestRate,
                EstimatedInterestPaid = _tempPayment.EstimatedInterestPaid,
                EstimatedPrincipalPaid = estimatedPrincipalPaid,
                InterestPaid = 0m,
                PrincipalPaid = 0m,
                CumulativeInterestPaid = context.Payments
                    .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                    .Sum(p => p.InterestPaid),
                DayCountConvention = _tempPayment.DayCountConvention,
                IsConfirmed = false
            };
            if (payment.PaymentDate.HasValue && payment.PaymentDate.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                payment.PaymentDate = payment.PaymentDate.Value.AddDays(2);
            }
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Payments.Add(payment);
                    UpdateLoanBalance(loan, 0m, context);
                    context.SaveChanges();
                    transaction.Commit();
                    MessageBox.Show("Thêm thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateEstimatedInterestDisplay();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowErrorMessage($"Lỗi thêm thanh toán: {ex.Message}");
                }
            }
        }

        private void UpdateLoanBalance(Loan loan, decimal principalPaid, KeToanDbContext context)
        {
            if (loan.IsCompleted)
            {
                loan.Balance = 0;
                return;
            }
            decimal totalPrincipalPaid = context.Payments
                .Where(p => p.LoanId == _loanId && p.IsConfirmed)
                .Sum(p => p.PrincipalPaid);
            if (_isEditing)
            {
                var oldPayment = context.Payments.FirstOrDefault(p => p.PaymentId == _editingPaymentId);
                if (oldPayment != null)
                {
                    totalPrincipalPaid = totalPrincipalPaid - oldPayment.PrincipalPaid + principalPaid;
                }
            }
            else
            {
                totalPrincipalPaid += principalPaid;
            }
            loan.Balance = loan.Amount - totalPrincipalPaid;
            if (loan.Balance < 0)
            {
                loan.Balance = 0;
            }
            loan.IsCompleted = loan.Balance <= 0;
        }

        private void lbcatgoc_Click(object sender, EventArgs e)
        {
            try
            {
                using var context = new KeToanDbContext();
                var loan = context.Loans.FirstOrDefault(l => l.LoanId == _loanId);
                if (loan != null)
                {
                    lbcatgoc.Text = $"{GetLabelTitle(lbcatgoc.Text)} {loan.Balance:N2}";
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi hiển thị số dư khoản vay: {ex.Message}");
            }
        }

        private void lblai_Click(object sender, EventArgs e) => UpdateInterestPaidDisplay();

        private void lbngaytralai_Click(object sender, EventArgs e) => UpdatePaymentDateDisplay();

        private void lbCumulativeInterestPaid_Click(object sender, EventArgs e) => UpdateCumulativeInterestPaidDisplay();

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine($"Lỗi: {message}");
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            if (!_isEditing)
            {
                dateEndDate.Value = StartDate.Value.AddMonths(1);
            }
            CalculatePayment();
        }

        private void ShowWarningMessage(string message) => MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void lbdutinh_Click(object sender, EventArgs e) => UpdateEstimatedInterestDisplay();
    }
}