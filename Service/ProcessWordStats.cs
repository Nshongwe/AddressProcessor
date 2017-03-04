using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface IProcessWordStats
    {
        List<AddressBook> AddressBooks { get; set; }
        List<WordStats> WordStats { get; set; }
        void CalculateStats();
    }

    public class ProcessWordStats : IProcessWordStats
    {
        public List<AddressBook> AddressBooks { get; set; }
        public List<WordStats> WordStats { get; set; }
        char[] _delimiters = { ' ' };

        public void CalculateStats()
        {
            string allWords = GetAllWords();
            string[] wordsList = SplitWords(allWords.ToLower());
            CalculateWordFrequency(wordsList);
            SortWordStats();
        }

        private void SortWordStats()
        {
             WordStats = WordStats.OrderByDescending(x => x.Count).ThenBy (x=>x.Word).ToList();
        }

        private string GetAllWords()
        {
            string words = string.Empty;
            AddressBooks?.ForEach(delegate(AddressBook addressBook)
            {
                words += addressBook.FullName + " ";
            });
            return words;
        }

        private string[] SplitWords(string allWords) =>  allWords.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

        private void CalculateWordFrequency(string [] wordsList)
        {
            List<WordStats> stats = new List<WordStats>();
            foreach (var word in wordsList)
            {
                if (stats?.FindAll(x => x.Word.Equals(word)).Count == 0)
                {
                    int count = wordsList.ToList().FindAll(x => x.Equals(word)).Count;
                    stats.Add(new WordStats {Word=word,Count=count});
                }
            }
            WordStats = stats;
        }


    }
}
