namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Messages;

    public class MessageManager : IMessageManager
    {
        private readonly IRepository<ApplicationUser> repository;

        public MessageManager(IRepository<ApplicationUser> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<UsersListViewModel> GetMatchingUsers(string query)
        {
            var users = this.repository.All()
                .Where(x => x.Name.StartsWith(query))
                .To<UsersListViewModel>()
                .ToList();

            return users;
        }
    }
}
