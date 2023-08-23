namespace DocService.Web.Models
{
    public record ConvertXmlDocumentToJson
    {
        public string Filename { get; set; }

        public IFormFile File { get; set; }
    }
}
