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
                Console.WriteLine($"The test data file and schema.ini is located at {inputfileLocation}, \nbut you can change the location in web config.\nThe file format is [Full Name,Address]");
                Console.WriteLine();
                Console.WriteLine($"The output files will be located at {outputFolder} \nor you can change the location to your preferred one in the web config");
                Console.WriteLine();
                Console.WriteLine("Please press 1 to continue 0 to exit");
                string userInput = Console.ReadLine();

                if (userInput != null && userInput.Equals("1"))
                {
                    _mainClass.ReadDataRows();
                    string sheetName = _mainClass.ReadExcel.GetFirstSheetName;
                    Console.WriteLine($"Sheet Name {sheetName}");
                    string HeaderNames = _mainClass.ReadExcel.HeaderNames;
                    Console.WriteLine($"Column Names {HeaderNames}");
                    PrintDataToScreen(_mainClass.AddressBooks);
                    Console.WriteLine("Reading.........");
                    _mainClass.ProcessWordFrequency();
                    _mainClass.SortAddress();
                    _mainClass.WriteOutputToFile();
                    Console.WriteLine("Finished, cheers");
                    Console.ReadLine();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops an error occurred");
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
