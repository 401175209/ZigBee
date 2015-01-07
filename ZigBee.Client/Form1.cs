using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using dbclasszzh;
using WindowsApplication1;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Room;
using WindowsFormsApplication1;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Media;

namespace ZigBee.Client
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)] 
    public partial class Form1 : Form
    {
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
        public Form1(string user)
        {
            InitializeComponent();
            label2.Text = user;
          // tabPage3.Parent = null;
            Rou = Image.FromFile(Application.StartupPath + "\\image\\NodeRouter.bmp");
            Rfd = Image.FromFile(Application.StartupPath + "\\image\\NodeEnd.bmp");
            AccessPoint = Image.FromFile(Application.StartupPath + "\\image\\AccessPoint.bmp");
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            to.bitmap(pictureBox1.Width, pictureBox1.Height);
        }
#region 变量定义部分
       
        Class1 cl1 = new Class1();
        DBClass db = new DBClass();
        public static DataTable dt;           //实时监测数据
        public string formload ="等待加载。。。"; 
        public string result;
        string[,] Node = null;                //网络节点字符串数组
        TOPO to = new TOPO();
        public static Image Rou = null;       //节点显示路由图片
        public static Image Rfd = null;       //节点显示终端图片
        public static Image AccessPoint = null;  //节点显示网关图片
        public static bool start = false;
#endregion
#region 数据初始化部分
        AutoSizeFormClass asc = new AutoSizeFormClass();
        TreeNode myTreeNode0 = new TreeNode("节点设备");
        TreeNode submyTreeNode0 = new TreeNode("地下");
        TreeNode submyTreeNode1 = new TreeNode("1楼");
        TreeNode submyTreeNode2 = new TreeNode("2楼");
        TreeNode submyTreeNode3 = new TreeNode("3楼");
        TreeNode submyTreeNode4 = new TreeNode("4楼");
        TreeNode submyTreeNode5 = new TreeNode("5楼");
        TreeNode submyTreeNode6 = new TreeNode("6楼");
        TreeNode submyTreeNode7 = new TreeNode("7楼");
        TreeNode submyTreeNode8 = new TreeNode("8楼");
        TreeNode submyTreeNode9 = new TreeNode("9楼");
        TreeNode submyTreeNode10 = new TreeNode("10楼");
        TreeNode submyTreeNode11 = new TreeNode("11楼");
        TreeNode submyTreeNode12 = new TreeNode("12楼");
      
        private void Form1_Load(object sender, EventArgs e)
        {
           
            asc.controllInitializeSize(this);

            string str_url = Application.StartupPath + "\\1.html";
            Uri url = new Uri(str_url);
            webBrowser1.Url = url;
            webBrowser1.ObjectForScripting = this;  

            label9.Text = DateTime.Now.ToString();
            formload = "数据读取中。。。";
            string stre = "select * from dbo.LunXunRiZhi where LX='RFD'";//显示节点
            string stre1 = "select * from dbo.LunXunRiZhi where LX='ROU'";//显示路由
            try
            {
                DataSet ds = db.GetDataSet(stre);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["ROOM"].HeaderText = "房间号";
                dataGridView1.Columns["state"].HeaderText = "状态";
                dataGridView1.Columns["DZ"].HeaderText = "设备序列号";
                dataGridView1.Columns["RSSI"].HeaderText = "信号";
                dataGridView1.Columns["DL"].HeaderText = "电量";
                dataGridView1.Columns["AL"].HeaderText = "报警信号";
                dataGridView1.Columns["WD"].HeaderText = "温度";
                dataGridView1.Columns["SD"].HeaderText = "湿度";
                dataGridView1.Columns["floor"].HeaderText = "楼层";
                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["WLDZ"].Visible = false;
                dataGridView1.Columns["FJDDZ"].Visible = false;
                dataGridView1.Columns["ONTIME"].Visible = false;
                dataGridView1.Columns["LX"].Visible = false;
                DataSet ds1 = db.GetDataSet(stre1);
                dataGridView2.DataSource = ds1.Tables[0];
                dataGridView2.Columns["ROOM"].HeaderText = "路由号";
                dataGridView2.Columns["state"].HeaderText = "状态";
                dataGridView2.Columns["DL"].HeaderText = "电量";
                dataGridView2.Columns["DZ"].HeaderText = "设备序列号";
                dataGridView2.Columns["RSSI"].HeaderText = "信号";
                dataGridView2.Columns["floor"].HeaderText = "楼层";
                dataGridView2.Columns["ID"].Visible = false;
                dataGridView2.Columns["WLDZ"].Visible = false;
                dataGridView2.Columns["FJDDZ"].Visible = false;
                dataGridView2.Columns["ONTIME"].Visible = false;
                dataGridView2.Columns["LX"].Visible = false;
                dataGridView2.Columns["WD"].Visible = false;
                dataGridView2.Columns["SD"].Visible = false;
                dataGridView2.Columns["AL"].Visible = false;
                formload = "读取数据完成";
            }
            catch { }
            formload = "绑定树形数据中。。。";
            treeView1.Nodes.Add(myTreeNode0);
            myTreeNode0.Nodes.Add(submyTreeNode0);
            myTreeNode0.Nodes.Add(submyTreeNode1);
            myTreeNode0.Nodes.Add(submyTreeNode2);
            myTreeNode0.Nodes.Add(submyTreeNode3);
            myTreeNode0.Nodes.Add(submyTreeNode4);
            myTreeNode0.Nodes.Add(submyTreeNode5);
            myTreeNode0.Nodes.Add(submyTreeNode6);
            myTreeNode0.Nodes.Add(submyTreeNode7);
            myTreeNode0.Nodes.Add(submyTreeNode8);
            myTreeNode0.Nodes.Add(submyTreeNode9);
            myTreeNode0.Nodes.Add(submyTreeNode10);
            myTreeNode0.Nodes.Add(submyTreeNode11);
            myTreeNode0.Nodes.Add(submyTreeNode12); 
            formload = "形数据加载完毕";
            CreateChildTree(); 
            treeView1.ExpandAll();
            formload = "加载楼层图和拓扑图。。。";
            bdbjt();
            formload = "所有数据加载完毕";
           
        }
        TreeNode addmyTreeNode;
        public void CreateChildTree()
        {
           
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                int louceng = Convert.ToInt32(dgr.Cells[13].Value);
                addmyTreeNode = new TreeNode(dgr.Cells[11].Value.ToString());
                addmyTreeNode.ForeColor = Color.Black;
                switch (louceng)
                {
                    case 0:  submyTreeNode0.Nodes.Add(addmyTreeNode);break;
                    case 1:  submyTreeNode1.Nodes.Add(addmyTreeNode); break;
                    case 2:  submyTreeNode2.Nodes.Add(addmyTreeNode); break;
                    case 3:  submyTreeNode3.Nodes.Add(addmyTreeNode); break;
                    case 4:  submyTreeNode4.Nodes.Add(addmyTreeNode); break;
                    case 5:  submyTreeNode5.Nodes.Add(addmyTreeNode); break;
                    case 6:  submyTreeNode6.Nodes.Add(addmyTreeNode); break;
                    case 7:  submyTreeNode7.Nodes.Add(addmyTreeNode); break;
                    case 8:  submyTreeNode8.Nodes.Add(addmyTreeNode); break;
                    case 9:  submyTreeNode9.Nodes.Add(addmyTreeNode); break;
                    case 10: submyTreeNode10.Nodes.Add(addmyTreeNode); break;
                    case 11: submyTreeNode11.Nodes.Add(addmyTreeNode); break;
                    case 12: submyTreeNode12.Nodes.Add(addmyTreeNode); break;
                }
            }
        }
