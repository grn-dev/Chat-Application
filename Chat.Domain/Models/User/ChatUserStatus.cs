using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public enum ChatUserStatus
    {
        [Display(Name = "فعال")]
        ACTIVE = 1,
        [Display(Name = "ویرایش اجباری")]
        FORCE_EDIT = 2,
        [Display(Name = "غیرفعال")]
        INACTIVE = 3
    }
}


