using Microsoft.AspNetCore.Http;

namespace GCBackEnd.Application.ServiceInterfaces.FileService
{
    public interface IFileService
    {
        Task<List<FileUploadReturnDto>> FileUploadAsync(string path, IFormFileCollection formFiles);

        string FileUploadRenameAsync(string fileName);
        Task<bool> FileCopyAsync(string uploadPath, string fileNewName, IFormFile file);
    }
}
