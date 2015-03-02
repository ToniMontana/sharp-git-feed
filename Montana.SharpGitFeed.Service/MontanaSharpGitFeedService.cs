using System;
using System.Configuration;
using System.ServiceProcess;
using Nancy.Hosting.Self;

namespace Montana.SharpGitFeed.Service
{
    public partial class MontanaSharpGitFeedService : ServiceBase
    {
        private NancyHost _nancyHost;

        public MontanaSharpGitFeedService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string baseUri = ConfigurationManager.AppSettings["BaseUri"];
            _nancyHost = new NancyHost(new Uri(baseUri));
            _nancyHost.Start();
        }

        protected override void OnStop()
        {
            _nancyHost.Stop();
        }
    }
}
