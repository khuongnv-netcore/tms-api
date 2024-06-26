using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.Tms.Models.Enums
{
    public enum EScheduleStatus
    {
        ASSINGED,
        INPROCESS,
        REFUSE,
        PICKUPED,
        DELIVERIED,
        COMPLETED,
        DELAY,
        ONTIME,
        CANCEL
    }
}
