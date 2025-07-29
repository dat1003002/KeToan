namespace Admin_KeToan
{
    partial class Lichsu
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
            datalisu = new DataGridView();
            groupBox1 = new GroupBox();
            btnexcel = new Button();
            btnclose = new Button();
            btnchitet = new Button();
            cbloc = new ComboBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datalisu).BeginInit();
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
            panel1.Size = new Size(1200, 77);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(434, 21);
            label1.Name = "label1";
            label1.Size = new Size(319, 35);
            label1.TabIndex = 0;
            label1.Text = "Lịch Sử Các Khoản Vay";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // datalisu
            // 
            datalisu.BackgroundColor = SystemColors.ActiveCaption;
            datalisu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datalisu.Location = new Point(0, 250);
            datalisu.Name = "datalisu";
            datalisu.RowHeadersWidth = 51;
            datalisu.Size = new Size(1200, 522);
            datalisu.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnexcel);
            groupBox1.Controls.Add(btnclose);
            groupBox1.Controls.Add(btnchitet);
            groupBox1.Controls.Add(cbloc);
            groupBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 93);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1173, 114);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chức Năng";
            // 
            // btnexcel
            // 
            btnexcel.BackColor = SystemColors.Highlight;
            btnexcel.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnexcel.ForeColor = SystemColors.ButtonHighlight;
            btnexcel.Location = new Point(696, 47);
            btnexcel.Name = "btnexcel";
            btnexcel.Size = new Size(109, 33);
            btnexcel.TabIndex = 3;
            btnexcel.Text = "Xuất Execl";
            btnexcel.UseVisualStyleBackColor = false;
            btnexcel.Click += btnexcel_Click;
            // 
            // btnclose
            // 
            btnclose.BackColor = Color.Crimson;
            btnclose.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnclose.ForeColor = SystemColors.Control;
            btnclose.Location = new Point(554, 47);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(107, 33);
            btnclose.TabIndex = 2;
            btnclose.Text = "Đóng";
            btnclose.UseVisualStyleBackColor = false;
            btnclose.Click += btnclose_Click;
            // 
            // btnchitet
            // 
            btnchitet.BackColor = Color.FromArgb(39, 185, 154);
            btnchitet.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnchitet.ForeColor = SystemColors.Control;
            btnchitet.Location = new Point(422, 47);
            btnchitet.Name = "btnchitet";
            btnchitet.Size = new Size(107, 33);
            btnchitet.TabIndex = 1;
            btnchitet.Text = "Chi Tiết";
            btnchitet.UseVisualStyleBackColor = false;
            btnchitet.Click += btnchitet_Click;
            // 
            // cbloc
            // 
            cbloc.FormattingEnabled = true;
            cbloc.Location = new Point(18, 47);
            cbloc.Name = "cbloc";
            cbloc.Size = new Size(340, 33);
            cbloc.TabIndex = 0;
            cbloc.SelectedIndexChanged += cbloc_SelectedIndexChanged;
            // 
            // Lichsu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 797);
            Controls.Add(groupBox1);
            Controls.Add(datalisu);
            Controls.Add(panel1);
            Name = "Lichsu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lichsu";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datalisu).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView datalisu;
        private GroupBox groupBox1;
        private ComboBox cbloc;
        private Button btnclose;
        private Button btnchitet;
        private Button btnexcel;
    }
}