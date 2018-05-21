using System.ServiceProcess;

namespace WS.SYNCDATA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var servicesToRun = new ServiceBase[]
            {
                new Main(args)
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
