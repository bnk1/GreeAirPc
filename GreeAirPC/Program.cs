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
        static async Task Main(string[] args)
        {
            if (args.Length > 0) //then we have command line args
            {
                string paramName;
                string value;

                if (args[0].Contains(','))
                {
                    var prm = args[0].Split(',');

                    paramName = prm[0];
                    value = prm[1];
                }
                else
                {
                    paramName = args[0];
                    value = args[1];
                }

                await GreeCmdLine.RunCmdAsync(paramName, value, args.Length > 2);
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
