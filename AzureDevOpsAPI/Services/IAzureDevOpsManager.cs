using AzureDevOpsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System.Collections.Generic;

namespace AzureDevOpsAPI.Services
{
    public interface IAzureDevOpsManager
    {
        List<GitRepository> GetGitRepos();

        List<PipelineEntity> GetPipelines();

        SprintEntity GetSprintData();

        void GetWorkItemsForSprint(SprintEntity sprintEntity);

        WorkItemEntity UpdateWorkItem(WorkItemEntity workItem);

        bool AddWorkItem(WorkItemEntity workItem, IFormFile file);
    }
}