using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SpeechLib;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WindowsApplication1
{
    class Class1
    {
        public Class1() { }
        public void PlaySound(string FileName)
        {//要加载COM组件:Microsoft speech object Library

            if (!System.IO.File.Exists(FileName))
            {
                return;
            }
            SpeechLib.SpVoiceClass pp = new SpeechLib.SpVoiceClass();
            SpeechLib.SpFileStreamClass spFs = new SpeechLib.SpFileStreamClass();
            spFs.Open(FileName, SpeechLib.SpeechStreamFileMode.SSFMOpenForRead, true);
            SpeechLib.ISpeechBaseStream Istream = spFs as SpeechLib.ISpeechBaseStream;
            pp.SpeakStream(Istream, SpeechLib.SpeechVoiceSpeakFlags.SVSFIsFilename);
            spFs.Close();
        }
        public Bitmap KiRotate(Bitmap b, int angle)
        {
            angle = angle % 360;
            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            g.TranslateTransform(b.Width / 2, b.Height / 2);
            g.RotateTransform(360 - angle);

            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);

            //重至绘图的所有变换
            g.ResetTransform();

            //g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }
        /// <summary>
        /// ASCII码字符串转字节包
        /// </summary>
        /// <param name="str">ASCII码字符串</param>
        /// <returns></returns>
        public byte[] ZHAC(string str)
        {
            byte[] dz = Encoding.ASCII.GetBytes(str);
            return dz;
        }
        /// <summary>
        /// 16进制字符串转字节包
        /// </summary>
        /// <param name="DZ">16进制字符串</param>
        /// <returns>字节包</returns>
        public byte[] ZH16(string DZ)
        {
            int len1 = DZ.Length / 2;
            byte[] ret = new byte[len1];
            for (int i = 0; i < len1; i++)
            {
                ret[i] = Convert.ToByte(DZ.Substring(i * 2, 2), 16);
            }
            return ret;
        }
        /// <summary>
        /// 串口以ASCII码字符串发送命令并在1000毫秒后返回字节包
        /// </summary>
        /// <param name="str">ASCII码字符串</param>
        /// <param name="sp">串口</param>
        /// <returns>返回的字节包</returns>
        public byte[] writeAC(string str, SerialPort sp)
        {
            byte[] by = ZHAC(str);
            sp.Write(by, 0, by.Length);
            Thread.Sleep(1000);
            int ili = sp.BytesToRead;
            byte[] s = new byte[ili];
            sp.Read(s, 0, s.Length);
            return s;
        }
        int hm = 500;
        /// <summary>
        /// 串口以ASCII码字符串发送命令并在hm豪秒后返回字节包
        /// </summary>
        /// <param name="str">ASCII码字符串</param>
        /// <param name="sp">串口</param>
        /// <returns>返回的字节包</returns>
        public byte[] writeAC1(string str, SerialPort sp)
        {
            byte[] by = ZHAC(str);
            sp.Write(by, 0, by.Length);
            Thread.Sleep(hm);
            int ili = sp.BytesToRead;
            byte[] s = new byte[ili];
            sp.Read(s, 0, s.Length);
            return s;
        }
        /// <summary>
        /// hm变量附值
        /// </summary>
        /// <param name="Hm">要附的值</param>
        public void HM(int Hm)
        {
            hm = Hm;
        }
        /// <summary>
        /// 串口以16进制字符串发送命令并在500毫秒后返回字符串
        /// </summary>
        /// <param name="str">16进制字符串</param>
        /// <param name="sp">串口</param>
        /// <returns>返回的字节包</returns>
        public byte[] write16(string str, SerialPort sp)
        {
            byte[] by = ZH16(str);
            sp.Write(by, 0, by.Length);
            Thread.Sleep(500);
            int ili = sp.BytesToRead;
            byte[] s = new byte[ili];
            sp.Read(s, 0, s.Length);
            return s;
        }
        /// <summary>
        /// 串口以ASCII码字符串发送命令
        /// </summary>
        /// <param name="str">ASCII码字符串</param>
        /// <param name="sp">串口</param>
        public void writeACNO(string str, SerialPort sp)
        {
            byte[] by = ZHAC(str);
            sp.Write(by, 0, by.Length);
        }
        /// <summary>
        /// 串口以16进制字符串发送命令
        /// </summary>
        /// <param name="str">16进制字符串</param>
        /// <param name="sp">串口</param>
        public void write16NO(string str, SerialPort sp)
        {
            byte[] by = ZH16(str);
            sp.Write(by, 0, by.Length);           
        }
        /// <summary>
        /// 字节包转ASCII码字符串
        /// </summary>
        /// <param name="str">字节包</param>
        /// <returns>ASCII码字符串</returns>
        public string ZHTOAC(byte[] str)
        {
            string dz = Encoding.ASCII.GetString(str);
            return dz;
        }
        /// <summary>
        /// 字节包转16进制字符串
        /// </summary>
        /// <param name="sz">字节包</param>
        /// <returns>16进制字符串</returns>
        public string ZHTO16(byte[] sz)
        {
            string ret = "";
            foreach (byte b in sz)
            {
                if (b.ToString("X").Length > 1)
                {
                    ret += b.ToString("X");
                }
                else
                {
                    ret += "0" + b.ToString("X");
                }
            }
            return ret;
        }
        /// <summary>
        /// 16进制字符串异或值计算
        /// </summary>
        /// <param name="ret">16进制字符串</param>
        /// <returns>16进制异或值</returns>
        public string yh(string ret)
        {
            byte[] by = ZH16(ret);
            int xay = 0;
            for (int i = 0; i < by.Length; i++)
            {
                xay ^= by[i];
            }
            if (xay.ToString("X").Length > 1)
            {
                return xay.ToString("X");
            }
            else
            {
                return "0" + xay.ToString("X");
            }
        }

        public string jh(byte[] by)
        {

            byte xay = 0;
            for (int i = 0; i < by.Length; i++)
            {
                xay += by[i];
            }
            if (xay.ToString("X").Length > 1)
            {
                return xay.ToString("X");
            }
            else
            {
                return "0" + xay.ToString("X");
            }

        }
        public string tz(string ret)
        {

            return ret;
        }

        public string bh(string stt)
        {
            //for (int i = 0; i < stt.Length; i = i + 0)
            //{
            //    if (stt.Substring(i, 2) == "FE")
            //    {

            //        if (stt.Substring(i + 6, 2) == "1E")
            //        {
            //            i = i + 70;
            //        }
            //        else
            //        {
            //            stt = stt.Remove(i, 2);
            //        }
            //    }
            //    else
            //    {
            //        stt = stt.Remove(i, 2);
            //    }
            //}

            return stt;
        }
        public string fxbh(string str, string dz)
        {
            // str = "01A02000" + dz.Substring(16, 4) + str.Substring(2, 58);
            return str + yh(str);
        }

        ////十六进制转十进制
        //public int from16to10(string num16)
        //{
        //    return Convert.ToInt32(num16, 16);
        //}
        
        ////十进制转十六进制
        //public int from10to16(int num10)
        //{
        //    return Convert.ToString(num10, 16);
        //}

    }
}
