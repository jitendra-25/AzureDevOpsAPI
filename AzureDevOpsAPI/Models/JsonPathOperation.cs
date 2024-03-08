namespace AzureDevOpsAPI.Models
{
    public class JsonPatchOperation
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }

    }
}
