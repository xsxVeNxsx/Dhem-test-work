using System.Collections.Generic;
using DHEM_TestWork.DB.Model;
using MySql.Data.MySqlClient;

namespace DHEM_TestWork.DB.Handler
{
    class StorageHandler : BaseHandler<StorageModel>
    {
        public StorageHandler(MySqlConnection connection) : base("storage", connection) { }

        public void Insert(List<StorageModel> items)
        {
            var fieldsList = new List<Dictionary<string, object>>();
            foreach (var item in items)
            {
                fieldsList.Add(new Dictionary<string, object>()
                    {
                        {"date", item.Date},
                        {"channel_id", item.ChannelId},
                        {"created_at", item.CreatedAt},
                        {"station_id", item.StationId},
                        {"value", item.Value}
                    }
                );
            }
            Insert(fieldsList);
        }
    }
}
