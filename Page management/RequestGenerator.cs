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

        public RequestGenerator(int pageLowerBound, int pageUpperBound, int timeLowerBound, int timeUpperBound)
        {
            lowerBoundID = pageLowerBound;
            higherBoundID = pageUpperBound;
            lowerBoundTime = timeLowerBound;
            higherBoundTime = timeUpperBound;
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
