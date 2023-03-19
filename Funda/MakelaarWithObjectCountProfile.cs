using AutoMapper;
using Core.Models;

namespace Funda
{
    public class MakelaarWithObjectCountProfile : Profile
	{
		public MakelaarWithObjectCountProfile()
		{
			CreateMap<MakelaarWithObjectCount, MakelaarWithObjectCountResponseModel>();
		}
	}
}
