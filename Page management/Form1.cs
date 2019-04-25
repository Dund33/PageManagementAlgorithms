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
            List<Request> requests = new List<Request>();
            requests.Add(new Request(1, 0));
            requests.Add(new Request(2, 1));
            requests.Add(new Request(3, 2));
            requests.Add(new Request(4, 3));

            List<Page> pages = new List<Page>();
            pages.Add(new Page(1));
            pages.Add(new Page(2));
            pages.Add(new Page(3));
            pages.Add(new Page(4));
            FIFO fifo = new FIFO(pages,requests,2);
            fifo.Run();
        }
    }
}
