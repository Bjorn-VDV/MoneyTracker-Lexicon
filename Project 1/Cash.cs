using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    internal class Cash
    {
        public int NewCash { get; set; }

        public Cash(string path)
        {
            this.NewCash = Convert.ToInt32(File.ReadLines(path).First());
        }
    }
}
