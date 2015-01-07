using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ZigBee.Client
{ 
    public partial class setting : Form
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        static string FileName = Application.StartupPath + "\\Config.ini";
        //读取INI文件指定

        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
        //写INI文件
        public void WriteIni(string Section, string Ident, string Value)
        {
            if (!WritePrivateProfileString(Section, Ident, Value, FileName))
            {

                throw (new ApplicationException("写入配置文件出错"));
            }

        }
        public setting()
        {
            InitializeComponent();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.ButtonHighlight;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("确定修改？", "郑重提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.OK)
            {
               WriteIni("FWIP", "FWIP", textBox1.Text);
               WriteIni("NAME", "NAME", textBox2.Text);
               WriteIni("YH", "YH", textBox3.Text);
               WriteIni("MM", "MM", textBox4.Text);
                this.Close();
            }
        }
        private void setting_Load_1(object sender, EventArgs e)
        {
            textBox1.Text = ReadIni("FWIP", "FWIP", "");
            textBox2.Text = ReadIni("NAME", "NAME", "");
            textBox3.Text = ReadIni("YH", "YH", "");
            textBox4.Text = ReadIni("MM", "MM", "");
        }
    }
}
