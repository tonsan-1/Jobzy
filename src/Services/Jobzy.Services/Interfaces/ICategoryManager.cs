namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryManager
    {
        Task<IEnumerable<T>> GetAllJobCategories<T>();
    }
}
