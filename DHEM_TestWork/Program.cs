using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;

namespace DHEM_TestWork
{
    class Program
    {
        static Dictionary<string, string> configArgsKeys = new Dictionary<string, string>()
        {
            {"-n", "DB_Name"},
            {"-h", "DB_Host"},
            {"-u", "DB_User"},
            {"-p", "DB_Password"}
        };

        static void Main(string[] args)
        {
            UpdateConfigs(args);
            PrintConfigValues();

            Console.Read();
        }

        static void UpdateConfigs(string[] args)
        {
            for (var i = 0; i < args.Length; i += 2)
            {
                if (configArgsKeys.ContainsKey(args[i]) && i + 1 < args.Length)
                {
                    Properties.Settings.Default[configArgsKeys[args[i]]] = args[i + 1];
                }
            }
            Properties.Settings.Default.Save();
        }

        static void PrintConfigValues()
        {
            Console.WriteLine("Current config values:");
            foreach (var configName in configArgsKeys.Values)
            {
                Console.WriteLine(configName + ": " + Properties.Settings.Default[configName]);
            }
        }
    }
}
