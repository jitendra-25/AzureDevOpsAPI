using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevOpsAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAzureDevOpsManager _devOpsManager;

        public IndexModel(ILogger<IndexModel> logger, IAzureDevOpsManager devOpsManager)
        {
            _logger = logger;
            this._devOpsManager = devOpsManager;
        }

        public List<GitRepository> Repositories { get; set; }

        public void OnGet()
        {
            Repositories = _devOpsManager.GetGitRepos();
        }
    }
}
