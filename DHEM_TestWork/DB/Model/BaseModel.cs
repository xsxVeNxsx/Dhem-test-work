using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DHEM_TestWork.DB.Model
{
    class BaseModel
    {
        public int? Id { get; }

        public BaseModel(int? id)
        {
            Id = id;
        }
    }
}
