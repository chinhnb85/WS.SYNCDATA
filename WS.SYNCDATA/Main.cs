using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Timers;

namespace WS.SYNCDATA
{
    public partial class Main : ServiceBase
    {
        private readonly string[] _args;
        private readonly EventLog _eventLog;
        private Timer _timeDelay;
        private int _eventId = 1;

        public Main(string[] args)
        {
            _args = args;
            InitializeComponent();

            var eventSourceName = "MySource";
            var logName = "MyNewLog";
            if (args.Any())
            {
                eventSourceName = args[0];
            }
            if (args.Count() > 1)
            {
                logName = args[1];
            }

            _eventLog = new EventLog();
            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }
            _eventLog.Source = eventSourceName;
            _eventLog.Log = logName;
        }

        protected override void OnStart(string[] args)
        {
            _eventLog.WriteEntry("In OnStart.");

            _timeDelay = new Timer {Interval = 60000};
            // 60 seconds  
            _timeDelay.Elapsed += OnTimer;
            _timeDelay.Start();

            //var serviceStatus = new ServiceStatus
            //{
            //    dwCurrentState = ServiceState.ServiceStartPending,
            //    dwWaitHint = 100000
            //};
            //SetServiceStatus(ServiceHandle, ref serviceStatus);

            //serviceStatus.dwCurrentState = ServiceState.ServiceRunning;
            //SetServiceStatus(ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            _eventLog.WriteEntry("In OnStop.");
            _timeDelay.Stop();
        }

        protected override void OnContinue()
        {
            _eventLog.WriteEntry("In OnContinue.");
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {            
            _eventLog.WriteEntry("Monitoring the System", EventLogEntryType.Information, _eventId++);
        }

        private void LogService(string content)
        {
            var fs = new FileStream(@"c:\ServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }

        //[DllImport("advapi32.dll", SetLastError = true)]
        //private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
    }

    //public enum ServiceState
    //{
    //    ServiceStopped = 0x00000001,
    //    ServiceStartPending = 0x00000002,
    //    ServiceStopPending = 0x00000003,
    //    ServiceRunning = 0x00000004,
    //    ServiceContinuePending = 0x00000005,
    //    ServicePausePending = 0x00000006,
    //    ServicePaused = 0x00000007,
    //}

    //[StructLayout(LayoutKind.Sequential)]
    //public struct ServiceStatus
    //{
    //    public int dwServiceType;
    //    public ServiceState dwCurrentState;
    //    public int dwControlsAccepted;
    //    public int dwWin32ExitCode;
    //    public int dwServiceSpecificExitCode;
    //    public int dwCheckPoint;
    //    public int dwWaitHint;
    //};
}
