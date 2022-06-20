using NUnit.Framework;

namespace Tests
{
    public class BookingHelperTests_OverlappingBookingsExist
    {
        private Mock<IBookingRepository> _bookingRepository;
        private BookingHelper _bookingHelper;

        private Booking _existingBooking;

        [SetUp]
        public void Setup()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _bookingHelper = new BookingHelper(_bookingRepository.Object);
            _existingBooking = new Booking()
            {
                Id = 1,
                Reference = "a",
                ArrivalDate = ArriveOn(2020, 1, 1),
                DepartureDate = DepartOn(2020, 1, 10)
            };
            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(new List<Booking>{
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeExistingBooking_ReturnEmptyString()
        {
            var booking =
                new Booking()
                {
                    Id = 2,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate),
                    Reference = "existingbooking"
                };

            var result = _bookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookignsReference()
        {
            var booking =
                new Booking()
                {
                    Id = 2,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate),
                    Reference = _existingBooking.Reference
                };

            var result = _bookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookignsReference()
        {
            var booking =
                new Booking()
                {
                    Id = 2,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Reference = _existingBooking.Reference
                };

            var result = _bookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookignsReference()
        {
            var booking =
                new Booking()
                {
                    Id = 2,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate),
                    Reference = _existingBooking.Reference
                };

            var result = _bookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsInTheMiddleAndFinishesAfterExistingBooking_ReturnExistingBookignsReference()
        {
            var booking =
                new Booking()
                {
                    Id = 2,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Reference = _existingBooking.Reference
                };

            var result = _bookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingsOverlapButExistingBookingIsCancelled_ReturnEmptyString()
        {
            _existingBooking.Status = "Cancelled";
            var booking =
                new Booking()
                {
                    Id = 2,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Reference = _existingBooking.Reference
                };

            var result = _bookingHelper.OverlappingBookingsExist(booking);
            Assert.That(result, Is.Empty);
        }
        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}