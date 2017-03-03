using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProcessWordStats
    {
        List<AddressBook> _addressBooks { get; set; }
        List<WordStats> WordStats { get; set; }
        void calculateStats();
    }

    public class ProcessWordStats : IProcessWordStats
    {
        public List<AddressBook> _addressBooks { get; set; }
        public List<WordStats> WordStats { get; set; }
        char[] delimiters = { ' ' };

        public void calculateStats()
        {
            string allWords = GetAllWords();
            string[] WordsList = SplitWords(allWords.ToLower());
            Process(WordsList);
            SortWordStats();

        }

        private void SortWordStats()
        {
             WordStats = WordStats.OrderByDescending(x => x.Count).ThenBy (x=>x.Word).ToList();
           
        }

        private string GetAllWords()
        {
            string words = string.Empty;
            _addressBooks?.ForEach(delegate(AddressBook addressBook)
            {
                words += addressBook.FullName + " ";
            });
            return words;
        }

        private string[] SplitWords(string allWords) =>  allWords.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        private void Process(string [] WordsList)
        {
            List<WordStats> stats = new List<WordStats>();
            foreach (var word in WordsList)
            {
                if (stats?.FindAll(x => x.Word.Equals(word)).Count == 0)
                {
                    int count = WordsList.ToList().FindAll(x => x.Equals(word)).Count;
                    stats.Add(new WordStats {Word=word,Count=count});
                }
            }
            WordStats = stats;
        }


    }
}
