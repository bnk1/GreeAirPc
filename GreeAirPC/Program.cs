using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GreeAirPC
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0) //then we have command line args
            {
                GreeCmd.RunCmd(args);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AirCondForm());
            }
        }
    }
}
