using System.Diagnostics.CodeAnalysis;
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
            var options = CreateDbContextOptions(nameof(ShouldSaveTheDeskBooking));

            var deskBooking = CreateDeskBooking(1, new DateTime(2022, 9, 7));

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

        [Fact]
        public void ShouldGetAllOrderedByDate()
        {
            //Arrange
            var options = CreateDbContextOptions(nameof(ShouldGetAllOrderedByDate));

            var storedList = new List<DeskBooking>
            {
                CreateDeskBooking(1,new DateTime(2020, 1, 27)),
                CreateDeskBooking(2,new DateTime(2020, 1, 25)),
                CreateDeskBooking(3,new DateTime(2020, 1, 29))
            };

            var expectedList = storedList.OrderBy(x => x.Date).ToList();

            using (var context = new DeskBookerContext(options))
            {
                foreach (var deskBooking in storedList)
                {
                    context.Add(deskBooking);
                    context.SaveChanges();
                }
            }

            // Act
            List<DeskBooking> actualList;
            using (var context = new DeskBookerContext(options))
            {
                var repository = new DeskBookingRepository(context);
                actualList = repository.GetAll().ToList();
            }

            // Assert
            Assert.Equal(expectedList, actualList, new DeskBookingEqualityComparer());
        }

        private class DeskBookingEqualityComparer : IEqualityComparer<DeskBooking>
        {
            public bool Equals([AllowNull] DeskBooking x, [AllowNull] DeskBooking y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode([DisallowNull] DeskBooking obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        private DbContextOptions<DeskBookerContext> CreateDbContextOptions(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DeskBookerContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            return options;
        }

        private DeskBooking CreateDeskBooking(int id, DateTime date)
        {
            var deskBooking = new DeskBooking
            {
                Id = id,
                FirstName = "Marc",
                LastName = "Piñol",
                Date = date,
                Email = "marc.pinol@tests.com",
                DeskId = 1
            };
            return deskBooking;
        }
    }
}