using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Montana.SharpGitFeed.Logic
{
    public class RssGenerator
    {
        private readonly RssSettings _rssSettings;

        public RssGenerator(RssSettings rssSettings)
        {
            _rssSettings = rssSettings;
        }

        public void WriteToStream(Stream stream, Encoding encoding, IEnumerable<GitCommit> committs)
        {
            XmlTextWriter feedWriter = new XmlTextWriter(stream, encoding);
            feedWriter.WriteStartDocument();

            feedWriter.WriteStartElement("rss");
            feedWriter.WriteAttributeString("version", "2.0");

            feedWriter.WriteStartElement("channel");
            feedWriter.WriteElementString("title", _rssSettings.ChannelTitle);
            feedWriter.WriteElementString("link", _rssSettings.ChannelLink);
            feedWriter.WriteElementString("description", _rssSettings.ChannelDescription);
            feedWriter.WriteElementString("copyright", _rssSettings.Copyright);

            foreach (var commit in committs)
            {
                feedWriter.WriteStartElement("item");
                feedWriter.WriteElementString("title", string.Format("{0}: {1} - {2} ({3})", commit.AuthorName, commit.RepositoryName, commit.MessageShort, commit.AuthorWhen.ToString("yyyy-MM-dd HH:mm")));
                feedWriter.WriteElementString("description", commit.Message);
                feedWriter.WriteElementString("link", string.Format(_rssSettings.ItemLinkFormat, commit.Id));
                feedWriter.WriteElementString("pubDate", ToPubDate(commit.AuthorWhen));
                feedWriter.WriteElementString("guid", string.Format("sharp-git-feed:{0}:{1}", commit.RepositoryName, commit.Id));
                feedWriter.WriteEndElement();
            }

            feedWriter.WriteEndElement();
            feedWriter.WriteEndElement();
            feedWriter.WriteEndDocument();
            feedWriter.Flush();
            feedWriter.Close();
        }

        private static string ToPubDate(DateTimeOffset pubDate)
        {
            var value = pubDate.ToString("ddd',' d MMM yyyy HH':'mm':'ss") + " " + pubDate.ToString("zzzz").Replace(":", "");
            return value;
        }
    }
}
