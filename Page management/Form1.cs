using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Page_management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            RequestGenerator requestGenerator = new RequestGenerator(0, 100, 0, 100);
            RequestGenerator requestGenerator2 = new RequestGenerator(200, 250, 0, 100);
            RequestGenerator requestGenerator3 = new RequestGenerator(450, 650, 50, 1000);

            List<Request> requestsFIFO = requestGenerator.Generate(500);
            requestsFIFO.AddRange(requestGenerator2.Generate(500));
            requestsFIFO.AddRange(requestGenerator3.Generate(500));
            List<Request> requestLRU = requestsFIFO.ConvertAll(Request => (Request)Request.Clone());
            List<Request> requestALRU = requestsFIFO.ConvertAll(Request => (Request)Request.Clone());
            List<Request> requestRandomA = requestsFIFO.ConvertAll(Request => (Request)Request.Clone());
            List<Request> requestOpt = requestsFIFO.ConvertAll(Request => (Request)Request.Clone());

            List<Page> pagesFIFO = new List<Page>();
            List<Page> pagesLRU = new List<Page>();
            List<Page> pagesALRU = new List<Page>();
            List<Page> pagesRandomA = new List<Page>();
            List<Page> pagesOPT = new List<Page>();

            for (int i = 0; i <= 650; i++)
            {
                pagesFIFO.Add(new Page(i));
                pagesLRU.Add(new Page(i));
                pagesALRU.Add(new Page(i));
                pagesRandomA.Add(new Page(i));
                pagesOPT.Add(new Page(i));
            }

            FIFO fifo = new FIFO(pagesFIFO, requestsFIFO, 64);
            LRU lru = new LRU(pagesLRU, requestLRU, 64);
            ALRU alru = new ALRU(pagesALRU, requestALRU, 64);
            RandomA randomA = new RandomA(pagesRandomA, requestRandomA, 64);
            Opt opt = new Opt(pagesOPT, requestOpt, 64);
            int fifoFaults = fifo.Run();
            int lruFaults = lru.Run();
            int alruFaults = alru.Run();
            int randomAFaults = randomA.Run();
            int optFaults = opt.Run();

            Console.WriteLine("FIFO faults: " + fifoFaults);
            Console.WriteLine("LRU faults: " + lruFaults);
            Console.WriteLine("ALRU faults: " + alruFaults);
            Console.WriteLine("Random page faults: " + randomAFaults);
            Console.WriteLine("Opt page faults: " + optFaults);
        }
    }
}
