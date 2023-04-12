//using Application;
//using Application.FundaExternalApiModels;
//using AutoFixture;
//using Core;
//using Core.Repository;
//using FluentAssertions;
//using Moq;

//namespace Tests.Unit.Application
//{
//	[TestFixture]
//    public class Tests
//    {
//        private Mock<IkeyProvider> fundaKeyServiceMock;
//        private Mock<IExternalCocktailApi> fundaExternalApiMock;
//		private Mock<IHouseRepository> houseRepositoryMock;
//		private Fixture fixture;
//        private CocktailService fundaService;

//        [SetUp]
//        public void Setup()
//        {
//            fundaKeyServiceMock = new Mock<IkeyProvider>();
//            fundaExternalApiMock = new Mock<IExternalCocktailApi>();
//			houseRepositoryMock = new Mock<IHouseRepository>();
//			fixture = new Fixture();
//            fundaService = new CocktailService(fundaExternalApiMock.Object, fundaKeyServiceMock.Object, houseRepositoryMock.Object);
//        }

//        [Test]
//        public async Task WhenResultIsReturnedFromExternalApiShouldOrderAndTakeTop10()
//        {
//            // Arrange
//            var key = fixture.Create<string>();
//            var idForMakelaarWithSixListings = fixture.Create<int>();
//            var idForMakelaarWithThreeListing = fixture.Create<int>();

//            var objectResponse = CreateObjectResponses(idForMakelaarWithSixListings, 6)
//                .Union(CreateObjectResponses(idForMakelaarWithThreeListing, 3))
//                .Union(fixture.CreateMany<CateogryListExtenalApiResponse>(20))
//                .ToList();

//            SetupFundaKeyMock(key);
//            SetupExternalApi(new CategoryDrink { Objects = objectResponse });

//            // Act
//            var actual = await fundaService.GetCocktailCategories();

//            // Assert
//            AssertOnlyTopTenResultsWasReported(actual);
//            AssertObjectCountWasCorrectlyReported(idForMakelaarWithSixListings, idForMakelaarWithThreeListing, actual);
//            AssertDescendingOrderWasEnforced(idForMakelaarWithSixListings, idForMakelaarWithThreeListing, actual);

//        }

//        [Test]
//        public async Task WhenExternalApiReturnsNullEmptyListsAreReturned()
//        {
//            // Arrange
//            var key = fixture.Create<string>();

//            SetupFundaKeyMock(key);
//            SetupExternalApi(new CategoryDrink { Objects = null });

//            // Act
//            var actual = await fundaService.GetCocktailCategories();

//            // Assert
//            actual.MakelaarWithObjectsForSaleInLocation.Count().Should().Be(0);
//            actual.MakelaarWithObjectsForSaleInLocationWithTuin.Count().Should().Be(0);
//        }

//        private void SetupExternalApi(CategoryDrink responseFromExternalApi)
//        {
//            fundaExternalApiMock.Setup(x => x.GetCateogries(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<Tuin>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
//                            .ReturnsAsync(responseFromExternalApi);
//        }

//        private IEnumerable<CateogryListExtenalApiResponse> CreateObjectResponses(int idForMakelaarWithFiveListing, int amountOfObjectsToCreate)
//        {
//            var result = fixture.Build<CateogryListExtenalApiResponse>()
//               .With(x => x.MakelaarId, idForMakelaarWithFiveListing)
//               .CreateMany(amountOfObjectsToCreate);

//            return result;
//        }

//        private void SetupFundaKeyMock(string key)
//        {
//            fundaKeyServiceMock.Setup(x => x.Get()).Returns(key);
//        }

//        private static void AssertOnlyTopTenResultsWasReported(Core.Models.MakelaarWithTuinAndLocation actual)
//        {
//            actual.MakelaarWithObjectsForSaleInLocation.Count().Should().Be(10);
//            actual.MakelaarWithObjectsForSaleInLocationWithTuin.Count().Should().Be(10);
//        }

//        private static void AssertDescendingOrderWasEnforced(int idForMakelaarWithSixListings, int idForMakelaarWithThreeListing, Core.Models.MakelaarWithTuinAndLocation actual)
//        {
//            actual.MakelaarWithObjectsForSaleInLocation.ElementAt(0).MakelaarId.Should().Be(idForMakelaarWithSixListings.ToString());
//            actual.MakelaarWithObjectsForSaleInLocation.ElementAt(1).MakelaarId.Should().Be(idForMakelaarWithThreeListing.ToString());
//        }

//        private static void AssertObjectCountWasCorrectlyReported(int idForMakelaarWithSixListings, int idForMakelaarWithThreeListing, Core.Models.MakelaarWithTuinAndLocation actual)
//        {
//            actual.MakelaarWithObjectsForSaleInLocation.First(x => x.MakelaarId == idForMakelaarWithSixListings.ToString()).ObjectCount.Should().Be(6);
//            actual.MakelaarWithObjectsForSaleInLocation.First(x => x.MakelaarId == idForMakelaarWithThreeListing.ToString()).ObjectCount.Should().Be(3);
//        }
//    }
//}