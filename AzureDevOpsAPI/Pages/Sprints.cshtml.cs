using AzureDevOpsAPI.Models;
using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AzureDevOpsAPI.Pages
{
    public class SprintsModel : PageModel
    {
        private readonly IAzureDevOpsManager _devOpsManager;

        public SprintsModel(IAzureDevOpsManager devOpsManager)
        {
            this._devOpsManager = devOpsManager;
        }

        //public List<Sprint> Sprints { get; set; }
        public SprintEntity SprintEntity { get; set; }

        public void OnGet()
        {
            SprintEntity = _devOpsManager.GetSprintData();
        }
    }
}
