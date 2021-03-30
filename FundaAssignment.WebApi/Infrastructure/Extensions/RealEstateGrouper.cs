

using FundaAssignment.WebApi.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FundaAssignment.WebApi.Infrastructure.Extensions
{
    public static class RealEstateGrouper
    {
        public static IEnumerable<RealEstateAgent> GroupByAgent(this IEnumerable<RealEstate> realEstates)
        {
            return realEstates.GroupBy(item => item.AgentId).Select(group => new RealEstateAgent(group.First().AgentId, group.First().AgentName).AddListings(group));
        }
    }
}
