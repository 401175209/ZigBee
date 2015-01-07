using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBee.Client.Models
{
    public class AlarmModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 设备SN
        /// </summary>
        public string DeviceSN { get; set; }

        public virtual DeviceModel Device { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public AlarmType Type { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
