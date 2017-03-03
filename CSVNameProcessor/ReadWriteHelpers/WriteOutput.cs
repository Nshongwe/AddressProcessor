using System;
using System.IO;

namespace CSVNameProcessor.ReadWriteHelpers
{
    public interface IWriteOutput
    {
        string[] lines { get; set; }
        string FileName { get; set; }
        string outputFolder { get; set; }
        void WriteToFile();
    }

    public class WriteOutput : IWriteOutput
    {
        public  string[] lines { get; set; }
        public string FileName { get; set; }
        public string outputFolder { get; set; }
        public void WriteToFile()
        {
          // string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(outputFolder + FileName))
            {
                foreach (string line in lines)
                {
                    outputFile.WriteLine(line);
                }
            }
        }
    }
}
