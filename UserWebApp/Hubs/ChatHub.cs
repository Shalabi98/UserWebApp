using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Web.Http;
using UserWebApp.Hubs.Common;

namespace UserWebApp.Hubs
{
    [Authorize]
    public class ChatHub : MainHub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", ApplicationUser.FirstName, message);
        }

    }
}
