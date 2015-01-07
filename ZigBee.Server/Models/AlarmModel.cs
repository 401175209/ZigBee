using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZigBee.Server.Models
{
    [Table("Alarms")]
    public class AlarmModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 设备SN
        /// </summary>
        [Column("DeviceSN", TypeName = "varchar")]
        [StringLength(64)]
        [ForeignKey("Device")]
        public string DeviceSN { get; set; }

        public virtual DeviceModel Device { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        [Column("Type")]
        [Index]
        public AlarmType Type { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        [Column("Time")]
        [Index]
        [Required]
        public DateTime Time { get; set; }
    }
}
