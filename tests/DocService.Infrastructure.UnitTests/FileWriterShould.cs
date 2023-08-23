namespace DocService.Infrastructure.UnitTests
{
    using DocService.Infrastructure.Persistence;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    using static DocService.Domain.GlobalConstants;
    using static DocService.Infrastructure.UnitTests.Common.Constants;

    public class FileWriterShould
    {
        private readonly Mock<ILogger<FileWriter>> loggerMock;

        public FileWriterShould()
        {
            this.loggerMock = new Mock<ILogger<FileWriter>>();

            this.CleanDocumentsDirectory();
        }

        [Fact]
        public async Task WriteFileToDisk()
        {
            var sut = new FileWriter(DocumentsPath, this.loggerMock.Object);

            var filename = "test.json";

            await sut.Write(filename, JsonContent);

            Assert.True(CheckForDirectory());
            Assert.True(CheckForFile(filename));
        }

        [Fact]
        public async Task WriteFileWithContent()
        {
            var sut = new FileWriter(DocumentsPath, this.loggerMock.Object);

            var filename = "test.json";

            await sut.Write(filename, JsonContent);

            var actual = await ReadFileContent(filename);

            actual.Should().BeEquivalentTo(JsonContent);
        }

        private async Task<string> ReadFileContent(string filename)
        {
            var filePath = $"{DocumentsPath}/{filename}";
            var content = await File.ReadAllTextAsync(filePath);
            return content.Trim();
        }

        private void CleanDocumentsDirectory()
        {
            if (Directory.Exists(DocumentsPath))
            {
                Directory.Delete(DocumentsPath, true);
            }
        }

        private bool CheckForDirectory()
            => Directory.Exists(DocumentsPath);

        private bool CheckForFile(string filename)
        {
            var filePath = $"{DocumentsPath}/{filename}";
            return File.Exists(filePath);
        }
    }
}
