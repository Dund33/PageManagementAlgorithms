using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_management
{
    class RequestGenerator
    {
        private int lowerBoundID;
        private int higherBoundID;

        private int lowerBoundTime;
        private int higherBoundTime;

        public RequestGenerator(int lb, int hb, int lbT, int hbT)
        {
            lowerBoundID = lb;
            higherBoundID = hb;
            lowerBoundTime = lbT;
            higherBoundTime = hbT;
        }

        public List<Request> Generate(int n)
        {
            Random random = new Random();

            List<Request> requests = new List<Request>();

            for(int i = 0; i < n; i++)
            {
                int t = random.Next(lowerBoundTime, higherBoundTime+1);
                int id = random.Next(lowerBoundID, higherBoundID+1);
                requests.Add(new Request(id, t));
            }

            return requests;
        }
    }
}
