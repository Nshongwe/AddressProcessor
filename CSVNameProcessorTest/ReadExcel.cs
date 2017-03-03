using CSVNameProcessor.ReadWriteHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace CSVNameProcessorTest
{
    public class ReadExcel : IReadExcel
    {
        public string FileName
        {
            get { throw new NotImplementedException(); }

            set{throw new NotImplementedException();}
        }

        public string GetFirstSheetName
        {
            get { throw new NotImplementedException(); }

            set{throw new NotImplementedException();}
        }

        public string HeaderNames
        {
            get { return "FullName,Address"; }

            set{throw new NotImplementedException();}
        }

        public List<AddressBook> GetDataRows()
        {
           List<AddressBook> mocAddressBook = new List<AddressBook>();
            mocAddressBook.Add(new AddressBook {FullName = "Sashen Pillay",Address = "AgriSETA House 529 Belvedere Road Arcadia 0083"});
            mocAddressBook.Add(new AddressBook { FullName = "Sashen Naidoo", Address = "12th Floor Durban Bay House 333 Smith Street Durban" });
            return mocAddressBook;
        }
    }
}
