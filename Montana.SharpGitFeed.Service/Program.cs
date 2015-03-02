using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using Nancy.Hosting.Self;

namespace Montana.SharpGitFeed.Service
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Any(x => x.EndsWith("console", StringComparison.InvariantCultureIgnoreCase)))
            {
                string baseUri = ConfigurationManager.AppSettings["BaseUri"];

                using (var nancyHost = new NancyHost(new Uri(baseUri)))
                {
                    nancyHost.Start();
                    Console.WriteLine("Now listening - navigate to {0} to test. Press enter to stop", baseUri);
                    Console.ReadKey();
                }

                Console.WriteLine("Stopped. Good bye!");                
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new MontanaSharpGitFeedService() 
                };
                ServiceBase.Run(ServicesToRun);                
            }
        }
    }
}
