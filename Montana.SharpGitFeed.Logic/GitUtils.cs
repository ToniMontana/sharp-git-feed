using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace Montana.SharpGitFeed.Logic
{
    public static class GitUtils
    {
        public static IReadOnlyCollection<GitCommit> GetCommits(IEnumerable<string> repositoryFolders, int maxCountPerRepository)
        {
            var committs = new List<GitCommit>();

            foreach (var repositoryFolder in repositoryFolders)
            {
                try
                {
                    using (var repository = new Repository(repositoryFolder))
                    {
                        foreach (Commit commit in repository.Commits.Take(maxCountPerRepository))
                        {
                            committs.Add(new GitCommit
                            {
                                Id = commit.Id.ToString(),
                                AuthorName = commit.Author.Name,
                                AuthorEmail = commit.Author.Email,
                                AuthorWhen = commit.Author.When,
                                MessageShort = commit.MessageShort,
                                Message = commit.Message,
                                CommitterName = commit.Committer.Name,
                                CommitterEmail = commit.Committer.Email,
                                CommitterWhen = commit.Committer.When,
                                RepositoryName = Path.GetFileName(repositoryFolder)
                            });
                        }
                    }
                }
                catch
                {
                }
            }

            return committs;
        }
    }
}
