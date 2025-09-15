namespace Admin_KeToan
{
    partial class ChiTiet
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
            dataPayment = new DataGridView();
            groupBox1 = new GroupBox();
            btnthemmoi = new Button();
            label7 = new Label();
            txtsongay = new TextBox();
            lbdutinh = new Label();
            lbngaytralai = new Label();
            lbCumulativeInterestPaid = new Label();
            lblai = new Label();
            lbEndDate = new Label();
            lbcatgoc = new Label();
            addpayment = new Button();
            txtcatgoc = new TextBox();
            label6 = new Label();
            txtlaisuat = new TextBox();
            label5 = new Label();
            radio360 = new RadioButton();
            radio365 = new RadioButton();
            label4 = new Label();
            dateEndDate = new DateTimePicker();
            StartDate = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataPayment).BeginInit();
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
            panel1.Size = new Size(1242, 78);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(397, 21);
            label1.Name = "label1";
            label1.Size = new Size(428, 38);
            label1.TabIndex = 0;
            label1.Text = "Chi Tiết Khoản Thanh Toán";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataPayment
            // 
            dataPayment.BackgroundColor = SystemColors.ActiveCaption;
            dataPayment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataPayment.Location = new Point(0, 343);
            dataPayment.Name = "dataPayment";
            dataPayment.RowHeadersWidth = 51;
            dataPayment.Size = new Size(1242, 438);
            dataPayment.TabIndex = 1;
            dataPayment.CellContentClick += dataPayment_CellContentClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnthemmoi);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtsongay);
            groupBox1.Controls.Add(lbdutinh);
            groupBox1.Controls.Add(lbngaytralai);
            groupBox1.Controls.Add(lbCumulativeInterestPaid);
            groupBox1.Controls.Add(lblai);
            groupBox1.Controls.Add(lbEndDate);
            groupBox1.Controls.Add(lbcatgoc);
            groupBox1.Controls.Add(addpayment);
            groupBox1.Controls.Add(txtcatgoc);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtlaisuat);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(radio360);
            groupBox1.Controls.Add(radio365);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dateEndDate);
            groupBox1.Controls.Add(StartDate);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 94);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1218, 230);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chi Tiết Thanh Toán";
            // 
            // btnthemmoi
            // 
            btnthemmoi.BackColor = Color.FromArgb(39, 185, 154);
            btnthemmoi.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnthemmoi.ForeColor = SystemColors.ButtonHighlight;
            btnthemmoi.Location = new Point(992, 168);
            btnthemmoi.Name = "btnthemmoi";
            btnthemmoi.Size = new Size(97, 33);
            btnthemmoi.TabIndex = 20;
            btnthemmoi.Text = "Tạo Mới";
            btnthemmoi.UseVisualStyleBackColor = false;
            btnthemmoi.Click += btnthemmoi_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(484, 173);
            label7.Name = "label7";
            label7.Size = new Size(92, 23);
            label7.TabIndex = 19;
            label7.Text = "Số Ngày :";
            // 
            // txtsongay
            // 
            txtsongay.Location = new Point(615, 168);
            txtsongay.Name = "txtsongay";
            txtsongay.Size = new Size(204, 34);
            txtsongay.TabIndex = 18;
            txtsongay.TextChanged += txtsongay_TextChanged;
            // 
            // lbdutinh
            // 
            lbdutinh.AutoSize = true;
            lbdutinh.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbdutinh.Location = new Point(866, 90);
            lbdutinh.Name = "lbdutinh";
            lbdutinh.Size = new Size(146, 23);
            lbdutinh.TabIndex = 17;
            lbdutinh.Text = "Khoản Dự Tính:";
            // 
            // lbngaytralai
            // 
            lbngaytralai.AutoSize = true;
            lbngaytralai.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbngaytralai.Location = new Point(866, 51);
            lbngaytralai.Name = "lbngaytralai";
            lbngaytralai.Size = new Size(175, 23);
            lbngaytralai.TabIndex = 16;
            lbngaytralai.Text = "Thời Gian Trả Lãi :";
            lbngaytralai.Click += lbngaytralai_Click;
            // 
            // lbCumulativeInterestPaid
            // 
            lbCumulativeInterestPaid.AutoSize = true;
            lbCumulativeInterestPaid.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCumulativeInterestPaid.Location = new Point(866, 132);
            lbCumulativeInterestPaid.Name = "lbCumulativeInterestPaid";
            lbCumulativeInterestPaid.Size = new Size(164, 23);
            lbCumulativeInterestPaid.TabIndex = 15;
            lbCumulativeInterestPaid.Text = "Tổng Thanh Toán:";
            lbCumulativeInterestPaid.Click += lbCumulativeInterestPaid_Click;
            // 
            // lblai
            // 
            lblai.AutoSize = true;
            lblai.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblai.Location = new Point(866, 19);
            lblai.Name = "lblai";
            lblai.Size = new Size(95, 19);
            lblai.TabIndex = 13;
            lblai.Text = "Sỗ Lãi Trả :";
            lblai.Click += lblai_Click;
            // 
            // lbEndDate
            // 
            lbEndDate.AutoSize = true;
            lbEndDate.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbEndDate.Location = new Point(547, 19);
            lbEndDate.Name = "lbEndDate";
            lbEndDate.Size = new Size(165, 19);
            lbEndDate.TabIndex = 12;
            lbEndDate.Text = "Kết Thúc Khoản Vay :";
            // 
            // lbcatgoc
            // 
            lbcatgoc.AutoSize = true;
            lbcatgoc.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbcatgoc.Location = new Point(263, 19);
            lbcatgoc.Name = "lbcatgoc";
            lbcatgoc.Size = new Size(119, 19);
            lbcatgoc.TabIndex = 3;
            lbcatgoc.Text = "Tiền Tính Lãi :";
            lbcatgoc.Click += lbcatgoc_Click;
            // 
            // addpayment
            // 
            addpayment.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            addpayment.Location = new Point(854, 167);
            addpayment.Name = "addpayment";
            addpayment.Size = new Size(107, 34);
            addpayment.TabIndex = 11;
            addpayment.Text = "Chỉnh Sửa";
            addpayment.UseVisualStyleBackColor = true;
            addpayment.Click += addpayment_Click;
            // 
            // txtcatgoc
            // 
            txtcatgoc.Location = new Point(164, 179);
            txtcatgoc.Name = "txtcatgoc";
            txtcatgoc.Size = new Size(246, 34);
            txtcatgoc.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(65, 179);
            label6.Name = "label6";
            label6.Size = new Size(93, 23);
            label6.TabIndex = 9;
            label6.Text = "Cắt Gốc :";
            // 
            // txtlaisuat
            // 
            txtlaisuat.Location = new Point(615, 116);
            txtlaisuat.Name = "txtlaisuat";
            txtlaisuat.Size = new Size(147, 34);
            txtlaisuat.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(484, 127);
            label5.Name = "label5";
            label5.Size = new Size(125, 23);
            label5.TabIndex = 7;
            label5.Text = "Lãi Suất (%):";
            // 
            // radio360
            // 
            radio360.AutoSize = true;
            radio360.Location = new Point(633, 71);
            radio360.Name = "radio360";
            radio360.Size = new Size(69, 29);
            radio360.TabIndex = 6;
            radio360.TabStop = true;
            radio360.Text = "360";
            radio360.UseVisualStyleBackColor = true;
            // 
            // radio365
            // 
            radio365.AutoSize = true;
            radio365.Location = new Point(708, 71);
            radio365.Name = "radio365";
            radio365.Size = new Size(69, 29);
            radio365.TabIndex = 5;
            radio365.TabStop = true;
            radio365.Text = "365";
            radio365.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(484, 71);
            label4.Name = "label4";
            label4.Size = new Size(143, 23);
            label4.TabIndex = 4;
            label4.Text = "Ngày Tính Lãi :";
            // 
            // dateEndDate
            // 
            dateEndDate.Format = DateTimePickerFormat.Custom;
            dateEndDate.Location = new Point(164, 121);
            dateEndDate.Name = "dateEndDate";
            dateEndDate.Size = new Size(246, 34);
            dateEndDate.TabIndex = 3;
            // 
            // StartDate
            // 
            StartDate.Format = DateTimePickerFormat.Custom;
            StartDate.Location = new Point(164, 62);
            StartDate.Name = "StartDate";
            StartDate.Size = new Size(246, 34);
            StartDate.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(24, 121);
            label3.Name = "label3";
            label3.Size = new Size(134, 23);
            label3.TabIndex = 1;
            label3.Text = "Ngày Trả Lãi :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(15, 71);
            label2.Name = "label2";
            label2.Size = new Size(143, 23);
            label2.TabIndex = 0;
            label2.Text = "Ngày Tính Lãi :";
            // 
            // ChiTiet
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1242, 807);
            Controls.Add(groupBox1);
            Controls.Add(dataPayment);
            Controls.Add(panel1);
            Name = "ChiTiet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChiTiet";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataPayment).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView dataPayment;
        private GroupBox groupBox1;
        private DateTimePicker dateEndDate;
        private DateTimePicker StartDate;
        private Label label3;
        private Label label2;
        private RadioButton radio365;
        private Label label4;
        private RadioButton radio360;
        private TextBox txtlaisuat;
        private Label label5;
        private Label label6;
        private TextBox txtcatgoc;
        private Button addpayment;
        private Label lbcatgoc;
        private Label lbEndDate;
        private Label lblai;
        private Label lbCumulativeInterestPaid;
        private Label lbngaytralai;
        private Label lbdutinh;
        private Label label7;
        private TextBox txtsongay;
        private Button btnthemmoi;
    }
}