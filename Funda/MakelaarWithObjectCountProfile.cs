using AutoMapper;
using Core.Models;

namespace Funda
{
    public class MakelaarWithObjectCountProfile : Profile
	{
		public MakelaarWithObjectCountProfile()
		{
			CreateMap<DrinkCategory, CocktailCategoryResponseModel>();
			CreateMap<MakelaarWithTuinAndLocation, CocktailCategoryResponseModel>();
		}
	}
}
