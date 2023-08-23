namespace DocService.Application.UnitTests
{
    using DocService.Application.Common.Factories;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Xunit;

    using static DocService.Application.UnitTests.Common.Constants;

    public class DocumentFactoryShould
    {
        private readonly Mock<ILogger<DocumentFactory>> loggerMock;

        public DocumentFactoryShould()
        {
            this.loggerMock = new Mock<ILogger<DocumentFactory>>();
        }

        [Fact]
        public void CreateXmlDocument()
        {
            var sut = new DocumentFactory(loggerMock.Object);

            var stream = GenerateStreamFromString(ValidXmlContent);
            var actual = sut.CreateXmlDocument(stream);

            Assert.IsType<XmlDocument>(actual);
        }

        [Fact]
        public void ThrowExceptionWhenXmlIsInvalid()
        {
            var sut = new DocumentFactory(loggerMock.Object);

            var stream = this.GenerateStreamFromString(InvalidXmlContent);

            Assert.Throws<ArgumentException>(() => sut.CreateXmlDocument(stream));
        }

        private Stream GenerateStreamFromString(string xml)
            => new MemoryStream(Encoding.UTF8.GetBytes(xml));
    }
}