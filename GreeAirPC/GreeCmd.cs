using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeAirPC
{
    public class GreeCmd
    {
        public static void RunCmdAsync(string[] args)
        {
            try
            {
                Gree.Controller ctrl = new Gree.Controller(MyGree.GetDefault());

                var vars = args[0].Split(',');

                ctrl.SetDeviceParameter(vars[0], int.Parse(vars[1])).Wait();
            }
            catch
            {

            }
        }
    }
}
