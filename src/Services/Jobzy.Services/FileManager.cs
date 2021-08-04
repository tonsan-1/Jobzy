namespace Jobzy.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class FileManager : IFileManager
    {
        private readonly IRepository<Attachment> repository;

        public FileManager(IRepository<Attachment> repository)
        {
            this.repository = repository;
        }

        public async Task AddAttachmentToContract(IFormFile file, string contractId)
        {
            var attachmentUrl = await this.UploadAttachment(file);

            var attachment = new Attachment
            {
                Url = attachmentUrl,
                ContractId = contractId,
                Name = file.FileName.Split(".").First().ToUpper(),
                Extension = file.FileName.Split(".").Last().ToUpper(),
            };

            await this.repository.AddAsync(attachment);
            await this.repository.SaveChangesAsync();
        }

        private async Task<string> UploadAttachment(IFormFile attachment)
        {
            // Cloudinary setup
            Account account = new Account(
                "jobzy",
                "239537293495227",
                "cPHTXZ86-9xLH7UkZccX4_XnlFw");

            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            using (var stream = new MemoryStream())
            {
                stream.Flush();
                await attachment.CopyToAsync(stream);
                stream.Position = 0;

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(attachment.FileName.Split(".").First(), stream),
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                var downloadableUrl = "https://res.cloudinary.com/jobzy/image/upload/fl_attachment/v" +
                                      $"{uploadResult.Version}/{uploadResult.PublicId}.{uploadResult.Format}";

                return downloadableUrl;
            }
        }
    }
}
