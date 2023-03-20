using Core.Models;
using MediatR;

namespace Application.Queries
{
	public class GetHousesQuery : IRequest<MakelaarWithTuinAndLocation>
	{
	}
}
