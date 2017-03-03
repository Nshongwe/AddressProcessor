using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AddressBook
    {
        public string FullName { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return FullName + "\t" + Address;
        }
    }
}
