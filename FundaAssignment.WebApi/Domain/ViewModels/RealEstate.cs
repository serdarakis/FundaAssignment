
namespace FundaAssignment.WebApi.Domain.ViewModels
{
    public class RealEstate
    {
        public string Id { get; }
        public string Url { get; }
        public int AgentId { get; }
        public string AgentName { get; }

        public RealEstate(string id, string url, int agentId, string agentName)
        {
            Id = id;
            Url = url;
            AgentId = agentId;
            AgentName = agentName;
        }
    }
}
