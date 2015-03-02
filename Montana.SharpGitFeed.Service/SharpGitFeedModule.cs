using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Montana.SharpGitFeed.Logic;
using Nancy;

namespace Montana.SharpGitFeed.Service
{
    public class SharpGitFeedModule : NancyModule
    {
        public SharpGitFeedModule()
        {
            RssGenerator rssGenerator = new RssGenerator(new RssSettings
            {
                ItemLinkFormat = ConfigurationManager.AppSettings["ItemLinkFormat"],
                Copyright = ConfigurationManager.AppSettings["Copyright"],
                ChannelTitle = ConfigurationManager.AppSettings["ChannelTitle"],
                ChannelLink = ConfigurationManager.AppSettings["ChannelLink"],
                ChannelDescription = ConfigurationManager.AppSettings["ChannelDescription"]
            });

            Get["/"] = parameters =>
            {
                var repositories = MemoryCache.Default.Get("sharp-git-feed") as IReadOnlyCollection<GitCommit>;

                if (repositories == null)
                {
                    var gitFolders = GitFolderLocator.GetGitFolders(ConfigurationManager.AppSettings["GitBaseFolders"].Split('|').ToArray());
                    int maxCountPerRepository = Convert.ToInt32(ConfigurationManager.AppSettings["MaxCommitts"]);
                    repositories = GitUtils.GetCommits(gitFolders, maxCountPerRepository).OrderByDescending(c => c.AuthorWhen).Take(maxCountPerRepository).OrderBy(c => c.AuthorWhen).ToList();
                    MemoryCache.Default.Set("sharp-git-feed", repositories, new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddMinutes(5)});
                }
              
                return new Response
                {
                    ContentType = "text/xml",
                    Contents = s => rssGenerator.WriteToStream(s, Encoding.UTF8, repositories)
                };
            };
        }
    }
}
