
using CSVNameProcessor.ReadWriteHelpers;
using Service;
using SimpleInjector;



namespace CSVNameProcessor
{
    class StartUp
    {
        public static Container Container;

        public static void Boot()
        {
            Container = new Container();
            Container.Register<IProcessWordStats, ProcessWordStats>();
            Container.Register<IMainClass, MainClass>();
            Container.Register<IWriteOutput, WriteOutput>();
            Container.Register<IReadExcel, ReadExcel>();
            Container.Register<IProcessAddress, ProcessAddress>();
            Container.Verify();

        }
    }
}
