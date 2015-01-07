using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZigBee.Server.Models
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType
    {
        Router,
        Node
    }

    /// <summary>
    /// 报警类型
    /// </summary>
    public enum AlarmType
    {
        Fire = 1,
        Suspicious = 2,
        Multiple = 3,
        Normal = 0
    }

    /// <summary>
    /// 设备状态
    /// </summary>
    public enum DeviceStatus
    {
        On,
        Off,
        Unknown
    }

    [Table("Devices")]
    public class DeviceModel
    {
        /// <summary>
        /// 设备序列号
        /// </summary>
        [Key]
        [Column("SN", Order = 0, TypeName = "varchar")]
        [StringLength(64)]
        [Index]
        public string SN { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        [Column("UserID")]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual UserModel User { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [Column("Type")]
        [Index]
        public DeviceType Type { get; set; }

        /// <summary>
        /// 网络地址
        /// </summary>
        [Column("NetAddress")]
        [Index(IsUnique = true)]
        [StringLength(32)]
        [Required]
        public string NetAddress { get; set; }

        /// <summary>
        /// 父网络地址
        /// </summary>
        [Column("ParentNetAddress")]
        [Index]
        [StringLength(32)]
        [Required]
        public string ParentNetAddress { get; set; }

        public virtual DeviceModel ParentDevice { get; set; }

        /// <summary>
        /// 报警信号
        /// </summary>
        [Column("Alarm")]
        [Index]
        [Required]
        public AlarmType Alarm { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        [Column("Temperature")]
        [Index]
        [Required]
        public int Temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        [Column("Humidity")]
        [Index]
        [Required]
        public int Humidity { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        [Column("Power")]
        [Index]
        [Required]
        public int Power { get; set; }

        /// <summary>
        /// 信号强度
        /// </summary>
        [Column("SignalIntensity")]
        [Required]
        public int SingalIntensity { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("Status")]
        [Index]
        [Required]
        public DeviceStatus Status { get; set; }

        /// <summary>
        /// 上线时间
        /// </summary>
        [Column("OnlineTime")]
        [Index]
        [Required]
        public DateTime OnlineTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [Column("Longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Column("Latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Column("City")]
        [Index]
        [Required]
        [StringLength(16)]
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [Column("District")]
        [Index]
        [Required]
        [StringLength(16)]
        public string District { get; set; }

        /// <summary>
        /// 地址（XXX号XXX单元XXX室）
        /// </summary>
        [Column("DetailAddress")]
        [Required]
        [StringLength(128)]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Column("Floor")]
        [Index]
        [Required]
        [StringLength(16)]
        public string Floor { get; set; }
    }
}
