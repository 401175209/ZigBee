using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZigBee.Server.Models
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

    [Table("Users")]
    public class UserModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Column("Username")]
        [StringLength(32)]
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// 密码(SHA256)
        /// </summary>
        [Column("Password")]
        [StringLength(64)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Column("Role")]
        [Index]
        public UserRole Role { get; set; }

        /// <summary>
        /// 隶属于用户
        /// </summary>
        [Column("AttachUserID")]
        [ForeignKey("AttachUser")]
        public int? AttachUserID { get; set; }

        public virtual UserModel AttachUser { get; set; }

        [Column("Phone")]
        [StringLength(32)]
        public string Phone { get; set; }

        [Column("Name")]
        [StringLength(16)]
        public string Name { get; set; }

        [Column("MobileType")]
        public MobileType? MobileType { get; set; }

        [Column("MoblieToken")]
        [StringLength(64)]
        public string MoblieToken { get; set; }

        public override int GetHashCode()
        {
            return this.ID;
        }

        public override bool Equals(object obj)
        {
            var data = obj as UserModel;
            if (data.ID == this.ID) return true;
            return false;
        }
    }
}
