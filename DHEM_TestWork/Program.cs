using System;
using System.Collections.Generic;
using System.Linq;
using DHEM_TestWork.DB;

namespace DHEM_TestWork
{
    class Program
    {
        static Dictionary<string, string> configArgsKeys = new Dictionary<string, string>()
        {
            {"-n", "DB_Name"},
            {"-h", "DB_Host"},
            {"-u", "DB_User"},
            {"-pass", "DB_Password"},
            {"-port", "DB_Port"}
        };

        static void Main(string[] args)
        {
            try
            {
                UpdateConfigs(args);
                PrintConfigValues();

                ORM.Init();
                Console.WriteLine("DB connected successfully\n");         

                Console.WriteLine("Files parsing starting...");
                var parser = new DataParser();
                var storageValues = parser.ParseAll();
                Console.WriteLine("Files parsing finished\n");

                Console.WriteLine("Storage table inserting...");
                ORM.Storage.Insert(storageValues);
                Console.WriteLine("Inserting finished\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
            if (args.Contains("-u") && !args.Contains("-pass"))
                Properties.Settings.Default[configArgsKeys["-pass"]] = "";
            Properties.Settings.Default.Save();
        }

        static void PrintConfigValues()
        {
            Console.WriteLine("Current config values:");
            foreach (var configName in configArgsKeys.Values)
            {
                Console.WriteLine(configName + ": " + Properties.Settings.Default[configName]);
            }
            Console.WriteLine("");
        }
    }
}
