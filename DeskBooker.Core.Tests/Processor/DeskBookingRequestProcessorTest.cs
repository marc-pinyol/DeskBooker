using System;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Xunit;

namespace DeskBooker.Core.Tests.Processor
{
    public class DeskBookingRequestProcessorTest
    {
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //Arrange
            var request = new DeskBookingRequest
            {
                FirstName = "Marc",
                LastName = "Piñol",
                Email = "marc.pinol@test.com",
                Date = new DateTime(2022, 03, 29)
            };
            
            var processor = new DeskBookingRequestProcessor();

            //Act
            DeskBookingResult result = processor.BookDesk(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }
    }
}
