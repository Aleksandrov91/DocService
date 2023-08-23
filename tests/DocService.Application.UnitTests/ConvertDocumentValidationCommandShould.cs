namespace DocService.Application.UnitTests
{
    using DocService.Application.Common.Contracts;
    using DocService.Application.Documents.Commands;
    using FluentValidation.TestHelper;
    using Moq;
    using Xunit;

    using static DocService.Application.Common.Constants;

    public class ConvertDocumentValidationCommandShould
    {
        private readonly Mock<IFileService> fileServiceMock;

        public ConvertDocumentValidationCommandShould()
        {
            this.fileServiceMock = new Mock<IFileService>();
        }

        [Fact]
        public void ValidationFailWhenEmptyFilename()
        {
            var sut = new ConvertXmlDocumentToJsonCommandValidation(fileServiceMock.Object);

            var model = new ConvertXmlDocumentToJsonCommand(string.Empty, XmlContentType, null);
            var actual = sut.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(model => model.Filename);
        }

        [Fact]
        public void ValidationFailWhenContentIsNull()
        {
            var sut = new ConvertXmlDocumentToJsonCommandValidation(fileServiceMock.Object);

            var model = new ConvertXmlDocumentToJsonCommand("test", XmlContentType, null);
            var actual = sut.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(model => model.Content);
        }

        [Fact]
        public void ValidationFailWhenContentTypeIsNotXml()
        {
            var sut = new ConvertXmlDocumentToJsonCommandValidation(fileServiceMock.Object);

            var model = new ConvertXmlDocumentToJsonCommand("test", "application/json", null);
            var actual = sut.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(model => model.ContentType);
        }

        [Fact]
        public void ValidationFailWhenFileAlreadyExists()
        {
            this.fileServiceMock
                .Setup(x => x.Exists(It.IsAny<string>()))
                .Returns(true);

            var sut = new ConvertXmlDocumentToJsonCommandValidation(fileServiceMock.Object);

            var model = new ConvertXmlDocumentToJsonCommand("test", XmlContentType, null);
            var actual = sut.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(model => model.Filename);
        }

        [Fact]
        public void PassValidationWhenFilenameIsNotEmptyAndContentTypeIsXml()
        {
            var sut = new ConvertXmlDocumentToJsonCommandValidation(fileServiceMock.Object);

            var model = new ConvertXmlDocumentToJsonCommand("test", XmlContentType, new MemoryStream());
            var actual = sut.TestValidate(model);

            Assert.True(actual.IsValid);
        }
    }
}
