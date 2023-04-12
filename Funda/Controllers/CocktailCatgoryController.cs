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
    public class CocktailCatgoryController : ControllerBase
    {
		private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
		private readonly IMediator mediator;

		public CocktailCatgoryController(        
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
			IEnumerable<CocktailCategoryResponseModel> result;
			if (!memoryCache.TryGetValue(CacheKeys.DrinkCategories, out result))
			{
				var drinkCategories = await mediator.Send(new GetCategoriesQuery());
                result = mapper.Map<IEnumerable<CocktailCategoryResponseModel>>(drinkCategories);
				memoryCache.Set(CacheKeys.DrinkCategories, result, TimeSpan.FromDays(1));
			}

			return Ok(result);
        }  
    }
}
