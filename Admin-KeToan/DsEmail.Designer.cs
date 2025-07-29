namespace Admin_KeToan
{
    partial class DsEmail
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
            dataEmail = new DataGridView();
            groupBox1 = new GroupBox();
            btndelete = new Button();
            btnedit = new Button();
            btnadd = new Button();
            txtemail = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataEmail).BeginInit();
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
            panel1.Size = new Size(800, 82);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(275, 22);
            label1.Name = "label1";
            label1.Size = new Size(234, 35);
            label1.TabIndex = 0;
            label1.Text = "Danh Sách Email";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataEmail
            // 
            dataEmail.BackgroundColor = SystemColors.GradientActiveCaption;
            dataEmail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataEmail.Location = new Point(0, 256);
            dataEmail.Name = "dataEmail";
            dataEmail.RowHeadersWidth = 51;
            dataEmail.Size = new Size(800, 303);
            dataEmail.TabIndex = 1;
            dataEmail.CellContentClick += dataEmail_CellContentClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btndelete);
            groupBox1.Controls.Add(btnedit);
            groupBox1.Controls.Add(btnadd);
            groupBox1.Controls.Add(txtemail);
            groupBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(0, 88);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 148);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chức Năng Email";
            // 
            // btndelete
            // 
            btndelete.BackColor = Color.Crimson;
            btndelete.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btndelete.ForeColor = SystemColors.ButtonHighlight;
            btndelete.Location = new Point(679, 62);
            btndelete.Name = "btndelete";
            btndelete.Size = new Size(109, 34);
            btndelete.TabIndex = 3;
            btndelete.Text = "Xóa";
            btndelete.UseVisualStyleBackColor = false;
            btndelete.Click += btndelete_Click;
            // 
            // btnedit
            // 
            btnedit.BackColor = SystemColors.HotTrack;
            btnedit.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnedit.ForeColor = SystemColors.ButtonHighlight;
            btnedit.Location = new Point(552, 63);
            btnedit.Name = "btnedit";
            btnedit.Size = new Size(109, 34);
            btnedit.TabIndex = 2;
            btnedit.Text = "Chỉnh Sửa";
            btnedit.UseVisualStyleBackColor = false;
            btnedit.Click += btnedit_Click;
            // 
            // btnadd
            // 
            btnadd.BackColor = Color.FromArgb(39, 185, 154);
            btnadd.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnadd.ForeColor = SystemColors.ButtonHighlight;
            btnadd.Location = new Point(426, 62);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(109, 34);
            btnadd.TabIndex = 1;
            btnadd.Text = "Tạo Mới";
            btnadd.UseVisualStyleBackColor = false;
            btnadd.Click += btnadd_Click;
            // 
            // txtemail
            // 
            txtemail.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtemail.Location = new Point(12, 63);
            txtemail.Multiline = true;
            txtemail.Name = "txtemail";
            txtemail.Size = new Size(395, 34);
            txtemail.TabIndex = 0;
            txtemail.TextChanged += txtemail_TextChanged;
            // 
            // DsEmail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 571);
            Controls.Add(groupBox1);
            Controls.Add(dataEmail);
            Controls.Add(panel1);
            Name = "DsEmail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DsEmail";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataEmail).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView dataEmail;
        private GroupBox groupBox1;
        private Button btnadd;
        private TextBox txtemail;
        private Button btndelete;
        private Button btnedit;
    }
}