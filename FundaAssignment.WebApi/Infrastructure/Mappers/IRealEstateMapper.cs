using FundaAssignment.WebApi.Domain.FundaModels;
using FundaAssignment.WebApi.Domain.ViewModels;

namespace FundaAssignment.WebApi.Infrastructure.Mappers
{

    public interface IRealEstateMapper
    {
        RealEstate Map(Huis koopwoningen);
    }
    public class RealEstateMapper: IRealEstateMapper
    {
        public RealEstate Map(Huis koopwoningen) => 
            new RealEstate(
                koopwoningen.Id, 
                koopwoningen.URL, 
                koopwoningen.MakelaarId, 
                koopwoningen.MakelaarNaam
                );
    }
}
