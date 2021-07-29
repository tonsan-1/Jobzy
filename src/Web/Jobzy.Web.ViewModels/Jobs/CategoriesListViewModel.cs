namespace Jobzy.Web.ViewModels.Jobs
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class CategoriesListViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
