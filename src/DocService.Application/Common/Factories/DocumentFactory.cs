namespace DocService.Application.Common.Factories
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Xml;

    public class DocumentFactory : IDocumentFactory
    {
        private readonly ILogger<DocumentFactory> logger;

        public DocumentFactory(ILogger<DocumentFactory> logger)
        {
            this.logger = logger;
        }

        public XmlDocument CreateXmlDocument(Stream stream)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(stream);

                return doc;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while creating XmlDocument from stream");
                throw new ArgumentException("Invalid Xml file");
            }

        }
    }
}
