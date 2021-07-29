namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Jobs;

    public class CategoryManager : ICategoryManager
    {
        private readonly IRepository<Category> repository;

        public CategoryManager(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<CategoriesListViewModel> GetAllJobCategories()
        {
            var categories = this.repository.All()
                .To<CategoriesListViewModel>()
                .OrderBy(x => x.Name)
                .ToList();

            return categories;
        }
    }
}
