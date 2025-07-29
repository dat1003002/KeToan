namespace Admin_KeToan
{
    partial class Login
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
            pictureBox1 = new PictureBox();
            textuser = new TextBox();
            textpass = new TextBox();
            btnlogin = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo_drb;
            pictureBox1.Location = new Point(40, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(423, 124);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textuser
            // 
            textuser.BorderStyle = BorderStyle.FixedSingle;
            textuser.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textuser.Location = new Point(51, 178);
            textuser.Multiline = true;
            textuser.Name = "textuser";
            textuser.Size = new Size(387, 37);
            textuser.TabIndex = 1;
            textuser.TextChanged += textuser_TextChanged;
            // 
            // textpass
            // 
            textpass.BorderStyle = BorderStyle.FixedSingle;
            textpass.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textpass.Location = new Point(51, 239);
            textpass.Multiline = true;
            textpass.Name = "textpass";
            textpass.Size = new Size(387, 37);
            textpass.TabIndex = 4;
            textpass.TextChanged += textpass_TextChanged;
            // 
            // btnlogin
            // 
            btnlogin.BackColor = Color.Crimson;
            btnlogin.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnlogin.ForeColor = SystemColors.ButtonHighlight;
            btnlogin.Location = new Point(156, 310);
            btnlogin.Name = "btnlogin";
            btnlogin.Size = new Size(179, 45);
            btnlogin.TabIndex = 6;
            btnlogin.Text = "Login";
            btnlogin.UseVisualStyleBackColor = false;
            btnlogin.Click += btnlogin_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 475);
            Controls.Add(btnlogin);
            Controls.Add(textpass);
            Controls.Add(textuser);
            Controls.Add(pictureBox1);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textuser;
        private TextBox textpass;
        private Button btnlogin;
    }
}