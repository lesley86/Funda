using Application;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            var filteredHouses = await fundaService.GetHouses();
            var result = mapper.Map<IEnumerable<MakelaarWithObjectCountResponseModel>>(filteredHouses);

			return Ok(result);
        }  
    }
}
