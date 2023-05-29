using Chat.Domain.Models;

namespace Chat.Domain.Commands.ApplicationSetting
{

    public class UpdateApplicationSettingCommand : BaseCommand<int>
    {
        public string AzureblobStorageConnectionString { get; set; }
        public string LocalFileSystemStoragePath { get; set; }
        public string LocalFileSystemStorageUriPrefix { get; set; }
        public int MaxFileUploadBytes { get; set; }
        public int MaxMessageLength { get; set; }
    }
}

