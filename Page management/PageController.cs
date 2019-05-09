using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    abstract class PageController
    {
        protected int memorySize;
        protected List<Page> memoryPages = new List<Page>();
        protected List<Page> diskPages = new List<Page>();
        protected List<Request> requests = new List<Request>();
        protected List<Request> requestPool = new List<Request>();
        protected int t=0;
        protected int pageFaults = 0;

        public PageController(List<Page> diskPages,List<Request> requests, int memorySize)
        {
            this.diskPages = diskPages;
            this.memorySize = memorySize;
            this.requestPool = requests;
        }

        protected Page GetPage(int id)
        {
            foreach (Page page in memoryPages)
            {
                if (page.ID == id)
                {
                    return page;
                }
            }

            pageFaults++;
            if (memoryPages.Count == memorySize) Remove();
            LoadPage(id);
            return GetPage(id);
        }


        protected void LoadPage(int id)
        {
            Page foundPage = null;
            foreach(Page page in diskPages)
            {
                if (page.ID == id)
                {
                    foundPage = page;
                    memoryPages.Add(page);
                    //Console.WriteLine("Loaded page "+page.ID);
                }
            }

          
           diskPages.Remove(foundPage);
            
        }

        protected void MakePagesOlder()
        {
            foreach(Page page in memoryPages)
            {
                    page.TimeInMemory++;
            }
        }

        protected void MakePagesRequestOlder(Page exception)
        {
            foreach(Page page in memoryPages)
            {
                if(page!=exception)
                    page.TimeSinceReq++;
            }
        }

        protected void UpdateRequests()
        {
            List<Request> tmp = new List<Request>();

            foreach (Request request in requestPool)
            {
                if (request.Time <= t)
                {
                    tmp.Add(request);
                }
            }

            foreach(Request request in tmp)
            {
                requests.Add(request);
                requestPool.Remove(request);
            }
        }

        public abstract int Run();
        protected abstract void Remove();

    }
}
