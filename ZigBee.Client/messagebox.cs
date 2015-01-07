using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using dbclasszzh;
using WindowsApplication1;
using System.Media;
using System.Threading;

namespace ZigBee.Client
{
    public partial class messagebox : Form
    {
        
       
        DBClass db = new DBClass();
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_HOR_POSITIVE = 0x0001;
        const int AW_HOR_NEGATIVE = 0x0002;
        const int AW_VER_POSITIVE = 0x0004;
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_CENTER = 0x0010;
        const int AW_HIDE = 0x10000;
        const int AW_ACTIVATE = 0x20000;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;
        string Room;
        string Type;
        bool start = true;
        public messagebox(string room,string type)
        { 
            Thread Play = new Thread(new ThreadStart(Non));
            Play.IsBackground = true;
            Play.Start(); 
            InitializeComponent();
            Room = room;
            Type = type;
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
           
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height;
            this.SetDesktopLocation(x, y);

           
        }
       
        public void Non()
        {
            while (start)
            {
                SoundPlayer play = new SoundPlayer(Properties.Resources.ALARM1 );
                play.Play();
                Thread.Sleep(2000);
            }
            Thread.CurrentThread.Abort();
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
        private void messagebox_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            start = false;
            this.Close();

        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = SystemColors.ButtonHighlight;
        }

        private void messagebox_Load(object sender, EventArgs e)
        {
            label3.Text = Room + "有" + Type; 
            label4.Text="报警时间：" + DateTime.Now.ToString(); ;
            switch (Type)
            {
                case "火警":
                    pictureBox1.Image= Image.FromFile(Application.StartupPath + "\\image\\f.gif");
                    break;
                case "人警":
                    pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\image\\p.jpg");
                    break;
                case "综合报警":
                    pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\image\\c.jpg");
                    break;
            }
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string qwe1 = "UPDATE dbo.Alarm SET FLAG = 'Yes' where ROOM='" + Room + "'";
               
                if (db.ExecuteSql(qwe1))
                {
                    warning.status = "报警已处理";
                    start = false;
                    this.Close();
                }
            }
            catch { }
        }
       
    }
}
