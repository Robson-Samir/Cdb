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
        public async Task CalCdbMonthsLessThanZero()
        {            
            var result = await _controller.CalCdb(1000, 0);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(400));           
        }

        [Test]
        public async Task CalCdbValueLessThanZero()
        {
            var result = await _controller.CalCdb(0, 5);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(400));
        }

        [Test]
        public async Task CalCdbMonthsGreaterThan12()
        {
            var result = await _controller.CalCdb(1000, 14);
            Assert.That(result.GetType()?.GetProperty("StatusCode")?.GetValue(result), Is.EqualTo(400));
        }

        [Test]
        public async Task CalCdb()
        {
            var result = await _controller.CalCdb(1000, 6);
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