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
        IReadExcel _readExcel { get; set; }
        void ReadDataRows();
        List<AddressBook> _addressBooks { get; set; }
        List<WordStats> WordStats { get; set; }
        List<string> _address { get; set; }
        void SortAddress();
        void WriteOutputToFile();
    }

    public class MainClass : IMainClass
    {
        private readonly IProcessWordStats _processWordStats;
        private readonly IProcessAddress _processAddress;
        private readonly IWriteOutput writeOutput;

        public List<AddressBook> _addressBooks { get; set; }
        public Settings Settings { get; set; }
        public IReadExcel _readExcel { get; set; }
        public List<WordStats> WordStats { get; set; }
        public List<string> _address { get; set; }

        public MainClass(IReadExcel readExcel, IProcessWordStats processWordStats, IProcessAddress processAddress, IWriteOutput writeOutput)
        {
            _readExcel = readExcel;
            _processWordStats = processWordStats;
            _processAddress = processAddress;
            this.writeOutput = writeOutput;

            if (_readExcel == null)
                throw new ArgumentNullException("ReadExcel");
            if (_processWordStats == null)
                throw new ArgumentNullException("ProcessWordStats");

            if (_processAddress == null)
                throw new ArgumentNullException("ProcessAddress");

            if (this.writeOutput == null)
                throw new ArgumentNullException("WriteOutput");

            LoadSettings();

        }

        public void ReadDataRows()
        {
            _readExcel.FileName = Settings.InputFile;
            _addressBooks = _readExcel.GetDataRows();
        }


        public void SortAddress()
        {
            _processAddress._addressBooks = _addressBooks;
            _processAddress.SortAddress();
            _address = _processAddress._address;
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
            _processWordStats.AddressBooks = _addressBooks;
            _processWordStats.CalculateStats();
            WordStats = _processWordStats.WordStats;
        }

        public void WriteOutputToFile()
        {
            writeOutput.FileName = "Stats.txt";
            writeOutput.outputFolder = Settings.OutputFolder;
            var statsArrayList = ConvertToArrayList(WordStats);
            writeOutput.lines = statsArrayList;
            writeOutput.WriteToFile();

            writeOutput.FileName = "SortedAddress.txt";
            writeOutput.lines = _address.ToArray();
            writeOutput.WriteToFile();
        }

        private string[] ConvertToArrayList(List<WordStats> wordStats)
        {
            List<string> lst = new List<string>();
            wordStats?.ForEach(delegate (WordStats Stats)
            {
                lst.Add(Stats.ToString());
            });
            return lst.ToArray();
        }

    }
}
