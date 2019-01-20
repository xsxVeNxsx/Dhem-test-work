using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using DHEM_TestWork.DB.Model;

namespace DHEM_TestWork.DB.Handler
{
    class BaseHandler<T> where T : BaseModel
    {
        protected MySqlConnection connection;
        public string TableName { get; }

        /// <summary>
        /// Максимальное количество запросов в одной транзакции
        /// </summary>
        private const int BatchPortionLength = 1000;

        public BaseHandler(string _tableName, MySqlConnection _connection, DataParserDelegator _dataParser = null)
        {
            TableName = _tableName;
            connection = _connection;
            dataParser = _dataParser ?? ParseData;
        }

        public delegate object DataParserDelegator(MySqlDataReader reader);
        protected DataParserDelegator dataParser;

        protected virtual object ParseData(MySqlDataReader reader)
        {
            var res = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; ++i)
            {
                res.Add(reader.GetName(i), reader.GetValue(i));
            }
            return res;
        }

        public List<T> SelectAll()
        {
            return SelectAll(new Dictionary<string, object>());
        }

        public List<T> SelectAll(Dictionary<string, object> fields)
        {
            return Select("Select * From " + TableName, fields);
        }

        protected List<T> Select(
            string sql,
            Dictionary<string, object> fields,
            DataParserDelegator parser = null,
            QueryOper oper = QueryOper.EQ,
            QueryPredicate predicate = QueryPredicate.AND)
        {
            sql = string.Format("{0} {1}", sql, QueryBuilder.Where(fields, oper, predicate));
            return ExecQuery<T>(sql, fields, parser);
        }

        public void Insert(List<Dictionary<string, object>> fieldsList)
        {
            if (fieldsList.Count == 0 || fieldsList.Any(x => x.Count == 0))
                throw new ConsoleMsgException("Insert: empty insertion list");
            for (var step = 0; step < fieldsList.Count; step += BatchPortionLength)
            {
                int segmentSize = Math.Min(BatchPortionLength, fieldsList.Count - step);
                var segment = new ArraySegment<Dictionary<string, object>>(fieldsList.ToArray(), step, segmentSize).ToList();
                var values = "";
                for (var segmentIndex = 0; segmentIndex < segment.Count; ++segmentIndex)
                    values += "(" + string.Join(",", segment[segmentIndex].Keys.Select(x => "@" + x.Replace("\"", "") + segmentIndex.ToString())) + "),";
                var query = string.Format("Insert into {0}({1}) values {2};",
                    TableName,
                    string.Join(",", fieldsList[0].Keys),
                    values.Remove(values.Length - 1, 1)
                );
                ExecSimpleQuery(query, segment);
            }
        }

        protected List<U> ExecQuery<U>(string query, Dictionary<string, object> fields, DataParserDelegator parser)
        {
            List<U> result = new List<U>();
            parser = parser ?? dataParser;
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;
                    foreach (var field in fields)
                        cmd.Parameters.AddWithValue(field.Key, field.Value ?? DBNull.Value);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add((U)parser(reader));
                        }
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                throw new ConsoleMsgException(e);
            }
        }

        protected void ExecSimpleQuery(string query, List<Dictionary<string, object>> fieldsList)
        {
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;

                    for (int i = 0, counter = 0; i < fieldsList.Count; ++i, ++counter)
                    {
                        foreach (var field in fieldsList[i])
                        {
                            string key = field.Key.Replace("\"", "");
                            cmd.Parameters.AddWithValue(key + i, field.Value ?? DBNull.Value);
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new ConsoleMsgException(e);
            }
        }
    }
}
