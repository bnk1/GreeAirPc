using GreeAirPC.Database;
using GreeAirPC.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeAirPC
{
    public class GreeCmd
    {
        public static void RunCmd(string[] args)
        {
            AirCondModel def = null;

            def = MyGree.GetDefault();

            if (def == null)
                throw new Exception("No device set");

            Gree.Controller ctrl = new Gree.Controller(def);

            var vars = args[0].Split(',');

                ctrl.SetDeviceParameter(vars[0], int.Parse(vars[1])).Wait();
        }
    }
}