#endregion
#region 绑定数据到布局图里面，每层楼一个
        Roomm[,] rom = new Roomm[13, 50];
        public void bdbjt()
        {
            lc0.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
            lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();

            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button1")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }

            dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Ascending);//排序使用
            
           int i = 0;
           int next = 0;
            int num = 0, y = 12;
            int lcs = 0, lcs1 = 0;

            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            { 
                lcs = Convert.ToInt32(dgr.Cells[13].Value) ;
                if (lcs1 != lcs)
                {
                    i = 0;
                   next=0;
                    num = 0;
                    y = 12;
                    lcs1 = lcs;
                }
              /* rom[0, i].RoomNum = dgr.Cells[11].Value.ToString(); //房间名
               * rom[0, i].Name = dgr.Cells[1].Value.ToString();//设备序列号 
               * rom[0, i].State = dgr.Cells[12].Value.ToString();//设备状态
               * rom[0, i].WenDu = dgr.Cells[6].Value.ToString(); //温度
               * rom[0, i].ShiDu = dgr.Cells[7].Value.ToString(); //湿度
               * rom[0, i].Warning = dgr.Cells[5].Value.ToString(); //报警信号
               * rom[0, i].EN = true; //时能
               * rom[0, i].DianLiang = dgr.Cells[8].Value.ToString(); //电量
               * rom[0, i].Ress = dgr.Cells[9].Value.ToString(); //信号强度
               */         
                switch (lcs)
                {
                    case 0:
                        
                        rom[0, i] = new Roomm();
                        rom[0,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[0,i].RoomID  = dgr.Cells[1].Value.ToString();rom[0,i].State=dgr.Cells[12].Value.ToString();
                        rom[0,i].WenDu = dgr.Cells[6].Value.ToString(); rom[0,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[0,i].Warning = dgr.Cells[5].Value.ToString(); rom[0,i].EN = true;rom[0,i].DianLiang=dgr.Cells[8].Value.ToString();rom[0,i].Ress=dgr.Cells[9].Value.ToString(); rom[0,i].Width = 150; rom[0,i].Height = 100; num = next* 155+30;
                        if (num >= 1000) { next= 0; y += 105; num =30; } rom[0,i].Location = new System.Drawing.Point(num, y);
                        lc0.Controls.Add(rom[0,i]);i++;next++;
                        break;
                    case 1:
                        
                        rom[1, i] = new Roomm();
                        rom[1,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[1,i].RoomID = dgr.Cells[1].Value.ToString();rom[1,i].State=dgr.Cells[12].Value.ToString();
                        rom[1,i].WenDu = dgr.Cells[6].Value.ToString(); rom[1,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[1,i].Warning = dgr.Cells[5].Value.ToString(); rom[1,i].EN = true;rom[1,i].DianLiang=dgr.Cells[8].Value.ToString();rom[1,i].Ress=dgr.Cells[9].Value.ToString(); rom[1,i].Width = 150; rom[1,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[1,i].Location = new System.Drawing.Point(num, y);
                        lc1.Controls.Add(rom[1,i]);i++;next++;
                        break;
                    case 2:
                       
                       rom[2, i] = new Roomm();
                        rom[2,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[2,i].RoomID= dgr.Cells[1].Value.ToString();rom[2,i].State=dgr.Cells[12].Value.ToString();
                        rom[2,i].WenDu = dgr.Cells[6].Value.ToString(); rom[2,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[2,i].Warning = dgr.Cells[5].Value.ToString(); rom[2,i].EN = true;rom[2,i].DianLiang=dgr.Cells[8].Value.ToString();rom[2,i].Ress=dgr.Cells[9].Value.ToString(); rom[2,i].Width = 150; rom[2,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[2,i].Location = new System.Drawing.Point(num, y);
                        lc2.Controls.Add(rom[2,i]);i++;next++;
                        break;
                    case 3:
                        
                        rom[3, i] = new Roomm();
                        rom[3,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[3,i].RoomID = dgr.Cells[1].Value.ToString();rom[3,i].State=dgr.Cells[12].Value.ToString();
                        rom[3,i].WenDu = dgr.Cells[6].Value.ToString(); rom[3,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[3,i].Warning = dgr.Cells[5].Value.ToString(); rom[3,i].EN = true;rom[3,i].DianLiang=dgr.Cells[8].Value.ToString();rom[3,i].Ress=dgr.Cells[9].Value.ToString(); rom[3,i].Width = 150; rom[3,i].Height = 100; num =next * 155+30;
                        if (num >= 1500) {next = 0; y += 105; num =30; } rom[3,i].Location = new System.Drawing.Point(num, y);
                        lc3.Controls.Add(rom[3,i]); i++;next++;
                       
                        break;
                    case 4:
                      
                        rom[4, i] = new Roomm();
                       rom[4,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[4,i].RoomID = dgr.Cells[1].Value.ToString();rom[4,i].State=dgr.Cells[12].Value.ToString();
                        rom[4,i].WenDu = dgr.Cells[6].Value.ToString(); rom[4,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[4,i].Warning = dgr.Cells[5].Value.ToString(); rom[4,i].EN = true;rom[4,i].DianLiang=dgr.Cells[8].Value.ToString();rom[4,i].Ress=dgr.Cells[9].Value.ToString(); rom[4,i].Width = 150; rom[4,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[4,i].Location = new System.Drawing.Point(num, y);
                        lc4.Controls.Add(rom[4,i]);  i++;next++;
                        break;
                    case 5:
                       
                        rom[5, i] = new Roomm();
                        rom[5,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[5,i].RoomID = dgr.Cells[1].Value.ToString();rom[5,i].State=dgr.Cells[12].Value.ToString();
                        rom[5,i].WenDu = dgr.Cells[6].Value.ToString(); rom[5,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[5,i].Warning = dgr.Cells[5].Value.ToString(); rom[5,i].EN = true;rom[5,i].DianLiang=dgr.Cells[8].Value.ToString();rom[5,i].Ress=dgr.Cells[9].Value.ToString(); rom[5,i].Width = 150; rom[5,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[5,i].Location = new System.Drawing.Point(num, y);
                        lc5.Controls.Add(rom[5, i]); i++;next++;
                        break;
                    case 6:
                       
                        rom[6, i] = new Roomm();
                        rom[6,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[6,i].RoomID = dgr.Cells[1].Value.ToString();rom[6,i].State=dgr.Cells[12].Value.ToString();
                        rom[6,i].WenDu = dgr.Cells[6].Value.ToString(); rom[6,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[6,i].Warning = dgr.Cells[5].Value.ToString(); rom[6,i].EN = true;rom[6,i].DianLiang=dgr.Cells[8].Value.ToString();rom[6,i].Ress=dgr.Cells[9].Value.ToString(); rom[6,i].Width = 150; rom[6,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[6,i].Location = new System.Drawing.Point(num, y);
                       
                        lc6.Controls.Add(rom[6,i]); i++;next++;
                        break;
                    case 7:
                       
                        rom[7, i] = new Roomm();
                         rom[7,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[7,i].RoomID = dgr.Cells[1].Value.ToString();rom[7,i].State=dgr.Cells[12].Value.ToString();
                        rom[7,i].WenDu = dgr.Cells[6].Value.ToString(); rom[7,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[7,i].Warning = dgr.Cells[5].Value.ToString(); rom[7,i].EN = true;rom[7,i].DianLiang=dgr.Cells[8].Value.ToString();rom[7,i].Ress=dgr.Cells[9].Value.ToString(); rom[7,i].Width = 150; rom[7,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[7,i].Location = new System.Drawing.Point(num, y);
                        lc7.Controls.Add(rom[7,i]); i++;next++;
                        break;
                    case 8:
                       
                        rom[8, i] = new Roomm();
                        rom[8,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[8,i].RoomID = dgr.Cells[1].Value.ToString();rom[8,i].State=dgr.Cells[12].Value.ToString();
                        rom[8,i].WenDu = dgr.Cells[6].Value.ToString(); rom[8,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[8,i].Warning = dgr.Cells[5].Value.ToString(); rom[8,i].EN = true;rom[8,i].DianLiang=dgr.Cells[8].Value.ToString();rom[8,i].Ress=dgr.Cells[9].Value.ToString(); rom[8,i].Width = 150; rom[8,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[8,i].Location = new System.Drawing.Point(num, y);
                        lc8.Controls.Add(rom[8,i]); i++;next++;
                        break;
                    case 9:
                       
                        rom[9, i] = new Roomm();
                        rom[9,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[9,i].RoomID = dgr.Cells[1].Value.ToString();rom[9,i].State=dgr.Cells[12].Value.ToString();
                        rom[9,i].WenDu = dgr.Cells[6].Value.ToString(); rom[9,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[9,i].Warning = dgr.Cells[5].Value.ToString(); rom[9,i].EN = true;rom[9,i].DianLiang=dgr.Cells[8].Value.ToString();rom[9,i].Ress=dgr.Cells[9].Value.ToString(); rom[9,i].Width = 150; rom[9,i].Height = 100; num =next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[9,i].Location = new System.Drawing.Point(num, y);
                        lc9.Controls.Add(rom[9,i]);start = true; i++;next++;
                        break;
                   case 10:
                       
                       rom[10, i] = new Roomm();
                        rom[10,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[10,i].RoomID = dgr.Cells[1].Value.ToString();rom[10,i].State=dgr.Cells[12].Value.ToString();
                        rom[10,i].WenDu = dgr.Cells[6].Value.ToString(); rom[10,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[10,i].Warning = dgr.Cells[5].Value.ToString(); rom[10,i].EN = true;rom[10,i].DianLiang=dgr.Cells[8].Value.ToString();rom[10,i].Ress=dgr.Cells[9].Value.ToString(); rom[10,i].Width = 150; rom[10,i].Height = 100; num = next * 155+30;
                        if (num >= 1000) { next= 0; y += 105; num =30; } rom[10,i].Location = new System.Drawing.Point(num, y);
                        lc10.Controls.Add(rom[10,i]); i++;next++;
                        break;
                    case 11:
                        
                       rom[11, i] = new Roomm();
                        rom[11,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[11,i].RoomID = dgr.Cells[1].Value.ToString();rom[11,i].State=dgr.Cells[12].Value.ToString();
                        rom[11,i].WenDu = dgr.Cells[6].Value.ToString(); rom[11,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[11,i].Warning = dgr.Cells[5].Value.ToString(); rom[11,i].EN = true;rom[11,i].DianLiang=dgr.Cells[8].Value.ToString();rom[11,i].Ress=dgr.Cells[9].Value.ToString(); rom[11,i].Width = 150; rom[11,i].Height = 100; num =next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[11,i].Location = new System.Drawing.Point(num, y);
                        lc11.Controls.Add(rom[11,i]);i++;next++;
                        break;
                    case 12:
                       
                      rom[12, i] = new Roomm();
                        rom[12,i].RoomNum = dgr.Cells[11].Value.ToString(); rom[9,i].RoomID = dgr.Cells[1].Value.ToString();rom[9,i].State=dgr.Cells[12].Value.ToString();
                        rom[12,i].WenDu = dgr.Cells[6].Value.ToString(); rom[9,i].ShiDu = dgr.Cells[7].Value.ToString(); rom[9,i].Warning = dgr.Cells[5].Value.ToString(); rom[9,i].EN = true;rom[9,i].DianLiang=dgr.Cells[8].Value.ToString();rom[9,i].Ress=dgr.Cells[9].Value.ToString(); rom[9,i].Width = 150; rom[9,i].Height = 100; num =next * 155+30;
                        if (num >= 1000) { next = 0; y += 105; num =30; } rom[9,i].Location = new System.Drawing.Point(num, y);
                        lc12.Controls.Add(rom[12,i]); i++;next++;

                        start = true;
                        break;
                }
            }
        }
#endregion       
#region 读取数据库轮询数据//掉线
        DataSet ds;
        private void timer3_Tick(object sender, EventArgs e)//定时读取数据库
        {
            string stre = "select * from dbo.LunXunRiZhi";//查找
            ds = db.GetDataSet(stre);
            label7.Text =dataGridView1.RowCount.ToString();//节点数量
            label8.Text = dataGridView2.RowCount.ToString();//路由数量
            if (ds.Tables[0].Rows.Count>0)
            {
                //绘制拓扑图
                try
                {
                    Node = new string[ds.Tables[0].Rows.Count, 9];                                                                  
                    to.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       // Node[i, 0] = ds.Tables[0].Rows[i][1].ToString(); //物理地址
                        Node[i, 0] = ds.Tables[0].Rows[i][2].ToString(); //网络地址
                        Node[i, 1] = ds.Tables[0].Rows[i][3].ToString(); //父网络地址 
                        Node[i, 2] = ds.Tables[0].Rows[i][4].ToString(); //类型
                        Node[i, 3] = ds.Tables[0].Rows[i][5].ToString(); //报警信号
                       // Node[i, 5] = ds.Tables[0].Rows[i][6].ToString(); //温度
                        //Node[i, 6] = ds.Tables[0].Rows[i][7].ToString(); //湿度
                        Node[i, 4] = ds.Tables[0].Rows[i][8].ToString(); //电量
                        Node[i, 5] = ds.Tables[0].Rows[i][9].ToString(); //信号强度
                        Node[i, 6] = ds.Tables[0].Rows[i][10].ToString(); //时间
                        Node[i, 7] = ds.Tables[0].Rows[i][11].ToString(); //房间号  
                        Node[i, 8] = ds.Tables[0].Rows[i][12].ToString(); //在线状态
                        if (Node[i, 8] == "off")
                        {
                            textBox1.ForeColor = Color.White;
                            textBox1.Text = ds.Tables[0].Rows[i][11].ToString() + "：掉线";
                            textBox1.BackColor = Color.DarkGray;
                            label9.BackColor = Color.DarkGray;
                            label9.Text = DateTime.Now.ToString();
                            textBox2.Text += textBox1.Text + "  " + label9.Text + Environment.NewLine;
                        }
                     } 
                    to.Node(Node, Rou, Rfd, AccessPoint, 30, 30);
                    to.Nodeline(Node);
                    pictureBox1.Refresh();
                    pictureBox1.Image = to.GetBitMap();
                  }
                catch { }
            }
        }
#endregion
#region 读取数据库报警信息 

        private void timer5_Tick(object sender, EventArgs e)
        {
            
            try
            {
                string warning="";
                string leixing = "";
                string fjh = "";
                string valu="";
                string stre = "select * from dbo.Alarm where FLAG='No'";//查找
                DataSet ds = db.GetDataSet(stre);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                        label17.Text = "有报警信号";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            switch (ds.Tables[0].Rows[i][4].ToString())
                            {
                                case "1": valu = "火情异常";
                                    textBox1.ForeColor = Color.White;
                                    textBox1.Text = ds.Tables[0].Rows[i][7].ToString() + "：火警";
                                    textBox1.BackColor = Color.Red;
                                    label9.BackColor = Color.Red;
                                    label9.Text = DateTime.Now.ToString();
                                    textBox2.Text += textBox1.Text + "  " + label9.Text + Environment.NewLine;
                                    leixing = "火警";

                                    break;
                                case "2": valu = "可疑人员";
                                    textBox1.ForeColor = Color.Black;
                                    textBox1.Text = ds.Tables[0].Rows[i][7].ToString() + "：人警";
                                    textBox1.BackColor = Color.Yellow;
                                    label9.BackColor = Color.Yellow;
                                    label9.Text = DateTime.Now.ToString();
                                    textBox2.Text += textBox1.Text + "  " + label9.Text + Environment.NewLine;
                                    leixing = "人警";

                                    break;
                                case "3": valu = "综合报警";
                                    textBox1.ForeColor = Color.White;
                                    textBox1.Text = ds.Tables[0].Rows[i][7].ToString() + "：综合";
                                    textBox1.BackColor = Color.Black;
                                    label9.BackColor = Color.Black;
                                    label9.Text = DateTime.Now.ToString();
                                    textBox2.Text += textBox1.Text + "  " + label9.Text + Environment.NewLine;
                                    leixing = "综合报警";

                                    break;
                            }
                            warning = ds.Tables[0].Rows[i][7].ToString() + "房间有" + valu;
                            fjh = ds.Tables[0].Rows[i][7].ToString();
                        }
                    

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        timer5.Enabled = false;

                        messagebox mb = new messagebox(fjh, leixing);
                        mb.Show();

                    }
                }
               
            }
            catch { }
        }
#endregion 
#region 拓扑图绑定部分
        /****************************************************************************************/
//拓扑图数据绑定
        public void bind()
        {
            to.Clear();
            to.Node(Node, Rou, Rfd, AccessPoint, 30, 30);
            to.Nodeline(Node);
            pictureBox1.Refresh();
            pictureBox1.Image = to.GetBitMap();
        }

        /****************************************************************************************/
#endregion 
#region 拓扑图鼠标点击事件
        //pictureBox1拓扑图鼠标事件
       
        private void pictureBox1_SizeChanged_1(object sender, EventArgs e)
        {
            to.bitmap(pictureBox1.Width, pictureBox1.Height);
            bind();
        }
        bool ax = false;

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                to.mouseclick(e.X, e.Y);
                if (to.getchecked() != null)
                {
                    to.Clear();
                    bind();
                    ax = true;
                }
                else
                {
                    ax = false;
                    to.Clear();
                    bind();
                }
            }
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ax == true)
            {
                try
                {
                    to.Clear();
                    to.mousemove(e.X, e.Y);
                    bind();
                }
                catch { }
            }
        }

        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (ax == true)
            {
                try
                {
                    to.Clear();
                    to.mousemove(e.X, e.Y);
                    bind();
                }
                catch
                { }
            }
        }

#endregion
#region 退出事件
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
#endregion
#region 数据库列表刷新和拓扑刷新
      
        private void timer2_Tick(object sender, EventArgs e)
        {
            string stre = "select * from dbo.LunXunRiZhi where LX='RFD'";//显示整张表
            DataSet ds = db.GetDataSet(stre);
            dataGridView1.DataSource = ds.Tables[0];
            string stre1 = "select * from dbo.LunXunRiZhi where LX='ROU'";//显示整张表
            DataSet ds1 = db.GetDataSet(stre1);
            dataGridView2.DataSource = ds1.Tables[0];
        }
#endregion
#region 显示系统时间
        private void timer4_Tick(object sender, EventArgs e)
        {
            label3.Text = Convert.ToString(DateTime.Now);
            label17.Text = warning.status;
        }
#endregion
#region 切换楼层布局图楼层按键事件
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button1")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button2")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button3")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button6")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button5")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button4")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button12")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button11")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button10")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button9")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button8")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button7")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (Control c in tabPage6.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "button13")
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                        c.BackColor = Color.WhiteSmoke;
                }
            }
        }
#endregion
#region 楼层切换按键颜色变化事件
        private void button1_BackColorChanged(object sender, EventArgs e)
        {

            if (button1.BackColor == Color.Red)
            {
                lc0.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }
        private void button2_BackColorChanged(object sender, EventArgs e)
        {
            if (button2.BackColor == Color.Red)
            {
                lc1.Show(); lc0.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button3_BackColorChanged(object sender, EventArgs e)
        {
            if (button3.BackColor == Color.Red)
            {
                lc2.Show(); lc1.Hide(); lc0.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button6_BackColorChanged(object sender, EventArgs e)
        {
            if (button6.BackColor == Color.Red)
            {
                lc5.Show(); lc1.Hide(); lc2.Hide(); lc0.Hide(); lc4.Hide(); lc3.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button5_BackColorChanged(object sender, EventArgs e)
        {
            if (button5.BackColor == Color.Red)
            {
                lc4.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc0.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button4_BackColorChanged(object sender, EventArgs e)
        {
            if (button4.BackColor == Color.Red)
            {
                lc3.Show(); lc1.Hide(); lc2.Hide(); lc5.Hide(); lc4.Hide(); lc0.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button12_BackColorChanged(object sender, EventArgs e)
        {
            if (button12.BackColor == Color.Red)
            {
                lc11.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc0.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc6.Hide(); lc12.Hide();
            }
        }

        private void button11_BackColorChanged(object sender, EventArgs e)
        {
            if (button11.BackColor == Color.Red)
            {
                lc10.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc0.Hide(); lc8.Hide(); lc9.Hide(); lc7.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button10_BackColorChanged(object sender, EventArgs e)
        {
            if (button10.BackColor == Color.Red)
            {
                lc9.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc0.Hide(); lc8.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button9_BackColorChanged(object sender, EventArgs e)
        {
            if (button9.BackColor == Color.Red)
            {
                lc8.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc9.Hide(); lc0.Hide(); lc10.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button8_BackColorChanged(object sender, EventArgs e)
        {
            if (button8.BackColor == Color.Red)
            {
                lc7.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc10.Hide(); lc8.Hide(); lc9.Hide(); lc0.Hide(); lc11.Hide(); lc12.Hide();
            }
        }

        private void button7_BackColorChanged(object sender, EventArgs e)
        {
            if (button7.BackColor == Color.Red)
            {
                lc6.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc11.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc0.Hide(); lc12.Hide();
            }
        }
        private void button13_BackColorChanged(object sender, EventArgs e)
        {
            if (button13.BackColor == Color.Red)
            {
                lc12.Show(); lc1.Hide(); lc2.Hide(); lc3.Hide(); lc4.Hide(); lc5.Hide(); lc6.Hide();
                lc7.Hide(); lc8.Hide(); lc9.Hide(); lc10.Hide(); lc11.Hide(); lc0.Hide();
            }
        }
 #endregion
#region 屏幕自适应及退出按钮事件
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
       
        private void label11_MouseEnter(object sender, EventArgs e)
        { 
            label11.BackColor = Color.Red;
           
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            label11.BackColor = SystemColors.HotTrack;
        }
        DialogResult r;
        private void label11_Click(object sender, EventArgs e)
        {
            r = MessageBox.Show("确定关闭软件，这样就不能使用本系统进行检测了！！！", "郑重提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.OK)
            {
                timer2.Enabled = false;
                Application.Exit();
            }
           
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            label10.Text = tabControl2.SelectedTab.Text;
        }
#endregion
#region 定时更新楼层示意图数据
        private void timer6_Tick(object sender, EventArgs e)//定时更新楼层示意图数据
        {
            int i = -1;
            int lcs = 0, lcs1 = 0;
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
              lcs = Convert.ToInt32(dgr.Cells[13].Value);
                if (lcs1 != lcs)
                {
                    i = -1;
                    lcs1 = lcs;
                }
                switch (lcs)
                {
                    case 0:
                        i++;
                        rom[0, i].State = dgr.Cells[12].Value.ToString();
                        rom[0, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[0, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[0, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[0, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 1:
                        i++;
                        rom[1, i].State = dgr.Cells[12].Value.ToString();
                        rom[1, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[1, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[1, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[1, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 2:
                        i++;
                         rom[2, i].State = dgr.Cells[12].Value.ToString();
                        rom[2, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[2, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[2, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[2, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 3:
                        i++;
                          rom[3, i].State = dgr.Cells[12].Value.ToString();
                        rom[3, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[3, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[3, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[3, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 4:
                        i++;
                      rom[4, i].State = dgr.Cells[12].Value.ToString();
                        rom[4, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[4, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[4, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[4, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 5:
                        i++;
                         rom[5, i].State = dgr.Cells[12].Value.ToString();
                        rom[5, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[5, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[5, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[5, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 6:
                        i++;
                         rom[6, i].State = dgr.Cells[12].Value.ToString();
                        rom[6, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[6, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[6, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[6, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 7:
                        i++;
                         rom[7, i].State = dgr.Cells[12].Value.ToString();
                        rom[7, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[7, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[7, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[7, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 8:
                        i++;
                         rom[8, i].State = dgr.Cells[12].Value.ToString();
                        rom[8, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[8, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[8, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[8, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 9:
                        i++;
                          rom[9, i].State = dgr.Cells[12].Value.ToString();
                        rom[9, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[9, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[9, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[9, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 10:
                        i++;
                       rom[10, i].State = dgr.Cells[12].Value.ToString();
                        rom[10, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[10, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[10, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[10, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 11:
                        i++;
                        rom[11, i].State = dgr.Cells[12].Value.ToString();
                        rom[11, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[11, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[11, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[11, i].DianLiang = dgr.Cells[8].Value.ToString();
                        break;
                    case 12:
                        i++;
                        rom[12, i].State = dgr.Cells[12].Value.ToString();
                        rom[12, i].WenDu = dgr.Cells[6].Value.ToString(); 
                        rom[12, i].ShiDu = dgr.Cells[7].Value.ToString(); 
                        rom[12, i].Warning = dgr.Cells[5].Value.ToString();
                        rom[12, i].DianLiang = dgr.Cells[8].Value.ToString();
                        
                        break;
                }

            }

        }
#endregion
#region treeview单击事件处理
private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
{
    try
    {   
        if (treeView1.SelectedNode.Level > 1)
        {  
            string selectroom =treeView1.SelectedNode.Text;
            if (e.Node.Text.Equals(selectroom))
            {
                string changfloor=selectroom.Substring (0,2);
               // label1.Text = changfloor;
                switch(changfloor)
               {
                   case "地下": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button1")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "01":
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button2")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "02": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button3")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "03": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button4")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "04": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button5")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "05":
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button6")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "06":
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button7")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "07": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button8")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "08": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button9")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "09":
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button10")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "10": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button11")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "11": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button12")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "12": 
                       foreach (Control c in tabPage6.Controls)
                       {
                           if (c is Button)
                           {
                               if (c.Name == "button13")
                               {
                                   c.BackColor = Color.Red;
                               }
                               else
                                   c.BackColor = Color.WhiteSmoke;
                           }
                       } break;
                   case "测试": button6.BackColor = Color.Red; break;
                  
                }
            }
        }
    }
    catch { }
}
#endregion        
#region 自定义菜单栏事件
private void label12_MouseEnter(object sender, EventArgs e)
{
    label12.ForeColor = Color.Black;
}

private void label12_MouseLeave(object sender, EventArgs e)
{
    label12.ForeColor = SystemColors.ButtonHighlight;
}

private void label13_MouseEnter(object sender, EventArgs e)
{
    label13.ForeColor = Color.Black;
}

private void label13_MouseLeave(object sender, EventArgs e)
{
    label13.ForeColor = SystemColors.ButtonHighlight;
}

private void label14_MouseEnter(object sender, EventArgs e)
{
    label14.ForeColor = Color.Black;
}

private void label14_MouseLeave(object sender, EventArgs e)
{
    label14.ForeColor = SystemColors.ButtonHighlight;
}

private void label16_MouseEnter(object sender, EventArgs e)
{
    label16.ForeColor = Color.Black;
}

private void label16_MouseLeave(object sender, EventArgs e)
{
    label16.ForeColor = SystemColors.ButtonHighlight;
}

private void label15_MouseEnter(object sender, EventArgs e)
{
    label15.ForeColor = Color.Black;
}

private void label15_MouseLeave(object sender, EventArgs e)
{
    label15.ForeColor = SystemColors.ButtonHighlight;
}

private void label12_Click(object sender, EventArgs e)//关于
{
    about a = new about();
    a.Show();
}
private void label16_Click(object sender, EventArgs e)//设备管理
{
    deviceManage dm = new deviceManage(label2.Text);
    dm.Show();
    //alarm();
}
private void label14_Click(object sender, EventArgs e)//系统配置
{
    setting s = new setting();
    s.Show();
}

private void label13_Click(object sender, EventArgs e)//权限管理和管理员注册
{
    admin ad = new admin(label2.Text);
    ad.Show();
}

private void label15_Click(object sender, EventArgs e)//日志文件
{
    file f = new file();
    f.Show();
}
#endregion  

private void timer1_Tick(object sender, EventArgs e)
{

}

private void button14_Click(object sender, EventArgs e)
{
    messagebox mb = new messagebox("1004","火警");
    AnimateWindow(mb.Handle, 1000, AW_VER_NEGATIVE | AW_ACTIVATE);//从下到上且不占其它程序焦点  
    mb.Show();
    SoundPlayer start = new SoundPlayer(Properties.Resources.ALARM1);
}

private void label17_TextChanged(object sender, EventArgs e)
{
    if(label17.Text=="报警已处理")
    {
         timer5.Enabled = true;
    }
}




/****************************************************************************************/
    }
}
