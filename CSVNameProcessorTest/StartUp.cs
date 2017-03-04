using SimpleInjector;

namespace CSVNameProcessorTest
{
   public class StartUp
    {
        public static Container Container;

        public static void Boot()
        {
            Container = new Container();
            Container.Register<CSVNameProcessor.ReadWriteHelpers.IReadExcel, CSVNameProcessorTest.ReadExcel>();
            Container.Verify();

        }
    }
}
