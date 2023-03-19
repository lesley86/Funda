using Application;
using Application.Exceptions;
using AutoFixture;
using FluentAssertions;

namespace Tests.Unit.ExteternalApi.Funda
{
	[TestFixture]
	public class FundaRelativeUrlBuilderUnitTests
	{
		private FundaRelativeUrlBuilder fundaRelativeUrlBuilder;
		private Fixture fixture;

		[SetUp]
		public void Setup()
		{
			fixture = new Fixture();
			fundaRelativeUrlBuilder = new FundaRelativeUrlBuilder();
		}

		[Test]
		public void ShouldReturnAUrlWithNoTuinIfNotSpecified()
		{
			// Arrange
			var key = fixture.Create<string>();
			var aanbodType = fixture.Create<string>();
			var locations = fixture.CreateMany<string>(1).ToList();
			var page = fixture.Create<int>();
			var pageSize = fixture.Create<int>();

			var expected = $"/json/{key}?type={aanbodType}&zo=/{string.Join(",", locations)}/&page={page}&pagesize={pageSize}";

			// Act
			var actual = fundaRelativeUrlBuilder.Build(key, aanbodType, locations, page, pageSize);

			// Assert
			actual.Should().Be(expected);

		}

		[Test]
		public void ShouldReturnTuinInUrlIfItIsSpecified()
		{
			// Arrange
			var key = fixture.Create<string>();
			var aanbodType = fixture.Create<string>();
			var locations = fixture.CreateMany<string>(1).ToList();
			var page = fixture.Create<int>();
			var pageSize = fixture.Create<int>();
			var tuin = fixture.Create<Tuin>();

			var expected = $"/json/{key}?type={aanbodType}&zo=/{string.Join(",", locations)}/tuin/&page={page}&pagesize={pageSize}";

			// Act
			var actual = fundaRelativeUrlBuilder.WithTuin(tuin).Build(key, aanbodType, locations, page, pageSize);

			// Assert
			actual.Should().Be(expected);

		}

		[TestCaseSource(nameof(_sourceLists))]
		public void ShouldReturnErrorIfMandatoryFieldsAreNotProvided(string key, string aanbodType, List<string> locations, int page, int pageSize)
		{
			// Arrange

			// Act
			Action actual = () => fundaRelativeUrlBuilder.Build(key, aanbodType, locations, page, pageSize);

			// Assert
			actual.Should().Throw<RequiredDataMissing>();

		}

		private static readonly object[] _sourceLists =
		{
			new object[] { "", "aanbodType", new List<string>{"Amsterdam"}, 1, 1},   
			new object[] { "key", "", new List<string> { "Amsterdam" }, 1, 1},
			new object[] { "key", "aanbodType", null, 1, 1},
			new object[] { "key", "aanbodType", new List<string>{"Amsterdam"}, -1, 1},
			new object[] { "key", "aanbodType", new List<string>{"Amsterdam"}, 1, -1}
		};
	}
}
