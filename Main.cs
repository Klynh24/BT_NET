using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace BT01_03
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            QLSV bll = new QLSV();
            cbBLSH.Items.Add("All");
            cbBLSH.Items.AddRange(bll.GetAllLSH().ToArray());
        }

        public void Reload(string LSH, string txt)
        {
            QLSV bll = new QLSV();
            dgv.DataSource = bll.ListSV(LSH, txt);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Reload(cbBLSH.SelectedItem.ToString(), txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Detail f = new Detail("", Reload);
            f.ShowDialog();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            QLSV bll = new QLSV();

            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            List<string> liDel = new List<string>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                string mssv = row.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(mssv)) // Kiểm tra MSSV hợp lệ
                {
                    liDel.Add(mssv);
                }
            }

            if (liDel.Count > 0)
            {
                bll.DelSV(liDel);
                Reload(cbBLSH.SelectedItem?.ToString() ?? "All", txtSearch.Text); // Gọi Reload thay vì LoadData
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count == 1)
            {
                string MSSV = dgv.SelectedRows[0].Cells["MSSV"].Value.ToString();
                Detail f = new Detail(MSSV, Reload);
                f.Show();
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            List<string> liview = new List<string>();
            foreach (DataGridViewRow i in dgv.Rows)
            {
                liview.Add(i.Cells["MSSV"].Value.ToString());
            }
            QLSV bll = new QLSV();
            dgv.DataSource = bll.ListSort(liview, cbBSort.SelectedItem.ToString());
        }
    }
}
