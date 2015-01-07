using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dbclasszzh;

namespace ZigBee.Client
{
    public partial class admin : Form
    {
        DBClass db = new DBClass();
        string user = "";
        public admin(string s)
        {
            InitializeComponent();
            user = s;
        }

        private void admin_Load(object sender, EventArgs e)
        {
            textBox1.Text = user;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = SystemColors.ButtonHighlight;
        }
        DialogResult dr;
        private void button1_Click(object sender, EventArgs e)
        {
            string stre = "select * from dbo.admin1 where users='"+textBox1.Text+"'and password='"+textBox2.Text+"'";//显示节点
            DataSet ds = db.GetDataSet(stre);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (textBox3.Text == textBox4.Text)
                {
                    dr = MessageBox.Show("确定修改用户密码？", "郑重提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        string qwe1 = "UPDATE dbo.admin1 SET password = '" + textBox3.Text +  "' WHERE users = '" + textBox1.Text + "'";
                        db.ExecuteSql(qwe1);
                        string qwe7 = "insert into dbo.operate_log values('" + user + "','" + DateTime.Now.ToString() + "','修改密码','')";
                        if (db.ExecuteSql(qwe7))
                        {
                            MessageBox.Show("修改成功");
                        }
                    }
                }
                else 
                {
                    MessageBox.Show("两次输入密码不一致！");
                }
            }
            else
                MessageBox.Show("不存在该用户，请核实");

        }
    }
}
