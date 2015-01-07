using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using System.Configuration;

namespace ZigBee.Client
{
    static class Program
    {
        public static HubConnection HubConnection;
        public static IHubProxy ZigBeeHub;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HubConnection = new HubConnection(ConfigurationManager.AppSettings["HostAddress"]);
            ZigBeeHub = HubConnection.CreateHubProxy("zigBeeHub");
            HubConnection.Start().Wait();
            HubConnection.TraceLevel = TraceLevels.All;
            HubConnection.TraceWriter = Console.Out;
            //TODO: SignalR Push Events Register.

            Application.Run(new login());
        }
    }
}
