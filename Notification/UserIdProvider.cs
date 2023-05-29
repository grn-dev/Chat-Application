using Microsoft.AspNetCore.SignalR;
using Chat.Extensions;

namespace Chat
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            //  string sss= connection.User?.FindFirst(ClaimTypes.Email)?.Value; 
            return connection.User?.GetUserId().ToString();
        }
    }
}
