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
                ApiEndpoint.Post => "api/PostAPI",
                ApiEndpoint.Contact => "api/ContactAPI",
                ApiEndpoint.Appointment => "api/AppointmentAPI",
                ApiEndpoint.Patient => "api/Patient",
                ApiEndpoint.UserProfile => "api/UserProfileAPI",
                ApiEndpoint.Agent => "api/AgentAPI",
                ApiEndpoint.LeadAgent => "api/LeadAgentAPI",
                ApiEndpoint.Developer => "api/DeveloperAPI",
                ApiEndpoint.SystemAdmin => "api/SystemAdminAPI",
                ApiEndpoint.TeamLead => "api/TeamLeadAPI",
                ApiEndpoint.CompanyType => "api/CompanyTypeAPI",
                ApiEndpoint.Opptype => "api/OpptypeAPI",
                _ => throw new ArgumentException("Invalid API endpoint")
            };
        }
    }
    public enum ApiEndpoint
    {
        Activity,
        Opportunity,
        User,
        Post,
        Contact,
        Appointment,
        Patient,
        UserProfile,
        Agent,
        LeadAgent,
        Developer,
        SystemAdmin,
        TeamLead,
        CompanyType,
        Opptype

    }

}
