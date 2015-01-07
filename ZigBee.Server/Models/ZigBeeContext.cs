using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ZigBee.Server.Models
{
    public class ZigBeeContext : DbContext
    {
        public DbSet<AlarmModel> Alarms { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public ZigBeeContext() : base("mssqldb")
        {
        }
    }
}
