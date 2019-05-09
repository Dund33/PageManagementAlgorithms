using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class ALRU : PageController
    {

        public ALRU(List<Page> diskPages, List<Request> requests, int memorySize)
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
            Page toRemove = null;

            for (int i = 0; i < memoryPages.Count; i++)
            {
                if (memoryPages[i].GotAChance)
                {
                    toRemove = memoryPages[i];
                    break;
                }
                else
                {
                    memoryPages[i].GotAChance = true;
                }
            }

            if (toRemove == null) Remove();
            else
            { 
                memoryPages.Remove(toRemove);
                diskPages.Add(toRemove);
                Console.WriteLine("Removing page: " + toRemove.ID);
            }
        }
    }
}
