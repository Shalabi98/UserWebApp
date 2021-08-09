using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebApp.Models;

namespace UserWebApp.IServices
{
    public interface IBlobService
    {
        public Task<BlobInfo> GetBlob(string name);

        public Task<IEnumerable<string>> GetAllBlobs();

        public Task<bool> UploadBlob(string name, IFormFile file);

        public Task<bool> DeleteBlob(string name);
    }
}
