using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class Request : ICloneable
    {
        public int PageID { get; }
        public int Time { get; }

        public Request(int pageID, int time)
        {
            PageID = pageID;
            Time = time;
        }

        public object Clone()
        {
            return new Request(PageID, Time);
        }
    }
}
