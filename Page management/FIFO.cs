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

        private void RemoveOldestPage()
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

        public override void Run()
        {
            List<Request> requestsToRemove = new List<Request>();
            while (requests.Count > 0)
            {
                requestsToRemove = new List<Request>();
                foreach (Request request in requests)
                {
                    if (request.Time <= t)
                    {
                        Page page = GetPage(request.PageID);
                        if (page == null)
                        { 
                            if (memoryPages.Count > memorySize)
                            {
                                RemoveOldestPage();
                            }
                        
                            LoadPage(request.PageID);
                        }

                        requestsToRemove.Add(request);
                    }
                    MakePagesOlder();
                }

                foreach(Request request in requestsToRemove) {
                    requests.Remove(request);
                }
                t++;
            }
        }
    }
}
