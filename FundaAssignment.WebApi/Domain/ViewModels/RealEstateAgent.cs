
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FundaAssignment.WebApi.Domain.ViewModels
{
    public class RealEstateAgent
    {
        public int Id { get; }
        public string Name { get; }
        public ImmutableList<RealEstate> Listings => _listings.ToImmutableList();

        private List<RealEstate> _listings;

        public RealEstateAgent(int id, string name)
        {
            Id = id;
            Name = name;
            _listings = new List<RealEstate>();
        }

        public RealEstateAgent AddListing(RealEstate realEstate)
        {
            _listings.Add(realEstate);
            return this;
        }
        public RealEstateAgent AddListings(IEnumerable<RealEstate> realEstates)
        {
            _listings.AddRange(realEstates);
            return this;
        }
    }
}
