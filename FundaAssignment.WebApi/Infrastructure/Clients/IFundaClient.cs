using FundaAssignment.WebApi.Domain.Exceptions;
using FundaAssignment.WebApi.Domain.FundaModels;
using FundaAssignment.WebApi.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FundaAssignment.WebApi.Infrastructure.Clients
{
    public interface IFundaClient
    {
        Task<Koopwoningen> GetHausesForSale(int page = 0);
        Task<Koopwoningen> GetHausesWithGardenForSale(int page = 0);
    }

    internal class FundaClient : IFundaClient
    {
        private const string ApiKey = "ac1b0b1572524640a0ecc54de453ea9f";
        private const string BaseAddress = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json";
        private const int MaxNumberOfRequestPerMinute = 95;
        private readonly HttpClient _httpClient;
        private List<DateTime> _calls = new List<DateTime>();
        private object locker = new object();
        public FundaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Koopwoningen> GetHausesWithGardenForSale(int page = 1)
        {
            CheckRequestLimit();
            var parameters = new Dictionary<string, string>
            {
                { "type", "koop" },
                { "zo", "/amsterdam/tuin/" }
            };

            if (page > 0)
                parameters.Add("page", page.ToString());

            parameters.Add("pagesize", "25");
            var url = $"{BaseAddress}/{ApiKey}/?{string.Join('&', parameters.Select(item => $"{item.Key}={item.Value}"))}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new ProxyServerException();

            var data = await response.Content.ReadAsStringAsync();

            return data.ToObject<Koopwoningen>();
        }

        public async Task<Koopwoningen> GetHausesForSale(int page = 1)
        {
            CheckRequestLimit();
            var parameters = new Dictionary<string, string>
            {
                { "type", "koop" },
                { "zo", "/amsterdam/" }
            };

            if (page > 0)
                parameters.Add("page", page.ToString());

            parameters.Add("pagesize", "25");
            var url = $"{BaseAddress}/{ApiKey}/?{string.Join('&', parameters.Select(item => $"{item.Key}={item.Value}"))}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new ProxyServerException();

            var data = await response.Content.ReadAsStringAsync();

            return data.ToObject<Koopwoningen>();
        }

        private void CheckRequestLimit()
        {
            //TODO Time Provider
            lock (locker)
            {
                var time = DateTime.Now;
                _calls.RemoveAll(item => (time - item).Minutes >= 1);
                if (_calls.Count >= MaxNumberOfRequestPerMinute)
                    throw new ToManyRequestException();
                _calls.Add(time);
            }

        }
    }
}
