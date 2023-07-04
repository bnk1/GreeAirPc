using System;
using System.Collections.Generic;
using System.Linq;

namespace GreeAirPC.Gree
{
    public struct DevParam
    {
        public struct Names
        {
            public const string Power            = "Pow";
            public const string Mode             = "Mod";
            public const string SetTemperature   = "SetTem";
            public const string FanSpeed         = "WdSpd";
            public const string AirMode          = "Air";
            public const string XfanMode         = "Blo";
            public const string HealthMode       = "Health";
            public const string SleepMode        = "SwhSlp";
            public const string Light            = "Lig";
            public const string VerticalSwing    = "SwUpDn";
            public const string QuietMode        = "Quiet";
            public const string TurboMode        = "Tur";
            public const string EnergySavingMode = "SvSt";
            public const string TemperatureUnit  = "TemUn";
        }

        public struct Description
        { 
            public const string Power            = "Power";
            public const string Mode             = "Mode";
            public const string SetTemperature   = "Temperature";
            public const string FanSpeed         = "Fan Speed";
            public const string AirMode          = "External Air";
            public const string XfanMode         = "X-Fan";
            public const string HealthMode       = "Health";
            public const string SleepMode        = "Sleep";
            public const string Light            = "Light";
            public const string VerticalSwing    = "Swip Up Down";
            public const string QuietMode        = "Quiet";
            public const string TurboMode        = "Turbo";
            public const string EnergySavingMode = "Energy Saver";
            public const string TemperatureUnit  = "Temp Units C/F";
        }

        public static IEqualityComparer<string> comparer = StringComparer.OrdinalIgnoreCase;

        public static Dictionary<string, string> DescToParam = new Dictionary<string, string>(comparer)
        {
            {  Description.Power               , Names.Power            },
            {  Description.TurboMode           , Names.TurboMode        },
            {  Description.EnergySavingMode    , Names.EnergySavingMode },
            {  Description.Mode                , Names.Mode             }, 
            {  Description.FanSpeed            , Names.FanSpeed         },         
            {  Description.AirMode             , Names.AirMode          },          
            {  Description.XfanMode            , Names.XfanMode         },         
            {  Description.HealthMode          , Names.HealthMode       },       
            {  Description.SleepMode           , Names.SleepMode        },        
            {  Description.Light               , Names.Light            },            
            {  Description.VerticalSwing       , Names.VerticalSwing    },    
            {  Description.SetTemperature      , Names.SetTemperature   },   
            {  Description.QuietMode           , Names.QuietMode        },
            {  Description.TemperatureUnit     , Names.TemperatureUnit  },
        };

        public static Dictionary<string, string> ParamToDesc = DescToParam.ToDictionary((i) => i.Value, (i) => i.Key);

        public static Dictionary<string, int> ModeNameToIdx = new Dictionary<string, int>(comparer)
        {
            { "Auto", 0 },
            { "Cool", 1 },
            { "Dry",  2 },
            { "Air",  3 },
            { "Heat", 4 },
        };

        public static Dictionary<int, string> ModeIdxToName = ModeNameToIdx.ToDictionary((i) => i.Value, (i) => i.Key);
    }
}