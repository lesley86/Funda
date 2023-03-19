using Core.Models;

namespace Application
{
    public class FundaService : IFundaService
    {
        private readonly IFundaExternalApi fundaServiceRepository;
        private readonly IFundaKeyService fundaKeyService;

        public FundaService(
            IFundaExternalApi fundaServiceRepository,
            IFundaKeyService fundaKeyService)
        {
            this.fundaServiceRepository = fundaServiceRepository;
            this.fundaKeyService = fundaKeyService;
        }

        public async Task<IEnumerable<MakelaarWithObjectCount>> GetHouses()
        {
            var groupedMakelaars = await GetMakelaarsForLocation();
            return groupedMakelaars;
		}

        private async Task<IEnumerable<MakelaarWithObjectCount>> GetMakelaarsForLocation()
        {
            var houses = await fundaServiceRepository.GetAsync(fundaKeyService.Get(), "koop", new List<string> { "amsterdam" }, null, 1, 25);
            var result = houses.Objects.GroupBy(houseObject => houseObject.MakelaarId)
                                       .Select(groupedObject => new MakelaarWithObjectCount(groupedObject.Key.ToString(), groupedObject.First().MakelaarNaam, groupedObject.Count()));

            return result.OrderByDescending(x => x.ObjectCount).Take(10);
		}

		private async Task<IEnumerable<MakelaarWithObjectCount>> GetMakelaarsWithTuin()
        {
            var houses = await fundaServiceRepository.GetAsync(fundaKeyService.Get(), "koop", new List<string> { "amsterdam" }, new Tuin(), 1, 25);
			var result = houses.Objects.GroupBy(houseObject => houseObject.MakelaarId)
									.Select(groupedObject => new MakelaarWithObjectCount(groupedObject.Key.ToString(), groupedObject.First().MakelaarNaam, groupedObject.Count()));

			return result.OrderByDescending(x => x.ObjectCount).Take(10);
		}
	}
}
