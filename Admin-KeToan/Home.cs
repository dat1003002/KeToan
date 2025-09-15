using System;
using System.Drawing;
using System.Windows.Forms;
using Admin_KeToan.Data;
using Admin_KeToan.Model;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Admin_KeToan
{
    public partial class Home : Form
    {
        private const int Gap = 10;
        private System.Windows.Forms.Timer _blinkTimer;
        private bool _blinkState;
        private Panel scrollPanelNganHan;
        private Panel scrollPanelDaiHan;
        private Lichsu dslichsu;

        public Home()
        {
            InitializeComponent();
            Load += Home_Load;
            Resize += Home_Resize;
            this.BackColor = Color.White;
            scrollPanelNganHan = new Panel { AutoScroll = true };
            scrollPanelDaiHan = new Panel { AutoScroll = true };
            scrollPanelNganHan.Controls.Add(panelnganhan);
            scrollPanelDaiHan.Controls.Add(paneldaihan);
            panelnganhang.Controls.Add(scrollPanelNganHan);
            panelnganhang.Controls.Add(scrollPanelDaiHan);
            cbchuky.DropDownStyle = ComboBoxStyle.DropDownList;
            cbnganhang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbchuky.MouseDown += cbchuky_MouseDown;
            cbnganhang.MouseDown += cbnganhang_MouseDown;
            inputAmount.MaxLength = 30;
            inputAmount.KeyPress += inputAmount_KeyPress;
            txtsotiencat.TextChanged += txtsotiencat_TextChanged;
            timecatgoc.ValueChanged += timecatgoc_ValueChanged;
            _blinkTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _blinkTimer.Tick += BlinkTimer_Tick;
            _blinkTimer.Start();
            this.FormClosed += Home_FormClosed;
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            _blinkState = !_blinkState;
            UpdateBankLabels();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadBanks(true);
            LoadBanks(false);
            LoadBanksToComboBox();
            LoadPaymentPeriods();
            UpdatePanelLayout();
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            UpdatePanelLayout();
            LoadBanks(true);
            LoadBanks(false);
        }

        private void UpdatePanelLayout()
        {
            int height = panelfolter.Location.Y - (panelheader.Location.Y + panelheader.Height) - 2 * Gap;
            int width = ClientSize.Width - Gap;
            int nganHangWidth = (int)(width * 0.7);
            panelnganhang.Location = new Point(0, panelheader.Location.Y + panelheader.Height + Gap);
            panelnganhang.Size = new Size(nganHangWidth, height);
            int subPanelHeight = (int)(height * 0.48);
            int gapBetween = (int)(height * 0.04);
            scrollPanelNganHan.Location = new Point(0, 0);
            scrollPanelNganHan.Size = new Size(nganHangWidth, subPanelHeight);
            scrollPanelDaiHan.Location = new Point(0, subPanelHeight + gapBetween);
            scrollPanelDaiHan.Size = new Size(nganHangWidth, subPanelHeight);
            panelnganhan.Location = new Point(5, 0);
            panelnganhan.Size = new Size(nganHangWidth - 5, Math.Max(subPanelHeight, GetBankListHeight(true)));
            paneldaihan.Location = new Point(5, 0);
            paneldaihan.Size = new Size(nganHangWidth - 5, Math.Max(subPanelHeight, GetBankListHeight(false)));
            panelchucnang.Location = new Point(nganHangWidth + Gap, panelheader.Location.Y + panelheader.Height + Gap);
            panelchucnang.Size = new Size(width - nganHangWidth, height);
            groupkhoanvay.Location = new Point(10, 10);
            groupkhoanvay.Size = new Size(panelchucnang.ClientSize.Width - 20, groupkhoanvay.Height);
            int totalAvailableHeight = panelchucnang.ClientSize.Height - groupkhoanvay.Height - 25;
            int gblichsuHeight = (int)(totalAvailableHeight * 0.2);
            int groupVayHeight = totalAvailableHeight - gblichsuHeight;
            groupvay.Location = new Point(10, groupkhoanvay.Location.Y + groupkhoanvay.Height + 5);
            groupvay.Size = new Size(panelchucnang.ClientSize.Width - 20, groupVayHeight);
            gblichsu.Location = new Point(10, groupvay.Location.Y + groupvay.Height + 5);
            gblichsu.Size = new Size(panelchucnang.ClientSize.Width - 20, gblichsuHeight);
            int leftMargin = 20;
            int rightMargin = 20;
            int buttonCount = 3;
            int totalButtonWidth = btnlichsu.Width + btEmail.Width + btnexport.Width;
            int availableWidth = gblichsu.ClientSize.Width - totalButtonWidth - leftMargin - rightMargin;
            int buttonGap = availableWidth >= 0 ? availableWidth / (buttonCount - 1) : 0;
            int maxButtonHeight = Math.Max(btnlichsu.Height, Math.Max(btEmail.Height, btnexport.Height));
            int buttonY = (gblichsu.ClientSize.Height - maxButtonHeight) / 2;
            btnlichsu.Location = new Point(leftMargin, buttonY);
            btEmail.Location = new Point(leftMargin + btnlichsu.Width + buttonGap, buttonY);
            btnexport.Location = new Point(leftMargin + btnlichsu.Width + buttonGap + btEmail.Width + buttonGap, buttonY);
            if (btnexport.Location.X + btnexport.Width + rightMargin > gblichsu.ClientSize.Width)
            {
                buttonGap = (gblichsu.ClientSize.Width - totalButtonWidth - leftMargin - rightMargin) / (buttonCount - 1);
                if (buttonGap < 0) buttonGap = 0;
                btnlichsu.Location = new Point(leftMargin, buttonY);
                btEmail.Location = new Point(leftMargin + btnlichsu.Width + buttonGap, buttonY);
                btnexport.Location = new Point(leftMargin + btnlichsu.Width + buttonGap + btEmail.Width + buttonGap, buttonY);
            }
            int controlMargin = 10;
            int controlGap = 3;
            int usableWidth = groupkhoanvay.ClientSize.Width - 2 * controlMargin - controlGap;
            int inputWidth = (int)(usableWidth * 0.8);
            int buttonWidth = usableWidth - inputWidth;
            inputaddbank.Location = new Point(controlMargin, inputaddbank.Location.Y);
            inputaddbank.Size = new Size(inputWidth, inputaddbank.Height);
            btnadd.Location = new Point(controlMargin + inputWidth + controlGap, inputaddbank.Location.Y);
            btnadd.Size = new Size(buttonWidth, inputaddbank.Height);
            int groupVayUsableWidth = groupvay.ClientSize.Width - 3 * controlMargin;
            int labelWidth = (int)(groupVayUsableWidth * 0.3);
            int inputControlWidth = groupVayUsableWidth - labelWidth - controlGap;
            int startY = 40;
            int bottomMargin = 30;
            int controlHeight = txtkhoanvay.Height;
            int totalControls = 9;
            int availableHeight = groupVayHeight - startY - bottomMargin;
            int verticalGap = (availableHeight - totalControls * controlHeight) / (totalControls - 1);
            txtkhoanvay.Location = new Point(controlMargin, startY);
            txtkhoanvay.Size = new Size(labelWidth, txtkhoanvay.Height);
            inputLoanName.Location = new Point(controlMargin + labelWidth + controlGap, startY);
            inputLoanName.Size = new Size(inputControlWidth, inputLoanName.Height);
            txtsotien.Location = new Point(controlMargin, startY + controlHeight + verticalGap);
            txtsotien.Size = new Size(labelWidth, txtsotien.Height);
            inputAmount.Location = new Point(controlMargin + labelWidth + controlGap, startY + controlHeight + verticalGap);
            inputAmount.Size = new Size(inputControlWidth, inputAmount.Height);
            txtkyhan.Location = new Point(controlMargin, txtsotien.Location.Y + controlHeight + verticalGap);
            txtkyhan.Size = new Size(labelWidth, txtkyhan.Height);
            inputDuration.Location = new Point(controlMargin + labelWidth + controlGap, txtsotien.Location.Y + controlHeight + verticalGap);
            inputDuration.Size = new Size(inputControlWidth, inputDuration.Height);
            lbthoigian.Location = new Point(controlMargin, txtkyhan.Location.Y + controlHeight + verticalGap);
            lbthoigian.Size = new Size(labelWidth, lbthoigian.Height);
            dateStartDate.Location = new Point(controlMargin + labelWidth + controlGap, txtkyhan.Location.Y + controlHeight + verticalGap);
            dateStartDate.Size = new Size(inputControlWidth, dateStartDate.Height);
            lbchuky.Location = new Point(controlMargin, lbthoigian.Location.Y + controlHeight + verticalGap);
            lbchuky.Size = new Size(labelWidth, lbchuky.Height);
            cbchuky.Location = new Point(controlMargin + labelWidth + controlGap, lbthoigian.Location.Y + controlHeight + verticalGap);
            cbchuky.Size = new Size(inputControlWidth, cbchuky.Height);
            lbnganhang.Location = new Point(controlMargin, lbchuky.Location.Y + controlHeight + verticalGap);
            lbnganhang.Size = new Size(labelWidth, lbnganhang.Height);
            cbnganhang.Location = new Point(controlMargin + labelWidth + controlGap, lbchuky.Location.Y + controlHeight + verticalGap);
            cbnganhang.Size = new Size(inputControlWidth, cbnganhang.Height);
            lbthoigiancat.Location = new Point(controlMargin, lbnganhang.Location.Y + controlHeight + verticalGap);
            lbthoigiancat.Size = new Size(labelWidth, lbthoigiancat.Height);
            timecatgoc.Location = new Point(controlMargin + labelWidth + controlGap, lbnganhang.Location.Y + controlHeight + verticalGap);
            timecatgoc.Size = new Size(inputControlWidth, timecatgoc.Height);
            lbsotiencat.Location = new Point(controlMargin, lbthoigiancat.Location.Y + controlHeight + verticalGap);
            lbsotiencat.Size = new Size(labelWidth, lbsotiencat.Height);
            txtsotiencat.Location = new Point(controlMargin + labelWidth + controlGap, lbthoigiancat.Location.Y + controlHeight + verticalGap);
            txtsotiencat.Size = new Size(inputControlWidth, txtsotiencat.Height);
            int currencyGroupWidth = (int)(groupVayUsableWidth * 0.7);
            int addLoanWidth = groupVayUsableWidth - currencyGroupWidth - controlGap;
            int currencyY = lbsotiencat.Location.Y + controlHeight + verticalGap;
            lbtiente.Location = new Point(controlMargin, currencyY);
            lbtiente.Size = new Size(labelWidth, lbtiente.Height);
            int radioWidth = (currencyGroupWidth - labelWidth - controlGap * 2) / 2;
            radioVND.Location = new Point(controlMargin + labelWidth + controlGap, currencyY);
            radioVND.Size = new Size(radioWidth, radioVND.Height);
            radioUSD.Location = new Point(controlMargin + labelWidth + controlGap + radioWidth + controlGap, currencyY);
            radioUSD.Size = new Size(radioWidth, radioUSD.Height);
            addLoan.Location = new Point(controlMargin + currencyGroupWidth + controlGap, currencyY);
            addLoan.Size = new Size(addLoanWidth, addLoan.Height);
        }

        private int GetBankListHeight(bool isShortTerm)
        {
            using (var context = new KeToanDbContext())
            {
                var banks = context.Banks
                    .Where(b => context.Loans.Any(l => l.BankId == b.BankId && l.PaymentPeriod == (isShortTerm ? "Tháng" : "Quý")))
                    .ToList();
                int gap = Math.Max(10, panelnganhang.ClientSize.Width / 50);
                int minBoxWidth = Math.Max(100, panelnganhang.ClientSize.Width / 10);
                int boxHeight = Math.Max(100, panelnganhang.ClientSize.Width / 10);
                int columns = Math.Min(7, Math.Max(1, (panelnganhang.ClientSize.Width - gap) / (minBoxWidth + gap)));
                int rows = (int)Math.Ceiling((double)banks.Count / columns);
                return rows * (boxHeight + gap) + gap + 20;
            }
        }

        private void LoadBanks(bool isShortTerm)
        {
            var targetGroupBox = isShortTerm ? panelnganhan : paneldaihan;
            targetGroupBox.Controls.Clear();
            using (var context = new KeToanDbContext())
            {
                var banks = context.Banks
                    .Where(b => context.Loans.Any(l => l.BankId == b.BankId && l.PaymentPeriod == (isShortTerm ? "Tháng" : "Quý") && !l.IsCompleted))
                    .Select(b => new
                    {
                        Bank = b,
                        HasDuePayment = context.Loans
                            .Where(l => l.BankId == b.BankId && l.PaymentPeriod == (isShortTerm ? "Tháng" : "Quý") && !l.IsCompleted)
                            .Join(context.Payments,
                                l => l.LoanId,
                                p => p.LoanId,
                                (l, p) => p)
                            .Any(p => !p.IsConfirmed && p.EndDate.Date <= DateTime.Today.AddDays(3))
                    })
                    .OrderByDescending(b => b.HasDuePayment)
                    .ThenBy(b => b.Bank.BankName)
                    .Select(b => b.Bank)
                    .ToList();
                int gap = Math.Max(10, targetGroupBox.ClientSize.Width / 50);
                int minBoxWidth = Math.Max(100, targetGroupBox.ClientSize.Width / 10);
                int boxHeight = Math.Max(100, targetGroupBox.ClientSize.Width / 10);
                int borderRadius = 5;
                int columns = Math.Min(7, Math.Max(1, (targetGroupBox.ClientSize.Width - gap) / (minBoxWidth + gap)));
                int boxWidth = (targetGroupBox.ClientSize.Width - (columns + 1) * gap) / columns;
                if (boxWidth < minBoxWidth) boxWidth = minBoxWidth;
                int x = gap, y = 40;
                for (int i = 0; i < banks.Count; i++)
                {
                    var bank = banks[i];
                    bool hasDuePayment = context.Loans
                        .Where(l => l.BankId == bank.BankId && l.PaymentPeriod == (isShortTerm ? "Tháng" : "Quý") && !l.IsCompleted)
                        .Join(context.Payments,
                            l => l.LoanId,
                            p => p.LoanId,
                            (l, p) => p)
                        .Any(p => !p.IsConfirmed && p.EndDate.Date <= DateTime.Today.AddDays(3));
                    var bankPanel = new Panel
                    {
                        Size = new Size(boxWidth, boxHeight),
                        Location = new Point(x, y),
                        BorderStyle = BorderStyle.None,
                        BackColor = Color.FromArgb(39, 185, 154)
                    };
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddArc(0, 0, borderRadius * 2, borderRadius * 2, 180, 90);
                        path.AddArc(boxWidth - borderRadius * 2, 0, borderRadius * 2, borderRadius * 2, 270, 90);
                        path.AddArc(boxWidth - borderRadius * 2, boxHeight - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
                        path.AddArc(0, boxHeight - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
                        path.CloseFigure();
                        bankPanel.Region = new Region(path);
                    }
                    bankPanel.Paint += (s, e) =>
                    {
                        using (var pen = new Pen(Color.FromArgb(114, 135, 167), 1))
                        {
                            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                            {
                                path.AddArc(0, 0, borderRadius * 2, borderRadius * 2, 180, 90);
                                path.AddArc(boxWidth - borderRadius * 2, 0, borderRadius * 2, borderRadius * 2, 270, 90);
                                path.AddArc(boxWidth - borderRadius * 2, boxHeight - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
                                path.AddArc(0, boxHeight - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
                                path.CloseFigure();
                                e.Graphics.DrawPath(pen, path);
                            }
                        }
                    };
                    var bankLabel = new Label
                    {
                        Text = bank.BankName,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", boxWidth / 15, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = hasDuePayment ? (_blinkState ? Color.FromArgb(39, 185, 154) : Color.Red) : Color.Transparent,
                        Tag = hasDuePayment
                    };
                    bankPanel.Controls.Add(bankLabel);
                    EventHandler clickHandler = (s, args) =>
                    {
                        var khoanVayForm = new KhoanVay(bank.BankId, isShortTerm ? "Tháng" : "Quý");
                        khoanVayForm.FormClosed += (s2, e2) => { LoadBanks(true); LoadBanks(false); };
                        khoanVayForm.ShowDialog();
                    };
                    bankPanel.Click += clickHandler;
                    bankLabel.Click += clickHandler;
                    targetGroupBox.Controls.Add(bankPanel);
                    x += boxWidth + gap;
                    if ((i + 1) % columns == 0)
                    {
                        x = gap;
                        y += boxHeight + gap;
                    }
                }
            }
        }

        private void UpdateBankLabels()
        {
            foreach (Control targetGroupBox in new[] { panelnganhan, paneldaihan })
            {
                foreach (Control control in targetGroupBox.Controls)
                {
                    if (control is Panel bankPanel)
                    {
                        foreach (Control child in bankPanel.Controls)
                        {
                            if (child is Label bankLabel && bankLabel.Tag is bool hasDuePayment && hasDuePayment)
                            {
                                bankLabel.BackColor = _blinkState ? Color.FromArgb(39, 185, 154) : Color.Red;
                            }
                        }
                    }
                }
            }
        }

        private void LoadBanksToComboBox()
        {
            using (var context = new KeToanDbContext())
            {
                cbnganhang.DataSource = context.Banks.ToList();
                cbnganhang.DisplayMember = "BankName";
                cbnganhang.ValueMember = "BankId";
                cbnganhang.SelectedIndex = -1;
            }
        }

        private void LoadPaymentPeriods()
        {
            cbchuky.Items.Clear();
            cbchuky.Items.AddRange(new string[] { "Tháng", "Quý" });
            cbchuky.SelectedIndex = -1;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new KeToanDbContext())
                {
                    var bank = new Bank { BankName = inputaddbank.Text.Trim() };
                    if (string.IsNullOrWhiteSpace(bank.BankName))
                    {
                        MessageBox.Show("Tên ngân hàng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    context.Banks.Add(bank);
                    context.SaveChanges();
                    MessageBox.Show("Thêm ngân hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inputaddbank.Clear();
                    LoadBanks(true);
                    LoadBanks(false);
                    LoadBanksToComboBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm ngân hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addLoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputLoanName.Text) ||
                    string.IsNullOrWhiteSpace(inputAmount.Text) ||
                    string.IsNullOrWhiteSpace(inputDuration.Text) ||
                    cbnganhang.SelectedValue == null ||
                    string.IsNullOrWhiteSpace(cbchuky.Text) ||
                    (!radioUSD.Checked && !radioVND.Checked))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!decimal.TryParse(inputAmount.Text.Replace(",", ""), out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (amount > decimal.MaxValue)
                {
                    MessageBox.Show($"Số tiền vượt quá giới hạn ({decimal.MaxValue:N2})!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(inputDuration.Text.Trim(), out int duration) || duration <= 0)
                {
                    MessageBox.Show("Kỳ hạn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decimal principalPaymentAmount = 0;
                if (!string.IsNullOrWhiteSpace(txtsotiencat.Text) && (!decimal.TryParse(txtsotiencat.Text.Replace(",", ""), out principalPaymentAmount) || principalPaymentAmount < 0))
                {
                    MessageBox.Show("Số tiền cắt gốc không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cbchuky.Text == "Quý")
                {
                    if (duration < 3)
                    {
                        MessageBox.Show("Với chu kỳ Quý, kỳ hạn tối thiểu là 3 tháng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (duration % 3 != 0)
                    {
                        MessageBox.Show("Với chu kỳ Quý, kỳ hạn phải là bội số của 3 tháng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (cbchuky.Text == "Tháng" && duration < 1)
                {
                    MessageBox.Show("Với chu kỳ Tháng, kỳ hạn tối thiểu là 1 tháng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (var context = new KeToanDbContext())
                {
                    int bankId = (int)cbnganhang.SelectedValue;
                    string loanName = inputLoanName.Text.Trim();
                    if (context.Loans.Any(l => l.BankId == bankId && l.LoanName == loanName))
                    {
                        MessageBox.Show("Tên khoản vay đã tồn tại trong ngân hàng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string paymentPeriod = cbchuky.Text.Trim().Normalize(System.Text.NormalizationForm.FormC);
                    if (paymentPeriod != "Tháng" && paymentPeriod != "Quý")
                    {
                        MessageBox.Show("Chu kỳ thanh toán không hợp lệ. Vui lòng chọn 'Tháng' hoặc 'Quý'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string currency = radioUSD.Checked ? "USD" : "VND";
                    DateTime startDate = dateStartDate.Value;
                    int totalPeriods = paymentPeriod == "Quý" ? duration / 3 : duration;
                    int startPrincipalPaymentMonth = string.IsNullOrWhiteSpace(txtsotiencat.Text) ? 0 : (int)((timecatgoc.Value - startDate).TotalDays / 30.0);
                    var loan = new Loan
                    {
                        BankId = bankId,
                        LoanName = loanName,
                        Amount = amount,
                        Duration = totalPeriods,
                        StartDate = startDate,
                        PaymentPeriod = paymentPeriod,
                        Currency = currency,
                        IsCompleted = false,
                        Balance = amount,
                        StartPrincipalPaymentMonth = startPrincipalPaymentMonth,
                        PrincipalPaymentAmount = principalPaymentAmount,
                        Payments = new List<Payment>()
                    };
                    GeneratePayments(loan);
                    context.Loans.Add(loan);
                    context.SaveChanges();
                    MessageBox.Show($"Thêm khoản vay thành công! Ngày kết thúc: {loan.EndDate:dd/MM/yyyy}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inputLoanName.Clear();
                    inputAmount.Clear();
                    inputDuration.Clear();
                    dateStartDate.Value = DateTime.Now;
                    cbchuky.SelectedIndex = -1;
                    cbnganhang.SelectedIndex = -1;
                    radioUSD.Checked = true;
                    radioVND.Checked = false;
                    timecatgoc.Value = DateTime.Now;
                    txtsotiencat.Clear();
                    LoadBanks(true);
                    LoadBanks(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khoản vay: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GeneratePayments(Loan loan)
        {
            loan.Payments.Clear();
            DateTime currentStartDate = loan.StartDate;
            int periodInMonths = loan.PaymentPeriod == "Tháng" ? 1 : 3;
            int totalPeriods = loan.Duration;
            decimal remainingBalance = loan.Amount;
            decimal principalPaymentPerPeriod = loan.PrincipalPaymentAmount;
            int startPrincipalPaymentPeriod = loan.PaymentPeriod == "Quý" ? loan.StartPrincipalPaymentMonth / 3 : loan.StartPrincipalPaymentMonth;
            decimal interestRate = 0.07m;
            loan.EndDate = loan.StartDate.AddMonths(loan.Duration * periodInMonths);
            for (int i = 0; i < totalPeriods; i++)
            {
                DateTime endDate = currentStartDate.AddMonths(periodInMonths);
                DateTime paymentDate = endDate;
                if (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    paymentDate = AdjustEndDate(endDate);
                }
                decimal estimatedPrincipalPaid = 0;
                if (i >= startPrincipalPaymentPeriod && remainingBalance > 0)
                {
                    estimatedPrincipalPaid = Math.Min(principalPaymentPerPeriod, remainingBalance);
                }
                int days = (endDate - currentStartDate).Days;
                decimal estimatedInterestPaid = Math.Round(remainingBalance * interestRate * days / 365, 2, MidpointRounding.AwayFromZero);
                var payment = new Payment
                {
                    LoanId = loan.LoanId,
                    StartDate = currentStartDate,
                    EndDate = endDate,
                    PaymentDate = paymentDate,
                    NumberOfDays = days,
                    InterestRate = interestRate * 100,
                    InterestPaid = 0m,
                    PrincipalPaid = 0m,
                    CumulativeInterestPaid = 0m,
                    EstimatedInterestPaid = estimatedInterestPaid,
                    EstimatedPrincipalPaid = estimatedPrincipalPaid,
                    DayCountConvention = 365,
                    IsConfirmed = false,
                    IsEmailSent = false
                };
                loan.Payments.Add(payment);
                remainingBalance -= estimatedPrincipalPaid;
                currentStartDate = endDate;
            }
            if (loan.Payments.Count > 0)
            {
                loan.Balance = remainingBalance;
            }
        }

        private DateTime AdjustEndDate(DateTime endDate)
        {
            if (endDate.DayOfWeek == DayOfWeek.Saturday)
                return endDate.AddDays(2);
            if (endDate.DayOfWeek == DayOfWeek.Sunday)
                return endDate.AddDays(1);
            return endDate;
        }

        private void cbchuky_MouseDown(object sender, MouseEventArgs e)
        {
            cbchuky.DroppedDown = true;
        }

        private void cbnganhang_MouseDown(object sender, MouseEventArgs e)
        {
            cbnganhang.DroppedDown = true;
        }

        private void inputAmount_TextChanged(object sender, EventArgs e)
        {
            string text = inputAmount.Text.Replace(",", "");
            if (string.IsNullOrEmpty(text))
            {
                inputAmount.Text = "";
                return;
            }
            if (text == "." || (!decimal.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal number) && text != ".") || number < 0)
            {
                inputAmount.Text = inputAmount.Text.Remove(inputAmount.Text.Length - 1);
                inputAmount.SelectionStart = inputAmount.Text.Length;
                return;
            }
            inputAmount.TextChanged -= inputAmount_TextChanged;
            int originalSelectionStart = inputAmount.SelectionStart;
            string originalText = inputAmount.Text;
            string formattedText;
            int decimalPointIndexInOriginal = text.IndexOf('.');
            string integerPart = decimalPointIndexInOriginal >= 0 ? text.Substring(0, decimalPointIndexInOriginal) : text;
            string decimalPart = decimalPointIndexInOriginal >= 0 && decimalPointIndexInOriginal < text.Length - 1 ? text.Substring(decimalPointIndexInOriginal + 1) : "";
            if (string.IsNullOrEmpty(integerPart) && !string.IsNullOrEmpty(decimalPart))
            {
                formattedText = "0." + (decimalPart.Length > 2 ? decimalPart.Substring(0, 2) : decimalPart.PadRight(2, '0'));
            }
            else if (decimal.TryParse(integerPart, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal integerNumber))
            {
                formattedText = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", integerNumber);
                formattedText += "." + (decimalPart.Length > 0 ? decimalPart.PadRight(2, '0').Substring(0, Math.Min(decimalPart.Length, 2)) : "00");
            }
            else
            {
                formattedText = text;
            }
            inputAmount.Text = formattedText;
            int decimalPointIndex = formattedText.IndexOf('.');
            if (decimalPointIndex == -1) decimalPointIndex = formattedText.Length;
            int commasBeforeOriginal = originalText.Take(Math.Min(originalSelectionStart, originalText.Length)).Count(c => c == ',');
            int commasBeforeFormatted = formattedText.Take(Math.Min(originalSelectionStart, formattedText.Length)).Count(c => c == ',');
            int newSelectionStart = originalSelectionStart + (commasBeforeFormatted - commasBeforeOriginal);
            inputAmount.SelectionStart = Math.Max(0, Math.Min(newSelectionStart, formattedText.Length));
            inputAmount.TextChanged += inputAmount_TextChanged;
        }

        private void inputAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                e.Handled = true;
            }
            else if ((e.KeyChar == '.' || e.KeyChar == ',') && inputAmount.Text.Contains("."))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }
        }

        private void txtsotiencat_TextChanged(object sender, EventArgs e)
        {
            string text = txtsotiencat.Text.Replace(",", "");
            if (string.IsNullOrEmpty(text))
            {
                txtsotiencat.Text = "";
                return;
            }
            if (text == "." || (!decimal.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal number) && text != ".") || number < 0)
            {
                txtsotiencat.Text = txtsotiencat.Text.Remove(txtsotiencat.Text.Length - 1);
                txtsotiencat.SelectionStart = txtsotiencat.Text.Length;
                return;
            }
            txtsotiencat.TextChanged -= txtsotiencat_TextChanged;
            int originalSelectionStart = txtsotiencat.SelectionStart;
            string originalText = txtsotiencat.Text;
            string formattedText;
            int decimalPointIndexInOriginal = text.IndexOf('.');
            string integerPart = decimalPointIndexInOriginal >= 0 ? text.Substring(0, decimalPointIndexInOriginal) : text;
            string decimalPart = decimalPointIndexInOriginal >= 0 && decimalPointIndexInOriginal < text.Length - 1 ? text.Substring(decimalPointIndexInOriginal + 1) : "";
            if (string.IsNullOrEmpty(integerPart) && !string.IsNullOrEmpty(decimalPart))
            {
                formattedText = "0." + (decimalPart.Length > 2 ? decimalPart.Substring(0, 2) : decimalPart.PadRight(2, '0'));
            }
            else if (decimal.TryParse(integerPart, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal integerNumber))
            {
                formattedText = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", integerNumber);
                formattedText += "." + (decimalPart.Length > 0 ? decimalPart.PadRight(2, '0').Substring(0, Math.Min(decimalPart.Length, 2)) : "00");
            }
            else
            {
                formattedText = text;
            }
            txtsotiencat.Text = formattedText;
            int decimalPointIndex = formattedText.IndexOf('.');
            if (decimalPointIndex == -1) decimalPointIndex = formattedText.Length;
            int commasBeforeOriginal = originalText.Take(Math.Min(originalSelectionStart, originalText.Length)).Count(c => c == ',');
            int commasBeforeFormatted = formattedText.Take(Math.Min(originalSelectionStart, formattedText.Length)).Count(c => c == ',');
            int newSelectionStart = originalSelectionStart + (commasBeforeFormatted - commasBeforeOriginal);
            txtsotiencat.SelectionStart = Math.Max(0, Math.Min(newSelectionStart, formattedText.Length));
            txtsotiencat.TextChanged += txtsotiencat_TextChanged;
        }

        private void timecatgoc_ValueChanged(object sender, EventArgs e)
        {
            if (timecatgoc.Value < dateStartDate.Value)
            {
                MessageBox.Show("Thời gian bắt đầu cắt gốc phải sau ngày bắt đầu khoản vay!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timecatgoc.Value = dateStartDate.Value;
            }
        }

        private void panelnganhan_Enter(object sender, EventArgs e)
        {
            LoadBanks(true);
        }

        private void paneldaihan_Enter(object sender, EventArgs e)
        {
            LoadBanks(false);
        }

        private void btEmail_Click(object sender, EventArgs e)
        {
            DsEmail emailForm = new DsEmail();
            emailForm.Show();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("Your Name");
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    Title = "Chọn nơi lưu file Excel",
                    FileName = $"ToanBoKhoanVay_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                })
                {
                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                        return;
                    string directory = Path.GetDirectoryName(saveFileDialog.FileName);
                    if (!Directory.Exists(directory) || !new DirectoryInfo(directory).GetAccessControl().AreAccessRulesCanonical)
                    {
                        MessageBox.Show("Không có quyền truy cập thư mục đích. Vui lòng chọn thư mục khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    using (var package = new ExcelPackage(new FileInfo(saveFileDialog.FileName)))
                    {
                        using (var context = new KeToanDbContext())
                        {
                            var allLoans = context.Loans
                                .Include(l => l.Bank)
                                .Include(l => l.Payments)
                                .Where(l => !l.IsCompleted)
                                .Select(l => new
                                {
                                    l.LoanId,
                                    l.LoanName,
                                    BankName = l.Bank.BankName,
                                    l.Amount,
                                    l.Currency,
                                    l.StartDate,
                                    l.EndDate,
                                    Status = l.IsCompleted ? "Đã Hoàn thành" : "Chưa hoàn thành",
                                    Payments = l.Payments.OrderBy(p => p.StartDate).ToList()
                                })
                                .ToList();
                            var groupedLoans = allLoans.GroupBy(l => l.BankName).ToList();
                            int worksheetIndex = 1;
                            foreach (var group in groupedLoans)
                            {
                                string bankName = group.Key ?? "Unknown";
                                string worksheetName = $"Bank_{worksheetIndex++}_{bankName.Replace(" ", "_").Replace("/", "_").Replace("\\", "_")}";
                                if (worksheetName.Length > 31) worksheetName = worksheetName.Substring(0, 31);
                                var worksheet = package.Workbook.Worksheets.Add(worksheetName);
                                int currentRow = 1;
                                worksheet.Cells[currentRow, 1].Value = $"Thông tin khoản vay - Ngân hàng {bankName}";
                                worksheet.Cells[currentRow, 1, currentRow, 7].Merge = true;
                                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                                currentRow++;
                                string[] loanHeaders = new[] { "Mã khoản vay", "Tên khoản vay", "Số tiền vay", "Tiền tệ", "Ngày bắt đầu", "Ngày kết thúc", "Trạng thái" };
                                for (int i = 0; i < loanHeaders.Length; i++)
                                {
                                    worksheet.Cells[currentRow, i + 1].Value = loanHeaders[i];
                                    worksheet.Cells[currentRow, i + 1].Style.Font.Bold = true;
                                }
                                currentRow++;
                                foreach (var loan in group)
                                {
                                    worksheet.Cells[currentRow, 1].Value = loan.LoanId;
                                    worksheet.Cells[currentRow, 2].Value = loan.LoanName;
                                    worksheet.Cells[currentRow, 3].Value = loan.Amount;
                                    worksheet.Cells[currentRow, 3].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[currentRow, 4].Value = loan.Currency;
                                    worksheet.Cells[currentRow, 5].Value = loan.StartDate;
                                    worksheet.Cells[currentRow, 5].Style.Numberformat.Format = "dd/mm/yyyy";
                                    worksheet.Cells[currentRow, 6].Value = loan.EndDate;
                                    worksheet.Cells[currentRow, 6].Style.Numberformat.Format = "dd/mm/yyyy";
                                    worksheet.Cells[currentRow, 7].Value = loan.Status;
                                    currentRow++;
                                    if (loan.Payments.Any())
                                    {
                                        worksheet.Cells[currentRow, 1].Value = $"Thông tin thanh toán - {loan.LoanName}";
                                        worksheet.Cells[currentRow, 1, currentRow, 11].Merge = true;
                                        worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                                        currentRow++;
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
                                        foreach (var payment in loan.Payments)
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
                                }
                                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                            }
                            File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
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

        private void btnlichsu_Click(object sender, EventArgs e)
        {
            Lichsu lichsuform = new Lichsu();
            lichsuform.Show();
        }
    }
}