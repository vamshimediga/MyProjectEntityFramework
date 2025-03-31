namespace MyProjectEntity
{
    public class ApiService
    {
        public string GetApiUrl(ApiEndpoint endpoint)
        {
            return endpoint switch
            {
                ApiEndpoint.Activity => "api/ActivityAPI",
                ApiEndpoint.Opportunity => "api/OpportunityAPI",
                ApiEndpoint.User =>  "api/UserAPI",
                _ => throw new ArgumentException("Invalid API endpoint")
            };
        }
    }
    public enum ApiEndpoint
    {
        Activity,
        Opportunity,
        User,
    }

}
