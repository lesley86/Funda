using Core.Models;
using Core.Repository;

namespace Application
{
	public class CocktailService : ICocktailService
	{
		private readonly IExternalCocktailApi externalCocktailApi;
		private readonly IkeyProvider keyProvider;
		private readonly IHouseRepository houseRepository;

		public CocktailService(
			IExternalCocktailApi fundaServiceRepository,
			IkeyProvider keyProvider,
			IHouseRepository houseRepository)
		{
			this.externalCocktailApi = fundaServiceRepository;
			this.keyProvider = keyProvider;
			this.houseRepository = houseRepository;
		}

		public async Task<IEnumerable<DrinkCategory>> GetCocktailCategories()
		{
			var categoryApiResponse = await externalCocktailApi.GetCategories(keyProvider.Get(), 1, 25);
			if (categoryApiResponse?.drinks == null)
			{
				return Enumerable.Empty<DrinkCategory>();
			}

			var result = categoryApiResponse.drinks.Select(drink => new DrinkCategory(drink.strCategory));
			return result;
		}
	}
}
