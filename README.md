# AzureDevOpsAPI
This respository demonstrates creating a .Net Core project and use Azure DevOps REST APIs to fetch Repository and Pipeline details from Azure DevOps portal.

Please replace following placeholders in the code - 
1. In AzureDevOpsManager.cs class - "https://dev.azure.com/{Your_DevOps_OrgURL}" -> Append your Azure DevOps Organization URL.
2. In appsettings.json - 
   a. {Your_AzureDevOpsToken} -> Replace with your Azure DevOps Token.
   b. {Your_AzureDevOpsProjectName} -> Replace with your Azure DevOps ProjectName.
