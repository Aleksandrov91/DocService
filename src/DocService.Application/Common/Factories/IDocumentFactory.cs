namespace DocService.Application.Common.Factories
{
    using System.IO;
    using System.Xml;

    public interface IDocumentFactory
    {
        XmlDocument CreateXmlDocument(Stream stream);
    }
}
