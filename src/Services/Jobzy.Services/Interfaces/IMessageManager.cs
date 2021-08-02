namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;

    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Messages;

    public interface IMessageManager
    {
        IEnumerable<UsersListViewModel> GetMatchingUsers(string query);

        IEnumerable<Message> GetConversations(string userId);
    }
}
