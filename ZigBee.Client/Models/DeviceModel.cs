using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBee.Client.Models
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

    public class DeviceModel
    {
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public int UserID { get; set; }

        public virtual UserModel User { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType Type { get; set; }

        /// <summary>
        /// 网络地址
        /// </summary>
        public string NetAddress { get; set; }

        /// <summary>
        /// 父网络地址
        /// </summary>
        public string ParentNetAddress { get; set; }

        public virtual DeviceModel ParentDevice { get; set; }

        /// <summary>
        /// 报警信号
        /// </summary>
        public AlarmType Alarm { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public int Humidity { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// 信号强度
        /// </summary>
        public int SingalIntensity { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DeviceStatus Status { get; set; }

        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime OnlineTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 地址（XXX号XXX单元XXX室）
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }
    }
}
