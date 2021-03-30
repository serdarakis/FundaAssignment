using FundaAssignment.WebApi.Domain.ViewModels;
using FundaAssignment.WebApi.Infrastructure.Clients;
using FundaAssignment.WebApi.Infrastructure.Extensions;
using FundaAssignment.WebApi.Infrastructure.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FundaAssignment.WebApi.Infrastructure.Services
{
    public interface IFundaService
    {
        Task<IEnumerable<RealEstateAgent>> GetTopAgents(HouseType type, int depth = 10);
    }

    internal class FundaService : IFundaService
    {
        private readonly IFundaClient _fundaClient;
        private readonly IRealEstateMapper _realEstateMapper;

        public FundaService(IFundaClient fundaClient, IRealEstateMapper realEstateMapper)
        {
            _fundaClient = fundaClient;
            _realEstateMapper = realEstateMapper;
        }
        public async Task<IEnumerable<RealEstateAgent>> GetTopAgents(HouseType type, int depth = 10)
        {
            var client = new HttpClient();
            int page = 0;
            Dictionary<int, RealEstateAgent> _dictionary = new Dictionary<int, RealEstateAgent>();
            List<RealEstate> realEstates = new List<RealEstate>();
            while (true)
            {
                page++;
                var result = type == HouseType.HasGarden ? await _fundaClient.GetHausesWithGardenForSale(page) : await _fundaClient.GetHausesForSale(page);
                realEstates.AddRange(result.Objects.Select(item => _realEstateMapper.Map(item)));

                if (string.IsNullOrWhiteSpace(result.Paging.VolgendeUrl))
                    break;
            }

            return realEstates.GroupByAgent().OrderByDescending(item => item.Listings.Count).Take(depth);
        }
    }
}
