using FileService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController
    {
        private readonly IFileProcessingService _fileProcessingService;
        public FileController(IFileProcessingService fileProcessingService)
        {
            _fileProcessingService = fileProcessingService;
        }

        [HttpPost]
        [Route("add-files-async")]
        public async Task<Dictionary<string, Guid>> AddFilesAsync(IFormFileCollection uploadedFiles)
        {
            return await _fileProcessingService.AddFilesAsync(uploadedFiles);
        }

        [HttpPost]
        [Route("add-file-async")]
        public async Task<Guid> AddFile(IFormFile uploadedFile)
        {
            return await _fileProcessingService.AddFileAsync(uploadedFile);
        }

        [HttpGet]
        [Route("get-file-async")]
        public async Task<IActionResult> GetFile(Guid guid)
        {
           var file = await _fileProcessingService.GetFileAsync(guid);

            return new FileContentResult(file.Content, "application/octet-stream")
            {
                FileDownloadName = file.FileName,
            };
        }

        [HttpPost]
        [Route("create-temporary-link")]
        public async Task<Guid> CreateTemporaryLink(Guid guid)
        {
            return await _fileProcessingService.CreateTemporaryLink(guid);
        }

        [HttpGet]
        [Route("download-by-temporary-link")]
        public async Task<IActionResult> DownloadByTemporaryLink(Guid guid)
        {
            var file = await _fileProcessingService.DownloadByTemporaryLink(guid);

            return new FileContentResult(file.Content, "application/octet-stream")
            {
                FileDownloadName = file.FileName,
            };
        }

        [HttpGet]
        [Route("get-file-list")]
        public object GetFileList()
        {
            return _fileProcessingService.GetFileList();
        }
    }
}
