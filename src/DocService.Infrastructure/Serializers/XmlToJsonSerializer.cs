namespace DocService.Infrastructure.Serializers
{
    using DocService.Application.Common.Contracts;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Xml;

    public class XmlToJsonSerializer : ISerializer<XmlDocument>
    {
        private readonly ILogger<XmlToJsonSerializer> logger;

        public XmlToJsonSerializer(ILogger<XmlToJsonSerializer> logger)
        {
            this.logger = logger;
        }

        public string Serialize(XmlDocument doc)
            => this.TrySerialize(doc);

        private string TrySerialize(XmlDocument doc)
        {
            try
            {
                return JsonConvert.SerializeXmlNode(doc.DocumentElement, Newtonsoft.Json.Formatting.Indented, true);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while deserializing document {DOCUMENT}", doc.Name);
                throw;
            }
        }
    }
}
