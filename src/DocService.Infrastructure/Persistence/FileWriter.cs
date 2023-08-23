namespace DocService.Infrastructure.Persistence
{
    using DocService.Application.Common.Contracts;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class FileWriter : IWriter
    {
        private readonly string documentsDirectory;
        private readonly ILogger<FileWriter> logger;

        public FileWriter(string documentsDirectory, ILogger<FileWriter> logger)
        {
            this.documentsDirectory = documentsDirectory;
            this.logger = logger;

            this.CreateDirectoryIfNotExists();
        }

        public async Task Write(string filename, string content)
        {
            var filePath = $"{this.documentsDirectory}/{filename}";

            using var writer = new StreamWriter(filePath);

            try
            {
                await writer.WriteLineAsync(content);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Saving document {DOCUMENT_NAME} with content {DOCMENT_CONTENT} failed.", filename, content);
            }
        }

        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(this.documentsDirectory))
            {
                Directory.CreateDirectory(this.documentsDirectory);
            }
        }
    }
}
