using AzureDevOpsAPI.Models;
using AzureDevOpsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

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
        public string Type { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string IterationPath { get; set; }

        [BindProperty]
        public IFormFile FileUpload { get; set; }

        public IActionResult OnGet([FromQuery] Guid sprintId)
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FileUpload != null)
            {
                var filePath = Path.Combine("uploads", FileUpload.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FileUpload.CopyTo(stream);
                }
            }

            var newWorkItem = new WorkItemEntity
            {
                fields = new Fields
                {
                    SystemTitle = Title,
                    SystemDescription = Description,
                    SystemWorkItemType = Type,
                    SystemIterationPath = IterationPath
                }
            };

            _devOpsManager.AddWorkItem(newWorkItem, FileUpload);
            return RedirectToPage("Sprints");
        }
    }
}
