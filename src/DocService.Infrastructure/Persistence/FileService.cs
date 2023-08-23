namespace DocService.Infrastructure.Persistence
{
    using DocService.Application.Common.Contracts;
    using System.IO;
    using System.Linq;

    public class FileService : IFileService
    {
        private readonly string documentsDirectory;

        public FileService(string documentsDirectory)
        {
            this.documentsDirectory = documentsDirectory;
        }

        public bool Exists(string filename)
        {
            var files = Directory.GetFiles(this.documentsDirectory, $"{filename}.*");

            return files.Any();
        }
    }
}
