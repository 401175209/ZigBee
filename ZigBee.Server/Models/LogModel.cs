using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZigBee.Server.Models
{
    public enum LogType
    {
        Login,
        Logout,
        ModifyDevice,
        InsertDevice,
        DeleteDevice
    }

    [Table("Logs")]
    public class LogModel
    {
        [Column("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        [Column("UserID")]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual UserModel User { get; set; }

        /// <summary>
        /// 操作指令
        /// </summary>
        [Column("Type")]
        [Index]
        public LogType Type { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("Time")]
        [Index]
        public DateTime Time { get; set; }

        /// <summary>
        /// 操作详情
        /// </summary>
        [Column("Detail")]
        [Required]
        public string Detail { get; set; }
    }
}
