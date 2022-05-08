using AzureDevOpsAPI.Models;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System.Collections.Generic;

namespace AzureDevOpsAPI.Services
{
    public interface IAzureDevOpsManager
    {
        List<GitRepository> GetGitRepos();

        List<PipelineEntity> GetPipelines();
    }
}