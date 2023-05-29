using System;

namespace Chat.Application.Service.Utilities
{
    public interface IPersianDateTimeUtility
    {
        DateTime? ConvertPersianDateTimeToGregorian(string persianDateTime);
    }
}