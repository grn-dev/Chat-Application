using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public enum ApplicationSettingsStatus
    {
        [Display(Name = "ACTIVE")]
        ACTIVE = 1,
        [Display(Name = "INACTIVE")]
        INACTIVE = 2,
    }
}

