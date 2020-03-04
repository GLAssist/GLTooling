using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace GitBusiness
{
    public class GitService : IGitService
    {
        private readonly string RepoSource;
        private readonly UsernamePasswordCredentials Credentials;
        private readonly DirectoryInfo GitLocalFolderInfo;
        private readonly DirectoryInfo FileSourceFolderInfo;

        private const string _username = "sujeet235@yahoo.co.in";
        private const string _password = "GitHub@123";
        private const string _gitRepoUrl = "https://github.com/SujeetBag/GLTooling.git";
        private const string _gitLocalFolder = @"D:\GITRepo\GLTooling";
        private const string _fileSourceFolder = @"D:\GITRepo\Somefile";


        public GitService()
        {
            string username = _username;
            string password = _password;
            string gitRepoUrl = _gitRepoUrl;
            string gitLocalFolder = _gitLocalFolder;
            string fileSourceFolder = _fileSourceFolder;

            var gitfolder = new DirectoryInfo(gitLocalFolder);
            if (!gitfolder.Exists)
            {
                throw new Exception(string.Format("git Source folder '{0}' does not exist.", gitLocalFolder));
            }

            GitLocalFolderInfo = gitfolder;

            var fileSource = new DirectoryInfo(fileSourceFolder);
            if (!fileSource.Exists)
            {
                throw new Exception(string.Format("File Source folder '{0}' does not exist.", fileSourceFolder));
            }

            FileSourceFolderInfo = fileSource;

             Credentials = new UsernamePasswordCredentials
            {
                Username = username,
                Password = password
            };

            RepoSource = gitRepoUrl;
        }


        /// <summary>
        /// Commits all changes.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.Exception"></exception>
        public void CommitAllChanges(string message)
        {
            using (var repo = new Repository(GitLocalFolderInfo.FullName))
            {
                var files = GitLocalFolderInfo.GetFiles("*", SearchOption.AllDirectories).Select(f => f.FullName);
                repo.Stage(files);

                repo.Commit(message);
            }
        }

        /// <summary>
        /// Pushes all commits.
        /// </summary>
        /// <param name="remoteName">Name of the remote server.</param>
        /// <param name="branchName">Name of the remote branch.</param>
        /// <exception cref="System.Exception"></exception>
        public void PushCommits(string remoteName, string branchName)
        {
            using (var repo = new Repository(GitLocalFolderInfo.FullName))
            {
                var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == remoteName);
                if (remote == null)
                {
                    repo.Network.Remotes.Add(remoteName, RepoSource);
                    remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == remoteName);
                }

                var options = new PushOptions
                {
                    CredentialsProvider = (url, usernameFromUrl, types) => Credentials
                };

                repo.Network.Push(remote, branchName, options);
            }
        }
        public void AddFile(string fileName)
        {
            using (var repo = new Repository(GitLocalFolderInfo.FullName))
            {
                repo.Stage(string.Format($"{GitLocalFolderInfo.FullName}/{fileName}"));
            }
        }

        public void CopyFileService(string fileName)
        {
            File.Copy($"{FileSourceFolderInfo.FullName}/{fileName}", $"{GitLocalFolderInfo.FullName}/{fileName}");
        }

    }
}

