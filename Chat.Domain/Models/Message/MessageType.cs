using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public enum MessageType
    {
        [Display(Name = "DEFAULT")] DEFAULT = 1,
        [Display(Name = "FILE")] FILE = 2
    }
}