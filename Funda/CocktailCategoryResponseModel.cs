namespace Core.Models
{
	public class CocktailCategoryResponseModel
	{
		public string Name { get; set; }

		public CocktailCategoryResponseModel(string name)
		{
			Name = name;
		}
	}
}
