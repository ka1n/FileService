using Domain.File;
using Microsoft.EntityFrameworkCore;

namespace FileService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }

        public DbSet<TemporaryLinksModel> Links { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
