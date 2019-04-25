using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class Page
    {
        public int IdleTime { get; set; }
        public int TimeInMemory { get; set; }
        public int ID { get; }
        public Page(int id)
        {
            ID = id;
        }
    }
}
