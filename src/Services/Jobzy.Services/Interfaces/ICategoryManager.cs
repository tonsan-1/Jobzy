namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;

    using Jobzy.Web.ViewModels.Jobs;

    public interface ICategoryManager
    {
        IEnumerable<CategoriesListViewModel> GetAllJobCategories();
    }
}
