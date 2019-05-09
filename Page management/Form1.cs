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
            RequestGenerator requestGenerator2 = new RequestGenerator(150, 250, 50, 100);
            RequestGenerator requestGenerator3 = new RequestGenerator(250, 350, 150, 1000);

            List<Request> requestsFIFO = requestGenerator.Generate(50);
            requestsFIFO.AddRange(requestGenerator2.Generate(50));
            requestsFIFO.AddRange(requestGenerator3.Generate(50));
            List<Request> requestLRU = requestsFIFO.ConvertAll(Request => (Request)Request.Clone());
            List<Request> requestALRU = requestsFIFO.ConvertAll(Request => (Request)Request.Clone());

            List<Page> pagesFIFO = new List<Page>();
            List<Page> pagesLRU = new List<Page>();
            List<Page> pagesALRU = new List<Page>();

            for (int i = 0; i <= 350; i++)
            {
                pagesFIFO.Add(new Page(i));
                pagesLRU.Add(new Page(i));
                pagesALRU.Add(new Page(i));
            }

            FIFO fifo = new FIFO(pagesFIFO, requestsFIFO, 4);
            LRU lru = new LRU(pagesLRU, requestLRU, 4);
            ALRU alru = new ALRU(pagesALRU, requestALRU, 4);
            int fifoFaults = fifo.Run();
            int lruFaults = lru.Run();
            int alruFaults = alru.Run();

            Console.WriteLine("Page faults: " + fifoFaults);
            Console.WriteLine("Page faults: " + lruFaults);
            Console.WriteLine("Page faults: " + alruFaults);
        }
    }
}
