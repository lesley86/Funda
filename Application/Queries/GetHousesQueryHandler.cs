using Core.Models;
using MediatR;

namespace Application.Queries
{
	public class GetHousesQueryHandler : IRequestHandler<GetHousesQuery, MakelaarWithTuinAndLocation>
	{
		private readonly IFundaService fundaservice;

		public GetHousesQueryHandler(IFundaService fundaservice)
		{
			this.fundaservice = fundaservice;
		}

		public async Task<MakelaarWithTuinAndLocation> Handle(GetHousesQuery request, CancellationToken cancellationToken)
		{
			return await fundaservice.GetHouses();
		}

	}
}
