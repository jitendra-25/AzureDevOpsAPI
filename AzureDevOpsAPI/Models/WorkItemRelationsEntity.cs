using System.Collections.Generic;

namespace AzureDevOpsAPI.Models
{

    public class WorkItemRelationsEntity
    {
        public List<Workitemrelation> workItemRelations { get; set; }
        public string url { get; set; }
        public WokrItemLinks _links { get; set; }
    }

    public class WokrItemLinks
    {
        public WorkItemSelf self { get; set; }
        public WorkItemProject project { get; set; }
        public WorkItemTeam team { get; set; }
        public Teamiteration teamIteration { get; set; }
    }

    public class WorkItemSelf
    {
        public string href { get; set; }
    }

    public class WorkItemProject
    {
        public string href { get; set; }
    }

    public class WorkItemTeam
    {
        public string href { get; set; }
    }

    public class Teamiteration
    {
        public string href { get; set; }
    }

    public class Workitemrelation
    {
        public object rel { get; set; }
        public object source { get; set; }
        public Target target { get; set; }
    }

    public class Target
    {
        public int id { get; set; }
        public string url { get; set; }
    }

}
