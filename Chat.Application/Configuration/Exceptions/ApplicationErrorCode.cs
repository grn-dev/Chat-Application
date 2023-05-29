using Chat.Domain.Core.SeedWork;

namespace Chat.Application.Configuration.Exceptions
{
    public class ApplicationErrorCode : Enumeration
    {
        public static ApplicationErrorCode INCORRECT_FILTER = new ApplicationErrorCode(1, nameof(INCORRECT_FILTER), "فیلتر گذاری اشتباه می باشد");
        public static ApplicationErrorCode CANT_DELETE_UPDATE = new ApplicationErrorCode(2, nameof(CANT_DELETE_UPDATE), "به دلیل استفاده شدن قابل ویرایش و یا حذف نمیباشد");
        public static ApplicationErrorCode EXIST_USER_IN_CHATROOM = new ApplicationErrorCode(3, nameof(EXIST_USER_IN_CHATROOM), "قبلا در چت روم عضو شده اید");
        public static ApplicationErrorCode TICKET_INPROGRESS = new ApplicationErrorCode(4, nameof(TICKET_INPROGRESS), "تیکت در حال بررسی میباشد");
        public static ApplicationErrorCode ENTITY_NOT_FOUND = new ApplicationErrorCode(5, nameof(ENTITY_NOT_FOUND), "entity not found");
        public static ApplicationErrorCode FAILED_TO_UPLOAD = new ApplicationErrorCode(6, nameof(FAILED_TO_UPLOAD), "Failed to upload");
        public static ApplicationErrorCode FAILED_TO_UPLOAD_LAGER = new ApplicationErrorCode(7, nameof(FAILED_TO_UPLOAD_LAGER), "Failed to upload Because is lager");
        public static ApplicationErrorCode CHAT_PRIVATE = new ApplicationErrorCode(7, nameof(CHAT_PRIVATE), "Chat is private and invited Code is not invalid");
        public static ApplicationErrorCode EXIST_DIRECT_USER = new ApplicationErrorCode(8, nameof(EXIST_DIRECT_USER), "Direct Is Exist");

        public ApplicationErrorCode(int id, string name, string desc)
            : base(id, name, desc)
        {
        }
    }
}

