using DeskBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DeskBooker.DataAccess.Repositories
{
    public class DeskBookingRepositoryTests
    {
        [Fact]
        public void ShouldSaveTheDeskBooking()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DeskBookerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSaveTheDeskBooking")
                .Options;

            var deskBooking = new DeskBooking
            {
                FirstName = "Marc",
                LastName = "Piñol",
                Date = new DateTime(2022, 9, 7),
                Email = "marc.pinol@tests.com",
                DeskId = 1
            };

            //Act
            using (var context = new DeskBookerContext(options))
            {
                var repository = new DeskBookingRepository(context);
                repository.Save(deskBooking);
            }

            //Assert
            using (var context = new DeskBookerContext(options))
            {
                var bookings = context.DeskBooking.ToList();
                var storedDeskBooking = Assert.Single(bookings);

                Assert.Equal(deskBooking.FirstName, storedDeskBooking.FirstName);
                Assert.Equal(deskBooking.LastName, storedDeskBooking.LastName);
                Assert.Equal(deskBooking.Date, storedDeskBooking.Date);
                Assert.Equal(deskBooking.Email, storedDeskBooking.Email);
                Assert.Equal(deskBooking.DeskId, storedDeskBooking.DeskId);

            }

        }
    }
}
