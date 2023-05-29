using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public enum ChatRoomUserStatus
    {
        [Display(Name = "DEFAULT")] DEFAULT = 1,
        [Display(Name = "LEFT")] LEFT = 2,
        [Display(Name = "REMOVE")] REMOVE = 3,
    }
}