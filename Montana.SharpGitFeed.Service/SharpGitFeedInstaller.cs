using System.ServiceProcess;
using System.ComponentModel;
namespace Montana.SharpGitFeed.Service
{
    [RunInstaller(true)]
    public partial class SharpGitFeedInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller _serviceInstaller;
        private ServiceProcessInstaller _processInstaller;

        public SharpGitFeedInstaller()
        {
            _processInstaller = new ServiceProcessInstaller();
            _serviceInstaller = new ServiceInstaller();
            _processInstaller.Account = ServiceAccount.LocalSystem;
            _serviceInstaller.StartType = ServiceStartMode.Automatic;
            _serviceInstaller.ServiceName = "SharpGitFeed Service";
            Installers.Add(_serviceInstaller);
            Installers.Add(_processInstaller);
        }
    }
}
