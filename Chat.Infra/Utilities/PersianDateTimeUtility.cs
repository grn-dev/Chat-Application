using System;
using Chat.Application.Service.Utilities;
using Chat.Domain.Attributes;
using MD.PersianDateTime.Standard;

namespace Chat.Infra.Utilities
{
    [Bean]
    public class PersianDateTimeUtility : IPersianDateTimeUtility
    {
        public DateTime? ConvertPersianDateTimeToGregorian(string persianDateTime)
        {
            if (int.TryParse(persianDateTime, out var intDate))
                return PersianDateTime.Parse(intDate).ToDateTime();

            if (long.TryParse(persianDateTime, out var longDate))
                return PersianDateTime.Parse(longDate).ToDateTime();

            return PersianDateTime.TryParse(persianDateTime, out var result)
                ? result.ToDateTime()
                : null;
        }
    }
}