using Domain;
using Domain.File;
using Microsoft.EntityFrameworkCore;

namespace FileService.Services
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly ApplicationContext _context;
        public FileProcessingService(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<Guid> AddFileAsync(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);

                var guid = Guid.NewGuid();

                var file = new FileModel
                {
                    FileName = formFile.FileName,
                    Content = memoryStream.ToArray(),
                    Guid = guid,
                };

                await AddFilesRawAsync(file);
                return guid;
            }
        }

        public async Task<Dictionary<Guid, string>> AddFilesAsync(IFormFileCollection uploadedFiles)
        {
            var filesDic = new Dictionary<Guid, string>();
            var files = new List<FileModel>();
            foreach (var uploadedFile in uploadedFiles)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await uploadedFile.CopyToAsync(memoryStream);

                    var fileName = uploadedFile.FileName;

                    var guid = Guid.NewGuid();
                    filesDic.Add(guid, fileName);

                    files.Add(
                        new FileModel
                        {
                            FileName = fileName,
                            Content = memoryStream.ToArray(),
                            Guid = guid,
                        });
                }

            }
            await AddFilesRawAsync(files.ToArray());

            return filesDic;
        }

        public async Task<Guid> CreateTemporaryLink(Guid guid)
        {
            var fileId = (await _context.Files.FirstOrDefaultAsync(f => f.Guid == guid))?.Id;
            if (fileId is null)
            {
                throw new Exception("File Not Found");
            }

            var newGuid = Guid.NewGuid();

            var newLink = new TemporaryLinksModel
            {
                FileId = fileId.Value,
                TempGuid = newGuid
            };

            await _context.Links.AddAsync(newLink);
            await _context.SaveChangesAsync();

            return newGuid;
        }

        public async Task<FileModel> DownloadByTemporaryLink(Guid guid)
        {
            var link = await _context.Links.FirstOrDefaultAsync(x=>x.TempGuid == guid && x.IsDownloaded == false);
            if (link == null)
            {
                throw new Exception("Link is expired");
            }

            link.IsDownloaded = true;
            await _context.SaveChangesAsync();

            var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == link.FileId);

            return file;
        }

        public async Task<FileModel> GetFileAsync(Guid guid)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Guid == guid);

            if (file == null)
            {
                throw new Exception("File not found");
            }

            return file;
        }

        public object GetFileList()
        {
            var files = _context.Files.Select(x => new { FileName = x.FileName, Guid = x.Guid, Id = x.Id });

            return files.ToArray();
        }

        private async Task AddFilesRawAsync(params FileModel[] files)
        {
            await _context.Files.AddRangeAsync(files);
            await _context.SaveChangesAsync();
        }
    }
}
