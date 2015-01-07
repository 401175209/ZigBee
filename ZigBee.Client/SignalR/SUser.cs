using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBee.Client.Models;

namespace ZigBee.Client.SignalR
{
    public static class SUser
    {
        public static async Task<bool> Login(string Username, string Password)
        {
            return await Program.ZigBeeHub.Invoke<bool>("Login",Username, Password);
        }

        public static async Task<bool> ChangePassword(string Username, string OldPassword, string NewPassword)
        {
            return await Program.ZigBeeHub.Invoke<bool>("ChangePassword",Username, OldPassword, NewPassword);
        }

        public static async Task<UserModel> GetUser(int? UserID)
        {
            return await Program.ZigBeeHub.Invoke<UserModel>("GetUser", UserID);
        }
    }
}
