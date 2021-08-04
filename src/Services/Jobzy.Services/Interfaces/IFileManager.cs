namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileManager
    {
        Task AddAttachmentToContract(IFormFile attachment, string contractId);

        Task UpdateProfilePicture(IFormFile picture, string userId);
    }
}
