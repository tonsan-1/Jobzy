namespace Jobzy.Web.Hubs
{
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.SignalR;

    public class MessageHub : Hub
    {
        private readonly IFreelancePlatform freelancePlatform;

        public MessageHub(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        public Task SendMessageToUser(string user, string message)
        {
            return this.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
