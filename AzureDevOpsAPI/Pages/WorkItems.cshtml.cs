using AzureDevOpsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevOpsAPI.Pages
{
    public class WorkItemsModel : PageModel
    {
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
            var updatedWorkItem = this.WorkItemEntity;

            return RedirectToPage("Index"); // Redirect to the page where the list of WorkItems is displayed
        }
    }
}
