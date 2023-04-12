using Core.Models;
using MediatR;

namespace Application.Queries
{
	public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<DrinkCategory>>
	{
		private readonly ICocktailService cocktailService;

		public GetCategoriesQueryHandler(ICocktailService cocktailService)
		{
			this.cocktailService = cocktailService;
		}

		public async Task<IEnumerable<DrinkCategory>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
		{
			return await cocktailService.GetCocktailCategories();
		}

	}
}
