using Chat.Domain.Models;

namespace Chat.Application.ViewModels
{
    public partial class ApplicationSettingViewModel
    {
        public class ApplicationSettingExpose
        {
            public int Id { get; set; }
            public string AzureblobStorageConnectionString { get; set; }
            public string LocalFileSystemStoragePath { get; set; }
            public string LocalFileSystemStorageUriPrefix { get; set; }
            public int MaxFileUploadBytes { get; set; }
            public int MaxMessageLength { get; set; }
            public ApplicationSettingsStatus Status { get; set; }
            public StoreType StoreType { get; set; }

        }

        public class UpdateApplicationSetting
        {
            public int Id { get; set; }
            public string AzureblobStorageConnectionString { get; set; }
            public string LocalFileSystemStoragePath { get; set; }
            public string LocalFileSystemStorageUriPrefix { get; set; }
            public int MaxFileUploadBytes { get; set; }
            public int MaxMessageLength { get; set; }
        }
    }
}

