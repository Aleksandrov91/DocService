namespace DocService.Application.Documents.Commands
{
    using DocService.Application.Common.Factories;
    using DocService.Domain;
    using MediatR;
    using System.IO;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;

    public record ConvertXmlDocumentToJsonCommand : IRequest
    {
        public ConvertXmlDocumentToJsonCommand(string filename, string contentType, Stream content)
        {
            this.Filename = filename;
            this.ContentType = contentType;
            this.Content = content;
        }

        public string Filename { get; }

        public string ContentType { get; }

        public Stream Content { get; }
    }

    public class ConvertXmlDocumentCommandHandler : IRequestHandler<ConvertXmlDocumentToJsonCommand>
    {
        private readonly IDocumentFactory documentFactory;
        private readonly ChannelWriter<XmlDocumentItem> writer;

        public ConvertXmlDocumentCommandHandler(IDocumentFactory documentFactory, ChannelWriter<XmlDocumentItem> writer)
        {
            this.documentFactory = documentFactory;
            this.writer = writer;
        }

        public async Task Handle(ConvertXmlDocumentToJsonCommand request, CancellationToken cancellationToken)
        {
            var xmlDoc = this.documentFactory.CreateXmlDocument(request.Content);

            await this.writer.WriteAsync(new XmlDocumentItem(request.Filename, xmlDoc));
        }
    }
}
