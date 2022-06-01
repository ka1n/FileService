namespace Domain.File
{
    public class FileModel
    {
        public int Id { get; private set; }

        public Guid Guid { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }
    }
}