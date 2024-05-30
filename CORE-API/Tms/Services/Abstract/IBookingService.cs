using CORE_API.CORE.Models.Views;
using CORE_API.Tms.Models.Entities;
using CORE_API.Tms.Models.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CORE_API.Tms.Services.Abstract
{
    public interface IBookingService
    {
        Task<List<Booking>> FilterBookingNo(string bookingNo);
        Task<List<BookingEx>> FilterEx(DateTime? Start, DateTime? End, List<string> bookingNos, int skip = 0, int count = 20);
        Task<List<ScheduleForBookingOutputResource>> LoadScheduleForBookingsEx(List<string> bookingIds);
        Task UpdateScheduleStatusForbooking(Guid bookingId);
    }
}
