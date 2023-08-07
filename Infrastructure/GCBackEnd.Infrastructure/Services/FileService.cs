using GCBackEnd.Application.ServiceInterfaces.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace GCBackEnd.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> FileCopyAsync(string uploadPath, string fileNewName, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new FileStream($"{uploadPath}\\{fileNewName}", FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                return false;
            }
        }

        public async Task<List<FileUploadReturnDto>> FileUploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            List<FileUploadReturnDto> filePaths = new();
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (IFormFile file in formFiles)
            {
                string fileNewName = FileUploadRenameAsync(Path.GetFileNameWithoutExtension(file.FileName)) + Path.GetExtension(file.FileName);
                filePaths.Add(new FileUploadReturnDto { fileName = fileNewName, filePath = uploadPath });
                if ((await FileCopyAsync(uploadPath, fileNewName, file)) == false)
                {
                    filePaths.RemoveAt(filePaths.Count - 1);
                    foreach (var item in filePaths)
                        File.Delete($"{item.filePath}\\{item.fileName}");
                    return null;
                }
            }
            return filePaths;
        }

        public string FileUploadRenameAsync(string fileName)
        {
            return Regex.Replace(fileName, @"[^0-9a-zA-Z]", "") + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
