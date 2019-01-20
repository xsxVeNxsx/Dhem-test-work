using System;

namespace DHEM_TestWork.DB.Model
{
    class StorageModel : BaseModel
    {
        public DateTime Date { get; }
        public double Value { get; }
        public DateTime CreatedAt { get; }
        public int StationId { get; }
        public int ChannelId { get; }

        public StorageModel(double value, DateTime date, DateTime createAt, int stationId, int channelId, int? id = null) : base(id)
        {
            Date = date;
            Value = value;
            CreatedAt = createAt;
            StationId = stationId;
            ChannelId = channelId;
        }
    }
}
