using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models.Ticket
{
    public enum TicketType
    {
        [Display(Name = "PROBLEM")]
        PROBLEM = 1,
        [Display(Name = "Explanation")]
        Explanation = 2,
        [Display(Name = "BUG")]
        BUG = 3
    }
}
