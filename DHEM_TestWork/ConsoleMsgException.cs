using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHEM_TestWork
{
    class ConsoleMsgException : Exception
    {
        public ConsoleMsgException(string msg) : base(msg)
        {
            //Console.WriteLine(msg);
        }


        public ConsoleMsgException(Exception e) : base(e.Message, e)
        {
            //Console.WriteLine(e.Message);
        }
    }
}
