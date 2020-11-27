using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace Shared.helpers
{
   public  class ConverterTime
    {
        public static Timestamp UtcConverter(DateTime date)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.SpecifyKind(date.Date, DateTimeKind.Unspecified), "Eastern Standard Time", "UTC").ToTimestamp();

        }
    }
}
