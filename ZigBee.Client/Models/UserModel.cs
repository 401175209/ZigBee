using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBee.Client.Models
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public enum UserRole
    {
        User,
        Master,
        Root
    }

    public enum MobileType
    {
        iOS,
        Android,
        WindowsPhone
    }

    public class UserModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码(SHA256)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// 隶属于用户
        /// </summary>
        public int? AttachUserID { get; set; }

        public virtual UserModel AttachUser { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public MobileType? MobileType { get; set; }

        public string MoblieToken { get; set; }
    }
}
