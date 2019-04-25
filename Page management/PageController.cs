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
        protected int t=0;

        public PageController(List<Page> diskPages,List<Request> requests, int memorySize)
        {
            this.diskPages = diskPages;
            this.memorySize = memorySize;
            this.requests = requests;
        }

        protected Page GetPage(int id)
        {
            foreach(Page page in memoryPages)
            {
                if (page.ID == id)
                {
                    return page;
                }
            }
            return null;
        }

        protected void LoadPage(int id)
        {
            List<Page> pagesToRemove = new List<Page>();
            foreach(Page page in diskPages)
            {
                if (page.ID == id)
                {
                    pagesToRemove.Add(page);
                    memoryPages.Add(page);
                    Console.WriteLine("Loaded page "+page.ID);
                }
            }

            foreach(Page page in pagesToRemove)
            {
                diskPages.Remove(page);
            }
        }

        protected void MakePagesOlder()
        {
            foreach(Page page in memoryPages)
            {
                page.TimeInMemory++;
            }
        }

        public abstract void Run();

    }
}
