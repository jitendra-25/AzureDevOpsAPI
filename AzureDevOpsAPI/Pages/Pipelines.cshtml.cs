using AzureDevOpsAPI.Models;
using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AzureDevOpsAPI.Pages
{
    public class PipelinesModel : PageModel
    {
        private readonly IAzureDevOpsManager _devOpsManager;

        public PipelinesModel(IAzureDevOpsManager devOpsManager)
        {
            this._devOpsManager = devOpsManager;
        }

        public List<PipelineEntity> Pipelines { get; set; }

        public void OnGet()
        {
            Pipelines = _devOpsManager.GetPipelines();
        }
    }
}
