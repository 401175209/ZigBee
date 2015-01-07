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
    public partial class file : Form
    {
        DBClass db = new DBClass();
        string stre;
        string str7;
        public file()
        {
            InitializeComponent();
        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
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

        private void file_Load(object sender, EventArgs e)
        {
            stre = "select * from dbo.Alarm where FLAG='Yes'";//记录报警数据
            DataSet ds = db.GetDataSet(stre);
            //label5.Text = ds.Tables[0].Rows.Count.ToString();
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
               
               richTextBox1.Text +="地点："+ds.Tables[0].Rows[i][7].ToString()+" 报警信号: "+ds.Tables[0].Rows[i][4].ToString()+" 设备类型: "+ds.Tables[0].Rows[i][3].ToString()+" 报警时间: "+ds.Tables[0].Rows[i][5].ToString()+ Environment.NewLine;              
            }

            str7 = "select * from dbo.operate_log";////记录各种操作
            DataSet ds1 = db.GetDataSet(str7);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
               
                richTextBox2.Text += "操作人：" + ds1.Tables[0].Rows[i][1].ToString() + " 时间: " + ds1.Tables[0].Rows[i][2].ToString() + " 操作类型: " + ds1.Tables[0].Rows[i][3].ToString() + " 详细情况: " + ds1.Tables[0].Rows[i][4].ToString() + Environment.NewLine;
            }
        }
    }
}
