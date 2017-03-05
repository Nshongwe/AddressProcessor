using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVNameProcessor.ReadWriteHelpers;
using NUnit.Framework;
using Service;

namespace CSVNameProcessorTest
{
    public class ProcessAddressTest
    {
        private IReadExcel _readExcel;
        [SetUp]
        public void TestFixtureSetup()
        {
            StartUp.Boot();
            _readExcel = StartUp.Container.GetInstance<IReadExcel>();
        }

        
        [Test]
        public void ProcessAddressTestTest_ShouldSortResultsBasedOnStreetName()
        {
            //---------------Set up test pack-------------------

            ProcessAddress processAddress = new ProcessAddress();
            processAddress.AddressBooks = _readExcel.GetDataRows();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            processAddress.SortAddress();
            //---------------Test Result -----------------------
            StringAssert.AreEqualIgnoringCase("AgriSETA House 529 Belvedere Road Arcadia 0083", processAddress.Address[0]);
            StringAssert.AreEqualIgnoringCase("12th Floor Durban Bay House 333 Smith Street Durban", processAddress.Address[1]);

        }
    }
}
