using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace ZigBee.Server.Helpers
{
    public static class StringHelper
    {
        public static string RandomString(int Length)
        {
            var rand = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Length; i++)
            {
                int ch = rand.Next(26 + 26 + 10);
                if (ch < 26) sb.Append((char)(ch + 'A'));
                else if (ch < 26 + 26) sb.Append((char)(ch - 26 + 'a'));
                else sb.Append((char)(ch - 26 - 26 + '0'));
            }
            return sb.ToString();
        }

        public static DateTime ToDateTime(string TimeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(TimeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        public static DateTime ToDateTime(int TimeStamp)
        {
            return ToDateTime(TimeStamp.ToString());
        }

        public static int ToTimeStamp(System.DateTime Time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(Time - startTime).TotalSeconds;
        }

        public static int RandomInt(int Begin, int End)
        {
            Random rand = new Random();
            var ret = rand.Next(End - Begin + 1);
            return ret + Begin;
        }

        public static int StringLen(string str)
        {
            byte[] sarr = System.Text.Encoding.Default.GetBytes(str);
            return sarr.Length;
        }
    }
}