using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Web.Http;
using UserWebApp.Hubs.Common;

namespace UserWebApp.Hubs
{
    [Authorize]
    public class ChatHub : MainHub
    {
        public async Task SendMessage(string sendTo, string message)
        {
            await Clients.User(sendTo).SendAsync("ReceiveMessage", ApplicationUser.FirstName, message);
        }
        
        public async Task PostNotification()
        {
            await Clients.All.SendAsync("Notification");
        }
    }
}
