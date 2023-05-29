using Chat.Domain.Core.SeedWork;

namespace Chat.Application.Configuration.Exceptions
{
    public class ApplicationHubErrorCode : Enumeration
    {
        public static ApplicationHubErrorCode INCORRECT_FILTER = new ApplicationHubErrorCode(1, nameof(INCORRECT_FILTER), "فیلتر گذاری اشتباه می باشد");
        public static ApplicationHubErrorCode CANT_DELETE_UPDATE = new ApplicationHubErrorCode(2, nameof(CANT_DELETE_UPDATE), "به دلیل استفاده شدن قابل ویرایش و یا حذف نمیباشد");
        public ApplicationHubErrorCode(int id, string name, string desc)
            : base(id, name, desc)
        {
        }
    }
}

