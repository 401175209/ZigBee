using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsApplication1
{
    class TOPO
    {
        public TOPO()
        { }

        Bitmap BT1 = null;
        Color co;
        public string[,] wh = null;
        int oldW;
        int oldH;
        Class1 cl1 = new Class1();
        Font fon = new Font("宋体", 9F);
        int ImageWidth;
        int ImageHeight;
        /// <summary>
        /// 画TOPO图节点
        /// </summary>
        /// <param name="node">节点数组</param>
        /// <param name="ROU">路由器图片</param>
        /// <param name="RFD">终端节点图片</param>
        /// <param name="GW">网关图片</param>破；
        /// <param name="ImageWidth">图片宽度</param>
        /// <param name="ImageHeight">图片高度</param>
     
        public void Node(string[,] node, Image ROU, Image RFD, Image GW, int imageWidth, int imageHeight)
        {
            try
            {
                if (node != null)
                {
                    int xx = 0;
                    int yy = 100;
                    ImageHeight = imageHeight; 
                    ImageWidth = imageWidth;
                    
                    if (wh == null)
                    {
                        wh = new string[node.Length / 9, 2];
                        wh[0, 0] = Convert.ToString((BT1.Width - ImageWidth) / 2);
                        wh[0, 1] = "0";
                    }
                    else
                    {
                        string[,] wh1 = wh;
                        wh = new string[node.Length / 9, 2];
                        if (wh1.Length <= wh.Length)
                        {
                            for (int i = 0; i < wh1.Length / 2; i++)
                            {
                                wh[i, 0] = wh1[i, 0];
                                wh[i, 1] = wh1[i, 1];
                            }
                        }
                        else
                        {
                            for (int i = 0; i < wh.Length / 2; i++)
                            {
                                wh[i, 0] = wh1[i, 0];
                                wh[i, 1] = wh1[i, 1];
                            }
                        }
                        for (int i = wh1.Length / 2 - 1; i >= 1; i--)
                        {
                            if (wh1[i, 0] != null)
                            {
                                xx = int.Parse(wh1[i, 0]);
                                yy = int.Parse(wh1[i, 1]);
                                break;
                            }
                        }
                    }


                    for (int i = 0; i < wh.Length / 2; i++)
                    {
                        if (wh[i, 0] == null)
                        {
                            if (xx + ImageWidth + 30 <= BT1.Width)
                            {
                                xx += ImageWidth + 30;
                            }
                            else
                            {
                                xx = 0;
                                yy += 50;
                            }
                            wh[i, 0] = xx.ToString();
                            wh[i, 1] = yy.ToString();
                        }
                        else
                        {
                            wh[i, 0] = Convert.ToString(int.Parse(wh[i, 0]) * BT1.Width / oldW);
                            wh[i, 1] = Convert.ToString(int.Parse(wh[i, 1]) * BT1.Height / oldH);
                            if (int.Parse(wh[i, 0]) + ImageWidth + 30 > BT1.Width)
                            {
                                wh[i, 0] = Convert.ToString(BT1.Width - ImageWidth - 30);
                            }
                            if (int.Parse(wh[i, 1]) + ImageHeight + 50 > BT1.Height)
                            {
                                wh[i, 1] = Convert.ToString(BT1.Height - ImageHeight - 50);
                            }
                        }

                        if (node[i, 2] == "Gateway")
                        {
                            DrawNode(GW, int.Parse(wh[i, 0]), int.Parse(wh[i, 1]), node[i, 4], node[i, 7]);
                        }
                        if (node[i, 2] == "ROU")
                        {

                            DrawNode(ROU, int.Parse(wh[i, 0]), int.Parse(wh[i, 1]), node[i, 4], node[i, 7]);
                            
                        }
                        if (node[i, 2] == "RFD")
                        {

                            DrawNode(RFD, int.Parse(wh[i, 0]), int.Parse(wh[i, 1]), node[i, 4], node[i, 7]);
                          
                        }

                    }
                    Drawborder();

                    oldW = BT1.Width;
                    oldH = BT1.Height;
                }
            }
            catch
            {
            }
        }
        public void DrawNode(Image Node, int x, int y,string dl, string room)
        {
            Graphics g = Graphics.FromImage(BT1);
            g.DrawImage(Node, x, y, ImageWidth, ImageHeight);
            int power = Convert.ToInt16(dl);
            if (power < 1)
            {
                g.DrawString(room, fon, Brushes.Red, x - 5, ImageHeight + y + 3);
            }
            else
                g.DrawString(room, fon, Brushes.Black, x - 5, ImageHeight + y + 3);
                      
        }
        /// <summary>
        /// 节点连线
        /// </summary>
        /// <param name="node">节点数组，包括节点地址，节点父地址，节点类型，节点信号强度</param>
        public void Nodeline(string[,] node)
        {
            if (node != null)
            {
                for (int i = 1; i < wh.Length / 2; i++)
                {
                    if (node[i, 0] != null)
                    {
                        for (int l = 0; l < wh.Length / 2; l++)
                        {
                            if (node[l, 1] != null)
                            {
                                if (node[i, 1] == node[l, 0])
                                {
                                    Graphics g = Graphics.FromImage(BT1);
                                    Pen linepen = new Pen(Color.Gray, 1);
                                   
                                        linepen = new Pen(Color.Red, 1);
                                
                                    float x = int.Parse(wh[i, 0]) + ImageWidth / 2 + (int.Parse(wh[l, 0]) - (int.Parse(wh[i, 0]) + ImageWidth / 2)) / 2;
                                    float y = int.Parse(wh[i, 1]) + (int.Parse(wh[l, 1]) + ImageHeight / 2 - int.Parse(wh[i, 1])) / 2;
                                    linepen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap((float)(linepen.Width * 3), (float)(linepen.Width * 4), true);
                                    linepen.CustomStartCap = new System.Drawing.Drawing2D.AdjustableArrowCap((float)(linepen.Width * 3), (float)(linepen.Width * 4), true);
                                    g.DrawLine(linepen, new Point(int.Parse(wh[i, 0]) + ImageWidth / 2, int.Parse(wh[i, 1])), new Point(int.Parse(wh[l, 0]) + ImageWidth / 2, int.Parse(wh[l, 1]) + ImageHeight / 2));
                                   // g.DrawString(xhqd.ToString(), fon, Brushes.Black, x, y);
                                }
                            }
                        }
                    }
                }
            }
        }
          
        /// <summary>
        /// 清除TOPO图
        /// </summary>
        public void Clear()
        {

            Graphics g = Graphics.FromImage(BT1);
            g.Clear(co);
        }

        public void ClearALL()
        {
            wh = null;
            CheckedNode = null;

            Graphics g = Graphics.FromImage(BT1);
            g.Clear(co);
        }
        /// <summary>
        /// 返回TOPO图
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitMap()
        {
            return BT1;
        }

        /// <summary>
        /// 设置TOPO图大小
        /// </summary>
        /// <param name="width">TOPO图宽</param>
        /// <param name="height">TOPO图高</param>
        public void bitmap(int width, int height)
        {
            if (BT1 != null)
            {
                oldW = BT1.Width;
                oldH = BT1.Height;
            }
            else
            {
                oldW = width;
                oldH = height;
            }
            BT1 = new Bitmap(width, height);
            
            co = BT1.GetPixel(10, 10);
        }
        string CheckedNode = null;

        public void mouseclick(int x, int y)
        {
            CheckedNode = null;
            if (wh != null)
            {
                for (int i = 0; i < wh.Length / 2; i++)
                {
                    if (wh[i, 0] != null && int.Parse(wh[i, 0]) <= x && x <= int.Parse(wh[i, 0]) + ImageWidth && int.Parse(wh[i, 1]) <= y && y <= int.Parse(wh[i, 1]) + ImageHeight)
                    {
                        if (CheckedNode == null || int.Parse(CheckedNode) != i)
                        {
                            CheckedNode = i.ToString();
                            return;
                        }
                    }
                }
            }
        }

        public void mousemove(int x, int y)
        {
            if (wh != null)
            {
                if (CheckedNode != null && int.Parse(CheckedNode) < wh.Length / 2)
                {
                    wh[int.Parse(CheckedNode), 0] = x.ToString();
                    wh[int.Parse(CheckedNode), 1] = y.ToString();
                }
            }
        }

        public void Drawborder()
        {
            if (CheckedNode != null && wh != null && int.Parse(CheckedNode) < wh.Length / 2)
            {
                Graphics g = Graphics.FromImage(BT1);
                Pen p = new Pen(Color.Blue);
                p.DashStyle = DashStyle.Dash;
                g.DrawRectangle(p, int.Parse(wh[int.Parse(CheckedNode), 0]) - 15, int.Parse(wh[int.Parse(CheckedNode), 1]) - 5, ImageWidth +30, ImageHeight + 30);
               
            }
        }

        public void DrawborderNUM(int i)
        {
            if (i < wh.Length / 2)
            {
                Graphics g = Graphics.FromImage(BT1);

                Pen p = new Pen(Color.Red);
                p.DashStyle = DashStyle.Dash;
                g.DrawRectangle(p, int.Parse(wh[i, 0]) - 27, int.Parse(wh[i, 1]) - 7, ImageWidth + 52, ImageHeight + 82);
            }
        }

        public void treechecked(int check)
        {
            CheckedNode = check.ToString();
        }

        public string getchecked()
        {
            return CheckedNode;
        }
        
    }
}
