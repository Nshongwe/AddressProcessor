using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WordStats
    {
        public string Word { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Word + " " + Count;
        }
    }
}
