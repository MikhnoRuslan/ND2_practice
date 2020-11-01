using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Services;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Test
{
    public class Tests
    {
        private List<City> _cities;

        [SetUp]
        public void Setup()
        {
            _cities = new List<City>
            {
                new City{ Name = "Brest"}
            };
        }

        [Test]
        public void WhetCorrectCityDTOProvided_ThenCityCreated()
        {
            var cityMock = DbContextMock.GetQueryableMockDbSet(_cities);

            var mock = new Mock<TicketsContext>();
            mock.Setup(c => c.Cities).Returns(cityMock.Object);

            var cityService = new CityService(mock.Object);
            var dto = new CityDTO { Name = "Brest" };
            var expected = new OperationDetails(true, "City is successfully created", "");

            var result = cityService.Create(dto);

            Assert.AreEqual(expected, result);
        }
    }
}