using System;

namespace Montana.SharpGitFeed.Logic
{
    public class GitCommit
    {
        public string Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTimeOffset AuthorWhen { get; set; }
        public string CommitterName { get; set; }
        public string CommitterEmail { get; set; }
        public DateTimeOffset CommitterWhen { get; set; }
        public string MessageShort { get; set; }
        public string Message { get; set; }
        public string RepositoryName { get; set; }
    }
}
