using DHEM_TestWork.DB.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DHEM_TestWork.DB.Handler
{
    class ChannelHandler : BaseHandler<ChannelModel>
    {
        public ChannelHandler(MySqlConnection connection) : base("channel", connection) { }

        protected override object ParseData(MySqlDataReader reader)
        {
            return new ChannelModel(
                (int)reader["number"],
                (int)reader["id"]
            );
        }

        public ChannelModel SelectByNumber(int number)
        {
            var fields = new Dictionary<string, object>() { { "number", number } };
            var result = SelectAll(fields);
            if (result.Count == 0)
                throw new ConsoleMsgException("Not found channel with number = " + number.ToString());
            return result[0];
        }
    }
}
