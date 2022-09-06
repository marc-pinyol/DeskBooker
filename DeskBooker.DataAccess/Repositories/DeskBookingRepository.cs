using System;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;

namespace DeskBooker.DataAccess.Repositories;

public class DeskBookingRepository : IDeskBookingRepository
{
    private readonly DeskBookerContext _context;

    public DeskBookingRepository(DeskBookerContext context)
    {
        _context = context;
    }

    public void Save(DeskBooking deskBooking)
    {
        _context.DeskBooking.Add(deskBooking);
        _context.SaveChanges();
    }
}