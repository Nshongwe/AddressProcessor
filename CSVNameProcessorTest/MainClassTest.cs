using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVNameProcessor;
using NSubstitute;
using NUnit.Framework;
using Service;
using CSVNameProcessor.ReadWriteHelpers;

namespace CSVNameProcessorTest
{
    public class MainClassTest
    {
        [Test]
        public void MainClassTest_ShouldThrowArgumentNullException_For_ReadExcel_WhenNot_Supplied()
        {
            //---------------Set up test pack-------------------


            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var argumentNullException =
                Assert.Throws<ArgumentNullException>(
                    () =>
                        new MainClass(null, Substitute.For<IProcessWordStats>(), Substitute.For<IProcessAddress>(),
                            Substitute.For<IWriteOutput>()));
            //---------------Test Result -----------------------
            Assert.AreEqual(argumentNullException.ParamName, "ReadExcel");
        }

        [Test]
        public void MainClassTest_ShouldThrowArgumentNullException_For_ProcessWordStats_WhenNot_Supplied()
        {
            //---------------Set up test pack-------------------


            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var argumentNullException =
                Assert.Throws<ArgumentNullException>(
                    () =>
                        new MainClass(Substitute.For<IReadExcel>(), null, Substitute.For<IProcessAddress>(),
                            Substitute.For<IWriteOutput>()));
            //---------------Test Result -----------------------
            Assert.AreEqual(argumentNullException.ParamName, "ProcessWordStats");
        }

        [Test]
        public void MainClassTest_ShouldThrowArgumentNullException_For_ProcessAddress_WhenNot_Supplied()
        {
            //---------------Set up test pack-------------------


            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var argumentNullException =
                Assert.Throws<ArgumentNullException>(
                    () =>
                        new MainClass(Substitute.For<IReadExcel>(), Substitute.For<IProcessWordStats>(), null,
                            Substitute.For<IWriteOutput>()));
            //---------------Test Result -----------------------
            Assert.AreEqual(argumentNullException.ParamName, "ProcessAddress");
        }

        [Test]
        public void MainClassTest_ShouldThrowArgumentNullException_For_WriteOutput_WhenNot_Supplied()
        {
            //---------------Set up test pack-------------------


            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var argumentNullException =
                Assert.Throws<ArgumentNullException>(
                    () =>
                        new MainClass(Substitute.For<IReadExcel>(), Substitute.For<IProcessWordStats>(),
                            Substitute.For<IProcessAddress>(), null));
            //---------------Test Result -----------------------
            Assert.AreEqual(argumentNullException.ParamName, "WriteOutput");
        }

        [Test]
        public void MainClassTest_ShouldSetSettings()
        {
            //---------------Set up test pack-------------------


            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var MainClass = new MainClass(new ReadExcel(), new ProcessWordStats(), new ProcessAddress(),
                new WriteOutput());
            //---------------Test Result -----------------------
            Assert.IsNotNull(MainClass.Settings);

        }
    }
}
