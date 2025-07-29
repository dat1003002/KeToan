namespace Admin_KeToan
{
    partial class Home
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            panelheader = new Panel();
            label2 = new Label();
            label1 = new Label();
            panelnganhang = new Panel();
            paneldaihan = new GroupBox();
            panelnganhan = new GroupBox();
            panelchucnang = new Panel();
            gblichsu = new GroupBox();
            btnexport = new Button();
            btEmail = new Button();
            btnlichsu = new Button();
            groupvay = new GroupBox();
            txtsotiencat = new TextBox();
            timecatgoc = new DateTimePicker();
            lbsotiencat = new Label();
            lbthoigiancat = new Label();
            radioUSD = new RadioButton();
            radioVND = new RadioButton();
            lbtiente = new Label();
            addLoan = new Button();
            cbnganhang = new ComboBox();
            cbchuky = new ComboBox();
            lbnganhang = new Label();
            lbchuky = new Label();
            dateStartDate = new DateTimePicker();
            lbthoigian = new Label();
            inputDuration = new TextBox();
            txtkyhan = new Label();
            inputAmount = new TextBox();
            txtsotien = new Label();
            inputLoanName = new TextBox();
            txtkhoanvay = new Label();
            groupkhoanvay = new GroupBox();
            btnadd = new Button();
            inputaddbank = new TextBox();
            panelfolter = new Panel();
            panelheader.SuspendLayout();
            panelnganhang.SuspendLayout();
            panelchucnang.SuspendLayout();
            gblichsu.SuspendLayout();
            groupvay.SuspendLayout();
            groupkhoanvay.SuspendLayout();
            SuspendLayout();
            // 
            // panelheader
            // 
            panelheader.BackColor = Color.Crimson;
            panelheader.Controls.Add(label2);
            panelheader.Controls.Add(label1);
            panelheader.Dock = DockStyle.Top;
            panelheader.Location = new Point(0, 0);
            panelheader.Name = "panelheader";
            panelheader.Size = new Size(1923, 96);
            panelheader.TabIndex = 0;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(625, 26);
            label2.Name = "label2";
            label2.Size = new Size(702, 45);
            label2.TabIndex = 1;
            label2.Text = "Hệ Thống Quản Lý Lãi Suất Ngân Hàng";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(12, 26);
            label1.Name = "label1";
            label1.Size = new Size(259, 45);
            label1.TabIndex = 0;
            label1.Text = "DRB Vietnam";
            // 
            // panelnganhang
            // 
            panelnganhang.BackColor = Color.White;
            panelnganhang.Controls.Add(paneldaihan);
            panelnganhang.Controls.Add(panelnganhan);
            panelnganhang.Location = new Point(0, 102);
            panelnganhang.Name = "panelnganhang";
            panelnganhang.Size = new Size(1305, 873);
            panelnganhang.TabIndex = 1;
            // 
            // paneldaihan
            // 
            paneldaihan.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            paneldaihan.Location = new Point(3, 431);
            paneldaihan.Name = "paneldaihan";
            paneldaihan.Size = new Size(1299, 439);
            paneldaihan.TabIndex = 1;
            paneldaihan.TabStop = false;
            paneldaihan.Text = "Khoản Vay Dài Hạn";
            paneldaihan.Enter += paneldaihan_Enter;
            // 
            // panelnganhan
            // 
            panelnganhan.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panelnganhan.Location = new Point(3, 11);
            panelnganhan.Name = "panelnganhan";
            panelnganhan.Size = new Size(1299, 410);
            panelnganhan.TabIndex = 0;
            panelnganhan.TabStop = false;
            panelnganhan.Text = "Khoản Vay Ngắn Hạn";
            panelnganhan.Enter += panelnganhan_Enter;
            // 
            // panelchucnang
            // 
            panelchucnang.BackColor = Color.FromArgb(44, 61, 77);
            panelchucnang.Controls.Add(gblichsu);
            panelchucnang.Controls.Add(groupvay);
            panelchucnang.Controls.Add(groupkhoanvay);
            panelchucnang.Location = new Point(1311, 102);
            panelchucnang.Name = "panelchucnang";
            panelchucnang.Size = new Size(612, 873);
            panelchucnang.TabIndex = 2;
            // 
            // gblichsu
            // 
            gblichsu.Controls.Add(btnexport);
            gblichsu.Controls.Add(btEmail);
            gblichsu.Controls.Add(btnlichsu);
            gblichsu.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gblichsu.ForeColor = SystemColors.ButtonHighlight;
            gblichsu.Location = new Point(3, 757);
            gblichsu.Name = "gblichsu";
            gblichsu.Size = new Size(603, 109);
            gblichsu.TabIndex = 2;
            gblichsu.TabStop = false;
            gblichsu.Text = "Lịch Sử";
            // 
            // btnexport
            // 
            btnexport.BackColor = Color.FromArgb(39, 185, 154);
            btnexport.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnexport.Location = new Point(426, 52);
            btnexport.Name = "btnexport";
            btnexport.Size = new Size(110, 37);
            btnexport.TabIndex = 19;
            btnexport.Text = "Xuất Execl";
            btnexport.UseVisualStyleBackColor = false;
            btnexport.Click += btnexport_Click;
            // 
            // btEmail
            // 
            btEmail.BackColor = Color.FromArgb(39, 185, 154);
            btEmail.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btEmail.Location = new Point(247, 52);
            btEmail.Name = "btEmail";
            btEmail.Size = new Size(110, 37);
            btEmail.TabIndex = 17;
            btEmail.Text = "Email";
            btEmail.UseVisualStyleBackColor = false;
            btEmail.Click += btEmail_Click;
            // 
            // btnlichsu
            // 
            btnlichsu.BackColor = Color.FromArgb(39, 185, 154);
            btnlichsu.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnlichsu.Location = new Point(55, 52);
            btnlichsu.Name = "btnlichsu";
            btnlichsu.Size = new Size(110, 37);
            btnlichsu.TabIndex = 16;
            btnlichsu.Text = "Lịch Sử";
            btnlichsu.UseVisualStyleBackColor = false;
            btnlichsu.Click += btnlichsu_Click;
            // 
            // groupvay
            // 
            groupvay.Controls.Add(txtsotiencat);
            groupvay.Controls.Add(timecatgoc);
            groupvay.Controls.Add(lbsotiencat);
            groupvay.Controls.Add(lbthoigiancat);
            groupvay.Controls.Add(radioUSD);
            groupvay.Controls.Add(radioVND);
            groupvay.Controls.Add(lbtiente);
            groupvay.Controls.Add(addLoan);
            groupvay.Controls.Add(cbnganhang);
            groupvay.Controls.Add(cbchuky);
            groupvay.Controls.Add(lbnganhang);
            groupvay.Controls.Add(lbchuky);
            groupvay.Controls.Add(dateStartDate);
            groupvay.Controls.Add(lbthoigian);
            groupvay.Controls.Add(inputDuration);
            groupvay.Controls.Add(txtkyhan);
            groupvay.Controls.Add(inputAmount);
            groupvay.Controls.Add(txtsotien);
            groupvay.Controls.Add(inputLoanName);
            groupvay.Controls.Add(txtkhoanvay);
            groupvay.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupvay.ForeColor = SystemColors.ButtonHighlight;
            groupvay.Location = new Point(3, 174);
            groupvay.Name = "groupvay";
            groupvay.Size = new Size(597, 577);
            groupvay.TabIndex = 1;
            groupvay.TabStop = false;
            groupvay.Text = "Tạo Khoản Vay";
            // 
            // txtsotiencat
            // 
            txtsotiencat.Location = new Point(165, 450);
            txtsotiencat.Name = "txtsotiencat";
            txtsotiencat.Size = new Size(410, 39);
            txtsotiencat.TabIndex = 19;
            txtsotiencat.TextChanged += txtsotiencat_TextChanged;
            // 
            // timecatgoc
            // 
            timecatgoc.Format = DateTimePickerFormat.Custom;
            timecatgoc.Location = new Point(165, 396);
            timecatgoc.Name = "timecatgoc";
            timecatgoc.Size = new Size(410, 39);
            timecatgoc.TabIndex = 18;
            timecatgoc.ValueChanged += timecatgoc_ValueChanged;
            // 
            // lbsotiencat
            // 
            lbsotiencat.AutoSize = true;
            lbsotiencat.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbsotiencat.Location = new Point(16, 460);
            lbsotiencat.Name = "lbsotiencat";
            lbsotiencat.Size = new Size(122, 23);
            lbsotiencat.TabIndex = 17;
            lbsotiencat.Text = "Số Tiền Cắt :";
            // 
            // lbthoigiancat
            // 
            lbthoigiancat.AutoSize = true;
            lbthoigiancat.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbthoigiancat.Location = new Point(21, 409);
            lbthoigiancat.Name = "lbthoigiancat";
            lbthoigiancat.Size = new Size(121, 23);
            lbthoigiancat.TabIndex = 16;
            lbthoigiancat.Text = "Tg Cắt Gốc :";
            // 
            // radioUSD
            // 
            radioUSD.AutoSize = true;
            radioUSD.Font = new Font("Times New Roman", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioUSD.Location = new Point(291, 518);
            radioUSD.Name = "radioUSD";
            radioUSD.Size = new Size(80, 29);
            radioUSD.TabIndex = 15;
            radioUSD.TabStop = true;
            radioUSD.Text = "USD";
            radioUSD.UseVisualStyleBackColor = true;
            // 
            // radioVND
            // 
            radioVND.AutoSize = true;
            radioVND.Font = new Font("Times New Roman", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioVND.Location = new Point(178, 518);
            radioVND.Name = "radioVND";
            radioVND.Size = new Size(83, 29);
            radioVND.TabIndex = 14;
            radioVND.TabStop = true;
            radioVND.Text = "VND";
            radioVND.UseVisualStyleBackColor = true;
            // 
            // lbtiente
            // 
            lbtiente.AutoSize = true;
            lbtiente.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbtiente.Location = new Point(34, 524);
            lbtiente.Name = "lbtiente";
            lbtiente.Size = new Size(104, 23);
            lbtiente.TabIndex = 13;
            lbtiente.Text = "Kiểu Tiền :";
            // 
            // addLoan
            // 
            addLoan.BackColor = Color.Blue;
            addLoan.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            addLoan.Location = new Point(456, 508);
            addLoan.Name = "addLoan";
            addLoan.Size = new Size(119, 39);
            addLoan.TabIndex = 12;
            addLoan.Text = "Tạo Mới";
            addLoan.UseVisualStyleBackColor = false;
            addLoan.Click += addLoan_Click;
            // 
            // cbnganhang
            // 
            cbnganhang.FormattingEnabled = true;
            cbnganhang.Location = new Point(165, 339);
            cbnganhang.Name = "cbnganhang";
            cbnganhang.Size = new Size(410, 39);
            cbnganhang.TabIndex = 11;
            // 
            // cbchuky
            // 
            cbchuky.FormattingEnabled = true;
            cbchuky.Location = new Point(165, 280);
            cbchuky.Name = "cbchuky";
            cbchuky.Size = new Size(410, 39);
            cbchuky.TabIndex = 10;
            // 
            // lbnganhang
            // 
            lbnganhang.AutoSize = true;
            lbnganhang.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbnganhang.Location = new Point(28, 349);
            lbnganhang.Name = "lbnganhang";
            lbnganhang.Size = new Size(116, 23);
            lbnganhang.TabIndex = 9;
            lbnganhang.Text = "Ngân Hàng :";
            // 
            // lbchuky
            // 
            lbchuky.AutoSize = true;
            lbchuky.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbchuky.Location = new Point(55, 290);
            lbchuky.Name = "lbchuky";
            lbchuky.Size = new Size(85, 23);
            lbchuky.TabIndex = 8;
            lbchuky.Text = "Chu Kỳ :";
            // 
            // dateStartDate
            // 
            dateStartDate.Format = DateTimePickerFormat.Custom;
            dateStartDate.Location = new Point(165, 221);
            dateStartDate.Name = "dateStartDate";
            dateStartDate.Size = new Size(410, 39);
            dateStartDate.TabIndex = 7;
            // 
            // lbthoigian
            // 
            lbthoigian.AutoSize = true;
            lbthoigian.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbthoigian.Location = new Point(34, 234);
            lbthoigian.Name = "lbthoigian";
            lbthoigian.Size = new Size(107, 23);
            lbthoigian.TabIndex = 6;
            lbthoigian.Text = "Thời Gian :";
            // 
            // inputDuration
            // 
            inputDuration.Location = new Point(165, 162);
            inputDuration.Name = "inputDuration";
            inputDuration.Size = new Size(410, 39);
            inputDuration.TabIndex = 5;
            // 
            // txtkyhan
            // 
            txtkyhan.AutoSize = true;
            txtkyhan.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtkyhan.Location = new Point(55, 172);
            txtkyhan.Name = "txtkyhan";
            txtkyhan.Size = new Size(86, 23);
            txtkyhan.TabIndex = 4;
            txtkyhan.Text = "Kỳ Hạn :";
            // 
            // inputAmount
            // 
            inputAmount.Location = new Point(165, 106);
            inputAmount.Name = "inputAmount";
            inputAmount.Size = new Size(410, 39);
            inputAmount.TabIndex = 3;
            inputAmount.TextChanged += inputAmount_TextChanged;
            // 
            // txtsotien
            // 
            txtsotien.AutoSize = true;
            txtsotien.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtsotien.Location = new Point(21, 122);
            txtsotien.Name = "txtsotien";
            txtsotien.Size = new Size(123, 23);
            txtsotien.TabIndex = 2;
            txtsotien.Text = "Số Tiền Vay :";
            // 
            // inputLoanName
            // 
            inputLoanName.Location = new Point(165, 51);
            inputLoanName.Name = "inputLoanName";
            inputLoanName.Size = new Size(410, 39);
            inputLoanName.TabIndex = 1;
            // 
            // txtkhoanvay
            // 
            txtkhoanvay.AutoSize = true;
            txtkhoanvay.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtkhoanvay.Location = new Point(28, 61);
            txtkhoanvay.Name = "txtkhoanvay";
            txtkhoanvay.Size = new Size(113, 23);
            txtkhoanvay.TabIndex = 0;
            txtkhoanvay.Text = "Khoản Vay :";
            // 
            // groupkhoanvay
            // 
            groupkhoanvay.Controls.Add(btnadd);
            groupkhoanvay.Controls.Add(inputaddbank);
            groupkhoanvay.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupkhoanvay.ForeColor = SystemColors.ButtonHighlight;
            groupkhoanvay.Location = new Point(3, 11);
            groupkhoanvay.Name = "groupkhoanvay";
            groupkhoanvay.Size = new Size(597, 138);
            groupkhoanvay.TabIndex = 0;
            groupkhoanvay.TabStop = false;
            groupkhoanvay.Text = "Ngân Hàng";
            // 
            // btnadd
            // 
            btnadd.BackColor = Color.FromArgb(39, 185, 154);
            btnadd.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnadd.ForeColor = SystemColors.ButtonHighlight;
            btnadd.Location = new Point(469, 64);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(106, 39);
            btnadd.TabIndex = 1;
            btnadd.Text = "Thêm Mới";
            btnadd.UseVisualStyleBackColor = false;
            btnadd.Click += btnadd_Click;
            // 
            // inputaddbank
            // 
            inputaddbank.Location = new Point(23, 64);
            inputaddbank.Name = "inputaddbank";
            inputaddbank.Size = new Size(418, 39);
            inputaddbank.TabIndex = 0;
            // 
            // panelfolter
            // 
            panelfolter.BackColor = Color.Crimson;
            panelfolter.Dock = DockStyle.Bottom;
            panelfolter.Location = new Point(0, 980);
            panelfolter.Name = "panelfolter";
            panelfolter.Size = new Size(1923, 29);
            panelfolter.TabIndex = 3;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1923, 1009);
            Controls.Add(panelfolter);
            Controls.Add(panelchucnang);
            Controls.Add(panelnganhang);
            Controls.Add(panelheader);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Home";
            WindowState = FormWindowState.Maximized;
            panelheader.ResumeLayout(false);
            panelheader.PerformLayout();
            panelnganhang.ResumeLayout(false);
            panelchucnang.ResumeLayout(false);
            gblichsu.ResumeLayout(false);
            groupvay.ResumeLayout(false);
            groupvay.PerformLayout();
            groupkhoanvay.ResumeLayout(false);
            groupkhoanvay.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelheader;
        private Panel panelnganhang;
        private Panel panelchucnang;
        private Panel panelfolter;
        private Label label1;
        private Label label2;
        private GroupBox groupkhoanvay;
        private Button btnadd;
        private TextBox inputaddbank;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private GroupBox groupvay;
        private TextBox inputLoanName;
        private Label txtkhoanvay;
        private Label txtsotien;
        private TextBox inputDuration;
        private Label txtkyhan;
        private TextBox inputAmount;
        private DateTimePicker dateStartDate;
        private Label lbthoigian;
        private ComboBox cbnganhang;
        private ComboBox cbchuky;
        private Label lbnganhang;
        private Label lbchuky;
        private Button addLoan;
        private RadioButton radioUSD;
        private RadioButton radioVND;
        private Label lbtiente;
        private GroupBox paneldaihan;
        private GroupBox panelnganhan;
        private GroupBox gblichsu;
        private Button btnlichsu;
        private Button btEmail;
        private TextBox txtsotiencat;
        private DateTimePicker timecatgoc;
        private Label lbsotiencat;
        private Label lbthoigiancat;
        private Button btnexport;
        private Label label3;
        private Button button1;
    }
}
