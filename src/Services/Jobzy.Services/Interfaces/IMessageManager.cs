namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;

    using Jobzy.Web.ViewModels.Messages;

    public interface IMessageManager
    {
        IEnumerable<UsersListViewModel> GetMatchingUsers(string query);
    }
}
