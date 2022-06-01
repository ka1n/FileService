using Domain.File;

namespace FileService.Services
{
    public interface IFileProcessingService
    {
        public Task<FileModel> GetFileAsync(Guid guid);

        public Task<Guid> AddFileAsync(IFormFile file);

        public Task<Dictionary<Guid, string>> AddFilesAsync(IFormFileCollection formFiles);

        public Task<Guid> CreateTemporaryLink(Guid guid);

        public Task<FileModel> DownloadByTemporaryLink(Guid guid);

        public object GetFileList();
    }
}
