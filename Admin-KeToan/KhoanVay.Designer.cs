namespace Admin_KeToan
{
    partial class KhoanVay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            dataLoan = new DataGridView();
            groupBox1 = new GroupBox();
            export = new Button();
            btnchitiet = new Button();
            btnxoa = new Button();
            btnEdit = new Button();
            cbnganhang = new ComboBox();
            inputDuration = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            cbchuky = new ComboBox();
            dateStartDate = new DateTimePicker();
            inputAmount = new TextBox();
            inputLoanName = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataLoan).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Crimson;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1242, 77);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(459, 18);
            label1.Name = "label1";
            label1.Size = new Size(410, 38);
            label1.TabIndex = 0;
            label1.Text = "Danh Sách Các Khoản Vay";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataLoan
            // 
            dataLoan.BackgroundColor = SystemColors.ActiveCaption;
            dataLoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataLoan.Location = new Point(0, 340);
            dataLoan.Name = "dataLoan";
            dataLoan.RowHeadersWidth = 51;
            dataLoan.Size = new Size(1242, 467);
            dataLoan.TabIndex = 1;
            dataLoan.CellContentClick += dataLoan_CellContentClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(export);
            groupBox1.Controls.Add(btnchitiet);
            groupBox1.Controls.Add(btnxoa);
            groupBox1.Controls.Add(btnEdit);
            groupBox1.Controls.Add(cbnganhang);
            groupBox1.Controls.Add(inputDuration);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cbchuky);
            groupBox1.Controls.Add(dateStartDate);
            groupBox1.Controls.Add(inputAmount);
            groupBox1.Controls.Add(inputLoanName);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 83);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1218, 239);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chi Tiết Khoản Vay";
            // 
            // export
            // 
            export.BackColor = Color.FromArgb(39, 185, 154);
            export.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            export.ForeColor = SystemColors.Control;
            export.Location = new Point(1033, 166);
            export.Name = "export";
            export.Size = new Size(104, 33);
            export.TabIndex = 15;
            export.Text = "Xuất execl";
            export.UseVisualStyleBackColor = false;
            export.Click += export_Click;
            // 
            // btnchitiet
            // 
            btnchitiet.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnchitiet.Location = new Point(1033, 125);
            btnchitiet.Name = "btnchitiet";
            btnchitiet.Size = new Size(104, 35);
            btnchitiet.TabIndex = 14;
            btnchitiet.Text = "Chi Tiết";
            btnchitiet.UseVisualStyleBackColor = true;
            btnchitiet.Click += btnchitiet_Click;
            // 
            // btnxoa
            // 
            btnxoa.BackColor = Color.Crimson;
            btnxoa.ForeColor = SystemColors.ButtonHighlight;
            btnxoa.Location = new Point(1033, 86);
            btnxoa.Name = "btnxoa";
            btnxoa.Size = new Size(104, 33);
            btnxoa.TabIndex = 13;
            btnxoa.Text = "Xóa";
            btnxoa.UseVisualStyleBackColor = false;
            btnxoa.Click += btnxoa_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = SystemColors.Highlight;
            btnEdit.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdit.ForeColor = SystemColors.InactiveBorder;
            btnEdit.Location = new Point(1033, 46);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(104, 34);
            btnEdit.TabIndex = 12;
            btnEdit.Text = "Chỉnh Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // cbnganhang
            // 
            cbnganhang.FormattingEnabled = true;
            cbnganhang.Location = new Point(692, 159);
            cbnganhang.Name = "cbnganhang";
            cbnganhang.Size = new Size(301, 33);
            cbnganhang.TabIndex = 11;
            // 
            // inputDuration
            // 
            inputDuration.Location = new Point(692, 46);
            inputDuration.Name = "inputDuration";
            inputDuration.Size = new Size(301, 34);
            inputDuration.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label7.Location = new Point(574, 116);
            label7.Name = "label7";
            label7.Size = new Size(85, 23);
            label7.TabIndex = 9;
            label7.Text = "Chu Kỳ :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label6.Location = new Point(542, 164);
            label6.Name = "label6";
            label6.Size = new Size(116, 23);
            label6.TabIndex = 8;
            label6.Text = "Ngân Hàng :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label5.Location = new Point(574, 57);
            label5.Name = "label5";
            label5.Size = new Size(86, 23);
            label5.TabIndex = 7;
            label5.Text = "Kỳ Hạn :";
            // 
            // cbchuky
            // 
            cbchuky.FormattingEnabled = true;
            cbchuky.Location = new Point(692, 103);
            cbchuky.Name = "cbchuky";
            cbchuky.Size = new Size(301, 33);
            cbchuky.TabIndex = 6;
            // 
            // dateStartDate
            // 
            dateStartDate.Format = DateTimePickerFormat.Custom;
            dateStartDate.Location = new Point(203, 165);
            dateStartDate.Name = "dateStartDate";
            dateStartDate.Size = new Size(250, 34);
            dateStartDate.TabIndex = 5;
            // 
            // inputAmount
            // 
            inputAmount.Location = new Point(203, 111);
            inputAmount.Name = "inputAmount";
            inputAmount.Size = new Size(301, 34);
            inputAmount.TabIndex = 4;
            inputAmount.TextChanged += inputAmount_TextChanged;
            // 
            // inputLoanName
            // 
            inputLoanName.Location = new Point(203, 46);
            inputLoanName.Name = "inputLoanName";
            inputLoanName.Size = new Size(301, 34);
            inputLoanName.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label4.Location = new Point(35, 171);
            label4.Name = "label4";
            label4.Size = new Size(107, 23);
            label4.TabIndex = 2;
            label4.Text = "Thời Gian :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label3.Location = new Point(35, 122);
            label3.Name = "label3";
            label3.Size = new Size(123, 23);
            label3.TabIndex = 1;
            label3.Text = "Số Tiền Vay :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label2.Location = new Point(35, 57);
            label2.Name = "label2";
            label2.Size = new Size(113, 23);
            label2.TabIndex = 0;
            label2.Text = "Khoản Vay :";
            // 
            // KhoanVay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1242, 807);
            Controls.Add(groupBox1);
            Controls.Add(dataLoan);
            Controls.Add(panel1);
            Name = "KhoanVay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KhoanVay";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataLoan).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView dataLoan;
        private GroupBox groupBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DateTimePicker dateStartDate;
        private TextBox inputAmount;
        private TextBox inputLoanName;
        private ComboBox cbchuky;
        private Label label5;
        private ComboBox cbnganhang;
        private TextBox inputDuration;
        private Label label7;
        private Label label6;
        private Button btnEdit;
        private Button btnxoa;
        private Button btnchitiet;
        private Button export;
    }
}