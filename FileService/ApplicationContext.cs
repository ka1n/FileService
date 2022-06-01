using Domain;
using Domain.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
