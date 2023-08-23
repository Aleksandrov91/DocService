namespace DocService.Application.Documents.Commands
{
    using DocService.Application.Common.Contracts;
    using FluentValidation;

    using static DocService.Application.Common.Constants;

    public class ConvertXmlDocumentToJsonCommandValidation : AbstractValidator<ConvertXmlDocumentToJsonCommand>
    {
        public ConvertXmlDocumentToJsonCommandValidation(IFileService fileService)
        {
            this.RuleFor(x => x.Filename)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Filename)
                .Must(x => !fileService.Exists(x))
                .WithMessage("File with this name already exists");

            this.RuleFor(x => x.ContentType)
                .Matches(XmlContentType)
                .WithMessage($"'ContentType' is not in the correct format. Allowed format is {XmlContentType}");

            this.RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty();
        }
    }
}
