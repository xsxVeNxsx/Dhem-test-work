using DHEM_TestWork.DB.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DHEM_TestWork.DB.Handler
{
    class StationHandler : BaseHandler<StationModel>
    {
        public StationHandler(MySqlConnection connection) : base("station", connection) { }

        protected override object ParseData(MySqlDataReader reader)
        {
            return new StationModel(
                (string)reader["name"],
                (int)reader["id"]
            );
        }

        public StationModel SelectByName(string name)
        {
            var fields = new Dictionary<string, object>() { { "name", name} };
            var result = SelectAll(fields);
            if (result.Count == 0)
                throw new ConsoleMsgException("Not found station with name = " + name);
            return result[0];
        }
    }
}
