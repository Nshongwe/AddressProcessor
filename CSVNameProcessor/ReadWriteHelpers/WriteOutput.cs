using System;
using System.IO;

namespace CSVNameProcessor.ReadWriteHelpers
{
    public interface IWriteOutput
    {
        string[] Lines { get; set; }
        string FileName { get; set; }
        string OutputFolder { get; set; }
        void WriteToFile();
    }

    public class WriteOutput : IWriteOutput
    {
        public string[] Lines { get; set; }
        public string FileName { get; set; }
        public string OutputFolder { get; set; }
        public void WriteToFile()
        {
            using (StreamWriter outputFile = new StreamWriter(OutputFolder + FileName))
            {
                foreach (string line in Lines)
                {
                    outputFile.WriteLine(line);
                }
            }
        }
    }
}
