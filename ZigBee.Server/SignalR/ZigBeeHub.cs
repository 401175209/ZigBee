using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ZigBee.Server.Models;
using ZigBee.Server.Helpers;

namespace ZigBee.Server.SignalR
{
    public class ZigBeeHub : Hub
    {
        public readonly ZigBeeContext DB = new ZigBeeContext();
        public Dictionary<string, int> Users = new Dictionary<string, int>();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool Login(string Username, string Password)
        {
            var pwd = SecurityHelper.SHA256(Password);
            var user = (from u in DB.Users
                        where u.Username == Username
                        && u.Password == Password
                        select u).SingleOrDefault();
            if (user == null) return false;
            Users[Context.ConnectionId] = user.ID;
            return true; 
        }

        private UserModel GetUser()
        {
            if (!Users.ContainsKey(Context.ConnectionId)) return null;
            else return DB.Users.Find(Users[Context.ConnectionId]);
        }

        private UserRole? GetRole()
        {
            if (!Users.ContainsKey(Context.ConnectionId)) return null;
            else return DB.Users.Find(Users[Context.ConnectionId]).Role;
        }
    }
}