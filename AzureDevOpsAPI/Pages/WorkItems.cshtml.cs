using AzureDevOpsAPI.Models;
using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevOpsAPI.Pages
{
    public class WorkItemsModel : PageModel
    {
        private readonly IAzureDevOpsManager _devOpsManager;

        public WorkItemsModel(IAzureDevOpsManager devOpsManager)
        {
            this._devOpsManager = devOpsManager;
        }

        [BindProperty]
        public WorkItemEntity WorkItemEntity { get; set; }

        public void OnGet()
        {
            if (TempData.TryGetValue("WorkItemEntity", out var workItemsEntity))
            {
                WorkItemEntity = JsonConvert.DeserializeObject<WorkItemEntity>(workItemsEntity.ToString());
            }
        }

        public IActionResult OnPost()
        {
            if (this.WorkItemEntity != null)
            {
                _devOpsManager.UpdateWorkItem(this.WorkItemEntity);
            }

            return RedirectToPage("Sprints");
        }
    }
}
