﻿using System;
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
        private IReadExcel _readExcel;
        [SetUp]
        public void TestFixtureSetup()
        {
            StartUp.Boot();
            _readExcel = StartUp.Container.GetInstance<IReadExcel>();
        }

        [Test]
        public void ProcessWordStatsTest_ShouldCalculateWordFrequency_SortResults()
        {
            //---------------Set up test pack-------------------
            ProcessWordStats processWordStats = new ProcessWordStats();
           processWordStats.AddressBooks = _readExcel.GetDataRows();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            processWordStats.CalculateStats();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, processWordStats.WordStats.Count);
            StringAssert.AreEqualIgnoringCase("Sashen", processWordStats.WordStats[0].Word);
            Assert.AreEqual(2, processWordStats.WordStats[0].Count);
            StringAssert.AreEqualIgnoringCase("Naidoo", processWordStats.WordStats[1].Word);

        }
    }
}
