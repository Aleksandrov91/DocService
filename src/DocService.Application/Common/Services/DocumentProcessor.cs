namespace DocService.Application.Common.Services
{
    using DocService.Application.Common.Contracts;
    using DocService.Domain;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using System.Xml;

    using static DocService.Domain.GlobalConstants;

    public class DocumentProcessor : BackgroundService
    {
        private readonly ChannelReader<XmlDocumentItem> reader;
        private readonly ISerializer<XmlDocument> serializer;
        private readonly IWriter writer;

        public DocumentProcessor(ChannelReader<XmlDocumentItem> reader, ISerializer<XmlDocument> serializer, IWriter writer)
        {
            this.reader = reader;
            this.serializer = serializer;
            this.writer = writer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var docItem in reader.ReadAllAsync(stoppingToken))
            {
                this.TryProcessDocument(docItem);
            }
        }

        private void TryProcessDocument(XmlDocumentItem documentItem)
        {
            try
            {
                var jsonContent = this.serializer.Serialize(documentItem.Document);

                this.WriteFile(documentItem.Name, jsonContent);
            }
            catch (Exception)
            {
            }
        }

        private void WriteFile(string filename, string content)
        {
            var filenameWithExtension = $"{filename}{JsonFileExtension}";
            this.writer.Write(filenameWithExtension, content);
        }
    }
}
