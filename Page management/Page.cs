using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class Page : ICloneable
    {
        public int IdleTime { get; set; }
        public int TimeInMemory { get; set; }
        public int TimeSinceReq { get; set; }
        public int ID { get; }
        public bool Used { get; set; }
        public bool GotAChance { get; set; }
        public Page(int id)
        {
            ID = id;
            TimeSinceReq = 0;
            TimeInMemory = 0;
        }

        public object Clone()
        {
            return new Page(ID);
        }
    }
}
