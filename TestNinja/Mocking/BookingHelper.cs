﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public class BookingHelper
    {
        private IBookingRepository _bookingRepository;

        public BookingHelper(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public string OverlappingBookingsExist(Booking booking)
        {
            if (booking.IsBookingCancelled())
                return string.Empty;

            var bookings = _bookingRepository.GetActiveBookings(booking.Id);

            var overlappingBooking = _bookingRepository.CheckOverlappingBooking(booking, bookings);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        private const string BookingCancelledKeyword = "Cancelled";
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }

        public bool IsBookingCancelled()
        {
            return this.Status == BookingCancelledKeyword;
        }
    }
}