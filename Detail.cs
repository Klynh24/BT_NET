using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT01_03
{
    public partial class Detail: Form
    {
        public delegate void MyDel(string LSH, string txt);
        public MyDel d { get; set; }
        public string M { get; set; }
        
        public Detail(string m, MyDel d)
        {
            InitializeComponent();
            M = m;
            this.d = d;
            QLSV bll = new QLSV();
            cbBLSH.Items.AddRange(bll.GetAllLSH().ToArray());
            GUI();
        }

        private void GUI()
        {
            txtMSSV.ReadOnly = false;
            if (M == "")
            {
                txtMSSV.Text = "";
                txtName.Text = "";
                txtDTB.Text = "";
                cbBLSH.SelectedIndex = -1; 
                rbM.Checked = false;
                rbF.Checked = false;
                dtpNS.Value = DateTime.Now; 
            }
            else
            {
                QLSV bll = new QLSV();
                SV sv = bll.GetSVByMSSV(M);
                if (sv != null)
                {
                    txtMSSV.ReadOnly = true;
                    txtMSSV.Text = sv.MSSV;
                    txtName.Text = sv.NameSV;
                    txtDTB.Text = sv.DTB.ToString();
                    cbBLSH.SelectedItem = sv.LSH;
                    rbM.Checked = sv.Gender;      
                    rbF.Checked = !sv.Gender;
                    cbBLSH.SelectedItem = sv.LSH;
                    if (sv.NS < dtpNS.MinDate)
                    {
                        dtpNS.Value = DateTime.Now;  
                    }
                    else
                    {
                        dtpNS.Value = sv.NS;
                    }

                }
            }
        }

        private SV GetSVByMSSV()
        {
            return CSDL.Instance.li.FirstOrDefault(sv => sv.MSSV == M);
        }

        private void SetByName(string MSSV, string newName)
        {
            foreach (SV sv in CSDL.Instance.li)
            {
                if (sv.MSSV == MSSV)
                {
                    sv.NameSV = newName; 
                    break;
                }
            }
        }

        private void SetByDTB(double DTB, double newDTB)
        {
            foreach (SV sv in CSDL.Instance.li)
            {
                if (sv.DTB == DTB)
                {
                    sv.DTB = newDTB;
                    break;
                }
            }
        }

        private void SetByLSH(string MSSV, string newLSH)
        {
            foreach (SV sv in CSDL.Instance.li)
            {
                if (sv.MSSV == MSSV)
                {
                    sv.LSH = newLSH;
                    break;
                }
            }
        }

        private void SetByNS(string MSSV, DateTime newNS)
        {
            foreach (SV sv in CSDL.Instance.li)
            {
                if (sv.MSSV == MSSV)
                {
                    sv.NS = newNS;
                    break;
                }
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            QLSV bll = new QLSV();

            if (string.IsNullOrEmpty(txtMSSV.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double newDTB;
            if (!double.TryParse(txtDTB.Text, out newDTB)) 
            {
                MessageBox.Show("Điểm trung bình không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string selectedLSH = cbBLSH.SelectedItem?.ToString();

            bool gender = rbM.Checked;

            DateTime newNS = dtpNS.Value;

            if (string.IsNullOrEmpty(M)) 
            {
                string mssv = txtMSSV.Text.Trim();
                if (string.IsNullOrEmpty(mssv))
                {
                    MessageBox.Show("MSSV không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (CSDL.Instance.li.Any(sv => sv.MSSV == mssv))
                {
                    MessageBox.Show("MSSV đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                SV newSV = new SV()
                {
                    MSSV = mssv,
                    NameSV = txtName.Text.Trim(),
                    DTB = newDTB,
                    Gender = gender,
                    NS = newNS,
                    LSH = selectedLSH
                };
                CSDL.Instance.li.Add(newSV);
            }

            else 
            {
                SV sv = GetSVByMSSV();
                SetByName(M, txtName.Text);
                SetByDTB(sv.DTB, newDTB);
                SetByLSH(M, selectedLSH);
                sv.Gender = gender;
                SetByNS(M, newNS);

            }
            if (string.IsNullOrEmpty(M))
            {
                CSDL.Instance.li.Add(newSV);
                bll.AddUpdate(newSV);
            }
            else
            {
                SV sv = GetSVByMSSV();
                if (sv != null)
                {
                    SetByName(M, txtName.Text);
                    SetByDTB(sv.DTB, newDTB);
                    SetByLSH(M, selectedLSH);
                    sv.Gender = gender;
                    SetByNS(M, newNS);
                    bll.AddUpdate(sv);
                }
            }

            d("All", ""); 
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
