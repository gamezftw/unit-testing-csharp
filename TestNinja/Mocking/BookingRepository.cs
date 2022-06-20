namespace TestNinja.Mocking;

public interface IBookingRepository
{
    Booking? CheckOverlappingBooking(Booking booking, IQueryable<Booking> bookings);
    IQueryable<Booking> GetActiveBookings(int? bookingId);
}

public class BookingRepository : IBookingRepository
{
    private UnitOfWork _unitOfWork;

    public BookingRepository(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Booking> GetActiveBookings(int? bookingId = null)
    {
        return _unitOfWork.Query<Booking>()
                   .Where(b => b.Status != "Cancelled"
                   && (bookingId == null || b.Id != bookingId));

    }

    public Booking? CheckOverlappingBooking(Booking booking, IQueryable<Booking> bookings)
    {
        return bookings.FirstOrDefault(
                b =>
                    booking.ArrivalDate < b.DepartureDate
                    && b.ArrivalDate < booking.DepartureDate);
    }

}