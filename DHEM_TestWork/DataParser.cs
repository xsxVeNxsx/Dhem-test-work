using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHEM_TestWork.DB.Model;
using System.IO;
using DHEM_TestWork.DB;
using System.Text.RegularExpressions;

namespace DHEM_TestWork
{
    class DataParser
    {
        const string DEF_DIR = "data";

        const int STATION_LINE = 3;
        const int CHANNEL_LINE = 15;
        const int EVALUATION_LINE = 7;
        const int VALUE_LINE = 33;

        public string Dir { get; private set; }

        public DataParser(string dataDir = DEF_DIR)
        {
            Dir = dataDir;
        }

        public List<StorageModel> ParseAll()
        {
            var result = new List<StorageModel>();

            var dirInfo = new DirectoryInfo(Dir);
            foreach (var file in dirInfo.GetFiles("*"))
            {
                try
                {
                    string text = file.OpenText().ReadToEnd();
                    var regVal = new Regex("(?<val_date>[\\d]{2}\\.[\\d]{2}\\.[\\d]{4} [\\d]{2}:[\\d]{2}:[\\d]{2})\\s*(?<value>[\\d]+,[\\d]+)", RegexOptions.Singleline);
                    var regAttrs = new Regex("Serial number\\s*:\\s*(?<station>[\\w]+).*?Evaluation at\\s*:\\s*(?<eval_date>[\\d]{2}\\.[\\d]{2}\\.[\\d]{4} [\\d]{2}:[\\d]{2}:[\\d]{2}).*?Channel\\s*:\\s*(?<channel>[\\d]+)", RegexOptions.Singleline);
                    var attrsMatch = regAttrs.Match(text);
                    var valMatches = regVal.Matches(text);

                    if (!ORM.StationCache.Exists(x => x.Name == attrsMatch.Groups["station"].Value))
                        throw new Exception("Not found station with name = " + attrsMatch.Groups["station"].Value);
                    var station = ORM.StationCache.Find(x => x.Name == attrsMatch.Groups["station"].Value);

                    if (!ORM.ChannelCache.Exists(x => x.Number == int.Parse(attrsMatch.Groups["channel"].Value)))
                        throw new Exception("Not found channel with number = " + int.Parse(attrsMatch.Groups["channel"].Value));
                    var channel = ORM.ChannelCache.Find(x => x.Number == int.Parse(attrsMatch.Groups["channel"].Value));

                    foreach (Match val in valMatches)
                    {
                        result.Add(new StorageModel(float.Parse(val.Groups["value"].Value), DateTime.ParseExact(val.Groups["val_date"].Value, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), DateTime.Now, station.Id.Value, channel.Id.Value));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(file.Name + " parsing error: " + e.Message);
                }
            }

            return result;
        }
    }
}
