using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using dbclasszzh;

namespace ZigBee.Client
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        Form1 f1;
        DBClass db = new DBClass();
        bool formMove = false;//窗体是否移动
        Point formPoint;//记录窗体的位置
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formPoint = new Point();
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - /*SystemInformation.CaptionHeight -*/ SystemInformation.FrameBorderSize.Height;
                formPoint = new Point(xOffset, yOffset);
                formMove = true;//开始移动
            }
        }
          private void login_MouseDown(object sender, MouseEventArgs e)
        {
             formPoint = new Point();
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - /*SystemInformation.CaptionHeight -*/ SystemInformation.FrameBorderSize.Height;
                formPoint = new Point(xOffset, yOffset);
                formMove = true;//开始移动
            }
        }
          private void panel1_MouseMove(object sender, MouseEventArgs e)
          {
              if (formMove == true)
              {
                  Point mousePos = Control.MousePosition;
                  mousePos.Offset(formPoint.X, formPoint.Y);
                  Location = mousePos;
              }
          }
          private void login_MouseMove(object sender, MouseEventArgs e)
          {
              if (formMove == true)
              {
                  Point mousePos = Control.MousePosition;
                  mousePos.Offset(formPoint.X, formPoint.Y);
                  Location = mousePos;
              }
          }
          private void login_MouseUp(object sender, MouseEventArgs e)
          {
              if (e.Button == MouseButtons.Left)//按下的是鼠标左键
              {
                  formMove = false;//停止移动
              }
          }
          private void panel1_MouseUp(object sender, MouseEventArgs e)
          {
              if (e.Button == MouseButtons.Left)//按下的是鼠标左键
              {
                  formMove = false;//停止移动
              }
          }

        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 10);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角  
            path.AddArc(arcRect, 180, 90);

            // 右上角  
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角  
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角  
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线  
            return path;
        }

        private void login_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                textBox1.Text = "";//清空用户名
                textBox2.Text = "";//清空密码
                MessageBox.Show("用户名和密码不能为空！");
                return;
            }
            var result = await SignalR.SUser.Login(textBox1.Text, textBox2.Text);
            if (result)
            {
                label1.Visible = false;
                label2.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button1.Visible = false;
                label3.Visible = true;
                pictureBox1.Visible = true;
                timer1.Enabled = true;
                return;
            }
            else
            {
                MessageBox.Show("用户名或密码错误，请重新输入！");
                return;
            }

            //string stre = "select * from dbo.admin1 where users= '"+textBox1.Text+"'and password ='"+textBox2.Text+"'" ;//比对数据库用户赎数据
            //if (textBox1.Text != "" && textBox2.Text != "")
            //{
            //    DataSet ds = db.GetDataSet(stre);
            //    if (ds.Tables[0].Rows.Count == 1)
            //    {
            //        string qwe7 = "insert into dbo.operate_log values('" + textBox1.Text + "','" + DateTime.Now.ToString() + "','登录','')";
            //        db.ExecuteSql(qwe7);
            //        label1.Visible = false;
            //        label2.Visible = false;
            //        textBox1.Visible = false;
            //        textBox2.Visible = false;
            //        button1.Visible = false;
            //        label3.Visible = true;
            //        pictureBox1.Visible = true;
            //        timer1.Enabled = true;
            //    }
            //    else
            //        MessageBox.Show("用户名或密码错误，请重新输入！");
            //}
            //else
            //{
            //    textBox1.Text = "";//清空用户名
            //    textBox2.Text = "";//清空密码
            //    MessageBox.Show("用户名和密码不能为空！");
            //}
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Red;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = SystemColors.ButtonHighlight;
        }
       
        private void label5_Click(object sender, EventArgs e)
        {
             Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            f1 = new Form1(textBox1.Text);
            f1.Visible = false;
            f1.Show();
            timer2.Enabled = true;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Text = f1.formload;

        }
    }
}
