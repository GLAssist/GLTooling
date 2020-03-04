using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GLToolingClient.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private IEmployee _employee;
        private IGitService _gitService;

        public GitHubController(IGitService gitService) {

            this._gitService = gitService;
        }

        public string help()
        {
            return "Please help me.";
        }

        /// <summary>
        /// this function is used to commit in local git repository.
        /// </summary>
        /// <param name="message"></param>
        public void commit(string message)
        {
            _gitService.CommitAllChanges(message);
        }
        /// <summary>
        /// this file is used to push to file to git repository.
        /// </summary>
        /// <param name="remoteName"></param>
        /// <param name="branchName"></param>
        public void push(string remoteName, string branchName)
        {
            _gitService.PushCommits(remoteName, branchName);
        }

        /// <summary>
        /// this function used for staging the git repository. 
        /// </summary>
        /// <param name="fileName"></param>
        public void Add(string fileName)
        {
            _gitService.AddFile(fileName);
        }
        /// <summary>
        /// copying file to git local repository
        /// </summary>
        /// <param name="fileName"></param>
        public void copyfiletolocalgitrepo(string fileName)
        {
            _gitService.CopyFileService(fileName);
        }
    }
}