using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models.Ticket
{
    public enum TicketStatus
    {

        [Display(Name = "OPEN")]
        OPEN = 1,
        [Display(Name = "INPROGRESS")]
        INPROGRESS = 2,
        [Display(Name = "CLOSE")]
        CLOSE = 3
    }
}
