using Application;
using Application.Queries;
using AutoMapper;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Funda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakelaarHousesController : ControllerBase
    {
		private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
		private readonly IMediator mediator;

		public MakelaarHousesController(        
            IMapper mapper,
			IMemoryCache memoryCache,
            IMediator mediator)
        {
			this.mapper = mapper;
			this.memoryCache = memoryCache;
			this.mediator = mediator;
		}


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            MakelaarWithTuinAndLocationResponseModel result;
			if (!memoryCache.TryGetValue(CacheKeys.TopHousesForSaleInAmsterdam, out result))
			{
				var makerlaarsWithHighestObjectCount = await mediator.Send(new GetHousesQuery());
                result = mapper.Map<MakelaarWithTuinAndLocationResponseModel>(makerlaarsWithHighestObjectCount);
				memoryCache.Set(CacheKeys.TopHousesForSaleInAmsterdam, result, TimeSpan.FromDays(1));
			}

			return Ok(result);
        }  
    }
}
