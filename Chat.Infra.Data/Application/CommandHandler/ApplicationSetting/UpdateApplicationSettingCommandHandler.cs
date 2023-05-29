using Chat.Domain.Attributes; 
using Chat.Infra.Data.Application.QueryHandler.RequestHandler;
using Chat.Infra.Data.Context;
using System.Threading.Tasks;


namespace Chat.Domain.Commands.ApplicationSetting
{
    [Bean]
    public class UpdateApplicationSettingCommandHandler : MyUpdateRequestHandler<
            UpdateApplicationSettingCommand, Models.ApplicationSetting, int>
    {

        public UpdateApplicationSettingCommandHandler(ChatContext context) : base(context)
        {
        }


        protected override Task UpdateFields(Models.ApplicationSetting domain, UpdateApplicationSettingCommand request)
        {
            domain.Update(request.AzureblobStorageConnectionString,
                    request.LocalFileSystemStoragePath,
                    request.LocalFileSystemStorageUriPrefix,
                    request.MaxFileUploadBytes,
                    request.MaxMessageLength);
            return Task.FromResult(0);
        }
    }
}

