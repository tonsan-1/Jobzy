namespace Jobzy.Web.Tests.Mocks
{
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Moq;

    public class FileManagerMock
    {
        public static IFileManager Instance
        {
            get
            {
                var fileManagerMock = new Mock<IFileManager>();

                fileManagerMock.Setup(
                        x =>
                            x.AddFileToContractAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                fileManagerMock.Setup(
                        x =>
                            x.UploadAttachmentAsync(It.IsAny<IFormFile>()))
                    .ReturnsAsync("testUrl");

                return fileManagerMock.Object;
            }
        }
    }
}
