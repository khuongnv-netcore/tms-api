using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.Tms.Models.Enums
{
    public enum EBookingStatus
    {
        ORDER,
        VIRTUAL,
        BOOKING,
        APPROVED,
        INPROCESS,
        DONE
    }
    public enum EScheduleStatusOfBooking
    {
        EMPTY,
        PARTIAL,
        FULL
    }
    public enum EBookingType
    {
        IMPORT,
        EXPORT,
        DOOR
    }
    public enum EUnitType
    {
        KG,
        GRAM
    }
}
