using Core.Models;

namespace Application
{
    public interface ICocktailService
    {
		Task<IEnumerable<DrinkCategory>> GetCocktailCategories();
    }
}