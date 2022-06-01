using Domain.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileService.Services
{
    public interface IFileProcessingService
    {
        public Task<FileModel> GetFileAsync(Guid guid);

        public Task<Guid> AddFileAsync(IFormFile file);

        public Task<Dictionary<string, Guid>> AddFilesAsync(IFormFileCollection formFiles);

        public Task<Guid> CreateTemporaryLink(Guid guid);

        public Task<FileModel> DownloadByTemporaryLink(Guid guid);

        public object GetFileList();
    }
}
