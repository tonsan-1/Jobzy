namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileManager
    {
        Task AddFileToContractAsync(IFormFile file, string contractId);

        Task<string> UploadAttachmentAsync(IFormFile attachment);
    }
}
