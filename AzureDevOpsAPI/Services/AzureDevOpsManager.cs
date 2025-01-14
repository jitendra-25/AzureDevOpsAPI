using AzureDevOpsAPI.Helpers;
using AzureDevOpsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AzureDevOpsAPI.Services
{
    public class AzureDevOpsManager : IAzureDevOpsManager
    {
        const string DEVOPS_ORG_URL = "https://dev.azure.com/DemoJPOrg";
        private readonly IConfiguration _config;

        public AzureDevOpsManager(IConfiguration config)
        {
            this._config = config;
        }

        private string GetTokenConfig()
        {
            return _config.GetSection("AzureToken").Value;
        }

        private string GetProjectNameConfig()
        {
            return _config.GetSection("ProjectName").Value;
        }

        private VssConnection Authenticate()
        {
            string token = GetTokenConfig();
            string projectName = GetProjectNameConfig();

            var credentials = new VssBasicCredential(string.Empty, token);
            var connection = new VssConnection(new Uri(DEVOPS_ORG_URL), credentials);

            return connection;
        }

        public List<GitRepository> GetGitRepos()
        {
            var conn = Authenticate();

            using (GitHttpClient gitClient = conn.GetClient<GitHttpClient>())
            {
                var allRepos = gitClient.GetRepositoriesAsync().Result;
                return allRepos;
            }
        }

        public List<PipelineEntity> GetPipelines()
        {
            List<PipelineEntity> pipelines = new List<PipelineEntity>();

            string tokenFormat = string.Format("{0}:{1}", "", GetTokenConfig());
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(tokenFormat));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(DEVOPS_ORG_URL + "/" + GetProjectNameConfig() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                HttpResponseMessage response = client.GetAsync("_apis/pipelines").Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    dynamic deserialzedJson = JsonConvert.DeserializeObject<ExpandoObject>(jsonResult, new ExpandoObjectConverter());

                    foreach (var item in (IEnumerable<dynamic>)deserialzedJson.value)
                    {
                        PipelineEntity entity = new PipelineEntity()
                        {
                            Name = item.name,
                            URL = item._links.web.href
                        };

                        pipelines.Add(entity);
                    }
                }

                return pipelines;
            }
        }

        public SprintEntity GetSprintData()
        {
            //https://dev.azure.com/{organization}/{project}/_apis/work/teamsettings/iterations?api-version=6.0

            string tokenFormat = string.Format("{0}:{1}", "", GetTokenConfig());
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(tokenFormat));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(DEVOPS_ORG_URL + "/" + GetProjectNameConfig() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                HttpResponseMessage response = client.GetAsync("_apis/work/teamsettings/iterations?api-version=6.0").Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    SprintEntity sprintEntity = CustomJsonHelper.GetDeserializedJson<SprintEntity>(jsonResult);

                    GetWorkItemsForSprint(sprintEntity);

                    return sprintEntity;
                }
            }

            return null;
        }

        public void GetWorkItemsForSprint(SprintEntity sprintEntity)
        {
            string tokenFormat = string.Format("{0}:{1}", "", GetTokenConfig());
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(tokenFormat));

            foreach (Sprint sprintItem in sprintEntity.Sprints)
            {
                using (var client = new HttpClient())
                {
                    var workItemsUrl = sprintItem.Url + "/workitems";

                    client.BaseAddress = new Uri(DEVOPS_ORG_URL + "/" + GetProjectNameConfig() + "/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                    //Get List Of WorkItems Relations
                    HttpResponseMessage response = client.GetAsync(workItemsUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var workItemRelationsJsonResult = response.Content.ReadAsStringAsync().Result;
                        WorkItemRelationsEntity workItemRelationsEntity = CustomJsonHelper.GetDeserializedJson<WorkItemRelationsEntity>(workItemRelationsJsonResult);

                        foreach (var item in workItemRelationsEntity.workItemRelations)
                        {
                            response = client.GetAsync(item.target.url).Result;
                            var workItemJsonResult = response.Content.ReadAsStringAsync().Result;
                            WorkItemEntity workItemEntity = CustomJsonHelper.GetDeserializedJson<WorkItemEntity>(workItemJsonResult);
                            workItemEntity.SprintName = sprintItem.Name;

                            sprintEntity.SprintWorkItems.Add(workItemEntity);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates a WorkItem
        /// </summary>
        /// <param name="workItem">WorkItem object to update</param>
        /// <returns>WorkItemEntity</returns>
        public WorkItemEntity UpdateWorkItem(WorkItemEntity workItem)
        {
            // Request URI - https://dev.azure.com/DemoJPOrg/DemoAgileProject/_apis/wit/workitems/0?api-version=7.1-preview.3

            string tokenFormat = string.Format("{0}:{1}", "", GetTokenConfig());
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(tokenFormat));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(DEVOPS_ORG_URL + "/" + GetProjectNameConfig() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                string uri = $"_apis/wit/workitems/{workItem.id}?api-version=7.1-preview.3";

                var patchOperation = new List<Models.JsonPatchOperation>
                {
                    new() {
                        Op = "test",
                        Path = "/rev",
                        Value = workItem.rev
                    },

                    new() {
                        Op = "add",
                        Path = "/fields/System.State",
                        Value = workItem.fields.SystemState
                    },

                    new() {
                        Op = "add",
                        Path = "/fields/System.Description",
                        Value = workItem.fields.SystemDescription
                    }
                };

                var jsonContent = JsonConvert.SerializeObject(patchOperation);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json-patch+json");

                HttpResponseMessage response = client.PatchAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    WorkItemEntity workItemEntity = JsonConvert.DeserializeObject<WorkItemEntity>(responseBody);
                    return workItemEntity;
                }
            }

            return null;
        }

        public bool AddWorkItem(WorkItemEntity workItemEntity, IFormFile file)
        {
            // Request URI - POST https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/${type}?api-version=7.1

            string tokenFormat = string.Format("{0}:{1}", "", GetTokenConfig());
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(tokenFormat));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(DEVOPS_ORG_URL + "/" + GetProjectNameConfig() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                string uri = $"_apis/wit/workitems/${workItemEntity.fields.SystemWorkItemType}?api-version=7.1";

                var patchOperation = new List<Models.JsonPatchOperation>
                {
                    new() {
                        Op = "add",
                        Path = "/fields/System.Title",
                        Value = workItemEntity.fields.SystemTitle
                    },
                    new() {
                        Op = "add",
                        Path = "/fields/System.Description",
                        Value = workItemEntity.fields.SystemDescription
                    },
                    new() {
                        Op = "add",
                        Path = "/fields/System.IterationPath",
                        Value = workItemEntity.fields.SystemIterationPath                        
                    }
                };

                // Upload Attachment
                if (file != null)
                {
                    string attachmentUrl = UploadAttachment(file);
                    if(!string.IsNullOrEmpty(attachmentUrl))
                    {
                        patchOperation.Add(new Models.JsonPatchOperation
                        {
                            Op = "add",
                            Path = "/relations/-",
                            Value = new
                            {
                                rel = "AttachedFile",
                                url = attachmentUrl,
                                attributes = new { comment = "Uploaded file" }
                            }
                        });
                    }
                }

                var jsonContent = JsonConvert.SerializeObject(patchOperation);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json-patch+json");

                HttpResponseMessage response = client.PostAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
            }

            return false;
        }

        private string UploadAttachment(IFormFile file)
        {
            // Request URI - POST https://dev.azure.com/{organization}/{project}/_apis/wit/attachments?fileName={fileName}&api-version=7.1

            string tokenFormat = string.Format("{0}:{1}", "", GetTokenConfig());
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(tokenFormat));

            using (var client = new HttpClient())
            {
                string areaPath = GetProjectNameConfig();

                client.BaseAddress = new Uri(DEVOPS_ORG_URL + "/" + GetProjectNameConfig() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                string filePath = Path.Combine("uploads", file.FileName);
                using (var content = new StreamContent(File.OpenRead(filePath)))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    string uri = $"_apis/wit/attachments?fileName={file.FileName}&api-version=7.1";

                    HttpResponseMessage response = client.PostAsync(uri, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResult = response.Content.ReadAsStringAsync().Result;
                        dynamic deserializedJson = JsonConvert.DeserializeObject<ExpandoObject>(jsonResult, new ExpandoObjectConverter());
                        return deserializedJson.url;
                    }
                }
            }
            return null;
        }
    }
}