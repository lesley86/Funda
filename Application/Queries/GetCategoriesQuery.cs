using Core.Models;
using MediatR;

namespace Application.Queries
{
	public class GetCategoriesQuery : IRequest<IEnumerable<DrinkCategory>>
	{
	}
}
