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

        public async Task<MakelaarWithTuinAndLocation> GetHouses()
        {
            var readModelForHousesInLocation = await GetMakelaarsForLocation();
			var readModelForHousesWithTuin = await GetMakelaarsWithTuin();
			return new MakelaarWithTuinAndLocation(readModelForHousesInLocation, readModelForHousesWithTuin);
		}

        private async Task<IEnumerable<MakelaarWithObjectCount>> GetMakelaarsForLocation()
        {
            var houses = await fundaServiceRepository.GetAsync(fundaKeyService.Get(), "koop", new List<string> { "amsterdam" }, null, 1, 25);
            if(houses?.Objects == null)
            {
                return Enumerable.Empty<MakelaarWithObjectCount>();
            }

            var result = houses.Objects.GroupBy(houseObject => houseObject.MakelaarId)
                                       .Select(groupedObject => new MakelaarWithObjectCount(groupedObject.Key.ToString(), groupedObject.First().MakelaarNaam, groupedObject.Count()));

            return result.OrderByDescending(x => x.ObjectCount).Take(10);
		}

		private async Task<IEnumerable<MakelaarWithObjectCount>> GetMakelaarsWithTuin()
        {
            var houses = await fundaServiceRepository.GetAsync(fundaKeyService.Get(), "koop", new List<string> { "amsterdam" }, new Tuin(), 1, 25);
			if (houses?.Objects == null)
			{
				return Enumerable.Empty<MakelaarWithObjectCount>();
			}

			var result = houses.Objects.GroupBy(houseObject => houseObject.MakelaarId)
									.Select(groupedObject => new MakelaarWithObjectCount(groupedObject.Key.ToString(), groupedObject.First().MakelaarNaam, groupedObject.Count()));

			return result.OrderByDescending(x => x.ObjectCount).Take(10);
		}
	}
}
