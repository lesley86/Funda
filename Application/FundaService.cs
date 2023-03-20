using Core;
using Core.Models;
using Core.Repository;

namespace Application
{
	public class FundaService : IFundaService
	{
		private readonly IFundaExternalApi fundaExternalApi;
		private readonly IFundaKeyService fundaKeyService;
		private readonly IHouseRepository houseRepository;

		public FundaService(
			IFundaExternalApi fundaServiceRepository,
			IFundaKeyService fundaKeyService,
			IHouseRepository houseRepository)
		{
			this.fundaExternalApi = fundaServiceRepository;
			this.fundaKeyService = fundaKeyService;
			this.houseRepository = houseRepository;
		}

		public async Task<MakelaarWithTuinAndLocation> GetHouses()
		{
			var readModelForHousesInLocation = await GetMakelaarsForLocation();
			var readModelForHousesWithTuin = await GetMakelaarsWithTuin();

			var _ = houseRepository.AddOrUpdateHouse(readModelForHousesInLocation.Union(readModelForHousesWithTuin));
			var result = new MakelaarWithTuinAndLocation(readModelForHousesInLocation, readModelForHousesWithTuin);
			return result;
		}

		private async Task<IEnumerable<MakelaarWithObjectCount>> GetMakelaarsForLocation()
		{
			var houses = await fundaExternalApi.GetAsync(fundaKeyService.Get(), "koop", new List<string> { "amsterdam" }, null, "beschikbaar", 1, 25);
			if (houses?.Objects == null)
			{
				return Enumerable.Empty<MakelaarWithObjectCount>();
			}

			var result = houses.Objects.GroupBy(houseObject => houseObject.MakelaarId)
									   .Select(groupedObject => new MakelaarWithObjectCount(groupedObject.Key.ToString(), groupedObject.First().MakelaarNaam, groupedObject.Count(), false));

		  

			return result.OrderByDescending(x => x.ObjectCount).Take(10);
		}

		private async Task<IEnumerable<MakelaarWithObjectCount>> GetMakelaarsWithTuin()
		{
			var houses = await fundaExternalApi.GetAsync(fundaKeyService.Get(), "koop", new List<string> { "amsterdam" }, new Tuin(), "beschikbaar", 1, 25);
			if (houses?.Objects == null)
			{
				return Enumerable.Empty<MakelaarWithObjectCount>();
			}

			var result = houses.Objects.GroupBy(houseObject => houseObject.MakelaarId)
									.Select(groupedObject => new MakelaarWithObjectCount(groupedObject.Key.ToString(), groupedObject.First().MakelaarNaam, groupedObject.Count(), true));

			return result.OrderByDescending(x => x.ObjectCount).Take(10);
		}
	}
}
