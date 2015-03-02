using System.Collections.Generic;
using System.IO;

namespace Montana.SharpGitFeed.Logic
{
    public static class GitFolderLocator
    {
        public static IReadOnlyCollection<string> GetGitFolders(IEnumerable<string> baseFolders)
        {
            List<string> result = new List<string>();

            foreach (var baseFolder in baseFolders)
            {
                if (File.Exists(Path.Combine(baseFolder, "HEAD")))
                {
                    result.Add(baseFolder);
                }

                foreach (var subFolder in new DirectoryInfo(baseFolder).GetDirectories())
                {
                    if (File.Exists(Path.Combine(subFolder.FullName, "HEAD")))
                    {
                        result.Add(subFolder.FullName);
                    }
                }
            }

            return result;
        }
    }
}
