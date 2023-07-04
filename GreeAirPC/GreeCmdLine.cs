using GreeAirPC.Database;
using GreeAirPC.Gree;
using GreeAirPC.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeAirPC
{
    public class GreeCmdLine
    {
        public static async Task RunCmdAsync(string paramName, string value, bool forceDiscover)
        {
            AirCondModel def = await MyGree.GetDefault();

            Gree.Controller ctrl = new Gree.Controller(def);

            await ctrl.SetParamByName(paramName, value);
        }
    }
}
