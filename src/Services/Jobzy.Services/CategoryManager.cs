namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoryManager : ICategoryManager
    {
        private readonly IRepository<Category> repository;

        public CategoryManager(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllJobCategories<T>()
        {
            var categories = await this.repository.All()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();

            return categories;
        }
    }
}
