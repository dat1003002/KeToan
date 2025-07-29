namespace Admin_KeToan
{
    partial class CtLichsu
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
            datactLichsu = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datactLichsu).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Crimson;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 87);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = Color.Crimson;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(371, 29);
            label1.Name = "label1";
            label1.Size = new Size(433, 32);
            label1.TabIndex = 0;
            label1.Text = "CHI TIẾT LỊCH SỬ KHOẢN VAY";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // datactLichsu
            // 
            datactLichsu.BackgroundColor = SystemColors.ActiveCaption;
            datactLichsu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datactLichsu.Location = new Point(0, 93);
            datactLichsu.Name = "datactLichsu";
            datactLichsu.RowHeadersWidth = 51;
            datactLichsu.Size = new Size(1200, 480);
            datactLichsu.TabIndex = 1;
            datactLichsu.CellContentClick += datactLichsu_CellContentClick;
            // 
            // CtLichsu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 605);
            Controls.Add(datactLichsu);
            Controls.Add(panel1);
            Name = "CtLichsu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CtLichsu";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datactLichsu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DataGridView datactLichsu;
    }
}