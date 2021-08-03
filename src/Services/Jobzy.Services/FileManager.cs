namespace Jobzy.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class FileManager : IFileManager
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            // Cloudinary setup
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(
                "jobzy",
                "239537293495227",
                "cPHTXZ86-9xLH7UkZccX4_XnlFw");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            using (var stream = new MemoryStream())
            {
                stream.Flush();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), stream),
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUrl.AbsolutePath;
            }
        }
    }
}
