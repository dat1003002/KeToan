using System;
using System.Windows.Forms;
using Admin_KeToan.Data;
using Admin_KeToan.Model;
using Microsoft.EntityFrameworkCore;

namespace Admin_KeToan
{
    public partial class DsEmail : Form
    {
        private readonly KeToanDbContext _context;

        public DsEmail()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            _context = new KeToanDbContext();
            ConfigureDataGridView();
            LoadEmails();
        }

        private void ConfigureDataGridView()
        {
            dataEmail.Columns.Clear();
            dataEmail.AutoGenerateColumns = false;
            dataEmail.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EmailId",
                HeaderText = "ID",
                Name = "EmailId",
                Width = 50 // Fixed width for ID column
            });
            dataEmail.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EmailAddress",
                HeaderText = "ĐỊA CHỈ EMAIL",
                Name = "EmailAddress",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // EmailAddress takes remaining space
            });

            // Customize header style: larger font and bold
            dataEmail.ColumnHeadersDefaultCellStyle.Font = new Font(dataEmail.Font.FontFamily, 12, FontStyle.Bold);
        }

        private void LoadEmails()
        {
            dataEmail.DataSource = _context.Emails.Select(e => new { e.EmailId, e.EmailAddress }).ToList();
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtemail.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_context.Emails.Any(e => e.EmailAddress == txtemail.Text))
            {
                _context.Emails.Add(new Email { EmailAddress = txtemail.Text });
                _context.SaveChanges();
                LoadEmails();
                txtemail.Clear();
                MessageBox.Show("Thêm email thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Email đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dataEmail.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn email để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtemail.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int emailId = (int)dataEmail.SelectedRows[0].Cells["EmailId"].Value;
            var email = _context.Emails.Find(emailId);

            if (email != null)
            {
                email.EmailAddress = txtemail.Text;
                _context.SaveChanges();
                LoadEmails();
                txtemail.Clear();
                MessageBox.Show("Sửa email thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataEmail.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn email để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int emailId = (int)dataEmail.SelectedRows[0].Cells["EmailId"].Value;
            var email = _context.Emails.Find(emailId);

            if (email != null)
            {
                _context.Emails.Remove(email);
                _context.SaveChanges();
                LoadEmails();
                txtemail.Clear();
                MessageBox.Show("Xóa email thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataEmail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataEmail.Rows[e.RowIndex];
                txtemail.Text = row.Cells["EmailAddress"].Value.ToString();
            }
        }
    }
}