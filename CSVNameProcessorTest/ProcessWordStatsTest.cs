using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVNameProcessor;
using CSVNameProcessor.ReadWriteHelpers;
using NUnit.Framework;
using Service;

namespace CSVNameProcessorTest
{
    public class ProcessWordStatsTest
    {

        [Test]
        public void ProcessWordStatsTest_ShouldCalculateWordFrequency()
        {
            //---------------Set up test pack-------------------
            ProcessWordStats processWordStats = new ProcessWordStats();
            IReadExcel readExcel = new ReadExcel();
            processWordStats._addressBooks = readExcel.GetDataRows();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            processWordStats.calculateStats();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, processWordStats.WordStats.Count);
            StringAssert.AreEqualIgnoringCase("Sashen", processWordStats.WordStats[0].Word);
            Assert.AreEqual(2, processWordStats.WordStats[0].Count);

        }
    }
}
