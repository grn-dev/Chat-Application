using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models.Ticket
{
    public enum Priority
    {
        [Display(Name = "1")]
        Global = 1,
        [Display(Name = "2")]
        Personal = 2,
        [Display(Name = "3")]
        Personal2 = 3
    }
}
