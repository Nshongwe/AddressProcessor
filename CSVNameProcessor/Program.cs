using CSVNameProcessor.ReadWriteHelpers;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVNameProcessor
{
    class Program
    {
        private static IReadExcel readExcel;
        private static IMainClass mainClass;

        private static void Main(string[] args)
        {
            try
            {


                StartUp.Boot();
                readExcel = StartUp.Container.GetInstance<IReadExcel>();
                mainClass = StartUp.Container.GetInstance<IMainClass>();
                mainClass.LoadSettings();
                string inputfileLocation = mainClass.Settings.InputFile;
                string outputFolder = mainClass.Settings.OutputFolder;
                Console.WriteLine($"Please ensure you have the data file in this location {inputfileLocation}");
                Console.WriteLine($"The output files will be located at {outputFolder}");
                Console.WriteLine("Please press 1 to continue 0 to exit");
                string userInput = Console.ReadLine();

                if (userInput != null && userInput.Equals("1"))
                {
                    mainClass.GetDataRows();
                    string sheetName = mainClass._readExcel.GetFirstSheetName;
                    Console.WriteLine($"Sheet Name {sheetName}");
                    string ColumnName = mainClass._readExcel.HeaderNames;
                    Console.WriteLine($"Column Names {ColumnName}");
                    PrintData(mainClass._addressBooks);
                    mainClass.ProcessWordOccurrence();
                    mainClass.SortAddress();
                    mainClass.WriteOutput();
                    Console.ReadLine();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Oops Error occurred");
                //Write exception to log
            }
        }


        private static void PrintData(List<AddressBook> _addressBooks)
        {
            _addressBooks?.ForEach(delegate(AddressBook addressBook)
            {
                Console.WriteLine(addressBook.ToString());
            });
        }
    }
}
