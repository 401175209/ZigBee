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
    public partial class deviceManage : Form
    {
        DBClass db = new DBClass();
        string users;
        public deviceManage(string user)
        {
            InitializeComponent();
            users = user;

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
        private void deviceManage_Load(object sender, EventArgs e)
        {
            string stre = "select * from dbo.LunXunRiZhi where LX='RFD'";//显示节点
            string stre1 = "select * from dbo.LunXunRiZhi where LX='ROU'";//显示路由
            try
            {
                DataSet ds = db.GetDataSet(stre);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["ROOM"].HeaderText = "房间号";
                dataGridView1.Columns["state"].Visible = false;
                dataGridView1.Columns["DZ"].HeaderText = "设备序列号";
                dataGridView1.Columns["RSSI"].Visible = false;
                dataGridView1.Columns["DL"].Visible = false;
                dataGridView1.Columns["AL"].Visible = false;
                dataGridView1.Columns["WD"].Visible = false;
                dataGridView1.Columns["SD"].Visible = false; ;
                dataGridView1.Columns["floor"].HeaderText = "楼层";
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["WLDZ"].Visible = false;
                dataGridView1.Columns["FJDDZ"].Visible = false;
                dataGridView1.Columns["ONTIME"].Visible = false;
                dataGridView1.Columns["LX"].Visible = false;
                DataSet ds1 = db.GetDataSet(stre1);
                dataGridView2.DataSource = ds1.Tables[0];
                dataGridView2.Columns["ROOM"].HeaderText = "路由号";
                dataGridView2.Columns["state"].Visible = false;
                dataGridView2.Columns["DL"].Visible = false;
                dataGridView2.Columns["DZ"].HeaderText = "设备序列号";
                dataGridView2.Columns["RSSI"].Visible = false;
                dataGridView2.Columns["floor"].HeaderText = "楼层";
                dataGridView2.Columns["ID"].Visible = false;
                dataGridView2.Columns["WLDZ"].Visible = false;
                dataGridView2.Columns["FJDDZ"].Visible = false;
                dataGridView2.Columns["ONTIME"].Visible = false;
                dataGridView2.Columns["LX"].Visible = false;
                dataGridView2.Columns["WD"].Visible = false;
                dataGridView2.Columns["SD"].Visible = false;
                dataGridView2.Columns["AL"].Visible = false;
                
            }
            catch { }
        }
        string dz;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           textBox1.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();//房间名称
           textBox2.Text= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//序列号
           dz = textBox2.Text;
           textBox7.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();//设备类型
           textBox3.Text = dataGridView1.SelectedRows[0].Cells[13].Value.ToString();//所属楼层
        }
        DialogResult r;
        private void button1_Click(object sender, EventArgs e)
        {
            r = MessageBox.Show("确定修改？", "郑重提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.OK)
            {
                string qwe1 = "UPDATE dbo.LunXunRiZhi SET DZ = '" + textBox2.Text +  "', ROOM='" + textBox1.Text + "',floor = '" +textBox3.Text +  "' WHERE DZ = '" + dz + "'";
                db.ExecuteSql(qwe1);
                string qwe7 = "insert into dbo.operate_log values('" +users+ "','" + DateTime.Now.ToString() + "','编辑修改','"+qwe1+"')";
                db.ExecuteSql(qwe7);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            r = MessageBox.Show("确定删除该设备？", "郑重提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.OK)
            {
                string qwe5 = "Delete from dbo.LunXunRiZhi where DZ='" + textBox2.Text+"'";
                db.ExecuteSql(qwe5);
                string qwe6 = "insert into dbo.operate_log values('" + users + "','" + DateTime.Now.ToString() + "','删除','" + qwe5 + "')";
                db.ExecuteSql(qwe6);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            r = MessageBox.Show("确定添加该设备？", "郑重提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.OK)
            {
                string qwe9 = "insert into dbo.LunXunRiZhi values('" + textBox5.Text + "','1234','5678','"+comboBox1.Text+"','0','23','56','2','34','" + DateTime.Now + "','"+textBox6.Text+"','on','"+textBox4.Text+"')";                               
                db.ExecuteSql(qwe9);
                string qwe4 = "insert into dbo.operate_log values('" + users + "','" + DateTime.Now.ToString() + "','新增设备','" + qwe9 + "')";
                db.ExecuteSql(qwe4);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.SelectedRows[0].Cells[11].Value.ToString();//房间名称
            textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();//序列号
            dz = textBox2.Text;
            textBox7.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();//设备类型
            textBox3.Text = dataGridView2.SelectedRows[0].Cells[13].Value.ToString();//所属楼层
        }
    }
}
