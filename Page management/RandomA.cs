using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class RandomA : PageController
    {

        public RandomA(List<Page> diskPages, List<Request> requests, int memorySize)
            : base(diskPages, requests, memorySize)
        { }

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

        protected override void Remove()
        {
            Random random = new Random();
            int toRemove = random.Next(0, memoryPages.Count);
            Page page = memoryPages[toRemove];
            memoryPages.Remove(page);
            diskPages.Add(page);
        }
    }
}
