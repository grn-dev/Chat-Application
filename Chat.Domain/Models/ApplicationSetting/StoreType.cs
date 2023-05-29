using System.ComponentModel.DataAnnotations;

namespace Chat.Domain.Models
{
    public enum StoreType
    {
        [Display(Name = "LOCALFILESTORE")]
        LOCALFILESTORE = 1,
        [Display(Name = "MINIO")]
        MINIO = 2,
        [Display(Name = "AZUREBLOB")]
        AZUREBLOB = 3
    }
}

