using System;
using System.Collections.Generic;

namespace Page_management
{
    class Opt : PageController
    {
        public Opt(List<Page> diskPages, List<Request> requests, int memorySize)
            : base(diskPages, requests, memorySize)
        { }

        public override int Run()
        {
           
            for(int i = 0; i < requestPool.Count-2; i++)
            {

                Page usedPage = null;
                bool found = false;

                foreach(Page page in memoryPages)
                {
                    if(page.ID == requestPool[i].PageID)
                    {
                        usedPage = page;
                        found = true;
                        break;
                    }
                }

                if(!found)
                {
                    pageFaults++;
                    if (memoryPages.Count == memorySize)
                    {
                        Page toRemove = null;
                        foreach (Page page in memoryPages)
                        {
                            if (requestPool[i+1].PageID != page.ID && requestPool[i+2].PageID != page.ID)
                            {
                                toRemove = page;
                                break;
                            }
                            
                        }
                        memoryPages.Remove(toRemove);
                        diskPages.Add(toRemove);

                    }
                    else
                    {
                        Page requestedPage = null;
                        foreach(Page page in diskPages)
                        {
                            if(page.ID == requestPool[i].PageID)
                            {
                                requestedPage = page;
                                break;
                            }
                        }

                        diskPages.Remove(requestedPage);
                        memoryPages.Add(requestedPage);
                    }
                }

                
            }
            return pageFaults;
        }

        protected override void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
