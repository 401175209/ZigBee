using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBee.Client.Models
{
    public enum LogType
    {
        Login,
        Logout,
        ModifyDevice,
        InsertDevice,
        DeleteDevice
    }

    public class LogModel
    {
        public Guid ID { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public int UserID { get; set; }

        public virtual UserModel User { get; set; }

        /// <summary>
        /// 操作指令
        /// </summary>
        public LogType Type { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 操作详情
        /// </summary>
        public string Detail { get; set; }
    }
}
