using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.File
{
    public class TemporaryLinksModel
    {
        public int Id { get; private set; }

        public Guid TempGuid { get; set; }

        public int FileId { get; set; }

        public FileModel File { get; set; }

        public bool IsDownloaded { get; set; }
    }
}
