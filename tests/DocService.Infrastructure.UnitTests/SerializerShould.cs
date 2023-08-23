namespace DocService.Infrastructure.UnitTests
{
    using DocService.Infrastructure.Serializers;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Xml;
    using Xunit;

    using static DocService.Infrastructure.UnitTests.Common.Constants;

    public class SerializerShould
    {
        private readonly Mock<ILogger<XmlToJsonSerializer>> loggerMock;

        public SerializerShould()
        {
            this.loggerMock = new Mock<ILogger<XmlToJsonSerializer>>();
        }

        [Fact]
        public void SerializeXmlContentToJson()
        {
            var sut = new XmlToJsonSerializer(this.loggerMock.Object);

            var doc = this.GetXmlDocument();
            var jsonResult = sut.Serialize(doc);

            Assert.Equal(JsonContent, jsonResult, ignoreWhiteSpaceDifferences: true, ignoreAllWhiteSpace: true);
        }

        private XmlDocument GetXmlDocument()
        {
            var doc = new XmlDocument();
            doc.LoadXml(XmlContent);

            return doc;
        }
    }
}
