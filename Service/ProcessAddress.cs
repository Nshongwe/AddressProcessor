using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProcessAddress
    {
        List<AddressBook> _addressBooks { get; set; }
        List<string> _address { get; set; }
        void SortAddress();
    }

    public class ProcessAddress : IProcessAddress
    {
        public List<AddressBook> _addressBooks { get; set; }
        public List<string> _address { get; set; }
        public Dictionary<string, string> _addressDict;
        List<string> Worddelimiters = new List<string>{ "street","road","rd","str"};
        char[] delimiters = { ' ' };

        public void SortAddress()
        {
            _addressDict = new Dictionary<string, string>();
            _addressBooks?.ForEach(delegate(AddressBook addressBook)
            {
                ExtractStreetName(addressBook.Address.ToLower());
            });
            SortAddressDict();
        }

        private void SortAddressDict()
        {
            var list = _addressDict.Keys.ToList();
            list.Sort();
            List<string> addresslList = list.Select(key => _addressDict[key]).ToList();
            _address = addresslList;
        }

        private void ExtractStreetName(string address)
        {
            string[] words = address.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string breakword = string.Empty;

            foreach (var wordd in Worddelimiters)
            {
                for (int i =0;i<words.Length;i++)
                {
                    if (!words[i].Equals(wordd)) continue;
                    _addressDict.Add(words[i-1], address);
                    breakword = wordd;
                    break;
                }

                if (!string.IsNullOrEmpty(breakword)) break;
            }

        }
    }
}
