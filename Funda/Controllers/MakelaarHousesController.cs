using Application;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Funda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakelaarHousesController : ControllerBase
    {
        private readonly IFundaService fundaService;
		private readonly IMapper mapper;

		public MakelaarHousesController(
            IFundaService fundaService,
            IMapper mapper)
        {
            this.fundaService = fundaService;
			this.mapper = mapper;
		}


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var makerlaarsWithHighestObjectCount = await fundaService.GetHouses();
            var result = mapper.Map<MakelaarWithTuinAndLocationResponseModel> (makerlaarsWithHighestObjectCount);

			return Ok(result);
        }  
    }
}
