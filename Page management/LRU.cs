using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class LRU : PageController
    {
        public LRU(List<Page> diskPages, List<Request> requests, int memorySize)
            : base(diskPages, requests, memorySize)
        { }

        private void UpdateLastRequest(Page used)
        {
            foreach(Page page in memoryPages)
            {
                if (page == used)
                {
                    page.TimeSinceReq = 0;
                }
                else
                {
                    page.TimeSinceReq++;
                }
            }
        }


        protected override void Remove()
        {
            Page last = memoryPages[0];
            foreach(Page page in memoryPages)
            {
                if (page.TimeSinceReq > last.TimeSinceReq)
                {
                    last = page;
                }
            }
            memoryPages.Remove(last);
            diskPages.Add(last);
            Console.WriteLine("Removing page: " + last.ID);
        }

        public override int Run()
        {
            UpdateRequests();

            while (requests.Count > 0 || requestPool.Count > 0)
            {
                UpdateRequests();
                Page page = null;
                if (requests.Count > 0)
                {
                    page = GetPage(requests[0].PageID);
                    requests.Remove(requests[0]);
                    page.TimeSinceReq = 0;
                }
                MakePagesOlder();
                MakePagesRequestOlder(page);
                t++;
            }
            return pageFaults;
        }

    }
}
