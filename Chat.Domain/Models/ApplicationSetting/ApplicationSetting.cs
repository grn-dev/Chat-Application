using Chat.Domain.Common;
using NetDevPack.Domain;

namespace Chat.Domain.Models
{
    public class ApplicationSetting : BaseModel<int>, IAggregateRoot
    {

        private ApplicationSetting(int id, string azureblobStorageConnectionString,
                    string localFileSystemStoragePath,
                    string localFileSystemStorageUriPrefix,
                    int maxFileUploadBytes,
                    int maxMessageLength, StoreType storeType)
        {
            Id = id;
            AzureblobStorageConnectionString = azureblobStorageConnectionString;
            LocalFileSystemStoragePath = localFileSystemStoragePath;
            LocalFileSystemStorageUriPrefix = localFileSystemStorageUriPrefix;
            MaxFileUploadBytes = maxFileUploadBytes;
            MaxMessageLength = maxMessageLength;
            Status = ApplicationSettingsStatus.ACTIVE;
            StoreType = storeType;
        }
        private ApplicationSetting() { }
        public string AzureblobStorageConnectionString { get; private set; }
        public string LocalFileSystemStoragePath { get; private set; }
        public string LocalFileSystemStorageUriPrefix { get; private set; }
        public int MaxFileUploadBytes { get; private set; }
        public int MaxMessageLength { get; private set; }
        public ApplicationSettingsStatus Status { get; private set; }
        public StoreType StoreType { get; private set; }


        public static ApplicationSetting Seed(int id, string azureblobStorageConnectionString,
                    string localFileSystemStoragePath,
                    string localFileSystemStorageUriPrefix,
                    int maxFileUploadBytes,
                    int maxMessageLength, StoreType storeType)
        {
            return new ApplicationSetting(id, azureblobStorageConnectionString, localFileSystemStoragePath, localFileSystemStorageUriPrefix, maxFileUploadBytes, maxMessageLength, storeType);
        }
        public void Update(string azureblobStorageConnectionString,
                    string localFileSystemStoragePath,
                    string localFileSystemStorageUriPrefix,
                    int maxFileUploadBytes,
                    int maxMessageLength)
        {
            AzureblobStorageConnectionString = azureblobStorageConnectionString;
            LocalFileSystemStoragePath = localFileSystemStoragePath;
            LocalFileSystemStorageUriPrefix = localFileSystemStorageUriPrefix;
            MaxFileUploadBytes = maxFileUploadBytes;
            MaxMessageLength = maxMessageLength;
        }
    }
}