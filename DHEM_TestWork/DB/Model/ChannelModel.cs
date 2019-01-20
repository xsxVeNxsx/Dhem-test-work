using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHEM_TestWork.DB.Model
{
    class ChannelModel : BaseModel
    {
        public int Number { get; }

        public ChannelModel(int number, int? id = null) : base(id)
        {
            Number = number;
        }
    }
}
