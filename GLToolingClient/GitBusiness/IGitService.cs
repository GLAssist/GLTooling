using System;
using System.Collections.Generic;
using System.Text;

namespace GitBusiness
{
    public interface IGitService
    {
        void CommitAllChanges(string message);
        void PushCommits(string remoteName, string branchName);
        void AddFile(string fileName);
        void CopyFileService(string fileName);
    }
}
