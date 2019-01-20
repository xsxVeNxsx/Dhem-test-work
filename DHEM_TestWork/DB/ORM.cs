using MySql.Data.MySqlClient;
using DHEM_TestWork.DB.Handler;
using DHEM_TestWork.DB.Model;
using System.Collections.Generic;

namespace DHEM_TestWork.DB
{
    static class ORM
    {
        private static MySqlConnection connection;

        public static StorageHandler Storage;
        public static ChannelHandler Channel;
        public static StationHandler Station;

        public static List<ChannelModel> ChannelCache = new List<ChannelModel>();
        public static List<StationModel> StationCache = new List<StationModel>();

        public static void Init()
        {
            connection = DBConnection.CreateConnection();
            Storage = new StorageHandler(connection);
            Channel = new ChannelHandler(connection);
            Station = new StationHandler(connection);

            UpdateChache();
        }

        public static void UpdateChache()
        {
            ChannelCache = Channel.SelectAll();
            StationCache = Station.SelectAll();
        }
    }
}
