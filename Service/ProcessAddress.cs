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
        List<AddressBook> AddressBooks { get; set; }
        List<string> Address { get; set; }
        void SortAddress();
    }

    public class ProcessAddress : IProcessAddress
    {
        public List<AddressBook> AddressBooks { get; set; }
        public List<string> Address { get; set; }
        public Dictionary<string, string> AddressDict;
        readonly List<string> _worddelimiters = new List<string>{ "street","road","rd","str"};
        readonly char[] _delimiters = { ' ' };

        public void SortAddress()
        {
            AddressDict = new Dictionary<string, string>();
            AddressBooks?.ForEach(delegate(AddressBook addressBook)
            {
                ExtractStreetName(addressBook.Address.ToLower());
            });
            SortAddressDict();
        }

        private void SortAddressDict()
        {
            var list = AddressDict.Keys.ToList();
            list.Sort();
            List<string> addresslList = list.Select(key => AddressDict[key]).ToList();
            Address = addresslList;
        }

        private void ExtractStreetName(string address)
        {
            string[] words = address.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
            string breakword = string.Empty;

            foreach (var wordd in _worddelimiters)
            {
                for (int i =0;i<words.Length;i++)
                {
                    if (!words[i].Equals(wordd)) continue;
                    AddressDict.Add(words[i-1], address);
                    breakword = wordd;
                    break;
                }

                if (!string.IsNullOrEmpty(breakword)) break;
            }

        }
    }
}
