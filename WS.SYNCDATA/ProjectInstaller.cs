using System.Collections;
using System.ComponentModel;

namespace WS.SYNCDATA
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        //protected override void OnBeforeInstall(IDictionary savedState)
        //{
        //    const string parameter = "MySource1\" \"MyLogFile1";
        //    Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
        //    base.OnBeforeInstall(savedState);
        //}
    }
}
