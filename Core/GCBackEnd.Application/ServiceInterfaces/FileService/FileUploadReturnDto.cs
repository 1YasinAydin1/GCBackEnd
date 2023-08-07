using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Application.ServiceInterfaces.FileService
{
    public class FileUploadReturnDto
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}
