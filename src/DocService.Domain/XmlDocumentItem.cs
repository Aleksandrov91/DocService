using System.Xml;

namespace DocService.Domain
{
    public class XmlDocumentItem : DocumentItem<XmlDocument>
    {
        public XmlDocumentItem(string name, XmlDocument document)
            :base(name, document)
        {
        }
    }

    public abstract class DocumentItem<TDocument>
    {
        protected DocumentItem(string name, TDocument document)
        {
            this.Name = name;
            this.Document = document;
        }

        public string Name { get; }

        public TDocument Document { get; }
    }
}
