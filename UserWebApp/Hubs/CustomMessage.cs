using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace UserWebApp.Hubs
{
    public class CustomMessage : IUserIdProvider 
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
