namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileManager
    {
        Task<string> UploadFile(IFormFile file);
    }
}
