using CalcCDB.Appication.Api.Controllers;
using CalcCDB.Domain.Entities;
using CalcCDB.Domain.Interfaces;
using Moq;

namespace CalcCDB.Test
{
    public class CDBTest
    {
        private readonly Mock<ICDBService> _cdbService;
        private readonly CDBController _controller;

        public CDBTest()
        {
           _cdbService = new Mock<ICDBService>();
           _controller = new CDBController(_cdbService.Object);
        }
       
        [Test]
        public async Task CalCdbMonthsLessThanOne()
        {            
            var result = await _controller.CalCdb(1000, 1);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(400));           
        }

        [Test]
        public async Task CalCdbValueLessThanZero()
        {
            var result = await _controller.CalCdb(0, 5);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(400));
        }        

        [Test]
        public async Task CalCdbUntilSixMonths()
        {
            var result = await _controller.CalCdb(1000, 6);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(200));
        }

        [Test]
        public async Task CalCdbUntilTwelveMonths()
        {
            var result = await _controller.CalCdb(1000, 12);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(200));
        }

        [Test]
        public async Task CalCdbUntilTwentyFourMonthsMonths()
        {
            var result = await _controller.CalCdb(1000, 24);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(200));
        }

        [Test]
        public async Task CalCdbGreaterThanTwentyFourMonths()
        {
            var result = await _controller.CalCdb(1000, 25);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(200));
        }

        [Test]
        public void CalCdbResult()
        {
            var result = _cdbService.Setup(s => s.CalCdb(1000, 6)).ReturnsAsync(
                new InvestingResult()
                {
                    GrossYield = 1000,
                    NetValue = 1500
                });

            Assert.NotNull(result);
        }    
    }
}