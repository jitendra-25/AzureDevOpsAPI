using AzureDevOpsAPI.Models;
using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
            TempData["SprintEntity"] = JsonConvert.SerializeObject(SprintEntity);
        }

        public IActionResult OnGetUpdateWorkItems(int workItemId)
        {
            SprintEntity = JsonConvert.DeserializeObject<SprintEntity>(TempData["SprintEntity"] as string);
            var workItemEntity = SprintEntity.SprintWorkItems.Where(s => s.id == workItemId).FirstOrDefault();

            if (workItemEntity != null)
            {
                TempData["WorkItemEntity"] = JsonConvert.SerializeObject(workItemEntity);
            }
            return RedirectToPage("WorkItems");
        }
    }
}
