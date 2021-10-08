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
        public static Dictionary<string, string> CmdToNameDic = new Dictionary<string, string>
        {
            { "Pow",        "Power"},
            { "Tur",        "Turbo"},
            { "SvSt",       "Energy Saver"},
            { "Mod",        "Mode"},
            { "WdSpd",      "Fan Speed"},
            { "Air",        "External Air"},
            { "Blo",        "X-Fan"},
            { "Health",     "Health"},
            { "SwhSlp",     "Sleep"},
            { "Lig",        "Light"},
            { "SwUpDn",     "Swip Up Down"},    // Vertical swing
            { "SetTem",     "Temperature"},
            { "Quiet",      "Quiet"},
            { "TemUn",      "Temp Units C/F"}
        };

        public static Dictionary<string, string> NameToCmdDic = new Dictionary<string, string>
        {
            { "Power"          , "Pow"        },
            { "Turbo"          , "Tur"        },
            { "Energy Saver"   , "SvSt"       },
            { "Mode"           , "Mod"        },
            { "Fan Speed"      , "WdSpd"      },
            { "External Air"   , "Air"        },
            { "X-Fan"          , "Blo"        },
            { "Health"         , "Health"     },
            { "Sleep"          , "SwhSlp"     },
            { "Light"          , "Lig"        },
            { "Swip Up Down"   , "SwUpDn"     },    // Vertical swing
            { "Temperature"    , "SetTem"     },
            { "Quiet"          , "Quiet"      },
            { "Temp Units C/F" , "TemUn"      }
        };

        public static AirCondModel GetDefault()
        {
            if (string.IsNullOrEmpty(Settings.Default.ID) || (Settings.Default.ID == "ID") || string.IsNullOrEmpty(Settings.Default.IP))
                throw new Exception("Null default");

            AirCondModel model = new AirCondModel(Settings.Default.ID, Settings.Default.Name, Settings.Default.PrivateKey, Settings.Default.IP);

            return model;
        }


        public static async Task<List<AirCondModel>> DiscoverDevices(string netMask)
        {
            List<AirCondModel> foundUnits = new List<Database.AirCondModel>();

            var results = await Gree.Scanner.Scan(netMask);

            foundUnits.AddRange(results);

            return foundUnits;
        }
    }
}
