using AzureDevOpsAPI.Models;
using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace AzureDevOpsAPI.Pages
{
    public class AddWorkItemModel : PageModel
    {
        private readonly IAzureDevOpsManager _devOpsManager;

        public AddWorkItemModel(IAzureDevOpsManager devOpsManager)
        {
            _devOpsManager = devOpsManager;
        }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string State { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        public Guid SprintId { get; set; }

        public IActionResult OnGet([FromQuery] Guid sprintId)
        {
            SprintId = sprintId;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Additional logic if needed
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newWorkItem = new WorkItemEntity
            {
                fields = new Fields
                {
                    SystemTitle = Title,
                    SystemState = State,
                    SystemIterationPath = $"DemoJPOrg\\DemoTeam\\Sprint {SprintId}"
                }
            };

            _devOpsManager.UpdateWorkItem(newWorkItem);
            return RedirectToPage("Sprints");
        }
    }
}
