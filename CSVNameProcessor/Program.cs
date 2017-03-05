using CSVNameProcessor.ReadWriteHelpers;
using Model;
using System;
using System.Collections.Generic;

namespace CSVNameProcessor
{
    class Program
    {
        private static IReadExcel _readExcel;
        private static IMainClass _mainClass;

        private static void Main(string[] args)
        {
            try
            {
                BootSystem();
                _mainClass.LoadSettings();
                string inputfileLocation = _mainClass.Settings.InputFile;
                string outputFolder = _mainClass.Settings.OutputFolder;
                Console.WriteLine($"Please ensure you have the data file and schema.ini in this location {inputfileLocation}, or change the location in web config");
                Console.WriteLine();
                Console.WriteLine($"The output files will be located at {outputFolder} or You can change the location to your preferred one in the web config");
                Console.WriteLine();
                Console.WriteLine("Please press 1 to continue 0 to exit");
                string userInput = Console.ReadLine();

                if (userInput != null && userInput.Equals("1"))
                {
                    _mainClass.ReadDataRows();
                    string sheetName = _mainClass._readExcel.GetFirstSheetName;
                    Console.WriteLine($"Sheet Name {sheetName}");
                    string HeaderNames = _mainClass._readExcel.HeaderNames;
                    Console.WriteLine($"Column Names {HeaderNames}");
                    PrintDataToScreen(_mainClass._addressBooks);
                    Console.WriteLine("Reading.........");
                    _mainClass.ProcessWordFrequency();
                    _mainClass.SortAddress();
                    _mainClass.WriteOutputToFile();
                    Console.WriteLine("Finished");
                    Console.ReadLine();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops an Error occurred");
                Console.ReadLine();
                //Write exception to log
            }
        }

        private static void BootSystem()
        {
            StartUp.Boot();
            _readExcel = StartUp.Container.GetInstance<IReadExcel>();
            _mainClass = StartUp.Container.GetInstance<IMainClass>();
        }


        private static void PrintDataToScreen(List<AddressBook> addressBooks)
        {
            addressBooks?.ForEach(delegate(AddressBook addressBook)
            {
                Console.WriteLine(addressBook.ToString());
            });
        }
    }
}
