using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class FIFO:PageController
    {
        public FIFO(List<Page> diskPages, List<Request> requests, int memorySize)
            :base(diskPages, requests, memorySize)
        {}

        protected override void Remove()
        {
            Page oldest = memoryPages[0];
            foreach(Page page in memoryPages)
            {
                if (page.TimeInMemory > oldest.TimeInMemory)
                {
                    oldest = page;
                }
            }
            memoryPages.Remove(oldest);
            diskPages.Add(oldest);
            Console.WriteLine("Removing page: "+oldest.ID);
        }

        public override int Run()
        {
            UpdateRequests();

            while(requests.Count>0 || requestPool.Count > 0)
            {
                UpdateRequests();
                if (requests.Count > 0)
                {
                    GetPage(requests[0].PageID);
                    requests.Remove(requests[0]);
                }
                MakePagesOlder();
                t++;
            }
            return pageFaults;
        }

        
    }
}
