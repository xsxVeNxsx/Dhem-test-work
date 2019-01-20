using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHEM_TestWork.DB.Model
{
    class StationModel : BaseModel
    {
        public string Name { get; }

        public StationModel(string name, int? id = null) : base(id)
        {
            Name = name;
        }
    }
}
