using System;
using System.Collections.Generic;

namespace Page_management
{
    class OPT : PageController
    {
        int currentI;
        public OPT(List<Page> diskPages, List<Request> requests, int memorySize)
            : base(diskPages, requests, memorySize)
        { }

        private int ComparePages(Page p1, Page p2)
        {
            int p1Occurences = 0;
            int p2Occurences = 0;

            for(int k = currentI; k < (currentI+300>=requestPool.Count ? requestPool.Count : currentI+300); k++)
            {
                if (requestPool[k].PageID == p1.ID) p1Occurences++;
                if (requestPool[k].PageID == p2.ID) p2Occurences++;
            }

            if (p1Occurences > p2Occurences) return 1;
            else if (p2Occurences > p1Occurences) return -1;
            else return 0;
        }

        private int CompareRequests(Request r1, Request r2)
        {
            if (r1.Time > r2.Time) return 1;
            else if (r1.Time < r2.Time) return -1;
            else return 0;
        }

        public override int Run()
        {
            requestPool.Sort(CompareRequests);
            for (int i = 0; i < requestPool.Count; i++)
            {
                currentI = i;
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
                        
                        memoryPages.Sort(ComparePages);
                        Page toRemove = memoryPages[0];
                        /*foreach (Page page in memoryPages)
                        {
                            if (requestPool[i+1].PageID != page.ID && requestPool[i+2].PageID != page.ID)
                            {
                                toRemove = page;
                                break;
                            }
                            
                        }*/
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
