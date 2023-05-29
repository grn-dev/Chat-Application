using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public enum ChatRoomType
    {
        [Display(Name = "CHANNEL")] CHANNEL = 1,
        [Display(Name = "GROUP")] GROUP = 2,
        [Display(Name = "TICKET")] TICKET = 3,
    }
}