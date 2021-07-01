using GreeAirPC.Database;
using GreeAirPC.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreeAirPC
{
    public static class MyGree
    {
        public static Dictionary<string, string> CmdList = new Dictionary<string, string>
        {
            { "Pow",        "Power          "},
            { "Mod",        "Mode           "},
            { "SetTem",     "Set Temp       "},
            { "WdSpd",      "Set Fan Speed  "},
            { "Air",        "Air            "},
            { "Blo",        "X-Fan          "},
            { "Health",     "Health         "},
            { "SwhSlp",     "Sleep          "},
            { "Lig",        "Light          "},
            { "SwUpDn",     "Swip Up Down   "},    // Vertical swing
            { "Quiet",      "Quiet          "},
            { "Tur",        "Turbo          "},
            { "SvSt",       "Energy Saver   "},
            { "TemUn",      "Temp Units C/F "}
        };

        public static AirCondModel GetDefault()
        {
            AirCondModel model = new AirCondModel(Settings.Default.ID, Settings.Default.Name, Settings.Default.PrivateKey, Settings.Default.IP);

            return model;
        }


        public static async Task<List<AirCondModel>> DiscoverDevices()
        {
            List<AirCondModel> foundUnits = new List<Database.AirCondModel>();
            var results = await Gree.Scanner.Scan("10.0.0.255");
            foundUnits.AddRange(results);

            return foundUnits;
        }
    }
}
