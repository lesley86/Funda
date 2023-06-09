
//using Application;
//using Application.FundaExternalApiModels;
//using Core.Models;
//using FluentAssertions;
//using Microsoft.AspNetCore.TestHost;
//using Moq;
//using NUnit.Framework;
//using Tests.Integration;

//namespace Test.Core
//{
//	[TestFixture]
//    public class Tests
//    {
//		private readonly CustomWebApplicationFactory<Program> _factory;
//		private readonly Mock<IHttpClientWrapper> clientWrapperMoq; 

//		public Tests()
//        {
//			_factory = new CustomWebApplicationFactory<Program>();
//			clientWrapperMoq = new Mock<IHttpClientWrapper>();

//		}


//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public async Task ShouldReturnObjectsOrderByDescendingBasedOnObjectCount()
//		{
//			CategoryDrink fundaResponses = CreateFundaExternalApiResponse();

//			clientWrapperMoq.Setup(x => x.GetAsync<CategoryDrink>(It.IsAny<string>())).ReturnsAsync(fundaResponses);

//			var client = _factory
//				.WithWebHostBuilder(builder =>
//					builder.ConfigureTestServices(ConfigureTestServices))
//				.CreateClient();

//			using var request = new HttpRequestMessage(HttpMethod.Get, $"/api/MakelaarHouses");
//			var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
//			var actual = await response.Content.ReadFromJsonAsync<CocktailCategoryResponseModel>();

//			actual!.MakelaarWithObjectsForSaleInLocation.ElementAt(0).MakelaarId.Should().Be("2");
//			actual!.MakelaarWithObjectsForSaleInLocation.ElementAt(0).ObjectCount.Should().Be(2);
//			actual!.MakelaarWithObjectsForSaleInLocation.ElementAt(1).MakelaarId.Should().Be("1");
//			actual!.MakelaarWithObjectsForSaleInLocation.ElementAt(1).ObjectCount.Should().Be(1);
//		}

//		private void ConfigureTestServices(IServiceCollection services)
//		{
//			services.AddSingleton<IHttpClientWrapper>(clientWrapperMoq.Object);
//		}

//		private static CategoryDrink CreateFundaExternalApiResponse()
//		{
//			return new CategoryDrink
//			{
//				Objects = new List<CateogryListExtenalApiResponse>()
//				{
//					new CateogryListExtenalApiResponse
//					{
//						MakelaarId = 1,
//						MakelaarNaam = "Moo"
//					},
//					new CateogryListExtenalApiResponse
//					{
//						MakelaarId = 2,
//						MakelaarNaam = "Moo2"
//					},
//					new CateogryListExtenalApiResponse
//					{
//						MakelaarId = 2,
//						MakelaarNaam = "Moo2"
//					}
//				}
//			};
//		}
//	}
//}