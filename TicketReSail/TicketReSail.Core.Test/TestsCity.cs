using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Services;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;
using FluentAssertions;

namespace TicketReSail.Core.Test
{
    public class TestsCity
    {
        private List<City> _cities;
        private Mock<DbSet<City>> _cityMock;
        private readonly Mock<TicketsContext> _mock = new Mock<TicketsContext>(new DbContextOptions<TicketsContext>());
        private CityService _cityService;
        private DataSeed _dataSeed = new DataSeed();

        [SetUp]
        public void Setup()
        {
            _cities = _dataSeed.GetCities();
            _cityMock = DbContextMock.GetQueryableMockDbSet(_cities);

            _mock.Setup(c => c.Cities).Returns(_cityMock.Object);

            _cityService = new CityService(_mock.Object);
        }

        [Test]
        public async Task WhetCorrectCityDTOProvided_ThenCityCreated()
        {
            // Arrange
            var dto = new CityDTO { Name = "Gomel" };
            var expected = new OperationDetails(true, "City is successfully created", "");

            // Act
            var result = await _cityService.Create(dto);
            
            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task WhetInCorrectCityDTOProvided_ThenCityCreated()
        {
            // Arrange
            var cityName = _cities[0].Name;
            var dto = new CityDTO { Name = cityName };
            var expected = new OperationDetails(false, "The city with this name still exists!", "Name");

            // Act
            var result = await _cityService.Create(dto);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task WhenCorrectCityIdProvided_ThenCityDelete()
        {
            // Arrange
            int expected = default;
            var cityName = _cities[0].Name;

            // Act
            await _cityService.Delete(0);
            var result = _cityService.GetCityIdByName(cityName);

            // Assert
            result.Should().Be(expected);
        }
    }
}