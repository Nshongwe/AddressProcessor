using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CSVNameProcessor.ReadWriteHelpers;

namespace CSVNameProcessor
{
    public interface IMainClass
    {
        void ProcessWordFrequency();
        void LoadSettings();
        Settings Settings { get; set; }
        IReadExcel ReadExcel { get; set; }
        void ReadDataRows();
        List<AddressBook> AddressBooks { get; set; }
        List<WordStats> WordStats { get; set; }
        List<string> Address { get; set; }
        void SortAddress();
        void WriteOutputToFile();
    }

    public class MainClass : IMainClass
    {
        private readonly IProcessWordStats _processWordStats;
        private readonly IProcessAddress _processAddress;
        private readonly IWriteOutput _writeOutput;

        public List<AddressBook> AddressBooks { get; set; }
        public Settings Settings { get; set; }
        public IReadExcel ReadExcel { get; set; }
        public List<WordStats> WordStats { get; set; }
        public List<string> Address { get; set; }

        public MainClass(IReadExcel readExcel, IProcessWordStats processWordStats, IProcessAddress processAddress, IWriteOutput writeOutput)
        {
            ReadExcel = readExcel;
            _processWordStats = processWordStats;
            _processAddress = processAddress;
            this._writeOutput = writeOutput;

            if (ReadExcel == null)
                throw new ArgumentNullException("ReadExcel");
            if (_processWordStats == null)
                throw new ArgumentNullException("ProcessWordStats");

            if (_processAddress == null)
                throw new ArgumentNullException("ProcessAddress");

            if (this._writeOutput == null)
                throw new ArgumentNullException("WriteOutput");

            LoadSettings();

        }

        public void ReadDataRows()
        {
            ReadExcel.FileName = Settings.InputFile;
            AddressBooks = ReadExcel.GetDataRows();
        }


        public void SortAddress()
        {
            _processAddress.AddressBooks = AddressBooks;
            _processAddress.SortAddress();
            Address = _processAddress.Address;
        }
        public void LoadSettings()
        {
            Settings = new Settings
            {
                InputFile = ConfigurationManager.AppSettings["InputFilePath"],
                OutputFolder = ConfigurationManager.AppSettings["OutputFolderPath"]
            };
        }

        public void ProcessWordFrequency()
        {
            _processWordStats.AddressBooks = AddressBooks;
            _processWordStats.CalculateStats();
            WordStats = _processWordStats.WordStats;
        }

        public void WriteOutputToFile()
        {
            _writeOutput.FileName = "Stats.txt";
            _writeOutput.OutputFolder = Settings.OutputFolder;
            var statsArrayList = ConvertToArrayList(WordStats);
            _writeOutput.Lines = statsArrayList;
            _writeOutput.WriteToFile();

            _writeOutput.FileName = "SortedAddress.txt";
            _writeOutput.Lines = Address.ToArray();
            _writeOutput.WriteToFile();
        }

        private string[] ConvertToArrayList(List<WordStats> wordStats)
        {
            List<string> lst = new List<string>();
            wordStats?.ForEach(delegate (WordStats stats)
            {
                lst.Add(stats.ToString());
            });
            return lst.ToArray();
        }

    }
}
