using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using UserWebApp.IServices;

namespace UserWebApp.Controllers
{
    [Authorize]
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var files = await _blobService.GetAllBlobs();
            return View(files);
        }

        [HttpGet]
        public IActionResult UploadBlob()
        {
            return View();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetBlob (string name)
        {
            var res = await _blobService.GetBlob(name);
            return File(res.Content, res.ContentType);
        }

        public async Task<IActionResult> UploadBlob(IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var res = await _blobService.UploadBlob(fileName, file);

            if (res)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBlob(string name)
        {
            await _blobService.DeleteBlob(name);
            return RedirectToAction("Index");
        }
    }
}
